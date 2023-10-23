using Newtonsoft.Json;
using StoreManagementSystem.Service.Interfaces;
using StoreManagementSystem.Service.Services;
using StoreManagementSystem.View.Constans;
using StoreManagementSystem.View.Helpers;
using StoreManagementSystem.View.Models;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StoreManagementSystem.View.Windows.StoreManagers;

/// <summary>
/// Interaction logic for Login.xaml
/// </summary>
public partial class Login : Window
{
    private bool IsPasswordHidden = true;
    private string PasswordText = "";
    private bool Check = false;
    private bool CheckCache = true;
    private IStoreManagerService storeManagerService;
    public Login()
    {
        this.storeManagerService = new StoreManagerService();

        InitializeComponent();
    }

    private async void tbUsername_GotFocus(object sender, RoutedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        if (textBox.Text == "Username")
        {
            textBox.Text = "";
        }
    }

    private async void tbUsername_LostFocus(object sender, RoutedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
            textBox.Text = "Username";
            textBox.FontWeight = FontWeights.Bold;
            textBox.Foreground = new SolidColorBrush(Color.FromRgb(175, 191, 253));
        }
    }

    private async void tbPassword_GotFocus(object sender, RoutedEventArgs e)
    {
        if (tbPassword.Text == "Password")
        {
            tbPassword.Text = "";
            PasswordText = "";
        }

    }

    private async void tbPassword_LostFocus(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(tbPassword.Text))
        {
            tbPassword.Text = "Password";
            tbPassword.FontWeight = FontWeights.Bold;
            tbPassword.Foreground = new SolidColorBrush(Color.FromRgb(175, 191, 253));
        }
    }

    private async void Hide_Click(object sender, RoutedEventArgs e)
    {
        if (IsPasswordHidden)
        {
            tbPassword.Text = PasswordText;
        }
        else
        {
            tbPassword.Text = new string('*', tbPassword.Text.Length);
        }

        IsPasswordHidden = !IsPasswordHidden;

        InvalidateVisual();
    }

    private async void SignUpOpener(object sender, RoutedEventArgs e)
    {
        var register = new Register();
        register.Show();
        this.Close();
    }

    private async void SignInEvent(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(tbUsername.Text))
            lworning.Content = "You need enter username";

        else if (string.IsNullOrEmpty(tbPassword.Text))
            lworning.Content = "you need enter password";

        var isValidPassword = await this.storeManagerService.IsValidPassword(tbUsername.Text, PasswordText);

        if (chbRemember.IsChecked == true)
        {
            try
            {
                LoginModel loginModel = new LoginModel()
                {
                    Username = tbUsername.Text,
                    Password = PasswordText
                };

                string json = JsonConvert.SerializeObject(loginModel);
                File.WriteAllText(FilePath.CACHE_PATH, json);
            }
            catch
            {

            }
        }

        if (isValidPassword.StatusCode == 404)
        {
            lworning.Content = isValidPassword.Message;
            return;
        }
        else if (isValidPassword.StatusCode == 422)
        {

            lworning.Content = isValidPassword.Message;
            return;
        }
        else
        {
            var storeManager = await storeManagerService.RetrieveByUsernameAsync(tbUsername.Text);

            StoreManagerHelper.StoreManagerId = storeManager.Data.Id;

            var mainwindow = new MainWindow();
            mainwindow.Show();
            tbUsername.Text = "Username";
            this.Close();
        }

        tbPassword.Text = "Password";
        PasswordText = "";
    }

    public bool FillLoginPage()
    {
        if (CheckCache)
        {
            CheckCache = false;
            try
            {
                var source = File.ReadAllText(FilePath.CACHE_PATH);

                var loginModel = JsonConvert.DeserializeObject<LoginModel>(source);

                if (loginModel is not null)
                {
                    tbUsername.Text = loginModel.Username;
                    tbPassword.Text = new string('*', loginModel.Password.Length);
                    PasswordText = loginModel.Password;
                }
                else
                {
                    return false;
                }
            }

            catch
            {
                return false;
            }
            return true;
        }
        CheckCache = false;
        return false;
    }

    private void tbTextChanged(object sender, TextChangedEventArgs e)
    {
        string currentPassword = "";
        var cursorPosition = tbPassword.CaretIndex;

        if(FillLoginPage())
        {
            return;
        }

        if (Check)
        {
            if (IsPasswordHidden)
            {

                if (tbPassword.Text.Length < PasswordText.Length)
                {
                    for (int i = 0; i < PasswordText.Length; i++)
                    {
                        if (i != cursorPosition)
                            currentPassword += PasswordText[i];
                    }
                }
                else if (tbPassword.Text.Length == PasswordText.Length + 1)
                {
                    int k = 0;
                    cursorPosition -= 1;
                    for (int i = 0; i < PasswordText.Length; i++)
                    {
                        if (i != cursorPosition)
                            currentPassword += PasswordText[i];
                        else
                        {
                            currentPassword += tbPassword.Text[i];
                            currentPassword += PasswordText[i];
                            k = 1;
                        }
                    }
                    if (k == 0)
                    {
                        currentPassword += tbPassword.Text[tbPassword.Text.Length - 1];
                    }
                    cursorPosition += 1;
                }
                else if (tbPassword.Text.Length == PasswordText.Length || tbPassword.Text.Length > PasswordText.Length)
                    return;
                PasswordText = string.Copy(currentPassword);
                tbPassword.Text = new string('*', PasswordText.Length);
                tbPassword.CaretIndex = cursorPosition;
            }
            else
            {
                if (!tbPassword.Text.Contains('*'))
                    PasswordText = tbPassword.Text;
            }
        }
        Check = true;
    }
}
