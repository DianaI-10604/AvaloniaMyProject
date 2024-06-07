using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaMyProject;

public partial class DeleteProductMessageBox : Window
{
    public string MessageFirst { get; set; } //первая часть сообщения
    public string MessageSecond { get; set; } //вторая часть сообщения

    public DeleteProductMessageBox()
    {
        InitializeComponent();
        DataContext = this;
    }
}