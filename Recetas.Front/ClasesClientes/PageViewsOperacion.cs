using Recetas.Front.Views.Recetas;

namespace Recetas.Front.ClasesClientes;

public static class PageViewsOperacion
{
    public static IServiceCollection AddPagesViewsOperacion(this IServiceCollection services)
    {
        services.AddTransient<HomePage>();
        services.AddTransient<NuevaRecetaPage>();
        services.AddTransient<DetalleRecetaPage>();
        return services;
    }
}

