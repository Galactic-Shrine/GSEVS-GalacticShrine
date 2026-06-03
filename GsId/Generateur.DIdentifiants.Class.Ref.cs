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
using System.Security.Cryptography;

namespace GalacticShrine.GsId {

	/**
	 * <summary>
	 *   [FR] Fournit les mécanismes de génération sécurisée des GsId.
	 *   [EN] Provides secure GsId generation mechanisms.
	 * </summary>
	 **/
	public static class GenerateurDIdentifiants {

		/**
     * <summary>
     *   [FR] Génère un nouveau GsId aléatoire sur 256 bits.
     *   [EN] Generates a new random 256-bit GsId.
     * </summary>
     * <returns>
     *   [FR] Un nouvel identifiant GsId.
     *   [EN] A new GsId identifier.
     * </returns>
     **/
		public static GSID Nouveau() {

			Span<byte> Octets = stackalloc byte[Constantes.LongueurDOctets];
			RandomNumberGenerator.Fill(Octets);

			return new GSID(Octets);
		}
	}
}
