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
using GalacticShrine.Enumeration;
using GalacticShrine.Exceptions;
using GalacticShrine.Properties;

namespace GalacticShrine.Systeme {

	/**
   * <summary>
   *   [FR] Fournit des méthodes utilitaires pour lire, tester, définir et supprimer
   *   des variables d'environnement.
   *   [EN] Provides helper methods to read, test, set, and remove
   *   environment variables.
   * </summary>
   */
	public static class Environnement {

		/**
     * <summary>
     *   [FR] Indique si une variable d'environnement existe dans le processus courant.
     *   [EN] Indicates whether an environment variable exists in the current process.
     * </summary>
     * <param name="Nom">
     *   [FR] Nom de la variable d'environnement.
     *   [EN] Name of the environment variable.
     * </param>
     * <returns>
     *   [FR] <see langword="true"/> si la variable existe ; sinon, <see langword="false"/>.
     *   [EN] <see langword="true"/> if the variable exists; otherwise, <see langword="false"/>.
     * </returns>
     */
		public static bool Existe(string Nom) => Existe(Nom, EnvironnementCible.Processus);

		/**
     * <summary>
     *   [FR] Indique si une variable d'environnement existe dans la cible spécifiée.
     *   [EN] Indicates whether an environment variable exists in the specified target.
     * </summary>
     * <param name="Nom">
     *   [FR] Nom de la variable d'environnement.
     *   [EN] Name of the environment variable.
     * </param>
     * <param name="Cible">
     *   [FR] Cible dans laquelle rechercher la variable.
     *   [EN] Target in which to search for the variable.
     * </param>
     * <returns>
     *   [FR] <see langword="true"/> si la variable existe ; sinon, <see langword="false"/>.
     *   [EN] <see langword="true"/> if the variable exists; otherwise, <see langword="false"/>.
     * </returns>
     */
		public static bool Existe(string Nom, EnvironnementCible Cible) {

			ValiderNom(Nom);
			return Obtenir(Nom, Cible) is not null;
		}

		/**
     * <summary>
     *   [FR] Indique si une variable d'environnement existe dans le processus courant
     *   et contient une valeur non vide.
     *   [EN] Indicates whether an environment variable exists in the current process
     *   and contains a non-empty value.
     * </summary>
     * <param name="Nom">
     *   [FR] Nom de la variable d'environnement.
     *   [EN] Name of the environment variable.
     * </param>
     * <returns>
     *   [FR] <see langword="true"/> si la variable existe et contient une valeur non vide ;
     *   sinon, <see langword="false"/>.
     *   [EN] <see langword="true"/> if the variable exists and contains a non-empty value;
     *   otherwise, <see langword="false"/>.
     * </returns>
     */
		public static bool ExisteEtNonVide(string Nom) => ExisteEtNonVide(Nom, EnvironnementCible.Processus);

		/**
     * <summary>
     *   [FR] Indique si une variable d'environnement existe dans la cible spécifiée
     *   et contient une valeur non vide.
     *   [EN] Indicates whether an environment variable exists in the specified target
     *   and contains a non-empty value.
     * </summary>
     * <param name="Nom">
     *   [FR] Nom de la variable d'environnement.
     *   [EN] Name of the environment variable.
     * </param>
     * <param name="Cible">
     *   [FR] Cible dans laquelle rechercher la variable.
     *   [EN] Target in which to search for the variable.
     * </param>
     * <returns>
     *   [FR] <see langword="true"/> si la variable existe et contient une valeur non vide ;
     *   sinon, <see langword="false"/>.
     *   [EN] <see langword="true"/> if the variable exists and contains a non-empty value;
     *   otherwise, <see langword="false"/>.
     * </returns>
     */
		public static bool ExisteEtNonVide(string Nom, EnvironnementCible Cible) {

			ValiderNom(Nom);
			return !string.IsNullOrEmpty(Obtenir(Nom, Cible));
		}

		/**
     * <summary>
     *   [FR] Obtient la valeur d'une variable d'environnement depuis le processus courant.
     *   [EN] Gets the value of an environment variable from the current process.
     * </summary>
     * <param name="Nom">
     *   [FR] Nom de la variable d'environnement.
     *   [EN] Name of the environment variable.
     * </param>
     * <returns>
     *   [FR] Valeur de la variable si elle existe ; sinon, <see langword="null"/>.
     *   [EN] Value of the variable if it exists; otherwise, <see langword="null"/>.
     * </returns>
     */
		public static string? Obtenir(string Nom) => Obtenir(Nom, EnvironnementCible.Processus);

		/**
     * <summary>
     *   [FR] Obtient la valeur d'une variable d'environnement depuis la cible spécifiée.
     *   [EN] Gets the value of an environment variable from the specified target.
     * </summary>
     * <param name="Nom">
     *   [FR] Nom de la variable d'environnement.
     *   [EN] Name of the environment variable.
     * </param>
     * <param name="Cible">
     *   [FR] Cible depuis laquelle lire la variable.
     *   [EN] Target from which to read the variable.
     * </param>
     * <returns>
     *   [FR] Valeur de la variable si elle existe ; sinon, <see langword="null"/>.
     *   [EN] Value of the variable if it exists; otherwise, <see langword="null"/>.
     * </returns>
     */
		public static string? Obtenir(string Nom, EnvironnementCible Cible) {

			ValiderNom(Nom);
			return Environment.GetEnvironmentVariable(Nom, ConvertirLaCible(Cible));
		}

