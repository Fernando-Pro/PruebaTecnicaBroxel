namespace Recetas.Front.ClasesClientes;

public static class RepositorioOperacion
{
    public static IServiceCollection AddRepositorios(this IServiceCollection services)
    {
        services.AddTransient<IRepositorioRecetas, RepositorioRecetas>();
        return services;
    }
}

