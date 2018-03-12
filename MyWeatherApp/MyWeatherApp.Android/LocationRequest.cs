using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using MyWeatherApp.Droid;
using Plugin.Permissions;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocationRequest))]
namespace MyWeatherApp.Droid
{


    // implementation of the location interface
    public class LocationRequest : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ILocationPermission, ActivityCompat.IOnRequestPermissionsResultCallback
    {
        const int MY_PERMISSIONS_REQUEST_GPS = 1;

       

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {

            switch (requestCode)
            {
                case MY_PERMISSIONS_REQUEST_GPS:
                    {
                        // If request is cancelled, the result arrays are empty.
                        if (grantResults.Length > 0 && grantResults[0] == Android.Content.PM.Permission.Granted)
                        {
                            LocationManager locationManager = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);

                            if (locationManager.IsProviderEnabled(LocationManager.GpsProvider) == false)
                            {
                                Intent gpsSettingIntent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                                Forms.Context.StartActivity(gpsSettingIntent);
                            }

                        }
                        else
                        {

                            MainPage.Instance.DisplayAlert("GPS Access denied", "Without the GPS Access the App will not be able to define your current Location!", "OK");
                        }
                        return;
                    }
            }
        }



        public Task<bool> IsLocationPermissionGranted()
        {
            if (ContextCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.AccessCoarseLocation) != Android.Content.PM.Permission.Granted)
                return Task.FromResult(false);

            else
                return Task.FromResult (true);


        }


        public Task<bool> TurnGPSOn()
        {
            LocationManager locationManager = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);
           
                Intent gpsSettingIntent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                Forms.Context.StartActivity(gpsSettingIntent);

            return Task.FromResult(true);

        }



        public Task<bool> IsGPSOn()
        {
            LocationManager locationManager = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);

            if (locationManager.IsProviderEnabled(LocationManager.GpsProvider) == false)
                return Task.FromResult(false);
            else
                return Task.FromResult(true);
        }



        public async Task<bool> RequestLocationPermission()
        {
            // Get the MainActivity instance  
            MainActivity activity = Forms.Context as MainActivity;
           
                // Should we show an explanation?
                if (ActivityCompat.ShouldShowRequestPermissionRationale(activity,
                        Manifest.Permission.AccessCoarseLocation))
                {

                    await MainPage.Instance.DisplayAlert("Access GPS Location", "The App requires accessing the GPS Location", "OK");
                ActivityCompat.RequestPermissions(activity,
                            new String[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation },
                            MY_PERMISSIONS_REQUEST_GPS);

            }
                else
                {

                    // No explanation needed, we can request the permission.
                    ActivityCompat.RequestPermissions(activity,
                            new String[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation },
                            MY_PERMISSIONS_REQUEST_GPS);

                   
                }
        
           

            return true;
        }




        



        
    }
}