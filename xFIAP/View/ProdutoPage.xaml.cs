﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using xFIAP.Model;
using xFIAP.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace xFIAP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProdutoPage : Page
    {
        public ProdutoPage()
        {
            this.InitializeComponent();
            this.Loaded += ProdutoView_Loaded;
        }

        private void ProdutoView_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = App.ProdutoVM;
        }

        private void lstProdutos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProdutoModel selectedProduct = (sender as ListBox).SelectedItem as ProdutoModel;

            if (selectedProduct != null)
            {
                System.Diagnostics.Debug.WriteLine(selectedProduct.Descricao);
                Frame.Navigate(typeof(ProdutoDetalhe),selectedProduct);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.ProdutoVM.Categoria = ((ComboBox)sender).SelectedValue.ToString();
            System.Diagnostics.Debug.WriteLine("Filtro Categoria => " + App.ProdutoVM.Categoria);
        }

        private void btnAgendarCliente_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AgendaPage));
        }
    }
}
