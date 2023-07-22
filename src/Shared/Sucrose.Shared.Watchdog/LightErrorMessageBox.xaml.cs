﻿using System.Media;
using System.Windows;
using SGHWL = Sucrose.Globalization.Helper.WatchdogLocalization;
using SMR = Sucrose.Memory.Readonly;
using SSDECT = Sucrose.Shared.Dependency.Enum.CommandsType;
using SSSHP = Sucrose.Shared.Space.Helper.Processor;
using SSSMI = Sucrose.Shared.Space.Manage.Internal;

namespace Sucrose.Shared.Watchdog
{
    /// <summary>
    /// Interaction logic for LightErrorMessageBox.xaml
    /// </summary>
    public partial class LightErrorMessageBox : Window
    {
        private static string Path = string.Empty;

        public LightErrorMessageBox(string ErrorMessage, string LogPath)
        {
            InitializeComponent();

            Path = LogPath;

            SystemSounds.Hand.Play();

            Title = SGHWL.GetValue("WindowTitle");
            Error_Title.Text = SGHWL.GetValue("ErrorTitle");
            Show_Button.Content = SGHWL.GetValue("ShowButton");
            Close_Button.Content = SGHWL.GetValue("CloseButton");
            Error_Message.Text = SGHWL.GetValue("ErrorMessage") + Environment.NewLine + ErrorMessage;
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            SSSHP.Run(SSSMI.Commandog, $"{SMR.StartCommand}{SSDECT.Log}{SMR.ValueSeparator}{Path}");
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}