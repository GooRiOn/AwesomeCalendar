namespace AwesomeCalendar.Infrastructure.DependencyInjection.Interfaces
{
    public interface ICustomDependencyResolver
    {
        TResolved Resolve<TResolved>();
    }
}