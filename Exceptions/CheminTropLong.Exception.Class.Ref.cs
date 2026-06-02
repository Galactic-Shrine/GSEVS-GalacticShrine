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
using System.IO;
using System.Runtime.Serialization;
using GalacticShrine.Properties;

namespace GalacticShrine.Exceptions {

	/**
	 * <summary>
	 *   [FR] Exception équivalente à <see cref="PathTooLongException"/> avec un message par défaut en français.
	 *   [EN] Exception equivalent to <see cref="PathTooLongException"/> with a default French message.
	 * </summary>
	 **/
	[Serializable]
	public class GsCheminTropLongException : PathTooLongException {

		/**
		 * <summary>
		 *   [FR] Message par défaut de l'exception.
		 *   [EN] Default exception message.
		 * </summary>
		 **/
		private static readonly string MessageParDefaut = Resources.MessageExceptionLeCheminSpecifieEstTropLong;

		/**
		 * <summary>
		 *   [FR] Initialise une nouvelle instance avec le message par défaut en français.
		 *   [EN] Initializes a new instance with the default French message.
		 * </summary>
		 **/
		public GsCheminTropLongException() : base(MessageParDefaut) {

		}

		/**
		 * <summary>
		 *   [FR] Initialise une nouvelle instance avec un message personnalisé, ou le message par défaut si vide.
		 *   [EN] Initializes a new instance with a custom message, or the default message if empty.
		 * </summary>
		 * <param name="Message">
		 *   [FR] Message décrivant l'erreur.
		 *   [EN] Message describing the error.
		 * </param>
		 **/
		public GsCheminTropLongException(string? Message) : base(NormaliserLeMessage(Message)) {

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
		public GsCheminTropLongException(string? Format, params object?[] Arguments) : base(FormaterLeMessage(Format, Arguments)) {

		}

		/**
		 * <summary>
		 *   [FR] Initialise une nouvelle instance avec un message personnalisé et une exception interne.
		 *   [EN] Initializes a new instance with a custom message and an inner exception.
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
		public GsCheminTropLongException(string? Message, Exception? ExceptionInterne) : base(NormaliserLeMessage(Message), ExceptionInterne) {

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
		public GsCheminTropLongException(Exception? ExceptionInterne, string? Format, params object?[] Arguments) 
			: base(FormaterLeMessage(Format, Arguments), ExceptionInterne) {

		}

		/**
		 * <summary>
		 *   [FR] Initialise une nouvelle instance à partir de données de sérialisation.
		 *   [EN] Initializes a new instance from serialization data.
		 * </summary>
		 **/
#pragma warning disable SYSLIB0051
		protected GsCheminTropLongException(SerializationInfo Info, StreamingContext Contexte) : base(Info, Contexte) {

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
