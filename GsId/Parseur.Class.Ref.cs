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
using GalacticShrine.Enumeration.GsId;
using GalacticShrine.Exceptions.GsId;

namespace GalacticShrine.GsId {

	/**
	 * <summary>
	 *   [FR] Fournit les opérations de normalisation et de conversion des chaînes GsId.
	 *   [EN] Provides normalization and conversion operations for GsId strings.
	 * </summary>
	 **/
	public static class Parseur {

		/**
     * <summary>
     *   [FR] Analyse une chaîne GsId et retourne l'identifiant correspondant.
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
		public static GSID Analyser(string Valeur) {

			string ValeurNormalisee = Normaliser(Valeur, CasseDIdentifiant.Majuscules);
			Span<byte> Octets = stackalloc byte[Constantes.LongueurDOctets];

			for(int Indice = 0; Indice < Constantes.LongueurDOctets; Indice++) {

				int Gauche = ConvertirCaractereHexadecimal(ValeurNormalisee[Indice * 2]);
				int Droite = ConvertirCaractereHexadecimal(ValeurNormalisee[(Indice * 2) + 1]);
				Octets[Indice] = (byte)((Gauche << 4) | Droite);
			}

			return new GSID(Octets);
		}

		/**
     * <summary>
     *   [FR] Tente d'analyser une chaîne GsId sans lever d'exception en cas d'échec.
     *   [EN] Attempts to parse a GsId string without throwing an exception on failure.
     * </summary>
     * <param name="Valeur">
     *   [FR] Chaîne GsId à analyser.
     *   [EN] GsId string to parse.
     * </param>
     * <param name="Resultat">
     *   [FR] Identifiant analysé lorsque l'opération réussit ; sinon <see cref="GSID.Vide"/>.
     *   [EN] Parsed identifier when the operation succeeds; otherwise <see cref="GSID.Vide"/>.
     * </param>
     * <returns>
     *   [FR] <see langword="true"/> si l'analyse réussit ; sinon <see langword="false"/>.
     *   [EN] <see langword="true"/> when parsing succeeds; otherwise <see langword="false"/>.
     * </returns>
     **/
		public static bool EssayerDAnalyser(string? Valeur, out GSID Resultat) {

			Resultat = GSID.Vide;

			if(string.IsNullOrWhiteSpace(Valeur)) {

				return false;
			}

			try {

				Resultat = Analyser(Valeur);
				return true;
			}
			catch(GsIdFormatException) {

				return false;
			}
		}

		/**
     * <summary>
     *   [FR] Normalise une chaîne GsId vers son format N en utilisant la casse globale par défaut.
     *   [EN] Normalizes a GsId string to its N format using the global default casing.
     * </summary>
     * <param name="Valeur">
     *   [FR] Valeur GsId à normaliser.
     *   [EN] GsId value to normalize.
     * </param>
     * <returns>
     *   [FR] Chaîne normalisée au format N.
     *   [EN] Normalized N-format string.
     * </returns>
     **/
		public static string Normaliser(string Valeur) => Normaliser(Valeur, Options.CasseParDefaut);

		/**
     * <summary>
     *   [FR] Normalise une chaîne GsId vers son format N avec la casse demandée.
     *   [EN] Normalizes a GsId string to its N format using the requested casing.
     * </summary>
     * <param name="Valeur">
     *   [FR] Valeur GsId à normaliser.
     *   [EN] GsId value to normalize.
     * </param>
     * <param name="Casse">
     *   [FR] Casse de sortie à appliquer.
     *   [EN] Output casing to apply.
     * </param>
     * <returns>
     *   [FR] Chaîne normalisée au format N.
     *   [EN] Normalized N-format string.
     * </returns>
     **/
		public static string Normaliser(string Valeur, CasseDIdentifiant Casse) {

			if(string.IsNullOrWhiteSpace(Valeur)) {

				throw new GsIdFormatException("La valeur GsId ne peut pas être nulle, vide ou blanche.");
			}

			string ValeurRognee = Valeur.Trim();

			if(ValeurRognee.Length == Constantes.LongueurHexadecimale) {

				return NormaliserN(ValeurRognee, Casse);
			}

			if(ValeurRognee.Length == Constantes.LongueurFormatee) {

				return NormaliserD(ValeurRognee, Casse);
			}

			throw new GsIdFormatException(
					$"La valeur GsId doit contenir {Constantes.LongueurHexadecimale} caractères sans tirets ou {Constantes.LongueurFormatee} caractères avec tirets.");
		}

		/**
		 * <summary>
		 *   [FR] Normalise une chaîne GsId vers son format D en utilisant la casse globale par défaut.
		 *   [EN] Normalizes a GsId string to its D format using the global default casing.
		 * </summary>
		 * <param name="Valeur">
		 *   [FR] Valeur GsId à normaliser.
		 *   [EN] GsId value to normalize.
		 * </param>
		 * <returns>
		 *   [FR] Chaîne normalisée au format D.
		 *   [EN] Normalized D-format string.
		 * </returns>
		 **/
		private static string NormaliserN(string Valeur, CasseDIdentifiant Casse) {

			Span<char> Tampon = stackalloc char[Constantes.LongueurHexadecimale];

			for(int Indice = 0; Indice < Valeur.Length; Indice++) {

				char Caractere = Valeur[Indice];

				if(!EstCaractereHexadecimal(Caractere)) {

					throw new GsIdFormatException($"Le caractère '{Caractere}' n'est pas hexadécimal.");
				}

				Tampon[Indice] = AppliquerCasse(Caractere, Casse);
			}

			return new string(Tampon);
		}

