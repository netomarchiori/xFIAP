using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;

namespace xFIAP.Model
{
    public class ProdutoModel
    {
        public ProdutoModel() { }

        public string Id { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string Quantidade { get; set; }
        private string _precounitario { get; set; }
        public string Precounitario
        {
            get { return "R$ " + _precounitario + ".00"; }
            set { _precounitario = value; }
        }
    }

    public static class ProdutoRepository
    {

        private static List<ProdutoModel> produtoSqlAzure;

        public static async Task<List<ProdutoModel>> GetProdutoWebserviceAsync()
        {
            if (produtoSqlAzure != null) return produtoSqlAzure;

            var httpRequest = new HttpClient();
            var stream = await httpRequest.GetStreamAsync("http://www.wopek.com/xml/produtos.xml");
            var reader = new StreamReader(stream, Encoding.UTF8);
            var xmlFile = reader.ReadToEnd();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile);

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/produtos/produto");
            produtoSqlAzure = new List<ProdutoModel>();


            foreach (IXmlNode node in nodes)
            {
                ProdutoModel produtoModel = new ProdutoModel();
                produtoModel.Descricao = node.SelectSingleNode("descricao").InnerText;
                produtoModel.Categoria = node.SelectSingleNode("categoria").InnerText;
                produtoModel.Quantidade = node.SelectSingleNode("quantidade").InnerText;
                produtoModel.Precounitario = node.SelectSingleNode("precounitario").InnerText;
                produtoModel.Id = node.Attributes.GetNamedItem("id").InnerText;
                produtoSqlAzure.Add(produtoModel);
            }

            return produtoSqlAzure;
        }


    }
}
