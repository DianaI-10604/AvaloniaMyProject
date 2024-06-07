using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaMyProject;

public partial class DeleteProductMessageBox : Window
{
    public string MessageFirst { get; set; } //������ ����� ���������
    public string MessageSecond { get; set; } //������ ����� ���������

    public DeleteProductMessageBox()
    {
        InitializeComponent();
        DataContext = this;
    }
}