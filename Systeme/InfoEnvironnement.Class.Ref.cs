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

using GalacticShrine.Enumeration;

namespace GalacticShrine.Systeme {

	/**
   * <summary>
   *   [FR] Représente les informations d'une variable d'environnement.
   *   [EN] Represents information about an environment variable.
   * </summary>
   */
	public sealed class InfoEnvironnement {

		/**
     * <summary>
     *   [FR] Obtient ou définit le nom de la variable d'environnement.
     *   [EN] Gets or sets the name of the environment variable.
     * </summary>
     */
		public string Nom { get; init; } = string.Empty;

		/**
     * <summary>
     *   [FR] Obtient ou définit la cible interrogée.
     *   [EN] Gets or sets the queried target.
     * </summary>
     */
		public EnvironnementCible Cible { get; init; }

		/**
     * <summary>
     *   [FR] Obtient ou définit une valeur indiquant si la variable existe.
     *   [EN] Gets or sets a value indicating whether the variable exists.
     * </summary>
     */
		public bool Existe { get; init; }

		/**
     * <summary>
     *   [FR] Obtient ou définit une valeur indiquant si la variable est vide.
     *   [EN] Gets or sets a value indicating whether the variable is empty.
     * </summary>
     */
		public bool EstVide { get; init; }

		/**
     * <summary>
     *   [FR] Obtient ou définit la valeur brute de la variable.
     *   [EN] Gets or sets the raw value of the variable.
     * </summary>
     */
		public string? Valeur { get; init; }
	}
}
