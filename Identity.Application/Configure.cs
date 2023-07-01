namespace Identity.Application;

public static class Configure
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddMediatR(options =>
                options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .RegisterServices();

        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        return services;
    }
}
