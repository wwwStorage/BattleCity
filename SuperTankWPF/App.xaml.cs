﻿using SuperTankWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SuperTankWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            ViewModelLocator.Cleanup();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            
        }
    }
}
