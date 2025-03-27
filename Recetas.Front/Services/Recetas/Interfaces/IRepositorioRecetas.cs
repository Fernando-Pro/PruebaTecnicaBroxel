namespace Recetas.Front.Services.Recetas.Interfaces;

public interface IRepositorioRecetas
{
    Task<Receta> Inserta(Receta receta);
    Task<IEnumerable<Receta>> ObtieneListaRecetas();
    Task<Receta> Actualiza(Receta receta);
    Task<Receta> Elimina(Receta receta);
}

