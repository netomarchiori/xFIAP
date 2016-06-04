﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
    }
}
