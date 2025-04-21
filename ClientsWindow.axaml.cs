using Avalonia.Controls;
using Avalonia.Interactivity;
using diplom2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diplom2
{
    public partial class ClientsWindow : Window
    {
        public class Client
        {
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public DateTime RegistrationDate { get; set; }
            public string Status { get; set; }
            public string StatusColor => Status == "Активен" ? "Green" : "Red";

            public string RegistrationDateString => RegistrationDate.ToString("dd.MM.yyyy");
        }

        private List<Client> _allClients;
        private List<Client> _filteredClients;

        public ClientsWindow()
        {
            InitializeComponent();
            var navPanel = this.FindControl<NavigationPanel>("NavigationPanel");
            navPanel.NavigationRequested += HandleNavigation;

            InitializeClientsList();
            SortComboBox.SelectedIndex = 0; //  сортировка по умолчанию
        }

        private void HandleNavigation(object sender, string destination)
        {
            Window newWindow = destination switch
            {
                "Main" => new Osnova(),
                "Calendar" => new CalendarWindow(),
                "Settings" => new SettingsWindow(),
                _ => null
            };

            if (newWindow != null)
            {
                newWindow.Show();
                this.Close();
            }
        }

        private void InitializeClientsList()
        {
            // Пример данных, потом будут загружаться из базы
            _allClients = new List<Client>
            {
                new Client
                {
                    Name = "Иван Иванов",
                    Phone = "+7 999 999-99-99",
                    Email = "ivan@example.com",
                    RegistrationDate = DateTime.Now.AddDays(-30),
                    Status = "Активен"
                },
                new Client
                {
                    Name = "Петр Петров",
                    Phone = "+7 888 888-88-88",
                    Email = "petr@example.com",
                    RegistrationDate = DateTime.Now.AddDays(-15),
                    Status = "Неактивен"
                },
                new Client
                {
                    Name = "Анна Сидорова",
                    Phone = "+7 777 777-77-77",
                    Email = "anna@example.com",
                    RegistrationDate = DateTime.Now.AddDays(-5),
                    Status = "Активен"
                }
            };

            _filteredClients = new List<Client>(_allClients);
            _allClients = _filteredClients;
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            // Здесь  будет логика добавления нового клиента
            
            var newClient = new Client
            {
                Name = "Новый Клиент",
                Phone = "+7 000 000-00-00",
                Email = "new@example.com",
                RegistrationDate = DateTime.Now,
                Status = "Активен"
            };

            _allClients.Add(newClient);
            ApplyFiltersAndSort();
        }

        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFiltersAndSort();
        }

        private void SortSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFiltersAndSort();
        }
        // сортировка и фильтрация
        private void ApplyFiltersAndSort()
        {
            string searchText = SearchBox.Text?.ToLower() ?? "";
            _filteredClients = _allClients
                .Where(c => c.Name.ToLower().Contains(searchText))
                .ToList();

            int sortIndex = SortComboBox.SelectedIndex;
            switch (sortIndex)
            {
                case 0: // По имени А-Я
                    _filteredClients.Sort((a, b) => a.Name.CompareTo(b.Name));
                    break;
                case 1: // По имени Я-А
                    _filteredClients.Sort((a, b) => b.Name.CompareTo(a.Name));
                    break;
                case 2: // По дате регистрации
                    _filteredClients.Sort((a, b) => a.RegistrationDate.CompareTo(b.RegistrationDate));
                    break;
            }

            ClientsList.ItemsSource = null; 
            ClientsList.ItemsSource = _filteredClients;
        }
    }
}