using Avalonia.Controls;
using Avalonia.Interactivity;
using diplom2;
using Diplom2.Models;
using System;
using System.Linq;

namespace Diplom2
{
    public partial class AddClientWindow : Window
    {
        private Window _parentWindow;
        private  Client _clientToEdit;

        public AddClientWindow()
        {
            InitializeComponent();
            _parentWindow = null;
            _clientToEdit = null;
            InitializeFields();
        }

        public AddClientWindow(Window parentWindow) : this()
        {
            _parentWindow = parentWindow;
            _clientToEdit = null;
        }

        public AddClientWindow(Window parentWindow, Client clientToEdit) : this(parentWindow)
        {
            _clientToEdit = clientToEdit;
            NameTextBox.Text = _clientToEdit.Name;
            PhoneTextBox.Text = _clientToEdit.Phone ?? "";
            EmailTextBox.Text = _clientToEdit.Email ?? "";
            StatusComboBox.SelectedItem = StatusComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(i => i.Content.ToString() == _clientToEdit.Status) ?? StatusComboBox.Items[0];
        }

        private void InitializeFields()
        {
            if (_clientToEdit == null)
            {
                NameTextBox.Text = "";
                PhoneTextBox.Text = "";
                EmailTextBox.Text = "";
                StatusComboBox.SelectedIndex = 0;
            }
            ErrorMessageTextBlock.Text = "";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Сбрасываем сообщение об ошибке
            ErrorMessageTextBlock.Text = "";

            // Проверяем, что все поля заполнены
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneTextBox.Text) ||
                string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                StatusComboBox.SelectedItem == null)
            {
                ErrorMessageTextBlock.Text = "Пожалуйста, заполните все поля.";
                return;
            }

            try
            {
                string selectedStatus = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                if (selectedStatus != "активен" && selectedStatus != "неактивен")
                {
                    ErrorMessageTextBlock.Text = "Статус должен быть 'активен' или 'неактивен'.";
                    return;
                }

                if (_clientToEdit == null)
                {
                    var newClient = new Client
                    {
                        Name = NameTextBox.Text,
                        Phone = PhoneTextBox.Text,
                        Email = EmailTextBox.Text,
                        Status = selectedStatus,
                        RegistrationDate = DateTime.Now // Исправлено: Registrationdate → RegistrationDate
                    };
                    Helper.Database.Clients.Add(newClient);
                }
                else
                {
                    _clientToEdit.Name = NameTextBox.Text;
                    _clientToEdit.Phone = PhoneTextBox.Text;
                    _clientToEdit.Email = EmailTextBox.Text;
                    _clientToEdit.Status = selectedStatus;
                }

                Helper.Database.SaveChanges();

                if (_parentWindow != null)
                {
                    if (_parentWindow is ClientsWindow clientsWindow)
                        clientsWindow.LoadClients();
                    else if (_parentWindow is Osnova osnova)
                        osnova.LoadClients();
                }

                Close();
            }
            catch (Exception ex)
            {
                ErrorMessageTextBlock.Text = $"Ошибка при сохранении: {ex.Message}";
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}