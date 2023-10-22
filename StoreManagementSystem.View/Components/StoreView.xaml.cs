using StoreManagementSystem.Domain.Entities;
using StoreManagementSystem.Service.DTOs.Stories;
using StoreManagementSystem.Service.Interfaces;
using StoreManagementSystem.Service.Services;
using StoreManagementSystem.View.Windows.Stores;
using System.Windows;
using System.Windows.Controls;

namespace StoreManagementSystem.View.Components
{
    /// <summary>
    /// Interaction logic for StoreView.xaml
    /// </summary>
    public partial class StoreView : UserControl
    {
        private StoreResultDto Store;
        private readonly IStoreService storeService;
        public StoreView()
        {
            this.storeService = new StoreService();
            InitializeComponent();
        }
        public void SetData(StoreResultDto store)
        {
            lbName.Content = store.Name;
            tbDescription.Text = store.Description;
            this.Store = store;
        }

        private void Clicked_Edit(object sender, RoutedEventArgs e)
        {

            StoreUpdateWindow storeUpdate = new StoreUpdateWindow(this.Store.Id);
            storeUpdate.Show();

            MainWindow.GetWindow(this).Close();
        }

        private async void Clicked_Delete(object sender, RoutedEventArgs e)
        {
            await this.storeService.RemoveAsync(this.Store.Id);

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            MainWindow.GetWindow(this).Close();
        }
    }
}
