namespace Recetas.Front.ViewModels;

public class NuevaRecetaViewModel : ObservableObject
{
    private readonly IRepositorioRecetas repositorioRecetas;
    public Receta? RecetaSeleccionada { get; set; }
    public NuevaRecetaViewModel(IRepositorioRecetas repositorioRecetas)
	{
        this.repositorioRecetas = repositorioRecetas;
	}

    public async Task GenerarNuevaReceta(Receta receta)
    {
        try
        {
            await repositorioRecetas.Inserta(receta);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error NuevaRecetaViewModel || GenerarNuevaReceta {ex.Message}");
        }
    }
    public void CargaInformacionRecetaSeleccionada(Receta receta)
    {
        RecetaSeleccionada = new Receta();
        RecetaSeleccionada = receta;
        OnPropertyChanged(nameof(RecetaSeleccionada));
    }
    public void LimpiarInformacionRecetaSeleccionada()
    {
        RecetaSeleccionada = new Receta();
        OnPropertyChanged(nameof(RecetaSeleccionada));
    }
    public async Task ActualizarReceta(Receta receta)
    {
        try
        {
            await repositorioRecetas.Actualiza(receta);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error NuevaRecetaViewModel || GenerarNuevaReceta {ex.Message}");
        }
    }
}

