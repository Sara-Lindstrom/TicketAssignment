using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TicketAssignment.MVVM.ModelView;
using TicketAssignment.Services;

namespace TicketAssignment
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        DatabaseService databaseService = new DatabaseService();
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(databaseService)
            };

            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
