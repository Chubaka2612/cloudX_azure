namespace CloudX.Azure.Core.Web.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public sealed class PageAttribute : Attribute
    {
        public string Url { get; set; }

        public string Name { get; set; }
    }
}