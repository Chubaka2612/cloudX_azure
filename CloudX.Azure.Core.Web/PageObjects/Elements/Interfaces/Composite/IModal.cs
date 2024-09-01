namespace CloudX.Azure.Core.Web.PageObjects.Elements.Interfaces.Composite
{
    public interface IModal<T> : IContainer where T : IContainer
    {
        T Content { get; }

        void VanishedWait();
    }
}
