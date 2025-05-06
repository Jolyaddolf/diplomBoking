using Avalonia.Controls;
using Avalonia.Interactivity;
using diplom2;
using Diplom2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diplom2
{
    public partial class CalendarWindow : Window
    {
        private List<Room> _rooms;

        public CalendarWindow()
        {
            InitializeComponent();
            LoadRooms();
        }

        public void LoadRooms()
        {
            _rooms = Helper.Database.Rooms.ToList();
            RoomsList.ItemsSource = _rooms;
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
               // var roomInfoWindow = new RoomInfoWindow(selectedRoom, info);
               // roomInfoWindow.Show();
            }
        }
    }
}