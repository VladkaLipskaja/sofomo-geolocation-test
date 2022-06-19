namespace Sofomo.Models
{
    public class InfrastructureException : Exception
    {
        private const string Unexpected = "Unexpected error.";

        private static readonly Dictionary<InfrastructureErrorCode, string> ErrorCodeToMessage = new()
        {
            {
                InfrastructureErrorCode.IPStackUnavailable, // smth wrong with ip stack.
                "Geolocation (ip: {0}) can't be determined. Please try later."
            },
            {
                InfrastructureErrorCode.IPStackResponseError, // need a developer check of ip stack response (reaching limit or smth).
                "Geolocation (ip: {0}) can't be determined because of temporarily works. Please try later."
            },
            {
                InfrastructureErrorCode.DatabaseUnavailable,
                "Geolocation (ip: {0}) can't be processed and updated in database. Please try later."
            }
        };

        public InfrastructureException(string ip, InfrastructureErrorCode errorCode)
            : base(GetErrorMessage(ip, errorCode))
        {
        }

        private static string GetErrorMessage(string ip, InfrastructureErrorCode errorCode)
        {
            return ErrorCodeToMessage.ContainsKey(errorCode)
                ? string.Format(ErrorCodeToMessage[errorCode], ip)
                : Unexpected;
        }
    }
}