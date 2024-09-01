using System;

namespace CloudX.Azure.Core.Web.Exceptions
{
    [Serializable]
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException(string message) : base(message) { }
    }
}
