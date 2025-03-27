namespace Recetas.Front.ClasesClientes;

public static class ViewModelsOperacion
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddScoped<HomeViewModel>();
        services.AddScoped<NuevaRecetaViewModel>();
        services.AddScoped<DetalleRecetaViewModel>();
        return services;
    }
}

