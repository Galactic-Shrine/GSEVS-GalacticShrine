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
using System.Diagnostics;
using Microsoft.Win32;
using GalacticShrine.Enumeration;

namespace GalacticShrine.Outils {

  /**
   * <summary>
   *   [FR] Assistant multiplateforme pour obtenir le thème clair/sombre du système.<br />
   *        Sous Windows, l'information est lue dans le registre.<br />
   *        Sous Linux, l'information est recherchée via XDG Portal, GNOME/GSettings puis KDE.<br />
   *        Sous macOS, l'information est lue via <c>defaults</c>.<br />
   *        Si aucune source n'est disponible, l'assistant retourne <see cref="WindowsTheme.Inconnu"/>.<br />
   *   [EN] Cross-platform helper to get the system light/dark theme.<br />
   *        On Windows, the information is read from the registry.<br />
   *        On Linux, the information is resolved through XDG Portal, GNOME/GSettings then KDE.<br />
   *        On macOS, the information is read through <c>defaults</c>.<br />
   *        If no source is available, the helper returns <see cref="WindowsTheme.Inconnu"/>.
   * </summary>
   **/
  public static class WindowsThemeAssistant {

    /**
     * <summary>
     *   [FR] Chemin du registre pour les thèmes Windows.<br />
     *   [EN] Windows registry path for themes.
     * </summary>
     **/
    private const string _CheminDuRegistre = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

    /**
     * <summary>
     *   [FR] Clé de registre pour le thème de l'application.<br />
     *   [EN] Registry value for the application theme.
     * </summary>
     **/
    private const string _CleThemeApplication = "AppsUseLightTheme";

    /**
     * <summary>
     *   [FR] Clé de registre pour le thème du système.<br />
     *   [EN] Registry value for the system theme.
     * </summary>
     **/
    private const string _CleThemeSysteme = "SystemUsesLightTheme";

    /**
     * <summary>
     *   [FR] Obtient le thème de l'application.<br />
     *   [EN] Gets the application theme.
     * </summary>
     * <returns>
     *   [FR] Une valeur de <see cref="WindowsTheme"/> représentant le thème de l'application.<br />
     *   [EN] A <see cref="WindowsTheme"/> value representing the application theme.
     * </returns>
     **/
    private static WindowsTheme ObtenirThemeApp() => ObtenirThemeMultiplateforme(CleDeRegistre: _CleThemeApplication);

    /**
     * <summary>
     *   [FR] Obtient le thème du système.<br />
     *   [EN] Gets the system theme.
     * </summary>
     * <returns>
     *   [FR] Une valeur de <see cref="WindowsTheme"/> représentant le thème du système.<br />
     *   [EN] A <see cref="WindowsTheme"/> value representing the system theme.
     * </returns>
     **/
    private static WindowsTheme ObtenirThemeSystem() => ObtenirThemeMultiplateforme(CleDeRegistre: _CleThemeSysteme);

    /**
     * <summary>
     *   [FR] Obtient le thème système via l'implémentation adaptée à la plateforme courante.<br />
     *   [EN] Gets the system theme using the implementation matching the current platform.
     * </summary>
     **/
    private static WindowsTheme ObtenirThemeMultiplateforme(string CleDeRegistre) {

      if(OperatingSystem.IsWindows()) {

        return ObtenirThemeWindowsDepuisRegistre(CleDeRegistre: CleDeRegistre);
      }

      if(OperatingSystem.IsMacOS()) {

        return ObtenirThemeMacOsDepuisDefaults();
      }

      if(OperatingSystem.IsLinux()) {

        return ObtenirThemeLinux();
      }

      return WindowsTheme.Inconnu;
    }

    /**
     * <summary>
     *   [FR] Obtient le thème Windows depuis le registre.<br />
     *   [EN] Gets the Windows theme from the registry.
     * </summary>
     **/
    private static WindowsTheme ObtenirThemeWindowsDepuisRegistre(string CleDeRegistre) {

      if(!OperatingSystem.IsWindows()) {

        return WindowsTheme.Inconnu;
      }

      try {

        using(RegistryKey? cle = Registry.CurrentUser.OpenSubKey(_CheminDuRegistre)) {

          if(cle == null) {

            return WindowsTheme.Inconnu;
          }

          object? valeur = cle.GetValue(CleDeRegistre);

          if(valeur is int valeurEntiere) {

            return valeurEntiere == 0 ? WindowsTheme.Sombre : WindowsTheme.Clair;
          }

          return WindowsTheme.Inconnu;
        }
      }
      catch {

        return WindowsTheme.Inconnu;
      }
    }

