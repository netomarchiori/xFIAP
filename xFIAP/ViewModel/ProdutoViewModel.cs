using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xFIAP.Model;

namespace xFIAP.ViewModel
{
    public class ProdutoViewModel
    {
        public List<ProdutoModel> Produtos { get; set; } = new List<ProdutoModel>();

        public ProdutoViewModel()
        {
            this.InitializeVM();

        }

        private async void InitializeVM()
        {
            Produtos = await ProdutoRepository.GetProdutoWebserviceAsync();
        }
    }
}
