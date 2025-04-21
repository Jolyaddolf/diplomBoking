using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Diplom2;

public partial class Login : Window
{
    public Login()
    {
        InitializeComponent();
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var Osnova = new Osnova();
        Osnova.Show();
        this.Close();
        
    }
}