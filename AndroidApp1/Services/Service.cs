using AndroidApp1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AndroidApp1.Services
{

    public class Service
    {
        public async Task<string> GetCurrentLocation(GeoLocation geoLocation)
        {
            HttpClient client;
            client = new HttpClient();
            string appid = "09220a3eaa7b1b278c4be7d01aaf0e82";

            var uri = new Uri($"https://api.openweathermap.org/data/2.5/weather?lat={geoLocation.latitude}&lon={geoLocation.longitude}&appid={appid}");
            HttpClient myClient = new HttpClient();

            var response = await myClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Items = JsonConvert.DeserializeObject<CurrentLocation.Root>(content);
                Console.WriteLine("");
            }

            return "dadasd";
        }
        public  async Task<Location> Location()
        {
            Location location = new Location();
            try
            {
                location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Latitude}");
                }

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Console.WriteLine($"error device not supported: {fnsEx}");
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                Console.WriteLine($"error not enables on device exception: {fneEx}");
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                Console.WriteLine($"error not permission: {pEx}");
                // Handle permission exception
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error to get location: {ex}");
                // Unable to get location
            }
            return location;
        }
    }
}
