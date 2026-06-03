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

namespace GalacticShrine.GsId {

	/**
	 * <summary>
	 *   [FR] Définit les constantes officielles du format GsId.
	 *   [EN] Defines the official constants of the GsId format.
	 * </summary>
	 **/
  public static class Constantes {

		/**
     * <summary>
     *   [FR] Nombre d'octets bruts d'un GsId.
     *   [EN] Number of raw bytes contained in a GsId.
     * </summary>
     **/
		public const int LongueurDOctets = 32;

		/**
     * <summary>
     *   [FR] Nombre de caractères hexadécimaux d'un GsId sans tirets.
     *   [EN] Number of hexadecimal characters of a GsId without hyphens.
     * </summary>
     **/
		public const int LongueurHexadecimale = 64;

		/**
     * <summary>
     *   [FR] Nombre de caractères d'un GsId au format D.
     *   [EN] Number of characters of a GsId in D format.
     * </summary>
     **/
		public const int LongueurFormatee = 69;

		/**
     * <summary>
     *   [FR] Nombre de tirets du format D.
     *   [EN] Number of hyphens in the D format.
     * </summary>
     **/
		public const int NombreDeTirets = 5;

		/**
     * <summary>
     *   [FR] Motif officiel des groupes pour le format D.
     *   [EN] Official group pattern for the D format.
     * </summary>
     **/
		public const string MotifDeGroupesD = "16-8-8-8-8-16";
	}
}
