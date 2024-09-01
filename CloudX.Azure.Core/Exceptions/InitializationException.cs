
namespace CloudX.Azure.Core.Exceptions
{
    [Serializable]
    public class InitializationException : Exception
    {
        public InitializationException(string message) : base(message) { }
    }
}
