using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using xFIAP.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace xFIAP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            LoginModel userAuth = App.LoginVM.Usuarios.Where(
                a => a.Username.ToLower() == txtUsuario.Text.ToLower() &&
                a.Password == pwdSenha.Password).FirstOrDefault();
                       
            if (userAuth != null)
            {
                this.Frame.Navigate(typeof(MainPage), userAuth);
            }
            else
            {
                var messageDialog = new MessageDialog(
                    "Usuário inválido", "Atenção");
                await messageDialog.ShowAsync();
            }
        }
    }
}
