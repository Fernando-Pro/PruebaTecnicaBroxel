namespace Recetas.Front.Services.DataBase.Interfaces;

public interface ISQLiteDataAccess
{
    Task<bool> AddItemAsync<TTable>(TTable item) where TTable : class, new();
    Task<bool> DeleteItemAsync<TTable>(TTable item) where TTable : class, new();
    Task<bool> DeleteItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new();
    Task<bool> DeleteItemTable<TTable>() where TTable : class, new();
    ValueTask DisposeAsync();
    Task<IEnumerable<TTable>> GetAllAsync<TTable>() where TTable : class, new();
    Task<List<TTable>> GetAllAsync<TTable>(string query) where TTable : class, new();
    Task<IEnumerable<TTable>> GetFileteredAsync<TTable>(Expression<Func<TTable, bool>> predicate) where TTable : class, new();
    Task<IEnumerable<TTable>> GetFileteredObjectAsync<TTable>(Expression<Func<TTable, bool>> predicate) where TTable : class, new();
    Task<TTable?> GetItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new();
    Task<TTable?> GetSingleItem<TTable>(object primaryKey) where TTable : class, new();
    Task<AsyncTableQuery<TTable>> GetTableAsync<TTable>() where TTable : class, new();
    Task<bool> UpdateItemAsync<TTable>(TTable item) where TTable : class, new();
}

