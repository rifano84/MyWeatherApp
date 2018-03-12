using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Android;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Plugin.Geolocator;
using Android.Locations;
using Android.Content;
using Xamarin.Forms;
using Rg.Plugins.Popup.Droid;


namespace MyWeatherApp.Droid
{
    [Activity(Label = "MyWeatherApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ActivityCompat.IOnRequestPermissionsResultCallback
    {


        const int MY_PERMISSIONS_REQUEST_GPS = 1;


        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }




        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {

            //PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

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







    }
}

