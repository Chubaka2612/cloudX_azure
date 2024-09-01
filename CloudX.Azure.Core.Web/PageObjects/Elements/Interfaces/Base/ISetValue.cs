namespace CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Base
{
    public interface ISetValue<T> : IGetValue<T>
    {
        new T Value { get; set; }
    }
}
