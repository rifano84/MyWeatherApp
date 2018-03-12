using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyWeatherApp
{

    // interface hosting methods related to location permission (especially for android!)
    public interface ILocationPermission
    {

        Task<bool> RequestLocationPermission();

        Task<bool> IsLocationPermissionGranted();

        Task<bool> IsGPSOn();

        Task<bool> TurnGPSOn();

    }
}
