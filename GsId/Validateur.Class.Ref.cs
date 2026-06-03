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

using GalacticShrine.Enumeration.GsId;

namespace GalacticShrine.GsId {

	/**
	 * <summary>
	 *   [FR] Fournit les opérations de validation des représentations textuelles GsId.
	 *   [EN] Provides validation operations for textual GsId representations.
	 * </summary>
	 **/
	public static class Validateur {

		/**
     * <summary>
     *   [FR] Indique si la valeur représente un GsId valide, quel que soit le format supporté.
     *   [EN] Indicates whether the value represents a valid GsId in any supported format.
     * </summary>
     * <param name="Valeur">
     *   [FR] Valeur à valider.
     *   [EN] Valeur to validate.
     * </param>
     * <returns>
     *   [FR] <see langword="true"/> si la valeur est valide ; sinon <see langword="false"/>.
     *   [EN] <see langword="true"/> when the value is valid; otherwise <see langword="false"/>.
     * </returns>
     **/
		public static bool EstValide(string? Valeur) => Parseur.EssayerDAnalyser(Valeur, out _);

		/**
     * <summary>
     *   [FR] Indique si la valeur représente un GsId valide dans un format précis.
     *   [EN] Indicates whether the value represents a valid GsId in a specific format.
     * </summary>
     * <param name="Valeur">
     *   [FR] Valeur à valider.
     *   [EN] Valeur to validate.
     * </param>
     * <param name="Format">
     *   [FR] Format attendu.
     *   [EN] Expected format.
     * </param>
     * <returns>
     *   [FR] <see langword="true"/> si la valeur correspond au format attendu ; sinon <see langword="false"/>.
     *   [EN] <see langword="true"/> when the value matches the expected format; otherwise <see langword="false"/>.
     * </returns>
     **/
		public static bool EstValide(string? Valeur, FormatDIdentifiant Format) {

			if(string.IsNullOrWhiteSpace(Valeur)) {

				return false;
			}

			return Format switch {

				FormatDIdentifiant.N => Valeur.Trim().Length == Constantes.LongueurHexadecimale && EstValide(Valeur),
				FormatDIdentifiant.D => Valeur.Trim().Length == Constantes.LongueurFormatee && EstValide(Valeur),
				_ => false,
			};
		}
	}
}
