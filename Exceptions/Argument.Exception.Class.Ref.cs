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

namespace GalacticShrine.Exceptions {

	/**
	 * <summary>
	 *   [FR] Exception levée lorsqu'un argument fourni est invalide.
	 *   [EN] Exception thrown when a provided argument is invalid.
	 * </summary>
	 * <remarks>
	 *   [FR] Utilise la logique standard de <see cref="ArgumentException"/> pour conserver correctement le nom du paramètre.
	 *   [EN] Uses the standard <see cref="ArgumentException"/> logic to correctly preserve the parameter name.
	 * </remarks>
	 **/
	[Serializable]
	public class GsArgumentException : ArgumentException {

		/**
		 * <summary>
		 *   [FR] Message par défaut de l'exception.
		 *   [EN] Default exception message.
		 * </summary>
		 **/
		private static readonly string MessageParDefaut = Resources.MessageExceptionArgumentFourniEstInvalide;

		/**
		 * <summary>
		 *   [FR] Obtient le nom du paramètre à l'origine de l'erreur.
		 *   [EN] Gets the name of the parameter that caused the error.
		 * </summary>
		 **/
		public string? Parametre => ParamName;

		/**
		 * <summary>
		 *   [FR] Crée une nouvelle instance avec un message par défaut.
		 *   [EN] Creates a new instance with a default message.
		 * </summary>
		 **/
		public GsArgumentException() : base(MessageParDefaut) {

		}

		/**
		 * <summary>
		 *   [FR] Crée une nouvelle instance avec un message personnalisé.
		 *   [EN] Creates a new instance with a custom message.
		 * </summary>
		 * <param name="Message">
		 *   [FR] Message décrivant l'erreur.
		 *   [EN] Message describing the error.
		 * </param>
		 **/
		public GsArgumentException(string? Message) : base(NormaliserLeMessage(Message)) {

		}

		/**
		 * <summary>
		 *   [FR] Crée une nouvelle instance avec un message formaté via string.Format.
		 *   [EN] Creates a new instance with a message formatted through string.Format.
		 * </summary>
		 * <param name="Format">
		 *   [FR] Format du message d'erreur.
		 *   [EN] Error message format.
		 * </param>
		 * <param name="Arguments">
		 *   [FR] Arguments injectés dans le format du message.
		 *   [EN] Arguments injected into the message format.
		 * </param>
		 **/
		public GsArgumentException(string? Format, params object?[] Arguments) : base(FormaterLeMessage(Format, Arguments)) {

		}

		/**
		 * <summary>
		 *   [FR] Crée une nouvelle instance avec un message et le nom du paramètre en erreur.
		 *   [EN] Creates a new instance with a message and the name of the invalid parameter.
		 * </summary>
		 * <param name="Message">
		 *   [FR] Message décrivant l'erreur.
		 *   [EN] Message describing the error.
		 * </param>
		 * <param name="Parametre">
		 *   [FR] Nom du paramètre à l'origine de l'erreur.
		 *   [EN] Name of the parameter that caused the error.
		 * </param>
		 **/
		public GsArgumentException(string? Message, string? Parametre) : base(NormaliserLeMessage(Message), Parametre) {

		}

		/**
		 * <summary>
		 *   [FR] Crée une nouvelle instance avec un message formaté via string.Format et le nom du paramètre en erreur.
		 *   [EN] Creates a new instance with a message formatted through string.Format and the name of the invalid parameter.
		 * </summary>
		 * <param name="Message">
		 *   [FR] Format du message décrivant l'erreur.
		 *   [EN] Format of the message describing the error.
		 * </param>
		 * <param name="Parametre">
		 *   [FR] Nom du paramètre à l'origine de l'erreur.
		 *   [EN] Name of the parameter that caused the error.
		 * </param>
		 * <param name="Arguments">
		 *   [FR] Arguments injectés dans le format du message.
		 *   [EN] Arguments injected into the message format.
		 * </param>
		 **/
		public GsArgumentException(string? Message, string? Parametre, params object?[] Arguments) : base(FormaterLeMessage(Message, Arguments), Parametre) {

		}

