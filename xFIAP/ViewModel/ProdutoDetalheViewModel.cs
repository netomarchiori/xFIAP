using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xFIAP.Model;

namespace xFIAP.ViewModel
{
    public class ProdutoDetalheViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public ProdutoModel Produto;

        private ObservableCollection<ComentarioProdutoModel> _comentario;
        public ObservableCollection<ComentarioProdutoModel> Comentarios
        {
            get { return _comentario; }
            set { _comentario = value; RaisePropertyChanged("Comentarios"); }

        }

        public ProdutoDetalheViewModel()
        {
            this.InitializeVM();
        }

        private async void InitializeVM()
        {
            _comentario = await ComentarioProdutoRepository.GetComentariosAsync();
        }

        public async void GetComentarios()
        {
            _comentario = await ComentarioProdutoRepository.GetComentariosAsync();
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
