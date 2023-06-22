﻿using Sucrose.Common.Manage;
using Application = System.Windows.Application;

namespace Sucrose.WPF.CS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Internal.TrayIconManager.Start();
        }
    }
}