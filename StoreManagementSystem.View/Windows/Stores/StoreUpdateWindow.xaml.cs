using StoreManagementSystem.Service.DTOs.StoreManagers;
using StoreManagementSystem.Service.DTOs.Stories;
using StoreManagementSystem.Service.Interfaces;
using StoreManagementSystem.Service.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace StoreManagementSystem.View.Windows.Stores
{
    /// <summary>
    /// Interaction logic for StoreUpdateWindow.xaml
    /// </summary>
    public partial class StoreUpdateWindow : Window
    {
        private readonly IStoreService storeService;
        private long storeId;
        public StoreUpdateWindow(long storeId)
        {
            this.storeId = storeId;
            this.storeService = new StoreService();
            InitializeComponent();
            FillUpdateWindow(this.storeId);
        }

        public async void FillUpdateWindow(long storeId)
        {
            var store = (await this.storeService.RetrieveByIdAsync(storeId)).Data;

            tbName.Text = store.Name;
            rtnDescription.Document.Blocks.Clear();
            rtnDescription.Document.Blocks.Add(new Paragraph(new Run($"{store.Description}")));
        }
        string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
              rtb.Document.ContentStart,

              rtb.Document.ContentEnd
            );

            return textRange.Text;
        }


        private async void Update_Button(object sender, RoutedEventArgs e)
        {
            if (tbName.Text != "")
            {
                StoreUpdateDto storeUpdate = new StoreUpdateDto()
                {
                    Id = storeId,
                    Name = tbName.Text,
                    Description = StringFromRichTextBox(rtnDescription),
                };

                await this.storeService.ModifyAsync(storeUpdate);

                MessageBox.Show("updated");

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
}
