using Android.Content.PM;
using Android.Content.Res;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidApp1.Models;
using AndroidApp1.Services;
using System;
using System.Reflection.Emit;
using Xamarin.Essentials;

using static Android.Views.View;
using Location = Xamarin.Essentials.Location;
using Service = AndroidApp1.Services.Service;

namespace AndroidApp1
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity , IOnMapReadyCallback
    {
        TextView? latitude;
        TextView? longitude;
        Button? button;
        Service service = new Service();
        //public EventHandler Button_Click { get; private set; }

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            button= FindViewById<Button>(Resource.Id.buttonId);
            if(button != null)
            button.Click += OnButtonClicked;
           
        }
        public async void OnMapReady(GoogleMap map)
        {
            var location = await service.Location();
            
            map.UiSettings.ZoomControlsEnabled = true;
            map.UiSettings.CompassEnabled = true;
            MarkerOptions markerOpt1 = new MarkerOptions();
            markerOpt1.SetPosition(new LatLng(location.Latitude, location.Latitude));
            markerOpt1.SetTitle("Location");
            map.AddMarker(markerOpt1);

            GeoLocation? geoLocation = new GeoLocation();
            geoLocation.latitude = location.Latitude;
            geoLocation.longitude = location.Longitude;

            var descriptionLocation = await service.GetCurrentLocation(geoLocation);
            

            longitude = FindViewById<TextView>(Resource.Id.lon);
            latitude = FindViewById<TextView>(Resource.Id.lat);
            if (longitude != null && latitude != null)
            {
                latitude.Text = location.Latitude.ToString();
                longitude.Text = location.Longitude.ToString();
            }
        }

        private async void OnButtonClicked(object? sender, EventArgs e)
        {
        
            
            var mapFragment = (MapFragment)FragmentManager?.FindFragmentById(Resource.Id.map);
            mapFragment?.GetMapAsync(this);

            
        }
    }
}