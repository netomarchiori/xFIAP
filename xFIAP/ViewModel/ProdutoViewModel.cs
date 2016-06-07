using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Popups;
using xFIAP.Model;

namespace xFIAP.ViewModel
{
    public class ProdutoViewModel : INotifyPropertyChanged
    {
        //public List<ProdutoModel> Produtos { get; set; } = new List<ProdutoModel>();
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ProdutoModel> Produtos { get; set; } = new ObservableCollection<ProdutoModel>();
        public ObservableCollection<string> Categorias { get; set; } = new ObservableCollection<string>();
        public List<ProdutoModel> FiltroProdutos { get; set; } = new List<ProdutoModel>();
        private ProdutoModel produtoSelecionado;

        public ProdutoViewModel()
        {
            //DeleteProdutoCommando = new DeleteProdutoCommand(this);

            this.LoadedProduto();
        }

        public async void LoadedProduto()
        {
            FiltroProdutos = await ProdutoRepository.GetProdutoWebserviceAsync();
            ExecutarFiltro();
            ListaCategorias();
        }

        private void ListaCategorias()
        {
            List<string> todasCategorias = new List<string>();
            foreach(ProdutoModel p in FiltroProdutos)
            {
                todasCategorias.Add(p.Categoria);
            }
            Categorias = new ObservableCollection<string>(todasCategorias.Distinct().OrderBy(i => i).ToList());
            Categorias.Insert(0, "Todos");
        }

        public ProdutoModel ProdutoSelecionado
        {
            get { return produtoSelecionado; }
            set
            {
                produtoSelecionado = value;
                //DeleteProdutoCommando.DeleteCanExecuteChanged();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProdutoSelecionado)));
            }
        }



        private string pesquisaNome;
        public string PesquisaNome
        {
            get { return pesquisaNome; }
            set
            {
                pesquisaNome = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PesquisaNome)));
                ExecutarFiltro();
            }
        }

        private string categoria;
        public string Categoria
        {
            get { return categoria; }
            set
            {
                categoria = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Categoria)));
                ExecutarFiltro();
            }
        }

        public void ExecutarFiltro()
        {
            if (pesquisaNome == null) pesquisaNome = "";
            var resultado = FiltroProdutos.Where(n => n.Descricao.ToLowerInvariant()
                                   .Contains(PesquisaNome.ToLowerInvariant().Trim())).ToList();

            if (categoria != null && categoria.ToLowerInvariant() != "todos" && categoria.Length > 0)
                resultado = resultado.Where(n => n.Categoria.ToLowerInvariant()
                                   .Contains(Categoria.ToLowerInvariant().Trim())).ToList();

            var removerDaLista = Produtos.Except(resultado).ToList();
            foreach (var item in removerDaLista)
            {
                Produtos.Remove(item);
            }

            for (int index = 0; index < resultado.Count; index++)
            {
                var item = resultado[index];
                if (index + 1 > Produtos.Count || !Produtos[index].Equals(item))
                    Produtos.Insert(index, item);
            }
        }
    }
}
