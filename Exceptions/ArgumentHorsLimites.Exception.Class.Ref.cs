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

#nullable enable
using System;
using System.Runtime.Serialization;

using GalacticShrine.Properties;
using GalacticShrine.Terminal;

namespace GalacticShrine.Exceptions {

	/**
	 * <summary>
	 *   [FR] Exception levée lorsqu'un argument est hors des limites autorisées.
	 *   [EN] Exception thrown when an argument is out of the allowed range.
	 * </summary>
	 **/
	[Serializable]
	public class GsArgumentHorsLimitesException : ArgumentOutOfRangeException {

		/**
		 * <summary>
		 *   [FR] Message par défaut de l'exception.
		 *   [EN] Default exception message.
		 * </summary>
		 **/
		private static readonly string MessageParDefaut = Resources.MessageExceptionArgumentEstHorsDesLimitesAutorisees;

		/**
		 * <summary>
		 *   [FR] Obtient la valeur minimale autorisée pour l'argument.
		 *   [EN] Gets the minimum allowed value for the argument.
		 * </summary>
		 **/
		public object? LimiteMinimale { get; }

		/**
		 * <summary>
		 *   [FR] Obtient la valeur maximale autorisée pour l'argument.
		 *   [EN] Gets the maximum allowed value for the argument.
		 * </summary>
		 **/
		public object? LimiteMaximale { get; }

		/**
		 * <summary>
		 *   [FR] Crée une nouvelle instance avec un message par défaut.
		 *   [EN] Creates a new instance with a default message.
		 * </summary>
		 **/
		public GsArgumentHorsLimitesException() : base(null, MessageParDefaut) {

		}

		/**
		 * <summary>
		 *   [FR] Crée une nouvelle instance avec le nom de l'argument concerné.
		 *   [EN] Creates a new instance with the name of the offending argument.
		 * </summary>
		 * <param name="NomArgument">
		 *   [FR] Nom de l'argument concerné.
		 *   [EN] Name of the offending argument.
		 * </param>
		 **/
		public GsArgumentHorsLimitesException(string? NomArgument) : base(NomArgument, MessageParDefaut) {

		}

		/**
		 * <summary>
		 *   [FR] Crée une nouvelle instance avec le nom de l'argument concerné et sa valeur actuelle.
		 *   [EN] Creates a new instance with the name of the offending argument and its current value.
		 * </summary>
		 * <param name="NomArgument">
		 *   [FR] Nom de l'argument concerné.
		 *   [EN] Name of the offending argument.
		 * </param>
		 * <param name="ValeurActuelle">
		 *   [FR] Valeur actuelle de l'argument.
		 *   [EN] Current value of the argument.
		 * </param>
		 **/
		public GsArgumentHorsLimitesException(string? NomArgument, object? ValeurActuelle) 
			: base(NomArgument, ValeurActuelle, CreerMessage(NomArgument, ValeurActuelle, null, null)) {

		}

		/**
		 * <summary>
		 *   [FR] Crée une nouvelle instance avec le nom de l'argument concerné, sa valeur actuelle et un message personnalisé.
		 *   [EN] Creates a new instance with the name of the offending argument, its current value, and a custom message.
		 * </summary>
		 * <param name="NomArgument">
		 *   [FR] Nom de l'argument concerné.
		 *   [EN] Name of the offending argument.
		 * </param>
		 * <param name="ValeurActuelle">
		 *   [FR] Valeur actuelle de l'argument.
		 *   [EN] Current value of the argument.
		 * </param>
		 * <param name="Message">
		 *   [FR] Message décrivant l'erreur.
		 *   [EN] Message describing the error.
		 * </param>
		 **/
		public GsArgumentHorsLimitesException(string? NomArgument, object? ValeurActuelle, string? Message) 
			: base(NomArgument, ValeurActuelle, NormaliserLeMessage(Message)) {

		}

		/**
		 * <summary>
		 *   [FR] Crée une nouvelle instance avec le nom de l'argument concerné, sa valeur actuelle et un message formaté via string.Format.
		 *   [EN] Creates a new instance with the name of the offending argument, its current value, and a message formatted through string.Format.
		 * </summary>
		 * <param name="NomArgument">
		 *   [FR] Nom de l'argument concerné.
		 *   [EN] Name of the offending argument.
		 * </param>
		 * <param name="ValeurActuelle">
		 *   [FR] Valeur actuelle de l'argument.
		 *   [EN] Current value of the argument.
		 * </param>
		 * <param name="Format">
		 *   [FR] Format du message décrivant l'erreur.
		 *   [EN] Format of the message describing the error.
		 * </param>
		 * <param name="Arguments">
		 *   [FR] Arguments injectés dans le format du message.
		 *   [EN] Arguments injected into the message format.
		 * </param>
		 **/
		public GsArgumentHorsLimitesException(string? NomArgument, object? ValeurActuelle, string? Format, object?[] Arguments) 
			: base(NomArgument, ValeurActuelle, FormaterLeMessage(Format, Arguments)) {

		}

