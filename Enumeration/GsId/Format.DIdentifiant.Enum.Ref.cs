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

namespace GalacticShrine.Enumeration.GsId {

	/**
   * <summary>
   *   [FR] Représente les formats de chaîne supportés par GsId.
   *   [EN] Represents the string formats supported by GsId.
   * </summary>
   **/
	public enum FormatDIdentifiant {

		/**
     * <summary>
     *   [FR] Format normalisé sans tirets, sur 64 caractères.
     *   [EN] Normalized format without hyphens, using 64 characters.
     * </summary>
     **/
		N = 0,

		/**
     * <summary>
     *   [FR] Format lisible avec 5 tirets selon le motif 16-8-8-8-8-16.
     *   [EN] Readable format with 5 hyphens using the 16-8-8-8-8-16 pattern.
     * </summary>
     **/
		D = 1,
	}
}
