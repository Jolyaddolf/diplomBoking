using Avalonia.Controls;
using Avalonia.Interactivity;
using diplom2;
using Diplom2.Models;
using System;
using System.Linq;

namespace Diplom2
{
    public partial class AddBookingWindow : Window
    {
        private readonly Osnova _parentWindow;
        private readonly Client _selectedClient;

        public Osnova Osnova { get; }
        public Client SelectedClient { get; }

        // Constructor to initialize fields and properties
        public AddBookingWindow(Osnova parentWindow, Client selectedClient)
        {
            InitializeComponent();
            _parentWindow = parentWindow ?? throw new ArgumentNullException(nameof(parentWindow));
            _selectedClient = selectedClient ?? throw new ArgumentNullException(nameof(selectedClient));
            ClientNameTextBlock.Text = _selectedClient.Name; // Display client name
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartDatePicker.SelectedDate == null || EndDatePicker.SelectedDate == null ||
                StartDatePicker.SelectedDate > EndDatePicker.SelectedDate)
            {
                return; // Optionally add an error message
            }

            var newBooking = new Booking
            {
                ClientId = _selectedClient.Id,
                RoomId = 1, // Default to the first available room (improve room selection logic)
                StartDate = StartDatePicker.SelectedDate.Value.DateTime, // Convert DateTimeOffset to DateTime
                EndDate = EndDatePicker.SelectedDate.Value.DateTime, // Convert DateTimeOffset to DateTime
                Status = "подтверждено"
            };

            // Check room availability (simple check)
            var occupiedRooms = Helper.Database.Bookings
                .Where(b => b.Status == "подтверждено" && b.EndDate > DateTime.Now)
                .Select(b => b.RoomId)
                .ToList();
            var availableRoom = Helper.Database.Rooms.FirstOrDefault(r => r.Status == "свободен" && !occupiedRooms.Contains(r.Id));
            if (availableRoom != null)
            {
                newBooking.RoomId = availableRoom.Id;
                availableRoom.Status = "занят";
                Helper.Database.Bookings.Add(newBooking);
                Helper.Database.SaveChanges();
                _parentWindow.LoadClients(); // Refresh the list
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}