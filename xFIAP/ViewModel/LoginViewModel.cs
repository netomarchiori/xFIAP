using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xFIAP.Model;

namespace xFIAP.ViewModel
{
    public class LoginViewModel
    {
        public List<LoginModel> Usuarios { get; set; } = new List<LoginModel>();

        public LoginViewModel()
        {
            this.InitializeVM();

        }

        private async void InitializeVM()
        {
            Usuarios = await LoginRepository.GetUsuarioWebserviceAsync();
        }


    }
}
