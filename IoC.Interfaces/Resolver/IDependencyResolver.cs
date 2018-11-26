namespace IoC.Interfaces
{
    public interface IDependencyResolver
    {
        IDependencyScope BeginScope();
    }
}
