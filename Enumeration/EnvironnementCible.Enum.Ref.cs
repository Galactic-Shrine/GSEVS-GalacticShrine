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

namespace GalacticShrine.Enumeration {

	/**
	 * <summary>
	 *   [FR] Représente la cible d'une variable d'environnement.
	 *   [EN] Represents the target of an environment variable.
	 * </summary>
	 */
	public enum EnvironnementCible {

		/**
     * <summary>
     *   [FR] Variable disponible uniquement pour le processus courant.
     *   [EN] Variable available only for the current process.
     * </summary>
     */
		Processus = 0,

		/**
     * <summary>
     *   [FR] Variable disponible pour l'utilisateur courant.
     *   [EN] Variable available for the current user.
     * </summary>
     */
		Utilisateur = 1,

		/**
     * <summary>
     *   [FR] Variable disponible pour toute la machine.
     *   [EN] Variable available for the whole machine.
     * </summary>
     */
		Machine = 2
	}
}
