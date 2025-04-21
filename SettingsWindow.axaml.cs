using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Diplom2;
using Npgsql;
using System;

namespace diplom2;

public partial class SettingsWindow : Window
{
    public SettingsWindow()
    {
        InitializeComponent();
        LoadSettings();
        var navPanel = this.FindControl<NavigationPanel>("NavigationPanel");
        navPanel.NavigationRequested += HandleNavigation;

    }

    private void HandleNavigation(object sender, string destination)
    {
        Window newWindow = destination switch
        {
            "Main" => new Osnova(),
            "Calendar" => new CalendarWindow(),
            "Clients" => new ClientsWindow(),
            _ => null
        };

        if (newWindow != null)
        {
            newWindow.Show();
            this.Close();
        }
    }
    private void LoadSettings()
    {
        //потом добавлю загрузку настроек из файла

    }

    private void CheckConnection(object sender, RoutedEventArgs e)
    {
        string host = HostTextBox.Text;
        string port = PortTextBox.Text;
        string database = DatabaseTextBox.Text;
        string username = UsernameTextBox.Text;
        string password = PasswordTextBox.Text;

        string connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password}";

        try
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                conn.Close();
                //добавить сообщение об успешном подключении
            }
        }
        catch (Exception ex)
        {
            
            //MessageBox.Show($"Ошибка подключения: {ex.Message}");
        }


    }

 
}
