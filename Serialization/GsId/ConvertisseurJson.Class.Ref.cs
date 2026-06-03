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
using GalacticShrine.GsId;
using System.Text.Json;
using System.Text.Json.Serialization;
using GalacticShrine.Enumeration.GsId;

namespace GalacticShrine.Serialization.GsId {

	/**
	 * <summary>
	 *   [FR] Fournit la sérialisation JSON d'un GsId.
	 *   [EN] Provides JSON serialization for a GsId.
	 * </summary>
	 **/
	public sealed class ConvertisseurJson : JsonConverter<GSID> {

		/**
     * <summary>
     *   [FR] Format JSON à utiliser pour la sérialisation, ou <see langword="null"/> pour utiliser les options globales.
     *   [EN] JSON format to use for serialization, or <see langword="null"/> to use global options.
     * </summary>
     **/
		private readonly FormatDIdentifiant? _FormatJson;

		/**
     * <summary>
     *   [FR] Casse JSON à utiliser pour la sérialisation, ou <see langword="null"/> pour utiliser les options globales.
     *   [EN] JSON casing to use for serialization, or <see langword="null"/> to use global options.
     * </summary>
     **/
		private readonly CasseDIdentifiant? _CasseJson;

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance en utilisant le format JSON global et la casse globale.
     *   [EN] Initializes a new instance using the global JSON format and global casing.
     * </summary>
     **/
		public ConvertisseurJson() : this(null, null) {

		}

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance avec un format explicite et la casse globale.
     *   [EN] Initializes a new instance with an explicit format and the global casing.
     * </summary>
     * <param name="Format">
     *   [FR] Format JSON à utiliser.
     *   [EN] JSON format to use.
     * </param>
     **/
		public ConvertisseurJson(FormatDIdentifiant Format) : this(Format, null) {

		}

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance avec un format et une casse explicites.
     *   [EN] Initializes a new instance with an explicit format and casing.
     * </summary>
     * <param name="Format">
     *   [FR] Format JSON à utiliser, ou <see langword="null"/> pour utiliser les options globales.
     *   [EN] JSON format to use, or <see langword="null"/> to use global options.
     * </param>
     * <param name="Casse">
     *   [FR] Casse JSON à utiliser, ou <see langword="null"/> pour utiliser les options globales.
     *   [EN] JSON casing to use, or <see langword="null"/> to use global options.
     * </param>
     **/
		public ConvertisseurJson(FormatDIdentifiant? Format, CasseDIdentifiant? Casse) {

			_FormatJson = Format;
			_CasseJson = Casse;
		}

		/**
     * <summary>
     *   [FR] Lit un GsId depuis une valeur chaîne JSON.
     *   [EN] Reads a GsId from a JSON string value.
     * </summary>
     * <param name="Lecteur">
     *   [FR] Lecteur JSON UTF-8.
     *   [EN] UTF-8 JSON reader.
     * </param>
     * <param name="TypeAConvertir">
     *   [FR] Type cible à convertir.
     *   [EN] Target type to convert.
     * </param>
     * <param name="OptionsJson">
     *   [FR] Options du sérialiseur JSON.
     *   [EN] JSON serializer options.
     * </param>
     * <returns>
     *   [FR] GsId désérialisé.
     *   [EN] Deserialized GsId.
     * </returns>
     **/
		public override GSID Read(ref Utf8JsonReader Lecteur, Type TypeAConvertir, JsonSerializerOptions OptionsJson) {
			if(Lecteur.TokenType != JsonTokenType.String) {

				throw new JsonException("La valeur JSON GsId doit être une chaîne non vide.");
			}

			string? Valeur = Lecteur.GetString();

			if(string.IsNullOrWhiteSpace(Valeur)) {

				throw new JsonException("La valeur JSON GsId doit être une chaîne non vide.");
			}

			try {

				return GSID.Analyser(Valeur);
			}
			catch(System.Exception Exception) {

				throw new JsonException("Impossible de désérialiser la valeur GsId.", Exception);
			}
		}

		/**
     * <summary>
     *   [FR] Écrit un GsId sous forme de chaîne JSON.
     *   [EN] Writes a GsId as a JSON string.
     * </summary>
     * <param name="Ecrivain">
     *   [FR] Écrivain JSON UTF-8.
     *   [EN] UTF-8 JSON writer.
     * </param>
     * <param name="Valeur">
     *   [FR] Valeur GsId à écrire.
     *   [EN] GsId value to write.
     * </param>
     * <param name="OptionsJson">
     *   [FR] Options du sérialiseur JSON.
     *   [EN] JSON serializer options.
     * </param>
     **/
        public override void Write(Utf8JsonWriter Ecrivain, GSID Valeur, JsonSerializerOptions OptionsJson) 
      => Ecrivain.WriteStringValue(Valeur.VersChaine(_FormatJson ?? Options.FormatJsonParDefaut, _CasseJson ?? Options.CasseParDefaut));
	}
}
