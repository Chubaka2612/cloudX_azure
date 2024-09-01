using System;

namespace CloudX.Azure.Core.Web.Exceptions
{
    [Serializable]
    public class PageObjectInitializationException : Exception
    {
        public PageObjectInitializationException(string message) : base(message) { }
    }
}