    /**
     * <summary>
     *   [FR] Obtient le thème Linux via XDG Portal, GSettings puis KDE.<br />
     *   [EN] Gets the Linux theme using XDG Portal, GSettings then KDE.
     * </summary>
     **/
    private static WindowsTheme ObtenirThemeLinux() {

      WindowsTheme theme = ObtenirThemeLinuxDepuisXdgPortal();

      if(theme != WindowsTheme.Inconnu) {

        return theme;
      }

      theme = ObtenirThemeLinuxDepuisGSettingsColorScheme();

      if(theme != WindowsTheme.Inconnu) {

        return theme;
      }

      theme = ObtenirThemeLinuxDepuisGSettingsGtkTheme();

      if(theme != WindowsTheme.Inconnu) {

        return theme;
      }

      theme = ObtenirThemeLinuxDepuisKde(Executable: "kreadconfig6");

      if(theme != WindowsTheme.Inconnu) {

        return theme;
      }

      theme = ObtenirThemeLinuxDepuisKde(Executable: "kreadconfig5");

      if(theme != WindowsTheme.Inconnu) {

        return theme;
      }

      return WindowsTheme.Inconnu;
    }

    /**
     * <summary>
     *   [FR] Obtient le thème Linux via XDG Desktop Portal.<br />
     *   [EN] Gets the Linux theme through XDG Desktop Portal.
     * </summary>
     **/
    private static WindowsTheme ObtenirThemeLinuxDepuisXdgPortal() {

      string sortie = ExecuterCommande(
        Fichier: "gdbus",
        Arguments: "call --session --dest org.freedesktop.portal.Desktop --object-path /org/freedesktop/portal/desktop --method org.freedesktop.portal.Settings.Read org.freedesktop.appearance color-scheme"
      );

      if(string.IsNullOrWhiteSpace(value: sortie)) {

        return WindowsTheme.Inconnu;
      }

      if(sortie.Contains(value: "uint32 1", comparisonType: StringComparison.OrdinalIgnoreCase) || sortie.Contains(value: "<1>", comparisonType: StringComparison.OrdinalIgnoreCase)) {

        return WindowsTheme.Sombre;
      }

      if(sortie.Contains(value: "uint32 2", comparisonType: StringComparison.OrdinalIgnoreCase) || sortie.Contains(value: "<2>", comparisonType: StringComparison.OrdinalIgnoreCase)) {

        return WindowsTheme.Clair;
      }

      return WindowsTheme.Inconnu;
    }

    /**
     * <summary>
     *   [FR] Obtient le thème Linux via GNOME/GSettings color-scheme.<br />
     *   [EN] Gets the Linux theme through GNOME/GSettings color-scheme.
     * </summary>
     **/
    private static WindowsTheme ObtenirThemeLinuxDepuisGSettingsColorScheme() {

      string sortie = ExecuterCommande(Fichier: "gsettings", Arguments: "get org.gnome.desktop.interface color-scheme");

      if(string.IsNullOrWhiteSpace(value: sortie)) {

        return WindowsTheme.Inconnu;
      }

      return ConvertirTexteEnTheme(Texte: sortie);
    }

    /**
     * <summary>
     *   [FR] Obtient le thème Linux via GNOME/GSettings gtk-theme.<br />
     *   [EN] Gets the Linux theme through GNOME/GSettings gtk-theme.
     * </summary>
     **/
    private static WindowsTheme ObtenirThemeLinuxDepuisGSettingsGtkTheme() {

      string sortie = ExecuterCommande(Fichier: "gsettings", Arguments: "get org.gnome.desktop.interface gtk-theme");

      if(string.IsNullOrWhiteSpace(value: sortie)) {

        return WindowsTheme.Inconnu;
      }

      return ConvertirTexteEnTheme(Texte: sortie);
    }

    /**
     * <summary>
     *   [FR] Obtient le thème Linux KDE via kreadconfig.<br />
     *   [EN] Gets the KDE Linux theme through kreadconfig.
     * </summary>
     **/
    private static WindowsTheme ObtenirThemeLinuxDepuisKde(string Executable) {

      string sortie = ExecuterCommande(Fichier: Executable, Arguments: "--file kdeglobals --group General --key ColorScheme");

      if(string.IsNullOrWhiteSpace(value: sortie)) {

        return WindowsTheme.Inconnu;
      }

      return ConvertirTexteEnTheme(Texte: sortie);
    }

    /**
     * <summary>
     *   [FR] Obtient le thème macOS via defaults.<br />
     *   [EN] Gets the macOS theme through defaults.
     * </summary>
     **/
    private static WindowsTheme ObtenirThemeMacOsDepuisDefaults() {

      string sortie = ExecuterCommande(Fichier: "defaults", Arguments: "read -g AppleInterfaceStyle");

      if(sortie.Trim().Equals(value: "Dark", comparisonType: StringComparison.OrdinalIgnoreCase)) {

        return WindowsTheme.Sombre;
      }

      return WindowsTheme.Clair;
    }

