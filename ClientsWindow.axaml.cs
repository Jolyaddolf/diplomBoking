using Avalonia.Controls;
using Avalonia.Interactivity;
using diplom2;
using Diplom2.Models;
using System.Collections.Generic;
using System.Linq;

namespace Diplom2
{
    public partial class ClientsWindow : Window
    {
        private List<Client> _clients;

        public ClientsWindow()
        {
            InitializeComponent();
            var navPanel = this.FindControl<NavigationPanel>("NavigationPanel");
            if (navPanel != null)
            {
                navPanel.NavigationRequested += HandleNavigation;
            }

            LoadClients();
            ClientsList.DoubleTapped += ClientsList_DoubleTapped;
        }

        private void HandleNavigation(object sender, string destination)
        {
            Window newWindow = destination switch
            {
                "Calendar" => new CalendarWindow(),
                "Main" => new Osnova(),
                "Settings" => new SettingsWindow(),
                _ => null
            };

            if (newWindow != null)
            {
                newWindow.Show();
                this.Close();
            }
        }

        public void LoadClients()
        {
            _clients = Helper.Database.Clients.ToList();
            ClientsList.ItemsSource = _clients;
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            var addClientWindow = new AddClientWindow(this);
            addClientWindow.Show();
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsList.SelectedItem is Client selectedClient)
            {
                Helper.Database.Clients.Remove(selectedClient);
                Helper.Database.SaveChanges();
                LoadClients();
            }
        }

        private void ClientsList_DoubleTapped(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (ClientsList.SelectedItem is Client selectedClient)
            {
                var editClientWindow = new AddClientWindow(this, selectedClient);
                editClientWindow.Show();
            }
        }
    }
}