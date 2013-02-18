using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
//using Bing.Maps;
using System.Net;
using Bing.Maps;
// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace WhereUAt
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class ResultsPage : WhereUAt.Common.LayoutAwarePage
    {
        //private string url = "https://dev.virtualearth.net/REST/v1/Locations/{0}?key={1}";
        public ResultsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected async override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            //resultsBlock.Text = "Your Location: \nAtlanta, GA";
            Geolocator geo = new Geolocator();
          //  Bing.Maps.Location location = new Location();
            Geoposition position = await geo.GetGeopositionAsync();

            //Setting map view
            Bing.Maps.Location location = new Bing.Maps.Location(position.Coordinate.Latitude, position.Coordinate.Longitude);
            

            //Putting pushpin at location
            Pushpin p = new Pushpin();
            MapLayer.SetPosition(p, location);
            map.Children.Add(p);

            map.ZoomLevel = 15;
            map.SetView(location);
            resultsBlock.Text = "Location: " + position.Coordinate.Latitude + " , " + position.Coordinate.Longitude;

        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            RadioButton clicked = (RadioButton)sender;
            if (clicked.Content as String == "Street")
                map.MapType = MapType.Road;
            if (clicked.Content as String == "Satellite")
            {
                map.MapType = MapType.Aerial;
            }
            if (clicked.Content as String == "Hybrid")
            {
                map.MapType = MapType.Birdseye;
            }
        }

     

        private void map_DoubleTapped_1(object sender, TappedRoutedEventArgs e)
        {
            Point clickedAt = e.GetPosition(sender as UIElement);
            Location clickedLocation;
            map.TryPixelToLocation(clickedAt, out clickedLocation);
            Pushpin p = new Pushpin();
            p.Name="Clicked Here";
            MapLayer.SetPosition(p, clickedLocation);
            map.Children.Add(p);
            


        }

        private void ToggleSwitch_Toggled_1(object sender, RoutedEventArgs e)
        {
            map.ShowTraffic = (sender as ToggleSwitch).IsOn;
           
        }

        private void ToggleSwitch_Toggled_2(object sender, RoutedEventArgs e)
        {
            map.ShowBreadcrumb = (sender as ToggleSwitch).IsOn;

        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Geolocator geo2 = new Geolocator();
            Geoposition position = await geo2.GetGeopositionAsync();
            Location location = new Location(position.Coordinate.Latitude, position.Coordinate.Longitude);
            map.SetView(location);
        }


      
    }
}
