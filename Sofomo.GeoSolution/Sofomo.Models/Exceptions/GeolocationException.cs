namespace Sofomo.Models
{
    public class GeolocationException : Exception
    {
        private const string Unexpected = "Unexpected error.";

        /// <summary>
        /// Mapping the error codes to the default error messages.
        /// </summary>
        private static readonly Dictionary<GeolocationErrorCode, string> ErrorCodeToMessage = new()
        {
            {GeolocationErrorCode.NullGeolocationRequest, "The geolocation request is null."},
            {GeolocationErrorCode.NoSuchGeolocation, "No such geolocation (ip: {0}) found."},
            {GeolocationErrorCode.GeolocationExists, "Such geolocation (ip: {0}) has already been added."}
        };
        
        public GeolocationException(GeolocationErrorCode errorCode)
            : base(GetErrorMessage(errorCode))
        {
        }
        
        public GeolocationException(string ip, GeolocationErrorCode errorCode)
            : base(GetErrorMessage(ip, errorCode))
        {
        }

        private static string GetErrorMessage(GeolocationErrorCode errorCode)
        {
            return ErrorCodeToMessage.ContainsKey(errorCode)
                ? ErrorCodeToMessage[errorCode]
                : Unexpected;
        }

        private static string GetErrorMessage(string ip, GeolocationErrorCode errorCode)
        {
            return ErrorCodeToMessage.ContainsKey(errorCode)
                ? string.Format(ErrorCodeToMessage[errorCode], ip)
                : Unexpected;
        }
    }
}