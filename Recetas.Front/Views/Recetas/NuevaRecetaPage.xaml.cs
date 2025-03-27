namespace Recetas.Front.Views.Recetas;

public partial class NuevaRecetaPage : ContentPage
{
	private readonly NuevaRecetaViewModel nuevaRecetaViewModel;
    public Receta? recetaSeleccionada { get; set; }
	public NuevaRecetaPage()
	{
		InitializeComponent();
        BindingContext = nuevaRecetaViewModel = ServiceHelper.GetService<NuevaRecetaViewModel>();
    }
    public NuevaRecetaPage(Receta receta)
    {
        InitializeComponent();
        BindingContext = nuevaRecetaViewModel = ServiceHelper.GetService<NuevaRecetaViewModel>();
        nuevaRecetaViewModel.CargaInformacionRecetaSeleccionada(receta);
        recetaSeleccionada = receta;
    }

    async void Cerrar_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        nuevaRecetaViewModel.LimpiarInformacionRecetaSeleccionada();
        await Navigation.PopModalAsync();
    }

    void Dificultad_CheckedChanged(System.Object sender, Microsoft.Maui.Controls.CheckedChangedEventArgs e)
    {
        RadioButton? radioButton = sender as RadioButton;
    }

    void ValidarCampo(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        //if (e.OldTextValue != e.NewTextValue)
        //    ValidarCamposFormulario();
    }
    public async Task<bool> ValidarCamposFormulario()
    {
        if (string.IsNullOrWhiteSpace(EntryNombreReceta.Text))
        {
            await this.DisplayAlert("Advertencia","El campo Nombre esta vacio","Aceptar");
            return false;
        }
        if (string.IsNullOrWhiteSpace(EntryIngredientes.Text))
        {
            await this.DisplayAlert("Advertencia", "El campo Ingredientes esta vacio", "Aceptar");
            return false;
        }
        if (string.IsNullOrWhiteSpace(EntryPreparacion.Text))
        {
            await this.DisplayAlert("Advertencia", "El campo Preparación esta vacio", "Aceptar");
            return false;
        }
        if (string.IsNullOrWhiteSpace(EntryTiempoPreparacion.Text))
        {
            await this.DisplayAlert("Advertencia", "El campo Tiempo Preparación esta vacio", "Aceptar");
            return false;
        }
        if (Dificultad.SelectedIndex == -1)
        {
            await this.DisplayAlert("Advertencia", "Seleccionar la Dificultad", "Aceptar");
            return false;
        }
        if(!EntryTiempoPreparacion.Text.ToCharArray().All(Char.IsDigit))
        {
            await this.DisplayAlert("Advertencia", "El campo Tiempo debe ser numérico", "Aceptar");
            return false;
        }
        return true;
    }

    async void GuardarReceta(System.Object sender, System.EventArgs e)
    {
        if(await ValidarCamposFormulario())
        {
            AppShell.Current.IsBusy = true;
            Receta nuevaReceta = new Receta()
            {
                NombreReceta = EntryNombreReceta.Text,
                Ingredientes = EntryIngredientes.Text,
                Preparacion = EntryPreparacion.Text,
                TiempoPreparacion = Convert.ToInt32(EntryTiempoPreparacion.Text),
                Dificultad = (EnumDificultad)Dificultad.SelectedIndex,
                ImagenSourceReceta = EntryImagen.Text
            };
            if(recetaSeleccionada != null)
                await nuevaRecetaViewModel.ActualizarReceta(nuevaReceta);
            else
                await nuevaRecetaViewModel.GenerarNuevaReceta(nuevaReceta);
            AppShell.Current.IsBusy = false;
            await Navigation.PopModalAsync();
        }
    }

    async void CancelarClicked(System.Object sender, System.EventArgs e)
    {
        nuevaRecetaViewModel.LimpiarInformacionRecetaSeleccionada();
        await Navigation.PopModalAsync();
    }
}
