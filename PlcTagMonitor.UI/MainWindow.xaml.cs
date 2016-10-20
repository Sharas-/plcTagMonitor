using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlcTagMonitor.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TagViewController controller;

        public MainWindow()
        {
            InitializeComponent();
            this.controller = new TagViewController();
            this.DataContext = controller.State;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var chkBox = (CheckBox)(sender);
            var tag = (string)chkBox.Content;
            if((bool)chkBox.IsChecked)
            {
                controller.AddMonitorTag(tag);
            }
            else
            {
                controller.RemoveMonitorTag(tag);
            }
        }
    }
}
