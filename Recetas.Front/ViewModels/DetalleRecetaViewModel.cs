namespace Recetas.Front.ViewModels;

public class DetalleRecetaViewModel : ObservableObject
{
	public Receta? Receta { get; set; }
    private readonly IRepositorioRecetas repositorioRecetas;
    public DetalleRecetaViewModel(IRepositorioRecetas repositorioRecetas)
    {
        this.repositorioRecetas = repositorioRecetas;
    }

    public void CargaInformacionRecetaSeleccionada(Receta receta)
    {
        Receta = new Receta();
        Receta = receta;
        OnPropertyChanged(nameof(Receta));
    }

    public async Task EliminarReceta(Receta recetaSeleccionada)
    {
        try
        {
            await repositorioRecetas.Elimina(recetaSeleccionada);
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error DetalleRecetaViewModel || Elimina {ex.Message}");
            throw;
        }
    }
}

