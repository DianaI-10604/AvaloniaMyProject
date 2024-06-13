using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.ComponentModel;

namespace AvaloniaMyProject;

public partial class Basket : Window
{
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

        if (product.BasketProductQuantity - 1 == 0)
        {
            _currentUser.UserBasket.Remove(product);
            BasketListbox.Items.Remove(product);
        }
        else
        {
            product.BasketProductQuantity -= 1;
        }
    }

    private void PlusProductCount_ButtonClick(object sender, RoutedEventArgs e)
    {
        Button changeCount = (Button)sender;
        Products product = (Products)changeCount.DataContext;

        //если пытаемся положитьо больше чем есть
        if ((product.BasketProductQuantity + 1) > product.Quantity)
        {
            DeleteProductMessageBox message = new DeleteProductMessageBox();
            message.firstmessage.Text = "Вы не можете положить в корзину больше, чем есть";
            message.secondmessage.Text = "на складе";
            message.Show();
        }
        else
        {
            product.BasketProductQuantity += 1;
        }
    }

    private void DeleteProductFromBasket_ButtonClick(object sender, RoutedEventArgs e)
    {
        Button delete = (Button)sender;
        Products producttodelete = (Products)delete.DataContext;

        _currentUser.UserBasket.Remove(producttodelete);

        BasketListbox.Items.Clear();
        foreach (var product in _currentUser.UserBasket)
        {
            BasketListbox.Items.Add(product);
        }
    }
}