		/**
		 * <summary>
		 *   [FR] Crée une nouvelle instance avec un message et une exception interne.
		 *   [EN] Creates a new instance with a message and an inner exception.
		 * </summary>
		 * <param name="Message">
		 *   [FR] Message décrivant l'erreur.
		 *   [EN] Message describing the error.
		 * </param>
		 * <param name="ExceptionInterne">
		 *   [FR] Exception interne à l'origine de l'erreur.
		 *   [EN] Inner exception that caused the error.
		 * </param>
		 **/
		public GsArgumentException(string? Message, Exception? ExceptionInterne) : base(NormaliserLeMessage(Message), ExceptionInterne) {

		}

		/**
		 * <summary>
		 *   [FR] Crée une nouvelle instance avec une exception interne et un message formaté via string.Format.
		 *   [EN] Creates a new instance with an inner exception and a message formatted through string.Format.
		 * </summary>
		 * <param name="ExceptionInterne">
		 *   [FR] Exception interne à l'origine de l'erreur.
		 *   [EN] Inner exception that caused the error.
		 * </param>
		 * <param name="Format">
		 *   [FR] Format du message d'erreur.
		 *   [EN] Error message format.
		 * </param>
		 * <param name="Arguments">
		 *   [FR] Arguments injectés dans le format du message.
		 *   [EN] Arguments injected into the message format.
		 * </param>
		 **/
		public GsArgumentException(Exception? ExceptionInterne, string? Format, params object?[] Arguments) 
			: base(FormaterLeMessage(Format, Arguments), ExceptionInterne) {

		}

		/**
		 * <summary>
		 *   [FR] Crée une nouvelle instance avec un message, le nom du paramètre et une exception interne.
		 *   [EN] Creates a new instance with a message, the parameter name, and an inner exception.
		 * </summary>
		 * <param name="Message">
		 *   [FR] Message décrivant l'erreur.
		 *   [EN] Message describing the error.
		 * </param>
		 * <param name="Parametre">
		 *   [FR] Nom du paramètre à l'origine de l'erreur.
		 *   [EN] Name of the parameter that caused the error.
		 * </param>
		 * <param name="ExceptionInterne">
		 *   [FR] Exception interne à l'origine de l'erreur.
		 *   [EN] Inner exception that caused the error.
		 * </param>
		 **/
		public GsArgumentException(string? Message, string? Parametre, Exception? ExceptionInterne) 
			: base(NormaliserLeMessage(Message), Parametre, ExceptionInterne) {

		}

		/**
		 * <summary>
		 *   [FR] Crée une nouvelle instance avec un message formaté via string.Format, le nom du paramètre et une exception interne.
		 *   [EN] Creates a new instance with a message formatted through string.Format, the parameter name, and an inner exception.
		 * </summary>
		 * <param name="Message">
		 *   [FR] Format du message décrivant l'erreur.
		 *   [EN] Format of the message describing the error.
		 * </param>
		 * <param name="Parametre">
		 *   [FR] Nom du paramètre à l'origine de l'erreur.
		 *   [EN] Name of the parameter that caused the error.
		 * </param>
		 * <param name="ExceptionInterne">
		 *   [FR] Exception interne à l'origine de l'erreur.
		 *   [EN] Inner exception that caused the error.
		 * </param>
		 * <param name="Arguments">
		 *   [FR] Arguments injectés dans le format du message.
		 *   [EN] Arguments injected into the message format.
		 * </param>
		 **/
		public GsArgumentException(string? Message, string? Parametre, Exception? ExceptionInterne, params object?[] Arguments) 
			: base(FormaterLeMessage(Message, Arguments), Parametre, ExceptionInterne) {

		}

		/**
		 * <summary>
		 *   [FR] Constructeur de sérialisation pour la désérialisation de l'exception.
		 *   [EN] Serialization constructor for exception deserialization.
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
#pragma warning disable SYSLIB0051
		protected GsArgumentException(SerializationInfo Info, StreamingContext Contexte) : base(Info, Contexte) {

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
		 *   [FR] Retourne le message fourni, ou le message par défaut lorsque la valeur est vide.
		 *   [EN] Returns the provided message, or the default message when the value is empty.
		 * </summary>
		 **/
		private static string NormaliserLeMessage(string? Message) => string.IsNullOrWhiteSpace(Message) ? MessageParDefaut : Message;
	}
}
