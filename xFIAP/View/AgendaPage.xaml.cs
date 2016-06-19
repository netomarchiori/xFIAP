using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Appointments;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.System;
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

        private async void btnRota_Click(object sender, RoutedEventArgs e)
        {
            string address = txtLocalizacao.Text;

            var locFinderResult = await MapLocationFinder.FindLocationsAsync(address, new Geopoint(new BasicGeoposition()));

            // add error checking here

            var geoPos = locFinderResult.Locations[0].Point.Position;

            var routeToUri = new Uri(String.Format(
                 "ms-drive-to:?destination.latitude={0}&destination.longitude={1}&destination.name={2}",
                 geoPos.Latitude,
                 geoPos.Longitude,
                 address));

            var launcherOptions = new LauncherOptions();
            launcherOptions.TargetApplicationPackageFamilyName = "Microsoft.WindowsMaps_8wekyb3d8bbwe";
            await Launcher.LaunchUriAsync(routeToUri, launcherOptions);
        }
    }
}
