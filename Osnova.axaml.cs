using Avalonia.Controls;
using Avalonia.Interactivity;
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
        private List<Room> _rooms;

        public Osnova()
        {
            InitializeComponent();
            LoadClients();
            LoadRooms();
            ClientsList.DoubleTapped += ClientsList_DoubleTapped;

            // Подписываемся на события навигационной панели
            var navPanel = this.FindControl<NavigationPanel>("NavigationPanel");
            if (navPanel != null)
            {
                navPanel.NavigationRequested += HandleNavigation;
            }
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

        public void LoadClients()
        {
            _clients = Helper.Database.Clients.ToList();
            ClientsList.ItemsSource = _clients;
        }

        public void LoadRooms()
        {
            _rooms = Helper.Database.Rooms.ToList();
            RoomsList.ItemsSource = _rooms;
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            var addClientWindow = new AddClientWindow(this);
            addClientWindow.Show();
        }

        private void AddBooking_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsList.SelectedItem is Client selectedClient)
            {
                var addBookingWindow = new AddBookingWindow(this, selectedClient);
                addBookingWindow.Show();
            }
        }

        private void RoomsList_DoubleTapped(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (RoomsList.SelectedItem is Room selectedRoom)
            {
                var booking = Helper.Database.Bookings
                    .Where(b => b.RoomId == selectedRoom.Id && b.EndDate > DateTime.Now && b.Status == "подтверждено")
                    .OrderByDescending(b => b.EndDate)
                    .FirstOrDefault();
                string info = booking != null
                    ? $"Занят клиентом: {Helper.Database.Clients.First(c => c.Id == booking.ClientId).Name}\nДо: {booking.EndDate}"
                    : "Свободен";
              //  var roomInfoWindow = new RoomInfoWindow(selectedRoom, info);
              //  roomInfoWindow.Show();
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