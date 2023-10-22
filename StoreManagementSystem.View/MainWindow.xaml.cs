using StoreManagementSystem.Service.Interfaces;
using StoreManagementSystem.Service.Services;
using StoreManagementSystem.View.Components;
using StoreManagementSystem.View.Helpers;
using StoreManagementSystem.View.Windows.Stores;
using System.Windows;
using System.Windows.Input;

namespace StoreManagementSystem.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private bool VisableMenuPanel = true;

    private readonly IStoreManagerService storeManagerService;
    public MainWindow()
    {
        this.storeManagerService = new StoreManagerService();
        InitializeComponent();
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        wrpStores.Children.Clear();

        var storeManager = await storeManagerService.RetrieveByIdAsync(StoreManagerHelper.StoreManagerId);

        var stores = storeManager.Data.Stores;

        foreach (var store in stores)
        {
            StoreView storesView = new StoreView();

            storesView.SetData(store);

            wrpStores.Children.Add(storesView);
        }
    }
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (VisableMenuPanel)
        {
            MenuPanel.Width = 35;
        }
        else
        {
            MenuPanel.Width = 140;
        }

        VisableMenuPanel = !VisableMenuPanel;
    }

    private void Click_Add(object sender, MouseButtonEventArgs e)
    {
        StoreCreationWindow storeCreation = new StoreCreationWindow();

        storeCreation.Show();

        this.Close();
    }
}
