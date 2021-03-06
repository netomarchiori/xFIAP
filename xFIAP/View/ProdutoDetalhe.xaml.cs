﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using xFIAP.Model;
using xFIAP.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace xFIAP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProdutoDetalhe : Page
    {
        public ProdutoDetalheViewModel ViewModel { get; private set; }
        public ProdutoDetalheViewModel ProdutoDetalheVM { get; private set; }

        public ProdutoDetalhe()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ProdutoModel produto = e.Parameter as ProdutoModel;

            ProdutoDetalheVM = new ProdutoDetalheViewModel(produto.Descricao);
            this.ViewModel = ProdutoDetalheVM;
            this.DataContext = ProdutoDetalheVM;
            ViewModel.Produto = produto;
            System.Diagnostics.Debug.WriteLine(string.Format("ProdutoDetalhe OnNavigatedTo {0}", produto.Descricao));
        }

        private void btnComentar_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(txtComentario.Text);
            ComentarioProdutoModel comentario = new ComentarioProdutoModel();
            comentario.Id = DateTime.Now.Millisecond;
            comentario.Produto = ViewModel.Produto.Descricao;
            comentario.Comentario = txtComentario.Text;
            ComentarioProdutoRepository.CriarComentario(comentario);
            //ToDo: Comentarios are not refreshing
            ViewModel.GetComentarios(ViewModel.Produto.Descricao);
            txtComentario.Text = "";
        }

        private void txtComentario_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
