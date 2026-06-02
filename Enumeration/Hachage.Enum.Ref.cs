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
   *   [FR] Énumération des différents algorithmes de hachage supportés.
   *   [EN] Enumeration of the supported hash algorithms.
   * </summary>
   **/
  public enum Hachage {

    /**
     * <summary>
     *   [FR] Hachage MD5 (128 bits).
     *   [EN] MD5 hash (128 bits).
     * </summary>
     **/
    MD5,

    /**
     * <summary>
     *   [FR] Hachage SHA-1 (160 bits).
     *   [EN] SHA-1 hash (160 bits).
     * </summary>
     **/
    SHA1,

    /**
     * <summary>
     *   [FR] Hachage SHA-256 (256 bits).
     *   [EN] SHA-256 hash (256 bits).
     * </summary>
     **/
    SHA256,

    /**
     * <summary>
     *   [FR] Hachage SHA-384 (384 bits).
     *   [EN] SHA-384 hash (384 bits).
     * </summary>
     **/
    SHA384,

    /**
     * <summary>
     *   [FR] Hachage SHA-512 (512 bits).
     *   [EN] SHA-512 hash (512 bits).
     * </summary>
     **/
    SHA512
  }
}
