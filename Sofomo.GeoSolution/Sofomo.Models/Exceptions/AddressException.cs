namespace Sofomo.Models
{
    public class AddressException : Exception
    {
        private const string Unexpected = "Unexpected error.";

        private static readonly Dictionary<AddressErrorCode, string> ErrorCodeToMessage = new()
        {
            {AddressErrorCode.InvalidAddress, "The address ({0}) is invalid."}
        };

        public AddressException(string address, AddressErrorCode errorCode)
            : base(GetErrorMessage(address, errorCode))
        {
        }

        private static string GetErrorMessage(string address, AddressErrorCode errorCode)
        {
            return ErrorCodeToMessage.ContainsKey(errorCode)
                ? string.Format(ErrorCodeToMessage[errorCode], address)
                : Unexpected;
        }
    }
}