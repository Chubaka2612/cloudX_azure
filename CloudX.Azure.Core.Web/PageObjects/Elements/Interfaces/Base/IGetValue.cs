namespace CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Base
{
    public interface IGetValue<out T>
    {
        T Value { get; }
    }
}
