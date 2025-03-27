namespace Recetas.Front.Views;

public partial class HomePage : ContentPage
{
	private readonly HomeViewModel viewModel;
    public List<Receta> Recetas { get; set; } = new List<Receta>();
    public HomePage()
	{
		InitializeComponent();
        BindingContext = viewModel = ServiceHelper.GetService<HomeViewModel>();
        
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        AppShell.Current.IsBusy = true;
        await ConsultarRecetas();
        AppShell.Current.IsBusy = false;
    }

    async void ListRecetas_ItemSeleccionado(System.Object sender, Microsoft.Maui.Controls.ItemTappedEventArgs e)
    {
        var recetaSeleccionada = (e.Item as Receta);
        if(recetaSeleccionada != null)
            await Navigation.PushModalAsync(new DetalleRecetaPage(recetaSeleccionada));
    }

    public async Task ConsultarRecetas()
    {
        var consultaRecetas = await viewModel.ConsultarRecetas();
    }

    async void AgregarReceta_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        await Navigation.PushModalAsync(new NuevaRecetaPage());
    }
}
