namespace Identity.Host.Extensions;

public static class ScrutorExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyDependencies(Assembly.GetExecutingAssembly())
                .AddClasses(classes => classes.AssignableTo<ITransientService>())
                    .AsMatchingInterface()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo<IScopedService>())
                    .AsMatchingInterface()
                    .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo<ISingletonService>())
                    .AsMatchingInterface()
                    .WithSingletonLifetime());
    }
}
