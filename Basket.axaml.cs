using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;

namespace AvaloniaMyProject;

public partial class Basket : Window
{
    private List<Products> _thisBasket = new List<Products>();
    public Basket()
    {
        InitializeComponent();
    }

    public Basket(List<Products> AddToBasket)
    {
        InitializeComponent();
        _thisBasket = AddToBasket;

        DataContext = this;

        foreach (var item in _thisBasket)
        {
            BasketListbox.Items.Add(item);
        }
    }
}