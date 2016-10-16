namespace Monoka.ExampleGame.Common.Infrastructure.Adapters
{
    public interface IAutoMapperAdapter
    {
        T Map<T>(object source);
        TDestination Map<TSource, TDestination>(TSource source);
    }
}