		/**
     * <summary>
     *   [FR] Obtient les informations complètes d'une variable d'environnement depuis
     *   le processus courant.
     *   [EN] Gets the full information of an environment variable from
     *   the current process.
     * </summary>
     * <param name="Nom">
     *   [FR] Nom de la variable d'environnement.
     *   [EN] Name of the environment variable.
     * </param>
     * <returns>
     *   [FR] Informations complètes sur la variable.
     *   [EN] Full information about the variable.
     * </returns>
     */
		public static InfoEnvironnement ObtenirInfo(string Nom) => ObtenirInfo(Nom, EnvironnementCible.Processus);

		/**
     * <summary>
     *   [FR] Obtient les informations complètes d'une variable d'environnement depuis
     *   la cible spécifiée.
     *   [EN] Gets the full information of an environment variable from
     *   the specified target.
     * </summary>
     * <param name="Nom">
     *   [FR] Nom de la variable d'environnement.
     *   [EN] Name of the environment variable.
     * </param>
     * <param name="Cible">
     *   [FR] Cible depuis laquelle lire la variable.
     *   [EN] Target from which to read the variable.
     * </param>
     * <returns>
     *   [FR] Informations complètes sur la variable.
     *   [EN] Full information about the variable.
     * </returns>
     */
		public static InfoEnvironnement ObtenirInfo(string Nom, EnvironnementCible Cible) {

			ValiderNom(Nom);

			string? Valeur = Obtenir(Nom, Cible);

			return new InfoEnvironnement {

				Nom = Nom,
				Cible = Cible,
				Existe = Valeur is not null,
				EstVide = string.IsNullOrEmpty(Valeur),
				Valeur = Valeur
			};
		}

		/**
     * <summary>
     *   [FR] Définit une variable d'environnement dans la cible spécifiée.
     *   [EN] Sets an environment variable in the specified target.
     * </summary>
     * <param name="Nom">
     *   [FR] Nom de la variable d'environnement.
     *   [EN] Name of the environment variable.
     * </param>
     * <param name="Valeur">
     *   [FR] Valeur à affecter à la variable.
     *   [EN] Value to assign to the variable.
     * </param>
     * <param name="Cible">
     *   [FR] Cible dans laquelle écrire la variable.
     *   [EN] Target in which to write the variable.
     * </param>
     */
		public static void Definir(string Nom, string? Valeur, EnvironnementCible Cible) {

			ValiderNom(Nom);
			Environment.SetEnvironmentVariable(Nom, Valeur, ConvertirLaCible(Cible));
		}

		/**
     * <summary>
     *   [FR] Supprime une variable d'environnement dans la cible spécifiée.
     *   [EN] Removes an environment variable from the specified target.
     * </summary>
     * <param name="Nom">
     *   [FR] Nom de la variable d'environnement.
     *   [EN] Name of the environment variable.
     * </param>
     * <param name="Cible">
     *   [FR] Cible dans laquelle supprimer la variable.
     *   [EN] Target from which to remove the variable.
     * </param>
     */
		public static void Supprimer(string Nom, EnvironnementCible Cible) {

			ValiderNom(Nom);
			Environment.SetEnvironmentVariable(Nom, null, ConvertirLaCible(Cible));
		}

		/**
     * <summary>
     *   [FR] Convertit la cible personnalisée vers la cible .NET équivalente.
     *   [EN] Converts the custom target to the equivalent .NET target.
     * </summary>
     * <param name="Cible">
     *   [FR] Cible personnalisée à convertir.
     *   [EN] Custom target to convert.
     * </param>
     * <returns>
     *   [FR] Cible .NET équivalente.
     *   [EN] Equivalent .NET target.
     * </returns>
     * <exception cref="GsArgumentHorsLimitesException">
     *   [FR] Levée lorsque la cible n'est pas prise en charge.
     *   [EN] Thrown when the target is not supported.
     * </exception>
     */
		private static EnvironmentVariableTarget ConvertirLaCible(EnvironnementCible Cible) {

			return Cible switch	{

				EnvironnementCible.Processus => EnvironmentVariableTarget.Process,
				EnvironnementCible.Utilisateur => EnvironmentVariableTarget.User,
				EnvironnementCible.Machine => EnvironmentVariableTarget.Machine,
				_ => throw new GsArgumentHorsLimitesException(
          nameof(Cible), 
          Cible,	
          EnvironnementCible.Processus,	
          EnvironnementCible.Utilisateur,	
          EnvironnementCible.Machine
				)
			};
		}

		/**
     * <summary>
     *   [FR] Valide le nom d'une variable d'environnement.
     *   [EN] Validates the name of an environment variable.
     * </summary>
     * <param name="Nom">
     *   [FR] Nom à valider.
     *   [EN] Name to validate.
     * </param>
     * <exception cref="GsArgumentException">
     *   [FR] Levée lorsque le nom est vide ou invalide.
     *   [EN] Thrown when the name is empty or invalid.
     * </exception>
     */
		private static void ValiderNom(string Nom) {

			if(string.IsNullOrWhiteSpace(Nom)) {

				throw new GsArgumentException(Resources.LeNomDeLaVariableDEnvironnementNePeutPasEtreVide, nameof(Nom));
			}
		}
	}
}
