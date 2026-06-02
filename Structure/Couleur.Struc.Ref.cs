/**
 * Copyright © 2017-2026, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2026, Galactic-Shrine - Tous droits réservés.
 * 
 * Mozilla Public License 2.0 / Licence Publique Mozilla 2.0
 *
 * This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
 * If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.
 * Modifications to this file must be shared under the same Mozilla Public License, v. 2.0.
 *
 * Cette Forme de Code Source est soumise aux termes de la Licence Publique Mozilla, version 2.0.
 * Si une copie de la MPL ne vous a pas été distribuée avec ce fichier, vous pouvez en obtenir une à l'adresse suivante : https://mozilla.org/MPL/2.0/.
 * Les modifications apportées à ce fichier doivent être partagées sous la même Licence Publique Mozilla, v. 2.0.
 **/

using System;
using System.Globalization;

namespace GalacticShrine.Structure {

  /**
   * <summary>
   *   [FR] Représente une couleur RGBA (Rouge, Vert, Bleu, Alpha).
   *        Alpha: 0 = transparent, 255 = opaque.
   *        La console ne gère pas la transparence : si Alpha != 255, il faut composer sur un fond avant rendu ANSI.
   *   [EN] Represents an RGBA color (Red, Green, Blue, Alpha).
   *        Alpha: 0 = transparent, 255 = opaque.
   *        Terminals do not support transparency: if Alpha != 255, you must alpha-blend over a background before ANSI rendering.
   * </summary>
   **/
  public readonly struct Couleur : IEquatable<Couleur> {

    /**
     * <summary>
     *   [FR] Composante rouge (0..255).
     *   [EN] Red channel (0..255).
     * </summary>
     **/
    public byte Rouge { get; }

    /**
     * <summary>
     *   [FR] Composante verte (0..255).
     *   [EN] Green channel (0..255).
     * </summary>
     **/
    public byte Vert { get; }

    /**
     * <summary>
     *   [FR] Composante bleue (0..255).
     *   [EN] Blue channel (0..255).
     * </summary>
     **/
    public byte Bleu { get; }

    /**
     * <summary>
     *   [FR] Composante alpha (0..255). 0 = transparent, 255 = opaque.
     *   [EN] Alpha channel (0..255). 0 = transparent, 255 = opaque.
     * </summary>
     **/
    public byte Alpha { get; }

    /**
     * <summary>
     *   [FR] Alias court de <see cref="Rouge"/>.
     *   [EN] Short alias for <see cref="Rouge"/>.
     * </summary>
     **/
    public byte R => Rouge;

    /**
     * <summary>
     *   [FR] Alias court de <see cref="Vert"/>.
     *   [EN] Short alias for <see cref="Vert"/>.
     * </summary>
     **/
    public byte G => Vert;

    /**
     * <summary>
     *   [FR] Alias court de <see cref="Bleu"/>.
     *   [EN] Short alias for <see cref="Bleu"/>.
     * </summary>
     **/
    public byte B => Bleu;

    /**
     * <summary>
     *   [FR] Alias court de <see cref="Alpha"/>.
     *   [EN] Short alias for <see cref="Alpha"/>.
     * </summary>
     **/
    public byte A => Alpha;

    /**
     * <summary>
     *   [FR] Indique si la couleur est totalement opaque (Alpha == 255).
     *   [EN] Indicates whether the color is fully opaque (Alpha == 255).
     * </summary>
     **/
    public bool EstOpaque => Alpha == 255;

    /**
     * <summary>
     *   [FR] Indique si la couleur est totalement transparente (Alpha == 0).
     *   [EN] Indicates whether the color is fully transparent (Alpha == 0).
     * </summary>
     **/
    public bool EstTransparent => Alpha == 0;

    /**
     * <summary>
     *   [FR] Construit une couleur RGB opaque (Alpha = 255).
     *   [EN] Builds an opaque RGB color (Alpha = 255).
     * </summary>
     * <param name="Rouge">
     *   [FR] Composante rouge (0..255).
     *   [EN] Red channel (0..255).
     * </param>
     * <param name="Vert">
     *   [FR] Composante verte (0..255).
     *   [EN] Green channel (0..255).
     * </param>
     * <param name="Bleu">
     *   [FR] Composante bleue (0..255).
     *   [EN] Blue channel (0..255).
     * </param>
     **/
    public Couleur(byte Rouge, byte Vert, byte Bleu) : this(Rouge, Vert, Bleu, 255) { }

    /**
     * <summary>
     *   [FR] Construit une couleur RGBA.
     *   [EN] Builds an RGBA color.
     * </summary>
     * <param name="Rouge">
     *   [FR] Composante rouge (0..255).
     *   [EN] Red channel (0..255).
     * </param>
     * <param name="Vert">
     *   [FR] Composante verte (0..255).
     *   [EN] Green channel (0..255).
     * </param>
     * <param name="Bleu">
     *   [FR] Composante bleue (0..255).
     *   [EN] Blue channel (0..255).
     * </param>
     * <param name="Alpha">
     *   [FR] Transparence (0..255). 0 = transparent, 255 = opaque.
     *   [EN] Transparency (0..255). 0 = transparent, 255 = opaque.
     * </param>
     **/
    public Couleur(byte Rouge, byte Vert, byte Bleu, byte Alpha) {
    
      this.Rouge = Rouge;
			this.Vert = Vert;
			this.Bleu = Bleu;
			this.Alpha = Alpha;
    }
    
    /**
     * <summary>
     *   [FR] Retourne une nouvelle couleur avec un alpha différent (les composantes RGB restent identiques).
     *   [EN] Returns a new color with a different alpha (RGB channels remain unchanged).
     * </summary>
     * <param name="Alpha">
     *   [FR] Nouvel alpha (0..255).
     *   [EN] New alpha (0..255).
     * </param>
     * <returns>
     *   [FR] Nouvelle instance de <see cref="Couleur"/>.
     *   [EN] A new <see cref="Couleur"/> instance.
     * </returns>
     **/
    public Couleur AvecAlpha(byte Alpha) => new(Rouge, Vert, Bleu, Alpha);

    /**
     * <summary>
     *   [FR] Retourne la représentation hexadécimale de la couleur.
     *        - Sans alpha : "#RRGGBB"
     *        - Avec alpha : "#AARRGGBB"
     *   [EN] Returns the hex representation of the color.
     *        - Without alpha: "#RRGGBB"
     *        - With alpha:    "#AARRGGBB"
     * </summary>
     * <param name="inclureAlpha">
     *   [FR] Si true, inclut l'alpha au format AARRGGBB.
     *   [EN] If true, includes alpha in AARRGGBB format.
     * </param>
     * <param name="prefixeDiese">
     *   [FR] Si true, préfixe avec '#'.
     *   [EN] If true, prefixes with '#'.
     * </param>
     * <returns>
     *   [FR] Chaîne hexadécimale de la couleur.
     *   [EN] Hex string for the color.
     * </returns>
     **/    
    public string VersHex(bool inclureAlpha = false, bool prefixeDiese = true) {

      string p = prefixeDiese ? "#" : "";
      
      if (!inclureAlpha) {
        return p 
          + Rouge.ToString("X2", CultureInfo.InvariantCulture) 
          + Vert.ToString("X2", CultureInfo.InvariantCulture) 
          + Bleu.ToString("X2", CultureInfo.InvariantCulture);
      }

      // AARRGGBB
      return p
          + Alpha.ToString("X2", CultureInfo.InvariantCulture)
          + Rouge.ToString("X2", CultureInfo.InvariantCulture)
          + Vert.ToString("X2", CultureInfo.InvariantCulture)
          + Bleu.ToString("X2", CultureInfo.InvariantCulture);
    }

        /**
         * <summary>
         *   [FR] Tente de parser une couleur depuis un hex.
         *        Formats acceptés (avec ou sans '#') :
         *        - RGB
         *        - ARGB
         *        - RRGGBB
         *        - AARRGGBB
         *   [EN] Tries to parse a color from hex.
         *        Accepted formats (with or without '#'):
         *        - RGB
         *        - ARGB
         *        - RRGGBB
         *        - AARRGGBB
         * </summary>
         * <param name="hex">
         *   [FR] Chaîne hexadécimale.
         *   [EN] Hex string.
         * </param>
         * <param name="couleur">
         *   [FR] Couleur résultante si succès.
         *   [EN] Resulting color on success.
         * </param>
         * <returns>
         *   [FR] true si parsing réussi, sinon false.
         *   [EN] true if parsing succeeded; otherwise false.
         * </returns>
         **/
        public static bool EssayerDepuisHex(string hex, out Couleur couleur)
        {
            couleur = default;

            if (string.IsNullOrWhiteSpace(hex))
                return false;

            hex = hex.Trim();
            if (hex.StartsWith("#", StringComparison.Ordinal))
                hex = hex[1..];

            // Courtes
            if (hex.Length == 3) // RGB
            {
                if (!EstHex(hex)) return false;
                byte r = Convert.ToByte(new string(hex[0], 2), 16);
                byte g = Convert.ToByte(new string(hex[1], 2), 16);
                byte b = Convert.ToByte(new string(hex[2], 2), 16);
                couleur = new Couleur(r, g, b, 255);
                return true;
            }

            if (hex.Length == 4) // ARGB
            {
                if (!EstHex(hex)) return false;
                byte a = Convert.ToByte(new string(hex[0], 2), 16);
                byte r = Convert.ToByte(new string(hex[1], 2), 16);
                byte g = Convert.ToByte(new string(hex[2], 2), 16);
                byte b = Convert.ToByte(new string(hex[3], 2), 16);
                couleur = new Couleur(r, g, b, a);
                return true;
            }

            // Longues
            if (hex.Length == 6) // RRGGBB
            {
                if (!EstHex(hex)) return false;
                byte r = Convert.ToByte(hex[..2], 16);
                byte g = Convert.ToByte(hex.Substring(2, 2), 16);
                byte b = Convert.ToByte(hex.Substring(4, 2), 16);
                couleur = new Couleur(r, g, b, 255);
                return true;
            }

            if (hex.Length == 8) // AARRGGBB
            {
                if (!EstHex(hex)) return false;
                byte a = Convert.ToByte(hex[..2], 16);
                byte r = Convert.ToByte(hex.Substring(2, 2), 16);
                byte g = Convert.ToByte(hex.Substring(4, 2), 16);
                byte b = Convert.ToByte(hex.Substring(6, 2), 16);
                couleur = new Couleur(r, g, b, a);
                return true;
            }

            return false;
        }

        /**
         * <summary>
         *   [FR] Parse une couleur depuis un hex. Voir <see cref="EssayerDepuisHex(string, out Couleur)"/>.
         *   [EN] Parses a color from hex. See <see cref="EssayerDepuisHex(string, out Couleur)"/>.
         * </summary>
         * <param name="hex">
         *   [FR] Chaîne hexadécimale.
         *   [EN] Hex string.
         * </param>
         * <returns>
         *   [FR] Couleur parsée.
         *   [EN] Parsed color.
         * </returns>
         * <exception cref="FormatException">
         *   [FR] Si le format hex est invalide.
         *   [EN] If the hex format is invalid.
         * </exception>
         **/
        public static Couleur DepuisHex(string hex)
        {
            if (!EssayerDepuisHex(hex, out var c))
                throw new FormatException("Format hex invalide. Attendu: RGB, ARGB, RRGGBB, AARRGGBB (avec ou sans #).");
            return c;
        }

        /**
         * <summary>
         *   [FR] Compose cette couleur RGBA sur un fond (considéré opaque) et renvoie une couleur opaque (Alpha=255).
         *        Formule: out = a*fg + (1-a)*bg, avec a dans [0..1].
         *   [EN] Alpha-composites this RGBA color over an (opaque) background and returns an opaque color (Alpha=255).
         *        Formula: out = a*fg + (1-a)*bg, with a in [0..1].
         * </summary>
         * <param name="fond">
         *   [FR] Couleur de fond (traitée comme opaque).
         *   [EN] Background color (treated as opaque).
         * </param>
         * <returns>
         *   [FR] Couleur résultante (opaque).
         *   [EN] Resulting color (opaque).
         * </returns>
         **/
        public Couleur ComposerSur(Couleur fond)
        {
            // On traite "fond" comme opaque (alpha du fond ignoré ici)
            int a = Alpha; // 0..255

            static byte Blend(byte fg, byte bg, int a255)
                => (byte)((fg * a255 + bg * (255 - a255)) / 255);

            byte r = Blend(Rouge, fond.Rouge, a);
            byte g = Blend(Vert,  fond.Vert,  a);
            byte b = Blend(Bleu,  fond.Bleu,  a);

            return new Couleur(r, g, b, 255);
        }

        // =========================
        // Console / ANSI (optionnel)
        // =========================

        /**
         * <summary>
         *   [FR] Code ANSI pour réinitialiser les styles/couleurs.
         *   [EN] ANSI code to reset styles/colors.
         * </summary>
         **/
        public static string AnsiReset => "\u001b[0m";

        /**
         * <summary>
         *   [FR] Retourne le code ANSI true color pour la couleur de premier plan (foreground).
         *        Ne supporte pas l'alpha : si Alpha != 255, utilise <see cref="VersAnsiPremierPlan(Couleur)"/>.
         *   [EN] Returns the ANSI true color code for the foreground color.
         *        Does not support alpha: if Alpha != 255, use <see cref="VersAnsiPremierPlan(Couleur)"/>.
         * </summary>
         * <returns>
         *   [FR] Séquence ANSI de premier plan.
         *   [EN] Foreground ANSI sequence.
         * </returns>
         * <exception cref="InvalidOperationException">
         *   [FR] Si la couleur n'est pas opaque (Alpha != 255).
         *   [EN] If the color is not opaque (Alpha != 255).
         * </exception>
         **/
        public string VersAnsiPremierPlan()
        {
            if (!EstOpaque)
                throw new InvalidOperationException("Alpha détecté. Utilise VersAnsiPremierPlan(fond) pour composer la transparence.");

            return $"\u001b[38;2;{Rouge};{Vert};{Bleu}m";
        }

        /**
         * <summary>
         *   [FR] Retourne le code ANSI true color pour la couleur de premier plan (foreground).
         *        Si Alpha != 255, la couleur est composée sur le fond avant génération ANSI.
         *   [EN] Returns the ANSI true color code for the foreground color.
         *        If Alpha != 255, the color is alpha-blended over the background before generating ANSI.
         * </summary>
         * <param name="fond">
         *   [FR] Couleur de fond utilisée pour composer l'alpha.
         *   [EN] Background color used for alpha blending.
         * </param>
         * <returns>
         *   [FR] Séquence ANSI de premier plan.
         *   [EN] Foreground ANSI sequence.
         * </returns>
         **/
        public string VersAnsiPremierPlan(Couleur fond) {

            var c = EstOpaque ? this : ComposerSur(fond);

            return $"\u001b[38;2;{c.Rouge};{c.Vert};{c.Bleu}m";
        }

        /**
         * <summary>
         *   [FR] Retourne le code ANSI true color pour la couleur d'arrière-plan (background).
         *        Ne supporte pas l'alpha : si Alpha != 255, utilise <see cref="VersAnsiArrierePlan(Couleur)"/>.
         *   [EN] Returns the ANSI true color code for the background color.
         *        Does not support alpha: if Alpha != 255, use <see cref="VersAnsiArrierePlan(Couleur)"/>.
         * </summary>
         * <returns>
         *   [FR] Séquence ANSI d'arrière-plan.
         *   [EN] Background ANSI sequence.
         * </returns>
         * <exception cref="InvalidOperationException">
         *   [FR] Si la couleur n'est pas opaque (Alpha != 255).
         *   [EN] If the color is not opaque (Alpha != 255).
         * </exception>
         **/
        public string VersAnsiArrierePlan() {

            if (!EstOpaque)
              throw new InvalidOperationException("Alpha détecté. Utilise VersAnsiArrierePlan(fond) pour composer la transparence.");

            return $"\u001b[48;2;{Rouge};{Vert};{Bleu}m";
        }

        /**
         * <summary>
         *   [FR] Retourne le code ANSI true color pour la couleur d'arrière-plan (background).
         *        Si Alpha != 255, la couleur est composée sur le fond avant génération ANSI.
         *   [EN] Returns the ANSI true color code for the background color.
         *        If Alpha != 255, the color is alpha-blended over the background before generating ANSI.
         * </summary>
         * <param name="fond">
         *   [FR] Couleur de fond utilisée pour composer l'alpha.
         *   [EN] Background color used for alpha blending.
         * </param>
         * <returns>
         *   [FR] Séquence ANSI d'arrière-plan.
         *   [EN] Background ANSI sequence.
         * </returns>
         **/
        public string VersAnsiArrierePlan(Couleur fond) {

            var c = EstOpaque ? this : ComposerSur(fond);
            return $"\u001b[48;2;{c.Rouge};{c.Vert};{c.Bleu}m";
        }

        /**
         * <summary>
         *   [FR] Approximation vers ConsoleColor (16 couleurs). Si Alpha != 255, utilise un fond pour composer.
         *   [EN] Approximation to ConsoleColor (16 colors). If Alpha != 255, blend over a background.
         * </summary>
         * <param name="fond">
         *   [FR] Fond utilisé pour la composition alpha si la couleur n'est pas opaque. Si null, un fond noir est utilisé.
         *   [EN] Background used for alpha blending if the color is not opaque. If null, a black background is used.
         * </param>
         * <returns>
         *   [FR] La ConsoleColor la plus proche.
         *   [EN] The closest ConsoleColor.
         * </returns>
         **/
        public ConsoleColor VersConsoleColorApprox(Couleur? fond = null) {

            Couleur c = EstOpaque ? this : this.ComposerSur(fond ?? NoirConsole);
            // Luminance approx (Rec. 709)
            int y = (c.Rouge * 2126 + c.Vert * 7152 + c.Bleu * 722) / 10000;
            bool estGris = Math.Abs(c.Rouge - c.Vert) < 10 && Math.Abs(c.Vert - c.Bleu) < 10;

            if (estGris) {

                if (y < 32) return ConsoleColor.Black;
                if (y < 96) return ConsoleColor.DarkGray;
                if (y < 160) return ConsoleColor.Gray;

                return ConsoleColor.White;
            }

            bool r = c.Rouge >= c.Vert && c.Rouge >= c.Bleu;
            bool g = c.Vert  >= c.Rouge && c.Vert  >= c.Bleu;
            bool b = c.Bleu  >= c.Rouge && c.Bleu  >= c.Vert;
            bool clair = y >= 128;

            if (r && g) return clair ? ConsoleColor.Yellow : ConsoleColor.DarkYellow;
            if (r && b) return clair ? ConsoleColor.Magenta : ConsoleColor.DarkMagenta;
            if (g && b) return clair ? ConsoleColor.Cyan : ConsoleColor.DarkCyan;

            if (r) return clair ? ConsoleColor.Red : ConsoleColor.DarkRed;
            if (g) return clair ? ConsoleColor.Green : ConsoleColor.DarkGreen;
            if (b) return clair ? ConsoleColor.Blue : ConsoleColor.DarkBlue;

            return clair ? ConsoleColor.Gray : ConsoleColor.DarkGray;
        }

        /**
         * <summary>
         *   [FR] Retourne une représentation texte de la couleur.
         *   [EN] Returns a text representation of the color.
         * </summary>
         * <returns>
         *   [FR] Chaîne "RGBA(r,g,b,a)".
         *   [EN] String "RGBA(r,g,b,a)".
         * </returns>
         **/
        public override string ToString() => $"RGBA({Rouge},{Vert},{Bleu},{Alpha})";

        /**
         * <summary>
         *   [FR] Compare cette couleur avec une autre.
         *   [EN] Compares this color with another.
         * </summary>
         * <param name="other">
         *   [FR] Autre couleur.
         *   [EN] Other color.
         * </param>
         * <returns>
         *   [FR] true si identiques.
         *   [EN] true if identical.
         * </returns>
         **/
        public bool Equals(Couleur other) => Rouge == other.Rouge && Vert == other.Vert && Bleu == other.Bleu && Alpha == other.Alpha;

        /**
         * <summary>
         *   [FR] Compare cette couleur avec un objet.
         *   [EN] Compares this color with an object.
         * </summary>
         * <param name="obj">
         *   [FR] Objet à comparer.
         *   [EN] Object to compare.
         * </param>
         * <returns>
         *   [FR] true si l'objet est une <see cref="Couleur"/> identique.
         *   [EN] true if the object is an identical <see cref="Couleur"/>.
         * </returns>
         **/
        public override bool Equals(object? obj) => obj is Couleur other && Equals(other);

        /**
         * <summary>
         *   [FR] Retourne le hash code de la couleur.
         *   [EN] Returns the hash code for the color.
         * </summary>
         * <returns>
         *   [FR] Hash code.
         *   [EN] Hash code.
         * </returns>
         **/
        public override int GetHashCode() => HashCode.Combine(Rouge, Vert, Bleu, Alpha);

        /**
         * <summary>
         *   [FR] Opérateur d'égalité.
         *   [EN] Equality operator.
         * </summary>
         **/
        public static bool operator ==(Couleur left, Couleur right) => left.Equals(right);

        /**
         * <summary>
         *   [FR] Opérateur d'inégalité.
         *   [EN] Inequality operator.
         * </summary>
         **/
        public static bool operator !=(Couleur left, Couleur right) => !left.Equals(right);

        // =========================
        // Palette mini (Hex)
        // =========================

        /**
         * <summary>
         *   [FR] Noir GS (hex) : "#0A0A0A".
         *   [EN] GS black (hex): "#0A0A0A".
         * </summary>
         **/
        public const string HexNoir = "#0A0A0A";

        /**
         * <summary>
         *   [FR] Rouge GS (hex) : "#DC143C".
         *   [EN] GS red (hex): "#DC143C".
         * </summary>
         **/
        public const string HexRouge = "#DC143C";

        /**
         * <summary>
         *   [FR] Turquoise GS (hex) : "#00DCDC".
         *   [EN] GS turquoise (hex): "#00DCDC".
         * </summary>
         **/
        public const string HexTurquoise = "#00DCDC";

        /**
         * <summary>
         *   [FR] Violet GS (hex) : "#8C46C8".
         *   [EN] GS violet (hex): "#8C46C8".
         * </summary>
         **/
        public const string HexViolet = "#8C46C8";

        /**
         * <summary>
         *   [FR] Rouge GS à 50% d'opacité (hex AARRGGBB) : "#80DC143C".
         *   [EN] GS red at 50% opacity (hex AARRGGBB): "#80DC143C".
         * </summary>
         **/
        public const string HexRouge50 = "#80DC143C";

        /**
         * <summary>
         *   [FR] Fond noir par défaut utile pour la console (pour composer l'alpha si aucun fond n'est fourni).
         *   [EN] Default black background useful for console (for alpha blending when no background is provided).
         * </summary>
         **/
        public static readonly Couleur NoirConsole = new(0, 0, 0, 255);

        // =========================
        // Helpers privés
        // =========================

        /**
         * <summary>
         *   [FR] Vérifie si une chaîne ne contient que des caractères hexadécimaux (0-9, A-F, a-f).
         *   [EN] Checks whether a string contains only hexadecimal characters (0-9, A-F, a-f).
         * </summary>
         * <param name="s">
         *   [FR] Chaîne à vérifier.
         *   [EN] String to validate.
         * </param>
         * <returns>
         *   [FR] true si hex valide.
         *   [EN] true if valid hex.
         * </returns>
         **/
        private static bool EstHex(string s) {

            for (int i = 0; i < s.Length; i++) {

                char c = s[i];
                bool ok = (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f');

                if (!ok) return false;
            }
            
            return true;
        }
    }
}
