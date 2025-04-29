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
        }

        private void HandleNavigation(object sender, string destination)
        {
            Window newWindow = destination switch
            {
                "Calendar" => new CalendarWindow(),
                "Main" => new Osnova(),
                "Settings" => new (),
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
            // Открываем новое окно для добавления клиента
            var addClientWindow = new AddClientWindow(this);
            addClientWindow.Show();
            // Текущее окно (ClientsWindow) не закрывается
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, выделен ли клиент в ListBox
            if (ClientsList.SelectedItem is Client selectedClient)
            {
                // Удаляем клиента из базы данных
                Helper.Database.Clients.Remove(selectedClient);
                Helper.Database.SaveChanges();

                // Обновляем список клиентов
                LoadClients();
            }
        }
    }
}