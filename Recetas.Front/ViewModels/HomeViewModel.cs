namespace Recetas.Front.ViewModels;

public class HomeViewModel : ObservableObject
{
	private readonly IRepositorioRecetas repositorioRecetas;
	public ObservableCollection<Receta> recetas { get; set; } = new ObservableCollection<Receta>();
	public ObservableCollection<Receta> recetasRespaldo { get; set; } = new ObservableCollection<Receta>();
    public ICommand FilterCommand { get; set; }
    public string FiltroIngresado { get; set; } = string.Empty;
    public HomeViewModel(IRepositorioRecetas repositorioRecetas)
	{
		this.repositorioRecetas = repositorioRecetas;
        FilterCommand = new Command(() => FilterCommands());
    }

	public async Task<IEnumerable<Receta>> ConsultarRecetas()
	{
		try
		{
			var recetasList = await repositorioRecetas.ObtieneListaRecetas();
			recetas = new ObservableCollection<Receta>(recetasList);
			recetasRespaldo = new ObservableCollection<Receta>(recetasList);
            OnPropertyChanged(nameof(recetas));

            return recetas;
		}
		catch(Exception ex)
		{
            Console.WriteLine($"Error HomeViewModel || ConsultarRecetas {ex.Message}");
			throw;
        }
	}

	public async Task AgregarReceta(Receta receta)
	{
		try
		{
			await repositorioRecetas.Inserta(receta);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error HomeViewModel || AgregarReceta {ex.Message}");
		}
	}

	public async Task ActualizaReceta(Receta receta)
	{
        try
        {
            await repositorioRecetas.Actualiza(receta);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error HomeViewModel || ActualizaReceta {ex.Message}");
        }
    }
    public void FilterCommands()
	{
		if (!string.IsNullOrWhiteSpace(FiltroIngresado))
		{
			var aplicarFiltro = recetas.Where(x=> x.NombreReceta.Contains(FiltroIngresado));
            recetas = new ObservableCollection<Receta>(aplicarFiltro);
            OnPropertyChanged(nameof(recetas));
        }
		else
		{
            recetas = new ObservableCollection<Receta>(recetasRespaldo);
            OnPropertyChanged(nameof(recetas));
        }
	}
}