		/**
		 * <summary>
		 *   [FR] Crée une nouvelle instance avec le nom de l'argument concerné, sa valeur actuelle et les limites autorisées.
		 *   [EN] Creates a new instance with the name of the offending argument, its current value, and the allowed limits.
		 * </summary>
		 * <param name="NomArgument">
		 *   [FR] Nom de l'argument concerné.
		 *   [EN] Name of the offending argument.
		 * </param>
		 * <param name="ValeurActuelle">
		 *   [FR] Valeur actuelle de l'argument.
		 *   [EN] Current value of the argument.
		 * </param>
		 * <param name="LimiteMinimale">
		 *   [FR] Limite minimale autorisée.
		 *   [EN] Minimum allowed limit.
		 * </param>
		 * <param name="LimiteMaximale">
		 *   [FR] Limite maximale autorisée.
		 *   [EN] Maximum allowed limit.
		 * </param>
		 **/
		public GsArgumentHorsLimitesException(string? NomArgument, object? ValeurActuelle, object? LimiteMinimale, object? LimiteMaximale) 
			: base(NomArgument, ValeurActuelle, CreerMessage(NomArgument, ValeurActuelle, LimiteMinimale, LimiteMaximale)) {

			this.LimiteMinimale = LimiteMinimale;
			this.LimiteMaximale = LimiteMaximale;
		}

		/**
		 * <summary>
		 *   [FR] Crée une nouvelle instance avec les limites autorisées et un message formaté via string.Format.
		 *   [EN] Creates a new instance with the allowed limits and a message formatted through string.Format.
		 * </summary>
		 * <param name="NomArgument">
		 *   [FR] Nom de l'argument concerné.
		 *   [EN] Name of the offending argument.
		 * </param>
		 * <param name="ValeurActuelle">
		 *   [FR] Valeur actuelle de l'argument.
		 *   [EN] Current value of the argument.
		 * </param>
		 * <param name="LimiteMinimale">
		 *   [FR] Limite minimale autorisée.
		 *   [EN] Minimum allowed limit.
		 * </param>
		 * <param name="LimiteMaximale">
		 *   [FR] Limite maximale autorisée.
		 *   [EN] Maximum allowed limit.
		 * </param>
		 * <param name="Format">
		 *   [FR] Format du message décrivant l'erreur.
		 *   [EN] Format of the message describing the error.
		 * </param>
		 * <param name="Arguments">
		 *   [FR] Arguments injectés dans le format du message.
		 *   [EN] Arguments injected into the message format.
		 * </param>
		 **/
		public GsArgumentHorsLimitesException(
			string? NomArgument, object? ValeurActuelle, object? LimiteMinimale, object? LimiteMaximale, string? Format, params object?[] Arguments)
			: base(NomArgument, ValeurActuelle, FormaterLeMessage(Format, Arguments)) {

			this.LimiteMinimale = LimiteMinimale;
			this.LimiteMaximale = LimiteMaximale;
		}

		/**
		 * <summary>
		 *   [FR] Crée une nouvelle instance avec le nom de l'argument concerné, sa valeur actuelle et les valeurs autorisées.
		 *   [EN] Creates a new instance with the name of the offending argument, its current value, and the allowed values.
		 * </summary>
		 * <param name="NomArgument">
		 *   [FR] Nom de l'argument concerné.
		 *   [EN] Name of the offending argument.
		 * </param>
		 * <param name="ValeurActuelle">
		 *   [FR] Valeur actuelle de l'argument.
		 *   [EN] Current value of the argument.
		 * </param>
		 * <param name="ValeursAutorisees">
		 *   [FR] Liste des valeurs autorisées.
		 *   [EN] List of allowed values.
		 * </param>
		 **/
		public GsArgumentHorsLimitesException(string? NomArgument, object? ValeurActuelle, params object?[] ValeursAutorisees) 
			: base(NomArgument, ValeurActuelle, CreerMessageAvecValeursAutorisees(NomArgument, ValeurActuelle, ValeursAutorisees)) {

		}

#pragma warning disable SYSLIB0051
		/**
		 * <summary>
		 *   [FR] Constructeur de sérialisation pour permettre la désérialisation de l'exception.
		 *   [EN] Serialization constructor to allow exception deserialization.
		 * </summary>
		 * <param name="Info">
		 *   [FR] Données de sérialisation.
		 *   [EN] Serialization data.
		 * </param>
		 * <param name="Contexte">
		 *   [FR] Contexte de sérialisation.
		 *   [EN] Streaming context.
		 * </param>
		 **/
		protected GsArgumentHorsLimitesException(SerializationInfo Info, StreamingContext Contexte) : base(Info, Contexte) {

			if(Info is null) {

				throw new ArgumentNullException(nameof(Info));
			}

			this.LimiteMinimale = Info.GetValue(nameof(LimiteMinimale), typeof(object));
			this.LimiteMaximale = Info.GetValue(nameof(LimiteMaximale), typeof(object));
		}

