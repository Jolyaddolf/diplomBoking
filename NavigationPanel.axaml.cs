using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace Diplom2
{
    public partial class NavigationPanel : UserControl
    {
        public event EventHandler<string> NavigationRequested;

        public NavigationPanel()
        {
            InitializeComponent();
        }

        private void NavigateToMain(object sender, RoutedEventArgs e)
        {
            NavigationRequested?.Invoke(this, "Main");
        }

        private void NavigateToCalendar(object sender, RoutedEventArgs e)
        {
            NavigationRequested?.Invoke(this, "Calendar");
        }

        private void NavigateToClients(object sender, RoutedEventArgs e)
        {
            NavigationRequested?.Invoke(this, "Clients");
        }

        private void NavigateToSettings(object sender, RoutedEventArgs e)
        {
            NavigationRequested?.Invoke(this, "Settings");
        }
    }
}