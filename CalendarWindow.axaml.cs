using Avalonia.Controls;
using Avalonia.Interactivity;
using diplom2;
using diplom2;
using System;
using System.Linq;

namespace Diplom2
{
    public partial class CalendarWindow : Window
    {
        public CalendarWindow()
        {
            InitializeComponent();
            var navPanel = this.FindControl<NavigationPanel>("NavigationPanel");
            navPanel.NavigationRequested += HandleNavigation;
        }

        private void HandleNavigation(object sender, string destination)
        {
            Window newWindow = destination switch
            {
                "Main" => new Osnova(),
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

        private void CalendarDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var calendar = sender as Calendar;
            if (calendar.SelectedDate.HasValue)
            {
                var selectedDate = calendar.SelectedDate.Value;
                var rooms = Helper.Database.Rooms.ToList();
                var bookings = Helper.Database.Bookings
                    .Where(b => b.Startdate <= selectedDate && b.Enddate >= selectedDate)
                    .ToList();

                var roomStatuses = rooms.Select(r => new 
                {
                    RoomNumber = r.Roomnumber,
                    Status = bookings.Any(b => b.Roomid == r.Id) ? "Занят" : "Свободен"
                }).ToList();

                RoomsList.ItemsSource = roomStatuses; 
            }
        }
    }
}