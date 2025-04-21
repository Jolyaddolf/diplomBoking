using Avalonia.Controls;
using Avalonia.Interactivity;
using diplom2;
using diplom2;
using Diplom2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diplom2
{
    public partial class Osnova : Window
    {
        private List<Client> _clients;

        public Osnova()
        {
            InitializeComponent();
            var navPanel = this.FindControl<NavigationPanel>("NavigationPanel");
            if (navPanel != null)
            {
                navPanel.NavigationRequested += HandleNavigation;
            }

            LoadClients();
            UpdateFreeRoomsCount();
        }

        private void HandleNavigation(object sender, string destination)
        {
            Window newWindow = destination switch
            {
              "Calendar" => new CalendarWindow(),
                "Clients" => new ClientsWindow(),
                "Settings" => new SettingsWindow(),
                _ => null
            };

            if (newWindow != null)
            {
                newWindow.Show();
                this.Close();
            }
        }

        private void LoadClients()
        {
            _clients = Helper.Database.Clients.ToList();
            ClientsList.ItemsSource = _clients;
        }

        private void UpdateFreeRoomsCount()
        {
            var freeRooms = Helper.Database.Rooms.Count(r => r.Status == "Свободен");
            FreeRoomsCount.Text = $"Свободных номеров: {freeRooms}";
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            var newClient = new Client
            {
                Name = "Новый Клиент",
                Phone = "+7 000 000-00-00",
                Email = "new@example.com",
                Registrationdate = DateTime.Now,
                Status = "Активен"
            };
            Helper.Database.Clients.Add(newClient);
            Helper.Database.SaveChanges();
            LoadClients();
        }

        private void AddBooking_Click(object sender, RoutedEventArgs e)
        {
            // Логика добавления брони
        }

        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text?.ToLower() ?? "";
            ClientsList.ItemsSource = _clients.Where(c => c.Name.ToLower().Contains(searchText)).ToList();
        }

        private void SortSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortComboBox.SelectedIndex == 0) // По имени
            {
                ClientsList.ItemsSource = _clients.OrderBy(c => c.Name).ToList();
            }
            else if (SortComboBox.SelectedIndex == 1) // По статусу
            {
                ClientsList.ItemsSource = _clients.OrderBy(c => c.Status).ToList();
            }
        }
    }
}