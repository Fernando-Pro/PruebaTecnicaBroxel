namespace Recetas.Front.Views.Recetas;

public partial class DetalleRecetaPage : ContentPage
{
    private readonly DetalleRecetaViewModel detalleRecetaViewModel;
    public Receta? recetaSeleccionada { get; set; }
	public DetalleRecetaPage()
	{
		InitializeComponent();
        BindingContext = detalleRecetaViewModel = ServiceHelper.GetService<DetalleRecetaViewModel>();
    }
    public DetalleRecetaPage(Receta receta)
    {
        InitializeComponent();
        BindingContext = detalleRecetaViewModel = ServiceHelper.GetService<DetalleRecetaViewModel>();
        detalleRecetaViewModel.CargaInformacionRecetaSeleccionada(receta);
        recetaSeleccionada = receta;
    }
    async void Cerrar_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    async void EditarReceta_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        if(recetaSeleccionada != null)
            await Navigation.PushModalAsync(new NuevaRecetaPage(recetaSeleccionada));
    }

    async void EliminarRecetaClicked(System.Object sender, System.EventArgs e)
    {
        if(recetaSeleccionada != null)
        {
            var confirmarEliminarReceta = DisplayAlert("Advertencia",$"Desea eliminar la receta {recetaSeleccionada.NombreReceta} ?","SI","NO");
            if (await confirmarEliminarReceta)
            {
                await detalleRecetaViewModel.EliminarReceta(recetaSeleccionada);
                await Navigation.PopModalAsync();
            }
        }
            
    }
}
