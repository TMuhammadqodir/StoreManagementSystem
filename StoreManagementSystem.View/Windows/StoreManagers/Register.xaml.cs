using Newtonsoft.Json;
using StoreManagementSystem.Service.DTOs.StoreManagers;
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
/// Interaction logic for Register.xaml
/// </summary>
public partial class Register : Window
{
    private bool IsPasswordHidden = true;
    private string PasswordText = "";
    private bool Check = false;
    private bool IsRetypePasswordHidden = true;
    private string RetypePasswordText = "";
    private bool Check2 = false;
    private readonly IStoreManagerService storeManagerService;

    public Register()
    {
        InitializeComponent();

        this.storeManagerService = new StoreManagerService();
    }

    private void tbUsername_GotFocus(object sender, RoutedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        if (textBox.Text == "Username")
        {
            textBox.Text = "";
        }
    }

    private void tbUsername_LostFocus(object sender, RoutedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
            textBox.Text = "Username";
            textBox.FontWeight = FontWeights.Bold;
            textBox.Foreground = new SolidColorBrush(Color.FromRgb(175, 191, 253));
        }
    }

    private void tbPassword_GotFocus(object sender, RoutedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        if (textBox.Text == "Password")
        {
            textBox.Text = "";
            PasswordText = "";
        }
    }

    private void tbPassword_LostFocus(object sender, RoutedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
            textBox.Text = "Password";
            textBox.FontWeight = FontWeights.Bold;
            textBox.Foreground = new SolidColorBrush(Color.FromRgb(175, 191, 253));
        }
    }

    private void tbRetypePassword_GotFocus(object sender, RoutedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        if (textBox.Text == "Retype password")
        {
            textBox.Text = "";
            RetypePasswordText = "";
        }
    }

    private void tbRetypePassword_LostFocus(object sender, RoutedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
            textBox.Text = "Retype password";
            textBox.FontWeight = FontWeights.Bold;
            textBox.Foreground = new SolidColorBrush(Color.FromRgb(175, 191, 253));
        }
    }

    private void SignInOpener(object sender, RoutedEventArgs e)
    {
        var login = new Login();
        login.Show();
        this.Close();
    }

    private void Hide_Click(object sender, RoutedEventArgs e)
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

    private void tbPassword_Changed(object sender, TextChangedEventArgs e)
    {
        string currentPassword = "";
        var cursorPosition = tbPassword.CaretIndex;

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


    private void Hide_Click2(object sender, RoutedEventArgs e)
    {
        if (IsRetypePasswordHidden)
        {
            tbRetypePassword.Text = RetypePasswordText;
        }
        else
        {
            tbRetypePassword.Text = new string('*', tbRetypePassword.Text.Length);
        }

        IsRetypePasswordHidden = !IsRetypePasswordHidden;

        InvalidateVisual();
    }

    private void tbRetypePassword_Changed(object sender, TextChangedEventArgs e)
    {
        string currentRetypePassword = "";
        var cursorPosition = tbRetypePassword.CaretIndex;

        if (Check2)
        {
            if (IsRetypePasswordHidden)
            {

                if (tbRetypePassword.Text.Length < RetypePasswordText.Length)
                {
                    for (int i = 0; i < RetypePasswordText.Length; i++)
                    {
                        if (i != cursorPosition)
                            currentRetypePassword += RetypePasswordText[i];
                    }
                }
                else if (tbRetypePassword.Text.Length == RetypePasswordText.Length + 1)
                {
                    int k = 0;
                    cursorPosition -= 1;
                    for (int i = 0; i < RetypePasswordText.Length; i++)
                    {
                        if (i != cursorPosition)
                            currentRetypePassword += RetypePasswordText[i];
                        else
                        {
                            currentRetypePassword += tbRetypePassword.Text[i];
                            currentRetypePassword += RetypePasswordText[i];
                            k = 1;
                        }
                    }
                    if (k == 0)
                    {
                        currentRetypePassword += tbRetypePassword.Text[tbRetypePassword.Text.Length - 1];
                    }
                    cursorPosition += 1;
                }
                else if (tbRetypePassword.Text.Length == RetypePasswordText.Length || tbRetypePassword.Text.Length > RetypePasswordText.Length)
                    return;
                RetypePasswordText = string.Copy(currentRetypePassword);
                tbRetypePassword.Text = new string('*', RetypePasswordText.Length);
                tbRetypePassword.CaretIndex = cursorPosition;
            }
            else
            {
                if (!tbRetypePassword.Text.Contains('*'))
                    RetypePasswordText = tbRetypePassword.Text;
            }
        }
        Check2 = true;
    }

    public void ChangeText()
    {
        tbUsername.Text = "Username";
        tbPassword.Text = "Password";
        tbRetypePassword.Text = "Retype password";
        PasswordText = "";
        RetypePasswordText = "";
    }

    private async void SignUpEvent(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(tbUsername.Text))
        {
            lworning.Content = "You need enter username";
            ChangeText();
            return;
        }
        else if (string.IsNullOrEmpty(tbPassword.Text))
        {
            lworning.Content = "you need enter password";
            ChangeText();
            return;
        }
        else if (string.IsNullOrEmpty(tbRetypePassword.Text))
        {
            lworning.Content = "you need enter retype password";
            ChangeText();
            return;
        }
        else if (!tbPassword.Text.Equals(tbRetypePassword.Text))
        {
            lworning.Content = "password need equal to retype password";
            ChangeText();
            return;
        }

        var isStrongPassword = await storeManagerService.IsStrongPassword(tbPassword.Text);

        if (isStrongPassword.StatusCode == 422)
        {
            lworning.Content = isStrongPassword.Message;
            ChangeText();
            return;
        }

        StoreManagerCreationDto storeManagerCreation = new StoreManagerCreationDto()
        {
            Username = tbUsername.Text,
            Password = tbPassword.Text,
        };

        var storeManager = await storeManagerService.AddAsync(storeManagerCreation);

        if(storeManager.StatusCode == 403)
        {
            lworning.Content = "This username already exist";
            ChangeText();
            return;
        }

        StoreManagerHelper.StoreManagerId = storeManager.Data.Id;

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

        ChangeText();

        var mainwindow = new MainWindow();
        mainwindow.Show();
        this.Close();
    }
}
