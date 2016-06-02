using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;

namespace xFIAP.Model
{
    public class LoginModel
    {
        public LoginModel() { }

        public string Nome { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public static class LoginRepository
    {
        private static List<LoginModel> usuarioSqlAzure;

        public static async Task<List<LoginModel>> GetUsuarioWebserviceAsync()
        {
            if (usuarioSqlAzure != null) return usuarioSqlAzure;

            var httpRequest = new HttpClient();
            var stream = await httpRequest.GetStreamAsync("http://www.wopek.com/xml/usuarios.xml");
            var reader = new StreamReader(stream,Encoding.UTF8);
            var xmlFile = reader.ReadToEnd();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile);
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/usuarios/usuario");
            usuarioSqlAzure = new List<LoginModel>();
            foreach (IXmlNode node in nodes)
            {
                LoginModel loginModel = new LoginModel();
                loginModel.Nome = node.SelectSingleNode("nome").InnerText;
                loginModel.Password = node.SelectSingleNode("password").InnerText;
                loginModel.Username = node.SelectSingleNode("username").InnerText;
                usuarioSqlAzure.Add(loginModel);
            }

            return usuarioSqlAzure;
        }
    }
    }
