using Avalonia.Controls;
using Avalonia.Interactivity;
using diplom2;
using Diplom2.Models;
using System;

namespace Diplom2
{
    public partial class AddClientWindow : Window
    {
        private readonly ClientsWindow? _parentWindow; // Сделали поле nullable

        // Конструктор без параметров для Avalonia
        public AddClientWindow()
        {
            InitializeComponent();
            _parentWindow = null; // Устанавливаем null, если конструктор без параметров
        }

        // Конструктор с параметром для вызова из кода
        public AddClientWindow(ClientsWindow parentWindow) : this()
        {
            _parentWindow = parentWindow;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, что все поля заполнены
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneTextBox.Text) ||
                string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                StatusComboBox.SelectedItem == null)
            {
                // Можно добавить сообщение об ошибке, если поля не заполнены
                return;
            }

            // Создаем нового клиента
            var newClient = new Client
            {
                Name = NameTextBox.Text,
                Phone = PhoneTextBox.Text,
                Email = EmailTextBox.Text,
                Status = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                Registrationdate = DateTime.Now
            };

            // Добавляем клиента в базу данных
            Helper.Database.Clients.Add(newClient);
            Helper.Database.SaveChanges();

            // Обновляем список клиентов в родительском окне, если оно есть
            if (_parentWindow != null)
            {
                _parentWindow.LoadClients();
            }

            // Закрываем окно
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрываем окно без сохранения
            Close();
        }
    }
}