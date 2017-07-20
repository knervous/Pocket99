using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ActionBar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.StartupUri = new Uri(@"C:\Users\root\source\Pocket99\Assets\NoesisGUI\Interface\InventoryUI\Inventory.xaml");
            InitializeComponent();
        }
    }
}
