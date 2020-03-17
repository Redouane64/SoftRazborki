﻿namespace SoftRazborki.Desktop
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using Prism.Ioc;
    using Prism.Unity;
    using SoftRazborki.Desktop.ViewModels;
    using SoftRazborki.Desktop.Views;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<StartUp, StartUpViewModel>("StartUp");
            containerRegistry.RegisterForNavigation<SignIn, SignInViewModel>("SignIn");
        }
    }
}
