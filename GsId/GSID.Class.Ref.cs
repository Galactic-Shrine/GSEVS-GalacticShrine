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
using System.Buffers.Binary;
using GalacticShrine.Enumeration.GsId;
using GalacticShrine.Exceptions.GsId;

namespace GalacticShrine.GsId {

	/**
	 * <summary>
	 *   [FR] Représente un identifiant GsId 256 bits.
	 *   [EN] Represents a 256-bit GsId identifier.
	 * </summary>
	 **/
	public readonly struct GSID : IEquatable<GSID>, IFormattable {

		/**
		 * <summary>
		 *   [FR] Alphabet hexadécimal en majuscules.
		 *   [EN] Uppercase hexadecimal alphabet.
		 * </summary>
		 **/
		private const string AlphabetHexadecimalMajuscule = "0123456789ABCDEF";

		/**
		 * <summary>
		 *   [FR] Alphabet hexadécimal en minuscules.
		 *   [EN] Lowercase hexadecimal alphabet.
		 * </summary>
		 **/
		private const string AlphabetHexadecimalMinuscule = "0123456789abcdef";

		/**
		 * <summary>
		 *   [FR] Les quatre parties de l'identifiant GsId, chacune étant un entier non signé de 64 bits.
		 *   [EN] The four parts of the GsId identifier, each being a 64-bit unsigned integer.
		 * </summary>
		 **/
		private readonly ulong _Partie1;
		private readonly ulong _Partie2;
		private readonly ulong _Partie3;
		private readonly ulong _Partie4;

		/**
		 * <summary>
		 *   [FR] Constructeur privé pour initialiser les quatre parties de l'identifiant GsId.
		 *   [EN] Private constructor to initialize the four parts of the GsId identifier.
		 * </summary>
		 * <param name="Partie1">
		 *   [FR] La première partie de l'identifiant GsId.
		 *   [EN] The first part of the GsId identifier.
		 * </param>
		 * <param name="Partie2">
		 *   [FR] La deuxième partie de l'identifiant GsId.
		 *   [EN] The second part of the GsId identifier.
		 * </param>
		 * <param name="Partie3">
		 *   [FR] La troisième partie de l'identifiant GsId.
		 *   [EN] The third part of the GsId identifier.
		 * </param>
		 * <param name="Partie4">
		 *   [FR] La quatrième partie de l'identifiant GsId.
		 *   [EN] The fourth part of the GsId identifier.
		 * </param>
		 **/
		private GSID(ulong Partie1, ulong Partie2, ulong Partie3, ulong Partie4) {

			_Partie1 = Partie1;
			_Partie2 = Partie2;
			_Partie3 = Partie3;
			_Partie4 = Partie4;
		}

		internal GSID(ReadOnlySpan<byte> Octets) {

			if(Octets.Length != Constantes.LongueurDOctets) {

				throw new ArgumentException($"Un GsId doit contenir exactement {Constantes.LongueurDOctets} octets.", nameof(Octets));
			}

			_Partie1 = BinaryPrimitives.ReadUInt64BigEndian(Octets[..8]);
			_Partie2 = BinaryPrimitives.ReadUInt64BigEndian(Octets.Slice(8, 8));
			_Partie3 = BinaryPrimitives.ReadUInt64BigEndian(Octets.Slice(16, 8));
			_Partie4 = BinaryPrimitives.ReadUInt64BigEndian(Octets.Slice(24, 8));
		}

		/**
		 * <summary>
		 *   [FR] Représente la valeur GsId vide, composée uniquement de zéros.
		 *   [EN] Represents the empty GsId value, composed only of zeroes.
		 * </summary>
		 **/
		public static GSID Vide => default;

		/**
     * <summary>
     *   [FR] Indique si l'identifiant courant est la valeur vide.
     *   [EN] Indicates whether the current identifier is the empty value.
     * </summary>
     **/
		public bool EstVide => _Partie1 == 0UL && _Partie2 == 0UL && _Partie3 == 0UL && _Partie4 == 0UL;

		/**
     * <summary>
     *   [FR] Génère un nouveau GsId 256 bits aléatoire.
     *   [EN] Generates a new random 256-bit GsId.
     * </summary>
     * <returns>
     *   [FR] Un nouvel identifiant GsId.
     *   [EN] A new GsId identifier.
     * </returns>
     **/
		public static GSID Nouveau() => GenerateurDIdentifiants.Nouveau();

		/**
     * <summary>
     *   [FR] Analyse une chaîne au format GsId et retourne l'identifiant correspondant.
     *   [EN] Parses a GsId string and returns the corresponding identifier.
     * </summary>
     * <param name="Valeur">
     *   [FR] Chaîne GsId à analyser.
     *   [EN] GsId string to parse.
     * </param>
     * <returns>
     *   [FR] L'identifiant GsId analysé.
     *   [EN] The parsed GsId identifier.
     * </returns>
     **/
		public static GSID Analyser(string Valeur) => Parseur.Analyser(Valeur);

		/**
     * <summary>
     *   [FR] Tente d'analyser une chaîne au format GsId sans lever d'exception en cas d'échec.
     *   [EN] Attempts to parse a GsId string without throwing an exception on failure.
     * </summary>
     * <param name="Valeur">
     *   [FR] Chaîne GsId à analyser.
     *   [EN] GsId string to parse.
     * </param>
     * <param name="Resultat">
     *   [FR] Identifiant analysé lorsque l'opération réussit ; sinon <see cref="Vide"/>.
     *   [EN] Parsed identifier when the operation succeeds; otherwise <see cref="Vide"/>.
     * </param>
     * <returns>
     *   [FR] <see langword="true"/> si l'analyse réussit ; sinon <see langword="false"/>.
     *   [EN] <see langword="true"/> when parsing succeeds; otherwise <see langword="false"/>.
     * </returns>
     **/
		public static bool EssayerDAnalyser(string? Valeur, out GSID Resultat)	=> Parseur.EssayerDAnalyser(Valeur, out Resultat);

		/**
     * <summary>
     *   [FR] Retourne les 32 octets bruts de l'identifiant.
     *   [EN] Returns the 32 raw bytes of the identifier.
     * </summary>
     * <returns>
     *   [FR] Tableau contenant les octets bruts du GsId.
     *   [EN] Array containing the raw GsId bytes.
     * </returns>
     **/
		public byte[] VersTableauDOctets() {

			byte[] Octets = new byte[Constantes.LongueurDOctets];
			EcrireOctets(Octets);

			return Octets;
		}

		/**
     * <summary>
     *   [FR] Retourne la représentation chaîne selon le format demandé avec la casse globale par défaut.
     *   [EN] Returns the string representation according to the requested format using the global default casing.
     * </summary>
     * <param name="Format">
     *   [FR] Format de sortie à utiliser.
     *   [EN] Output format to use.
     * </param>
     * <returns>
     *   [FR] Représentation texte du GsId.
     *   [EN] Text representation of the GsId.
     * </returns>
     **/
		public string VersChaine(FormatDIdentifiant Format) => VersChaine(Format, Options.CasseParDefaut);

		/**
     * <summary>
     *   [FR] Retourne la représentation chaîne selon le format et la casse demandés.
     *   [EN] Returns the string representation according to the requested format and casing.
     * </summary>
     * <param name="Format">
     *   [FR] Format de sortie à utiliser.
     *   [EN] Output format to use.
     * </param>
     * <param name="Casse">
     *   [FR] Casse de sortie à utiliser.
     *   [EN] Output casing to use.
     * </param>
     * <returns>
     *   [FR] Représentation texte du GsId.
     *   [EN] Text representation of the GsId.
     * </returns>
     **/
		public string VersChaine(FormatDIdentifiant Format, CasseDIdentifiant Casse) => Format switch {
      
      FormatDIdentifiant.N => FormaterN(Casse),
      FormatDIdentifiant.D => FormaterD(Casse),
      _ => throw new GsIdFormatException($"Le format GsId '{Format}' n'est pas supporté."),
		};

		/**
     * <summary>
     *   [FR] Retourne la représentation normalisée au format N avec la casse globale par défaut.
     *   [EN] Returns the normalized N-format representation using the global default casing.
     * </summary>
     * <returns>
     *   [FR] Chaîne normalisée au format N.
     *   [EN] Normalized N-format string.
     * </returns>
     **/
		public string VersChaineNormalisee() => VersChaine(FormatDIdentifiant.N, Options.CasseParDefaut);

		/**
     * <summary>
     *   [FR] Retourne la représentation normalisée au format N avec la casse demandée.
     *   [EN] Returns the normalized N-format representation using the requested casing.
     * </summary>
     * <param name="Casse">
     *   [FR] Casse de sortie à utiliser.
     *   [EN] Output casing to use.
     * </param>
     * <returns>
     *   [FR] Chaîne normalisée au format N.
     *   [EN] Normalized N-format string.
     * </returns>
     **/
		public string VersChaineNormalisee(CasseDIdentifiant Casse) => VersChaine(FormatDIdentifiant.N, Casse);

		/**
     * <summary>
     *   [FR] Retourne la représentation texte par défaut selon <see cref="Options.FormatTexteParDefaut"/> et <see cref="Options.CasseParDefaut"/>.
     *   [EN] Returns the default text representation according to <see cref="Options.FormatTexteParDefaut"/> and <see cref="Options.CasseParDefaut"/>.
     * </summary>
     * <returns>
     *   [FR] Représentation texte par défaut du GsId.
     *   [EN] Default text representation of the GsId.
     * </returns>
     **/
		public override string ToString() => VersChaine(Options.FormatTexteParDefaut, Options.CasseParDefaut);

		/**
     * <summary>
     *   [FR] Retourne une représentation formatée compatible avec <see cref="IFormattable"/>.
     *   [EN] Returns a formatted representation compatible with <see cref="IFormattable"/>.
     * </summary>
     * <param name="Format">
     *   [FR] Format demandé. `N` et `D` forcent les majuscules ; `n` et `d` forcent les minuscules.
     *   [EN] Requested format. `N` and `D` force uppercase; `n` and `d` force lowercase.
     * </param>
     * <param name="FournisseurDeFormat">
     *   [FR] Fournisseur de format, non utilisé.
     *   [EN] Format provider, unused.
     * </param>
     * <returns>
     *   [FR] Représentation texte formatée.
     *   [EN] Formatted text representation.
     * </returns>
     **/
		public string ToString(string? Format, IFormatProvider? FournisseurDeFormat) {

			_ = FournisseurDeFormat;

			if(string.IsNullOrWhiteSpace(Format)) {

				return ToString();
			}

			return Format[0] switch {

				'N' => VersChaine(FormatDIdentifiant.N, CasseDIdentifiant.Majuscules),
				'D' => VersChaine(FormatDIdentifiant.D, CasseDIdentifiant.Majuscules),
				'n' => VersChaine(FormatDIdentifiant.N, CasseDIdentifiant.Minuscules),
				'd' => VersChaine(FormatDIdentifiant.D, CasseDIdentifiant.Minuscules),
				_ => throw new GsIdFormatException($"Le format GsId '{Format}' n'est pas supporté."),
			};
		}

		/**
     * <summary>
     *   [FR] Tente d'écrire la représentation formatée dans un tampon de caractères.
     *   [EN] Attempts to write the formatted representation into a character buffer.
     * </summary>
     * <param name="Destination">
     *   [FR] Tampon de destination.
     *   [EN] Destination buffer.
     * </param>
     * <param name="CharsWritten">
     *   [FR] Nombre de caractères écrits.
     *   [EN] Number of characters written.
     * </param>
     * <param name="Format">
     *   [FR] Format demandé. Vide utilise les options globales ; `N`/`D` majuscules ; `n`/`d` minuscules.
     *   [EN] Requested format. Vide uses global options; `N`/`D` uppercase; `n`/`d` lowercase.
     * </param>
     * <returns>
     *   [FR] <see langword="true"/> si l'écriture réussit ; sinon <see langword="false"/>.
     *   [EN] <see langword="true"/> when writing succeeds; otherwise <see langword="false"/>.
     * </returns>
     **/
		public bool EssayerFormater(Span<char> Destination, out int CharsWritten, ReadOnlySpan<char> Format = default) {

			if(Format.IsEmpty) {

				return EssayerFormater(Destination, out CharsWritten, Options.FormatTexteParDefaut, Options.CasseParDefaut);
			}

			return Format[0] switch {

				'N' => EssayerFormater(Destination, out CharsWritten, FormatDIdentifiant.N, CasseDIdentifiant.Majuscules),
				'D' => EssayerFormater(Destination, out CharsWritten, FormatDIdentifiant.D, CasseDIdentifiant.Majuscules),
				'n' => EssayerFormater(Destination, out CharsWritten, FormatDIdentifiant.N, CasseDIdentifiant.Minuscules),
				'd' => EssayerFormater(Destination, out CharsWritten, FormatDIdentifiant.D, CasseDIdentifiant.Minuscules),
				_ => throw new GsIdFormatException($"Le format GsId '{Format[0]}' n'est pas supporté."),
			};
		}

		/**
     * <summary>
     *   [FR] Tente d'écrire la représentation formatée avec la casse globale par défaut.
     *   [EN] Attempts to write the formatted representation using the global default casing.
     * </summary>
     * <param name="Destination">
     *   [FR] Tampon de destination.
     *   [EN] Destination buffer.
     * </param>
     * <param name="CharsWritten">
     *   [FR] Nombre de caractères écrits.
     *   [EN] Number of characters written.
     * </param>
     * <param name="Format">
     *   [FR] Format de sortie à utiliser.
     *   [EN] Output format to use.
     * </param>
     * <returns>
     *   [FR] <see langword="true"/> si l'écriture réussit ; sinon <see langword="false"/>.
     *   [EN] <see langword="true"/> when writing succeeds; otherwise <see langword="false"/>.
     * </returns>
     **/
		public bool EssayerFormater(Span<char> Destination, out int CharsWritten, FormatDIdentifiant Format)
      => EssayerFormater(Destination, out CharsWritten, Format, Options.CasseParDefaut);

		/**
     * <summary>
     *   [FR] Tente d'écrire la représentation formatée avec le format et la casse demandés.
     *   [EN] Attempts to write the formatted representation using the requested format and casing.
     * </summary>
     * <param name="Destination">
     *   [FR] Tampon de destination.
     *   [EN] Destination buffer.
     * </param>
     * <param name="CharsWritten">
     *   [FR] Nombre de caractères écrits.
     *   [EN] Number of characters written.
     * </param>
     * <param name="Format">
     *   [FR] Format de sortie à utiliser.
     *   [EN] Output format to use.
     * </param>
     * <param name="Casse">
     *   [FR] Casse de sortie à utiliser.
     *   [EN] Output casing to use.
     * </param>
     * <returns>
     *   [FR] <see langword="true"/> si l'écriture réussit ; sinon <see langword="false"/>.
     *   [EN] <see langword="true"/> when writing succeeds; otherwise <see langword="false"/>.
     * </returns>
     **/
		public bool EssayerFormater(Span<char> Destination, out int CharsWritten, FormatDIdentifiant Format, CasseDIdentifiant Casse) {

			Span<char> ValeurNormalisee = stackalloc char[Constantes.LongueurHexadecimale];
			EcrireCaracteresNormalises(ValeurNormalisee, Casse);

			if(Format == FormatDIdentifiant.N) {

				if(Destination.Length < Constantes.LongueurHexadecimale) {

					CharsWritten = 0;
					return false;
				}

				ValeurNormalisee.CopyTo(Destination);
				CharsWritten = Constantes.LongueurHexadecimale;
				return true;
			}

			if(Format == FormatDIdentifiant.D) {

				if(Destination.Length < Constantes.LongueurFormatee) {

					CharsWritten = 0;
					return false;
				}

				ValeurNormalisee[..16].CopyTo(Destination[..16]);
				Destination[16] = '-';

				ValeurNormalisee.Slice(16, 8).CopyTo(Destination.Slice(17, 8));
				Destination[25] = '-';

				ValeurNormalisee.Slice(24, 8).CopyTo(Destination.Slice(26, 8));
				Destination[34] = '-';

				ValeurNormalisee.Slice(32, 8).CopyTo(Destination.Slice(35, 8));
				Destination[43] = '-';

				ValeurNormalisee.Slice(40, 8).CopyTo(Destination.Slice(44, 8));
				Destination[52] = '-';

				ValeurNormalisee.Slice(48, 16).CopyTo(Destination.Slice(53, 16));

				CharsWritten = Constantes.LongueurFormatee;
				return true;
			}

			throw new GsIdFormatException(string.Format("Le format GsId '{0}' n'est pas supporté.", Format));
		}

		/**
     * <summary>
     *   [FR] Compare l'identifiant courant avec un autre GsId.
     *   [EN] Compares the current identifier with another GsId.
     * </summary>
     * <param name="Autre">
     *   [FR] Autre identifiant à comparer.
     *   [EN] Autre identifier to compare.
     * </param>
     * <returns>
     *   [FR] <see langword="true"/> si les deux identifiants sont égaux ; sinon <see langword="false"/>.
     *   [EN] <see langword="true"/> when both identifiers are equal; otherwise <see langword="false"/>.
     * </returns>
     **/
		public bool Equals(GSID Autre) => _Partie1 == Autre._Partie1 && _Partie2 == Autre._Partie2 && _Partie3 == Autre._Partie3 && _Partie4 == Autre._Partie4;

		/**
     * <summary>
     *   [FR] Compare l'identifiant courant avec un objet.
     *   [EN] Compares the current identifier with an object.
     * </summary>
     * <param name="Objet">
     *   [FR] Objet à comparer.
     *   [EN] Objet to compare.
     * </param>
     * <returns>
     *   [FR] <see langword="true"/> si l'objet représente le même GsId ; sinon <see langword="false"/>.
     *   [EN] <see langword="true"/> when the object represents the same GsId; otherwise <see langword="false"/>.
     * </returns>
     **/
		public override bool Equals(object? Objet) => Objet is GSID Autre && Equals(Autre);

		/**
     * <summary>
     *   [FR] Retourne le code de hachage de l'identifiant courant.
     *   [EN] Returns the hash code of the current identifier.
     * </summary>
     * <returns>
     *   [FR] Code de hachage de l'identifiant.
     *   [EN] Hash code of the identifier.
     * </returns>
     **/
		public override int GetHashCode() => HashCode.Combine(_Partie1, _Partie2, _Partie3, _Partie4);

		/**
     * <summary>
     *   [FR] Indique si deux GsId sont égaux.
     *   [EN] Indicates whether two GsId values are equal.
     * </summary>
     * <param name="Gauche">
     *   [FR] Premier GsId à comparer.
     *   [EN] First GsId to compare.
     * </param>
     * <param name="Droite">
     *   [FR] Second GsId à comparer.
     *   [EN] Second GsId to compare.
     * </param>
     * <returns>
     *   [FR] <see langword="true"/> si les valeurs sont égales ; sinon <see langword="false"/>.
     *   [EN] <see langword="true"/> when the values are equal; otherwise <see langword="false"/>.
     * </returns>
     **/
		public static bool operator ==(GSID Gauche, GSID Droite) => Gauche.Equals(Droite);

		/**
     * <summary>
     *   [FR] Indique si deux GsId sont différents.
     *   [EN] Indicates whether two GsId values are different.
     * </summary>
     * <param name="Gauche">
     *   [FR] Premier GsId à comparer.
     *   [EN] First GsId to compare.
     * </param>
     * <param name="Droite">
     *   [FR] Second GsId à comparer.
     *   [EN] Second GsId to compare.
     * </param>
     * <returns>
     *   [FR] <see langword="true"/> si les valeurs sont différentes ; sinon <see langword="false"/>.
     *   [EN] <see langword="true"/> when the values are different; otherwise <see langword="false"/>.
     * </returns>
     **/
		public static bool operator !=(GSID Gauche, GSID Droite) => !Gauche.Equals(Droite);

		/**
     * <summary>
     *   [FR] Formate l'identifiant au format N avec la casse demandée.
     *   [EN] Formats the identifier in N format with the requested casing.
     * </summary>
     * <param name="Casse">
     *   [FR] Casse de sortie à utiliser.
     *   [EN] Output casing to use.
     * </param>
     * <returns>
     *   [FR] Chaîne formatée au format N.
     *   [EN] Formatted string in N format.
     * </returns>
     **/
		private string FormaterN(CasseDIdentifiant Casse) {

			Span<char> Tampon = stackalloc char[Constantes.LongueurHexadecimale];
			_ = EssayerFormater(Tampon, out _, FormatDIdentifiant.N, Casse);

			return new string(Tampon);
		}

		/**
     * <summary>
     *   [FR] Formate l'identifiant au format D avec la casse demandée.
     *   [EN] Formats the identifier in D format with the requested casing.
     * </summary>
     * <param name="Casse">
     *   [FR] Casse de sortie à utiliser.
     *   [EN] Output casing to use.
     * </param>
     * <returns>
     *   [FR] Chaîne formatée au format D.
     *   [EN] Formatted string in D format.
     * </returns>
     **/
		private string FormaterD(CasseDIdentifiant Casse) {

			Span<char> Tampon = stackalloc char[Constantes.LongueurFormatee];
			_ = EssayerFormater(Tampon, out _, FormatDIdentifiant.D, Casse);

			return new string(Tampon);
		}

		/**
     * <summary>
     *   [FR] Écrit les caractères hexadécimaux normalisés de l'identifiant dans un tampon.
     *   [EN] Writes the normalized hexadecimal characters of the identifier into a buffer.
     * </summary>
     * <param name="Destination">
     *   [FR] Tampon de destination pour les caractères normalisés.
     *   [EN] Destination buffer for the normalized characters.
     * </param>
     * <param name="Casse">
     *   [FR] Casse de sortie à utiliser pour les caractères hexadécimaux.
     *   [EN] Output casing to use for the hexadecimal characters.
     * </param>
     **/
		private void EcrireCaracteresNormalises(Span<char> Destination, CasseDIdentifiant Casse) {

			string AlphabetHexadecimal = Casse == CasseDIdentifiant.Minuscules ? AlphabetHexadecimalMinuscule : AlphabetHexadecimalMajuscule;

			Span<byte> Octets = stackalloc byte[Constantes.LongueurDOctets];
			EcrireOctets(Octets);

			for(int Indice = 0; Indice < Octets.Length; Indice++) {

				byte Valeur = Octets[Indice];
				Destination[Indice * 2] = AlphabetHexadecimal[Valeur >> 4];
				Destination[(Indice * 2) + 1] = AlphabetHexadecimal[Valeur & 0x0F];
			}
		}

		/**
     * <summary>
     *   [FR] Écrit les octets bruts de l'identifiant dans un tampon.
     *   [EN] Writes the raw bytes of the identifier into a buffer.
     * </summary>
     * <param name="Destination">
     *   [FR] Tampon de destination pour les octets bruts.
     *   [EN] Destination buffer for the raw bytes.
     * </param>
     **/
		private void EcrireOctets(Span<byte> Destination) {

			if(Destination.Length < Constantes.LongueurDOctets) {

				throw new ArgumentException($"Le tampon de destination doit contenir au moins {Constantes.LongueurDOctets} octets.", nameof(Destination));
			}

			BinaryPrimitives.WriteUInt64BigEndian(Destination[..8], _Partie1);
			BinaryPrimitives.WriteUInt64BigEndian(Destination.Slice(8, 8), _Partie2);
			BinaryPrimitives.WriteUInt64BigEndian(Destination.Slice(16, 8), _Partie3);
			BinaryPrimitives.WriteUInt64BigEndian(Destination.Slice(24, 8), _Partie4);
		}
	}
}
