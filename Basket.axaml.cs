using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.ComponentModel;

namespace AvaloniaMyProject;

public partial class Basket : Window
{
    //private List<Products> _thisBasket = new List<Products>();
    private User _currentUser;
    public Basket()
    {
        InitializeComponent();
    }

    public Basket(User user)
    {
        InitializeComponent();
        _currentUser = user;
        DataContext = this;

        foreach (var item in _currentUser.UserBasket)
        {
            BasketListbox.Items.Add(item);
        }
    }
    private void MinusProductCount_ButtonClick(object sender, RoutedEventArgs e)
    {
        Button changeCount = (Button)sender;
        Products product = (Products)changeCount.DataContext;

        if (product.Quantity - 1 == 0)
        {
            _currentUser.UserBasket.Remove(product);
            BasketListbox.Items.Remove(product);
        }
        else
        {
            product.Quantity -= 1;
        }
    }

    private void PlusProductCount_ButtonClick(object sender, RoutedEventArgs e)
    {
        Button changeCount = (Button)sender;
        Products product = (Products)changeCount.DataContext;

        product.Quantity += 1;
    }

}