		/**
		 * <summary>
		 *   [FR] Normalise une chaîne GsId vers son format D en utilisant la casse globale par défaut.
		 *   [EN] Normalizes a GsId string to its D format using the global default casing.
		 * </summary>
		 * <param name="Valeur">
		 *   [FR] Valeur GsId à normaliser.
		 *   [EN] GsId value to normalize.
		 * </param>
		 * <param name="Casse">
		 *   [FR] Casse de sortie à appliquer.
		 *   [EN] Output casing to apply.
		 * </param>
		 * <returns>
		 *   [FR] Chaîne normalisée au format D.
		 *   [EN] Normalized D-format string.
		 * </returns>
		 **/
		private static string NormaliserD(string Valeur, CasseDIdentifiant Casse) {

			Span<char> Tampon = stackalloc char[Constantes.LongueurHexadecimale];
			int IndexTampon = 0;

			for(int Indice = 0; Indice < Valeur.Length; Indice++) {

				char Caractere = Valeur[Indice];

				if(EstPositionDeTiret(Indice)) {

					if(Caractere != '-') {

						throw new GsIdFormatException("La valeur GsId n'utilise pas les positions de tirets officielles.");
					}

					continue;
				}

				if(!EstCaractereHexadecimal(Caractere)) {
					throw new GsIdFormatException($"Le caractère '{Caractere}' n'est pas hexadécimal.");
				}

				Tampon[IndexTampon++] = AppliquerCasse(Caractere, Casse);
			}

			return new string(Tampon);
		}

		/**
		 * <summary>
		 *   [FR] Indique si l'indice correspond à une position de tiret officielle dans le format D.
		 *   [EN] Indicates whether the index corresponds to an official hyphen position in the D format.
		 * </summary>
		 * <param name="Indice">
		 *   [FR] Indice à vérifier.
		 *   [EN] Index to check.
		 * </param>
		 * <returns>
		 *   [FR] <see langword="true"/> si l'indice correspond à une position de tiret ; sinon <see langword="false"/>.
		 *   [EN] <see langword="true"/> when the index corresponds to a hyphen position; otherwise <see langword="false"/>.
		 * </returns>
		 **/
		private static bool EstPositionDeTiret(int Indice) => Indice is 16 or 25 or 34 or 43 or 52;

		/**
		 * <summary>
		 *   [FR] Indique si le caractère est un chiffre hexadécimal valide (0-9, a-f, A-F).
		 *   [EN] Indicates whether the character is a valid hexadecimal digit (0-9, a-f, A-F).
		 * </summary>
		 * <param name="Caractere">
		 *   [FR] Caractère à vérifier.
		 *   [EN] Character to check.
		 * </param>
		 * <returns>
		 *   [FR] <see langword="true"/> si le caractère est hexadécimal ; sinon <see langword="false"/>.
		 *   [EN] <see langword="true"/> when the character is hexadecimal; otherwise <see langword="false"/>.
		 * </returns>
		 **/
		private static bool EstCaractereHexadecimal(char Caractere)	
			=> (Caractere >= '0' && Caractere <= '9') 
				|| (Caractere >= 'a' && Caractere <= 'f') 
				|| (Caractere >= 'A' && Caractere <= 'F');

		/**
		 * <summary>
		 *   [FR] Applique la casse demandée à un caractère hexadécimal.
		 *   [EN] Applies the requested casing to a hexadecimal character.
		 * </summary>
		 * <param name="Caractere">
		 *   [FR] Caractère hexadécimal à transformer.
		 *   [EN] Hexadecimal character to transform.
		 * </param>
		 * <param name="Casse">
		 *   [FR] Casse à appliquer.
		 *   [EN] Casing to apply.
		 * </param>
		 * <returns>
		 *   [FR] Caractère transformé selon la casse demandée.
		 *   [EN] Character transformed according to the requested casing.
		 * </returns>
		 **/
		private static char AppliquerCasse(char Caractere, CasseDIdentifiant Casse) 
			=> Casse == CasseDIdentifiant.Minuscules 
				? char.ToLowerInvariant(Caractere) 
				: char.ToUpperInvariant(Caractere);

		/**
		 * <summary>
		 *   [FR] Convertit un caractère hexadécimal en sa valeur numérique correspondante.
		 *   [EN] Converts a hexadecimal character to its corresponding numeric value.
		 * </summary>
		 * <param name="Caractere">
		 *   [FR] Caractère hexadécimal à convertir.
		 *   [EN] Hexadecimal character to convert.
		 * </param>
		 * <returns>
		 *   [FR] Valeur numérique du caractère hexadécimal.
		 *   [EN] Numeric value of the hexadecimal character.
		 * </returns>
		 **/
		private static int ConvertirCaractereHexadecimal(char Caractere) => Caractere switch {
			>= '0' and <= '9' => Caractere - '0',
			>= 'a' and <= 'f' => 10 + (Caractere - 'a'),
			>= 'A' and <= 'F' => 10 + (Caractere - 'A'),
			_ => throw new GsIdFormatException($"Le caractère '{Caractere}' n'est pas hexadécimal."),
		};
	}
}
