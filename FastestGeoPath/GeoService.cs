namespace FastestGeoPath
{
  /// <summary>
  /// Service for Geo functions.
  /// </summary>
  public class GeoService
  {
    /// <summary>
    /// Get current location.
    /// </summary>
    /// <param name="accuracy">Accuracy for getting location.</param>
    /// <param name="timeout">Timeout for getting location.</param>
    /// <returns>Location or null</returns>
    /// <exception cref="FeatureNotSupportedException"/>
    /// <exception cref="FeatureNotEnabledException"/>
    /// <exception cref="PermissionException"/>
    public static async Task<Location?> GetLocation(GeolocationAccuracy accuracy, TimeSpan timeout)
    {
      GeolocationRequest request = new(accuracy, timeout);
      return await Geolocation.Default.GetLocationAsync(request, new CancellationToken());
    }
  }
}
