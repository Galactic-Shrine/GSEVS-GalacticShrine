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

namespace GalacticShrine.GsId {

	/**
	 * <summary>
	 *   [FR] Centralise les options globales du système GsId.
	 *   [EN] Centralizes the global options of the GsId system.
	 * </summary>
	 **/
	public static class Options {

		/**
		 * <summary>
		 *   [FR] Casse par défaut utilisée par les opérations qui ne la précisent pas explicitement.
		 *   [EN] Default casing used by operations that do not explicitly specify it.
		 * </summary>
		 **/
		private static CasseDIdentifiant _CasseParDefaut = CasseDIdentifiant.Majuscules;

		/**
		 * <summary>
		 *   [FR] Format texte par défaut utilisé par ToString() et EssayerFormater() sans format explicite.
		 *   [EN] Default text format used by ToString() and EssayerFormater() without an explicit format.
		 * </summary>
		 **/
		private static FormatDIdentifiant _FormatTexteParDefaut = FormatDIdentifiant.D;

		/**
		 * <summary>
		 *   [FR] Format JSON par défaut utilisé par ConvertisseurJson sans format explicite.
		 *   [EN] Default JSON format used by ConvertisseurJson without an explicit format.
		 * </summary>
		 **/
		private static FormatDIdentifiant _FormatJsonParDefaut = FormatDIdentifiant.D;
		
		/**
		 * <summary>
		 *   [FR] Format base de données par défaut utilisé par les intégrations de persistance.
		 *   [EN] Default database format used by persistence integrations.
		 * </summary>
		 **/
		private static FormatDIdentifiant _FormatBaseDeDonneesParDefaut = FormatDIdentifiant.N;

		/**
		 * <summary>
		 *   [FR] Indique si les options globales sont verrouillées.
		 *   [EN] Indicates whether the global options are locked.
		 * </summary>
		 **/
		private static bool _EstVerrouille;

		/**
     * <summary>
     *   [FR] Définit la casse par défaut utilisée par les opérations qui ne la précisent pas explicitement.
     *   [EN] Defines the default casing used by operations that do not explicitly specify it.
     * </summary>
     **/
		public static CasseDIdentifiant CasseParDefaut {

			get => _CasseParDefaut;
			set {

				VerifierNonVerrouille();
				_CasseParDefaut = value;
			}
		}

		/**
     * <summary>
     *   [FR] Définit le format texte par défaut utilisé par ToString() et EssayerFormater() sans format explicite.
     *   [EN] Defines the default text format used by ToString() and EssayerFormater() without an explicit format.
     * </summary>
     **/
		public static FormatDIdentifiant FormatTexteParDefaut {

			get => _FormatTexteParDefaut;
			set {

				VerifierNonVerrouille();
				_FormatTexteParDefaut = ValiderFormat(value, nameof(FormatTexteParDefaut));
			}
		}

		/**
     * <summary>
     *   [FR] Définit le format JSON par défaut utilisé par ConvertisseurJson sans format explicite.
     *   [EN] Defines the default JSON format used by ConvertisseurJson without an explicit format.
     * </summary>
     **/
		public static FormatDIdentifiant FormatJsonParDefaut {

			get => _FormatJsonParDefaut;
			set {

				VerifierNonVerrouille();
				_FormatJsonParDefaut = ValiderFormat(value, nameof(FormatJsonParDefaut));
			}
		}

		/**
     * <summary>
     *   [FR] Définit le format base de données par défaut utilisé par les intégrations de persistance.
     *   [EN] Defines the default database format used by persistence integrations.
     * </summary>
     **/
		public static FormatDIdentifiant FormatBaseDeDonneesParDefaut {

			get => _FormatBaseDeDonneesParDefaut;
			set {

				VerifierNonVerrouille();
				_FormatBaseDeDonneesParDefaut = ValiderFormat(value, nameof(FormatBaseDeDonneesParDefaut));
			}
		}

		/**
     * <summary>
     *   [FR] Indique si les options globales sont verrouillées.
     *   [EN] Indicates whether the global options are locked.
     * </summary>
     **/
		public static bool EstVerrouille
				=> _EstVerrouille;

		/**
     * <summary>
     *   [FR] Configurer en une seule opération les options globales du système GsId.
     *   [EN] Configures the global options of the GsId system in a single operation.
     * </summary>
     **/
		public static void Configurer(
			CasseDIdentifiant? Casse = null, FormatDIdentifiant? FormatTexte = null, 
			FormatDIdentifiant? FormatJson = null, FormatDIdentifiant? FormatBaseDeDonnees = null
		) {
			
			VerifierNonVerrouille();

			if(Casse.HasValue) {
				_CasseParDefaut = Casse.Value;
			}

			if(FormatTexte.HasValue) {
				_FormatTexteParDefaut = ValiderFormat(FormatTexte.Value, nameof(FormatTexte));
			}

			if(FormatJson.HasValue) {
				_FormatJsonParDefaut = ValiderFormat(FormatJson.Value, nameof(FormatJson));
			}

			if(FormatBaseDeDonnees.HasValue) {
				_FormatBaseDeDonneesParDefaut = ValiderFormat(FormatBaseDeDonnees.Value, nameof(FormatBaseDeDonnees));
			}
		}

		/**
     * <summary>
     *   [FR] Verrouille les options globales pour empêcher toute modification ultérieure.
     *   [EN] Locks the global options to prevent any further changes.
     * </summary>
     **/
		public static void Verrouiller() => _EstVerrouille = true;

		/**
     * <summary>
     *   [FR] Restaure les options globales par défaut du système GsId.
     *   [EN] Restores the default global options for the GsId system.
     * </summary>
     **/
		public static void Reinitialiser() {

			VerifierNonVerrouille();
			_CasseParDefaut = CasseDIdentifiant.Majuscules;
			_FormatTexteParDefaut = FormatDIdentifiant.D;
			_FormatJsonParDefaut = FormatDIdentifiant.D;
			_FormatBaseDeDonneesParDefaut = FormatDIdentifiant.N;
		}

		/**
		 * <summary>
		 *   [FR] Vérifie que les options globales ne sont pas verrouillées avant de permettre une modification.
		 *   [EN] Verifies that the global options are not locked before allowing a modification.
		 * </summary>
		 **/
		private static void VerifierNonVerrouille() {

			if(_EstVerrouille) {

				throw new InvalidOperationException("Les options GsId sont verrouillées et ne peuvent plus être modifiées.");
			}
		}

		/**
		 * <summary>
		 *   [FR] Valide que le format d'identifiant spécifié est supporté.
		 *   [EN] Validates that the specified identifier format is supported.
		 * </summary>
		 **/
		private static FormatDIdentifiant ValiderFormat(FormatDIdentifiant Format, string NomDuParametre) => Format switch {

			FormatDIdentifiant.N => FormatDIdentifiant.N,
			FormatDIdentifiant.D => FormatDIdentifiant.D,
			_ => throw new ArgumentOutOfRangeException(NomDuParametre, Format, "Le format GsId demandé n'est pas supporté."),
		};
	}
}
