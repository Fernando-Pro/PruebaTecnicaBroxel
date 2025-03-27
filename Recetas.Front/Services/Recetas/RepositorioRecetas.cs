namespace Recetas.Front.Services.Recetas;

public class RepositorioRecetas : IRepositorioRecetas
{
    private readonly ISQLiteDataAccess sQLiteDataAccess;

    public RepositorioRecetas(ISQLiteDataAccess sQLiteDataAccess)
    {
        this.sQLiteDataAccess = sQLiteDataAccess;
    }

    public async Task<Receta> Inserta(Receta receta)
    {
        await sQLiteDataAccess.AddItemAsync(receta);
        return receta;
    }

    public async Task<Receta> Actualiza(Receta receta)
    {
        await sQLiteDataAccess.UpdateItemAsync(receta);
        return receta;
    }

    public async Task<IEnumerable<Receta>> ObtieneListaRecetas()
    {
        return await sQLiteDataAccess.GetAllAsync<Receta>();
    }

    public async Task<Receta> Elimina(Receta receta)
    {
        await sQLiteDataAccess.DeleteItemAsync(receta);
        return receta;
    }
}

