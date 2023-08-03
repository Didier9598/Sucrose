﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.Windows;
using Sucrose.Portal.Services;
using SEWTT = Skylark.Enum.WindowsThemeType;
using SHC = Skylark.Helper.Culture;
using SMC = Sucrose.Memory.Constant;
using SMMI = Sucrose.Manager.Manage.Internal;
using SMR = Sucrose.Memory.Readonly;
using SSRHR = Sucrose.Shared.Resources.Helper.Resources;
using SSWDEMB = Sucrose.Shared.Watchdog.DarkErrorMessageBox;
using SSWLEMB = Sucrose.Shared.Watchdog.LightErrorMessageBox;
using SSWW = Sucrose.Shared.Watchdog.Watch;
using SWHWT = Skylark.Wing.Helper.WindowsTheme;
using Wpf.Ui.Contracts;
using Wpf.Ui.Services;
using Sucrose.Portal.ViewModels;
using Sucrose.Portal.Views.Windows;
using Sucrose.Portal.Services.Contracts;

namespace Sucrose.Portal
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string Culture => SMMI.GeneralSettingManager.GetSetting(SMC.CultureName, SHC.CurrentUITwoLetterISOLanguageName);

        private static SEWTT Theme => SMMI.GeneralSettingManager.GetSetting(SMC.ThemeType, SWHWT.GetTheme());

        private static Mutex Mutex => new(true, SMR.Portal);

        private static bool HasError { get; set; } = true;

        // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
        // https://docs.microsoft.com/dotnet/core/extensions/generic-host
        // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
        // https://docs.microsoft.com/dotnet/core/extensions/configuration
        // https://docs.microsoft.com/dotnet/core/extensions/logging
        private static readonly IHost _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(c =>
            {
                c.SetBasePath(AppContext.BaseDirectory);
            })
            .ConfigureServices(
                (context, services) =>
                {
                    // App Host
                    services.AddHostedService<ApplicationHostService>();

                    // Main window container with navigation
                    services.AddSingleton<IWindow, MainWindow>();
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton<INavigationService, NavigationService>();
                    services.AddSingleton<ISnackbarService, SnackbarService>();
                    services.AddSingleton<IContentDialogService, ContentDialogService>();
                    services.AddSingleton<WindowsProviderService>();
                }
            )
            .Build();

        public App()
        {
            AppDomain.CurrentDomain.FirstChanceException += (s, e) =>
            {
                Exception Exception = e.Exception;

                SSWW.Watch_FirstChanceException(Exception);

                //Close();
                //Message(Exception.Message);
            };

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                Exception Exception = (Exception)e.ExceptionObject;

                SSWW.Watch_GlobalUnhandledExceptionHandler(Exception);

                //Close();
                Message(Exception.Message);
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                Exception Exception = e.Exception;

                SSWW.Watch_UnobservedTaskException(Exception);

                e.SetObserved();

                //Close();
                Message(Exception.Message);
            };

            Current.DispatcherUnhandledException += (s, e) =>
            {
                Exception Exception = e.Exception;

                SSWW.Watch_DispatcherUnhandledException(Exception);

                e.Handled = true;

                //Close();
                Message(Exception.Message);
            };

            SHC.All = new CultureInfo(Culture, true);
        }

        protected void Close()
        {
            _host.StopAsync().Wait();

            _host.Dispose();

            Environment.Exit(0);
            Current.Shutdown();
            Shutdown();
        }

        protected void Message(string Message)
        {
            if (HasError)
            {
                HasError = false;

                string Path = SMMI.PortalLogManager.LogFile();

                switch (Theme)
                {
                    case SEWTT.Dark:
                        SSWDEMB DarkMessageBox = new(Message, Path);
                        DarkMessageBox.ShowDialog();
                        break;
                    default:
                        SSWLEMB LightMessageBox = new(Message, Path);
                        LightMessageBox.ShowDialog();
                        break;
                }

                Close();
            }
        }

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            Close();
        }

        /// <summary>
        /// Occurs when the application is loading.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SSRHR.SetLanguage(Culture);

            ShutdownMode = ShutdownMode.OnLastWindowClose;

            if (Mutex.WaitOne(TimeSpan.Zero, true))
            {
                Mutex.ReleaseMutex();

                _host.Start();

                //Main Interface = new();
                //Interface.Show();
            }
            else
            {
                Close();
            }
        }

        /// <summary>
        /// Gets registered service.
        /// </summary>
        /// <typeparam name="T">Type of the service to get.</typeparam>
        /// <returns>Instance of the service or <see langword="null"/>.</returns>
        public static T GetService<T>() where T : class
        {
            return _host.Services.GetService(typeof(T)) as T ?? null;
        }
    }
}