		/**
		 * <summary>
		 *   [FR] Sérialise les données personnalisées de l'exception.
		 *   [EN] Serializes the custom exception data.
		 * </summary>
		 * <param name="Info">
		 *   [FR] Données de sérialisation.
		 *   [EN] Serialization data.
		 * </param>
		 * <param name="Contexte">
		 *   [FR] Contexte de sérialisation.
		 *   [EN] Streaming context.
		 * </param>
		 **/
		public override void GetObjectData(SerializationInfo Info, StreamingContext Contexte) {

			if(Info is null) {

				throw new ArgumentNullException(nameof(Info));
			}

			Info.AddValue(nameof(LimiteMinimale), LimiteMinimale);
			Info.AddValue(nameof(LimiteMaximale), LimiteMaximale);

			base.GetObjectData(Info, Contexte);
		}
#pragma warning restore SYSLIB0051

		/**
		 * <summary>
		 *   [FR] Formate le message avec string.Format, ou retourne le message par défaut lorsque le format est vide.
		 *   [EN] Formats the message with string.Format, or returns the default message when the format is empty.
		 * </summary>
		 * <param name="Format">
		 *   [FR] Format du message.
		 *   [EN] Message format.
		 * </param>
		 * <param name="Arguments">
		 *   [FR] Arguments du format.
		 *   [EN] Format arguments.
		 * </param>
		 * <returns>
		 *   [FR] Message normalisé et formaté.
		 *   [EN] Normalized and formatted message.
		 * </returns>
		 **/
		private static string FormaterLeMessage(string? Format, params object?[] Arguments) {

			var Message = NormaliserLeMessage(Format);

			if(Arguments is null || Arguments.Length == 0) {

				return Message;
			}

			return string.Format(Message, Arguments);
		}

		/**
		 * <summary>
		 *   [FR] Crée un message d'erreur avec le nom de l'argument, sa valeur actuelle et les limites autorisées.
		 *   [EN] Creates an error message with the argument name, its current value, and the allowed limits.
		 * </summary>
		 **/
		private static string CreerMessage(string? NomArgument, object? ValeurActuelle, object? LimiteMinimale, object? LimiteMaximale) {

			var Nom = string.IsNullOrWhiteSpace(NomArgument) ? Resources.LArgument : string.Format(Resources.MessageExceptionLArgumentAvecVars, NomArgument);

			if(LimiteMinimale is not null && LimiteMaximale is not null) {

				return string.Format(
					Resources.MessageExceptionParamsPossedeUneValeurHorsLimitesValeurActuelleLimitesAutoriseesAvecVars, 
					Nom, 
					ValeurActuelle, 
					LimiteMinimale,
					LimiteMaximale
				);
			}

			return string.Format(Resources.MessageExceptionParamsPossedeUneValeurHorsDesLimitesAutoriseesValeurActuelleAvecVars, Nom, ValeurActuelle);
		}

		/**
		 * <summary>
		 *   [FR] Crée un message d'erreur avec le nom de l'argument, sa valeur actuelle et les valeurs autorisées.
		 *   [EN] Creates an error message with the argument name, its current value, and the allowed values.
		 * </summary>
		 **/
		private static string CreerMessageAvecValeursAutorisees(string? NomArgument, object? ValeurActuelle, object?[] ValeursAutorisees) {

			var Nom = string.IsNullOrWhiteSpace(NomArgument) ? Resources.LArgument : FormaterLeMessage(Resources.MessageExceptionLArgumentAvecVars, NomArgument);

			if(ValeursAutorisees.Length == 0) {

				return string.Format(Resources.MessageExceptionParamsPossedeUneValeurNonPriseEnChargeValeurActuelleAvecVars, Nom, ValeurActuelle);
			}

			var Valeurs = string.Join(", ", ValeursAutorisees);

			return string.Format(
				Resources.MessageExceptionParamsPossedeUneValeurNonPriseEnChargeValeurActuelleValeursAutoriseesAvecVars, 
				Nom, 
				ValeurActuelle, 
				Valeurs
			);
		}

		/**
		 * <summary>
		 *   [FR] Retourne le message fourni, ou le message par défaut lorsque la valeur est vide.
		 *   [EN] Returns the provided message, or the default message when the value is empty.
		 * </summary>
		 **/
		private static string NormaliserLeMessage(string? Message) => string.IsNullOrWhiteSpace(Message) ? MessageParDefaut : Message;
	}
}
