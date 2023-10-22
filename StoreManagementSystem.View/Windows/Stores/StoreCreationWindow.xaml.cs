using StoreManagementSystem.Data.DbContexts;
using StoreManagementSystem.Service.DTOs.StoreManagers;
using StoreManagementSystem.Service.DTOs.Stories;
using StoreManagementSystem.Service.Interfaces;
using StoreManagementSystem.Service.Services;
using StoreManagementSystem.View.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace StoreManagementSystem.View.Windows.Stores;

/// <summary>
/// Interaction logic for StoreCreationWindow.xaml
/// </summary>
public partial class StoreCreationWindow : Window
{
    private readonly IStoreService storeService;
    public StoreCreationWindow()
    {
        this.storeService = new StoreService();
        InitializeComponent();
    }

    string StringFromRichTextBox(RichTextBox rtb)
    {
        TextRange textRange = new TextRange(
          rtb.Document.ContentStart,

          rtb.Document.ContentEnd
        );

        return textRange.Text;
    }


    private async void Create_Button(object sender, RoutedEventArgs e)
    {
        if (tbName.Text != "")
        {
            StoreCreationDto storeCreation = new StoreCreationDto()
            {
                Name = tbName.Text,
                Description = StringFromRichTextBox(rtnDescription),
                StoreManagerId = StoreManagerHelper.StoreManagerId
            };

            await this.storeService.AddAsync(storeCreation);

            MessageBox.Show("created");

            MainWindow mainWindow = new MainWindow();

            mainWindow.Show();

            this.Close();
        }
        else
        {
            MessageBox.Show("You need enter store name");
        }
    }
}