    /**
     * <summary>
     *   [FR] Convertit un texte de thème en valeur <see cref="WindowsTheme"/>.<br />
     *   [EN] Converts theme text to a <see cref="WindowsTheme"/> value.
     * </summary>
     **/
    private static WindowsTheme ConvertirTexteEnTheme(string Texte) {

      string valeur = Texte.Trim().Trim(trimChar: '\'').Trim(trimChar: '"');

      if(valeur.Contains(value: "prefer-dark", comparisonType: StringComparison.OrdinalIgnoreCase) || valeur.Contains(value: "dark", comparisonType: StringComparison.OrdinalIgnoreCase) || valeur.Contains(value: "sombre", comparisonType: StringComparison.OrdinalIgnoreCase)) {

        return WindowsTheme.Sombre;
      }

      if(valeur.Contains(value: "prefer-light", comparisonType: StringComparison.OrdinalIgnoreCase) || valeur.Contains(value: "light", comparisonType: StringComparison.OrdinalIgnoreCase) || valeur.Contains(value: "clair", comparisonType: StringComparison.OrdinalIgnoreCase) || valeur.Equals(value: "default", comparisonType: StringComparison.OrdinalIgnoreCase)) {

        return WindowsTheme.Clair;
      }

      return WindowsTheme.Inconnu;
    }

    /**
     * <summary>
     *   [FR] Exécute une commande système de manière sécurisée et retourne sa sortie standard.<br />
     *   [EN] Safely runs a system command and returns its standard output.
     * </summary>
     **/
    private static string ExecuterCommande(string Fichier, string Arguments) {

      try {

        ProcessStartInfo demarrage = new() {
          FileName = Fichier,
          Arguments = Arguments,
          RedirectStandardOutput = true,
          RedirectStandardError = true,
          UseShellExecute = false,
          CreateNoWindow = true
        };

        using(Process? processus = Process.Start(startInfo: demarrage)) {

          if(processus == null) {

            return string.Empty;
          }

          if(!processus.WaitForExit(milliseconds: 1000)) {

            try {

              processus.Kill(entireProcessTree: true);
            }
            catch {
              // [FR] Ignore l'échec éventuel de l'arrêt du processus.
              // [EN] Ignore possible process kill failure.
            }

            return string.Empty;
          }

          string sortie = processus.StandardOutput.ReadToEnd();

          if(string.IsNullOrWhiteSpace(value: sortie)) {

            sortie = processus.StandardError.ReadToEnd();
          }

          return sortie;
        }
      }
      catch {

        return string.Empty;
      }
    }

    /**
     * <summary>
     *   [FR] Obtenir le thème de l'application sous forme de texte.<br />
     *   [EN] Get the application theme as text.
     * </summary>
     * <returns>
     *   [FR] Chaîne représentant le thème de l'application : <c>Clair</c>, <c>Sombre</c> ou <c>Inconnu</c>.<br />
     *   [EN] A string representing the application theme: <c>Clair</c>, <c>Sombre</c> or <c>Inconnu</c>.
     * </returns>
     **/
    public static string ObtenirThemeAppText() => ObtenirThemeApp().ToString();

    /**
     * <summary>
     *   [FR] Obtenir le thème de l'application sous forme d'identifiant.<br />
     *   [EN] Get the application theme as an identifier.
     * </summary>
     * <returns>
     *   [FR] Entier correspondant à la valeur de <see cref="WindowsTheme"/>.<br />
     *   [EN] Integer corresponding to the <see cref="WindowsTheme"/> value.
     * </returns>
     **/
    public static int ObtenirThemeAppId() => (int)ObtenirThemeApp();

    /**
     * <summary>
     *   [FR] Obtenir le thème du système sous forme de texte.<br />
     *   [EN] Get the system theme as text.
     * </summary>
     * <returns>
     *   [FR] Chaîne représentant le thème du système : <c>Clair</c>, <c>Sombre</c> ou <c>Inconnu</c>.<br />
     *   [EN] A string representing the system theme: <c>Clair</c>, <c>Sombre</c> or <c>Inconnu</c>.
     * </returns>
     **/
    public static string ObtenirThemeSystemText() => ObtenirThemeSystem().ToString();

    /**
     * <summary>
     *   [FR] Obtenir le thème du système sous forme d'identifiant.<br />
     *   [EN] Get the system theme as an identifier.
     * </summary>
     * <returns>
     *   [FR] Entier correspondant à la valeur de <see cref="WindowsTheme"/>.<br />
     *   [EN] Integer corresponding to the <see cref="WindowsTheme"/> value.
     * </returns>
     **/
    public static int ObtenirThemeSystemId() => (int)ObtenirThemeSystem();
  }
}
