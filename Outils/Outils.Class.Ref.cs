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

namespace GalacticShrine.Outils {

  /**
   * <summary>
   *   [FR] Classe de base pour les outil.
   *   [EN] Basic class for tools.
   * </summary>
   **/
  [Serializable]
  public abstract class Outils {

    /**
     * <summary>
     *   [FR] Le comparateur à utiliser pour les références de systèmes de fichiers
     *   [EN] The comparator to use for file system references
     * </summary>
     **/
    public static readonly StringComparer Comparateur = StringComparer.OrdinalIgnoreCase;

    /**
     * <summary>
     *   [FR] La comparaison à utiliser pour les références de systèmes de fichiers
     *   [EN] The comparison to be used for file system references
     * </summary>
     **/
    public static readonly StringComparison Comparaison = StringComparison.OrdinalIgnoreCase;
  }
}
