using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Appointments;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace xFIAP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AgendaPage : Page
    {
        private Appointment agendamento;

        public AgendaPage()
        {
            this.InitializeComponent();
            agendamento = new Appointment();
        }

        private void btnListaProdutos_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ProdutoPage));
        }

        public Rect getElementRect(FrameworkElement element)
        {
            GeneralTransform buttonTransform = element.TransformToVisual(null);
            Point point = buttonTransform.TransformPoint(new Point());
            return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
        }

        private async void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            var rect = getElementRect(sender as FrameworkElement);

            agendamento.Subject = txtAssunto.Text;
            agendamento.AllDay = (chkDiainteiro.IsChecked == null) ? false : (bool)chkDiainteiro.IsChecked;
            agendamento.Location = txtLocalizacao.Text;
            agendamento.BusyStatus = AppointmentBusyStatus.Busy;
            agendamento.StartTime = (dteInicio.Date == null) ? DateTime.Now : dteInicio.Date.Value;
            agendamento.Duration = GetDuracao();

            await AppointmentManager.ShowAddAppointmentAsync(agendamento, rect);
        }

        private TimeSpan GetDuracao()
        {
            DateTimeOffset Inicio;
            DateTimeOffset Termino;

            Inicio = dteInicio.Date.Value.AddHours(tmeInicio.Time.Hours).AddMinutes(tmeInicio.Time.Minutes);
            Termino = dteTermino.Date.Value.AddHours(tmeTermino.Time.Hours).AddMinutes(tmeTermino.Time.Minutes);

            return (dteTermino.Date.Value - dteInicio.Date.Value);
        }
    }
}
