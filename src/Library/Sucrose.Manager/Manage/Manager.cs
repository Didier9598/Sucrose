﻿using SHC = Skylark.Helper.Culture;
using SHS = Skylark.Helper.Skymath;
using SMC = Sucrose.Memory.Constant;
using SMMI = Sucrose.Manager.Manage.Internal;
using SMR = Sucrose.Memory.Readonly;

namespace Sucrose.Manager.Manage
{
    public static class Manager
    {
        public static IList<char> Chars => Enumerable.Range('A', 'Z' - 'A' + 1).Concat(Enumerable.Range('a', 'z' - 'a' + 1)).Concat(Enumerable.Range('0', '9' - '0' + 1)).Select(C => (char)C).ToList();

        public static Dictionary<string, string> CefArguments => SMMI.EngineSettingManager.GetSetting(SMC.CefArguments, new Dictionary<string, string>());

        public static string LibraryLocation => SMMI.LibrarySettingManager.GetSetting(SMC.LibraryLocation, Path.Combine(SMR.DocumentsPath, SMR.AppName));

        public static int BackgroundOpacity => SHS.Clamp(SMMI.PortalSettingManager.GetSettingStable(SMC.BackgroundOpacity, 100), 0, 100);

        public static int DescriptionLength => SHS.Clamp(SMMI.PortalSettingManager.GetSettingStable(SMC.DescriptionLength, 30), 10, 100);

        public static int LibraryPagination => SHS.Clamp(SMMI.PortalSettingManager.GetSettingStable(SMC.LibraryPagination, 36), 1, 100);

        public static string Culture => SMMI.GeneralSettingManager.GetSetting(SMC.CultureName, SHC.CurrentUITwoLetterISOLanguageName);

        public static int StorePagination => SHS.Clamp(SMMI.PortalSettingManager.GetSettingStable(SMC.StorePagination, 30), 1, 100);

        public static int AdaptiveLayout => SHS.Clamp(SMMI.PortalSettingManager.GetSettingStable(SMC.AdaptiveLayout, 0), 0, 100);

        public static int AdvertisingDelay => SHS.Clamp(SMMI.DonateManager.GetSettingStable(SMC.AdvertisingDelay, 30), 30, 720);

        public static int AdaptiveMargin => SHS.Clamp(SMMI.PortalSettingManager.GetSettingStable(SMC.AdaptiveMargin, 8), 5, 25);

        public static int StoreDuration => SHS.Clamp(SMMI.PortalSettingManager.GetSettingStable(SMC.StoreDuration, 12), 1, 24);

        public static List<string> WebArguments => SMMI.EngineSettingManager.GetSetting(SMC.WebArguments, new List<string>());

        public static int DiscordDelay => SHS.Clamp(SMMI.HookSettingManager.GetSettingStable(SMC.DiscordDelay, 60), 60, 3600);

        public static int TitleLength => SHS.Clamp(SMMI.PortalSettingManager.GetSettingStable(SMC.TitleLength, 25), 10, 100);

        public static string GraphicAdapter => SMMI.BackgroundogSettingManager.GetSetting(SMC.GraphicAdapter, string.Empty);

        public static string NetworkAdapter => SMMI.BackgroundogSettingManager.GetSetting(SMC.NetworkAdapter, string.Empty);

        public static bool PerformanceCounter => SMMI.BackgroundogSettingManager.GetSetting(SMC.PerformanceCounter, true);

        public static string LibrarySelected => SMMI.LibrarySettingManager.GetSetting(SMC.LibrarySelected, string.Empty);

        public static string BackgroundImage => SMMI.PortalSettingManager.GetSetting(SMC.BackgroundImage, string.Empty);

        public static int Volume => SHS.Clamp(SMMI.EngineSettingManager.GetSettingStable(SMC.Volume, 100), 0, 100);

        public static string UserAgent => SMMI.GeneralSettingManager.GetSetting(SMC.UserAgent, SMR.UserAgent);

        public static bool LibraryConfirm => SMMI.LibrarySettingManager.GetSetting(SMC.LibraryConfirm, true);

        public static bool LibraryDelete => SMMI.LibrarySettingManager.GetSetting(SMC.LibraryDelete, false);

        public static bool VolumeDesktop => SMMI.EngineSettingManager.GetSetting(SMC.VolumeDesktop, false);

        public static List<string> Themes => SMMI.ThemesManager.GetSetting(SMC.Themes, new List<string>());

        public static bool DiscordRefresh => SMMI.HookSettingManager.GetSetting(SMC.DiscordRefresh, true);

        public static bool AdvertisingState => SMMI.DonateManager.GetSetting(SMC.AdvertisingState, true);

        public static int ScreenIndex => SMMI.EngineSettingManager.GetSettingStable(SMC.ScreenIndex, 0);

        public static bool LibraryMove => SMMI.LibrarySettingManager.GetSetting(SMC.LibraryMove, true);

        public static bool DiscordState => SMMI.HookSettingManager.GetSetting(SMC.DiscordState, true);

        public static bool DonateVisible => SMMI.DonateManager.GetSetting(SMC.DonateVisible, true);

        public static int Startup => SMMI.GeneralSettingManager.GetSettingStable(SMC.Startup, 0);

        public static bool Visible => SMMI.LauncherSettingManager.GetSetting(SMC.Visible, true);

        public static string App => SMMI.AuroraSettingManager.GetSetting(SMC.App, string.Empty);

        public static bool Shuffle => SMMI.EngineSettingManager.GetSetting(SMC.Shuffle, true);

        public static int Port => SMMI.LauncherSettingManager.GetSettingStable(SMC.Port, 0);

        public static string Key => SMMI.PrivateSettingManager.GetSetting(SMC.Key, SMR.Key);

        public static bool Start => SMMI.EngineSettingManager.GetSetting(SMC.Start, false);

        public static bool Adult => SMMI.PortalSettingManager.GetSetting(SMC.Adult, false);

        public static bool Exit => SMMI.LauncherSettingManager.GetSetting(SMC.Exit, false);

        public static bool Loop => SMMI.EngineSettingManager.GetSetting(SMC.Loop, true);
    }
}