namespace Recetas.Front.Services.DataBase;

public class SQLiteDataAccess : IAsyncDisposable, ISQLiteDataAccess
{
    private const string DbName = "Recetas.db3";
    private static string DBPath => Path.Combine(FileSystem.AppDataDirectory, DbName);
    private SQLiteAsyncConnection _connection;
    private SQLiteAsyncConnection _Database =>
        (_connection ??= new SQLiteAsyncConnection(DBPath,
            SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));
    private async Task CreateTableIfNotExists<TTable>() where TTable : class, new()
    {
        await _Database.CreateTableAsync<TTable>();
    }

    public async Task<AsyncTableQuery<TTable>> GetTableAsync<TTable>() where TTable : class, new()
    {
        await CreateTableIfNotExists<TTable>();
        return _Database.Table<TTable>();
    }

    public async Task<IEnumerable<TTable>> GetAllAsync<TTable>() where TTable : class, new()
    {
        var table = await GetTableAsync<TTable>();
        return await table.ToListAsync();
    }

    public async Task<List<TTable>> GetAllAsync<TTable>(string query) where TTable : class, new()
    {
        await CreateTableIfNotExists<TTable>();
        var r = await _connection.QueryAsync<TTable>(query);
        return r;
    }

    public async Task<IEnumerable<TTable>> GetFileteredAsync<TTable>(Expression<Func<TTable, bool>> predicate) where TTable : class, new()
    {
        var table = await GetTableAsync<TTable>();
        return await table.Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<TTable>> GetFileteredObjectAsync<TTable>(Expression<Func<TTable, bool>> predicate) where TTable : class, new()
    {
        var table = await GetTableAsync<TTable>();
        return await table.Where(predicate).ToListAsync();
    }

    private async Task<TResult?> Execute<TTable, TResult>(Func<Task<TResult>> action) where TTable : class, new()
    {
        await CreateTableIfNotExists<TTable>();
        return await (action() ?? null);
    }

    public async Task<TTable?> GetItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new()
    {
        return await Execute<TTable, TTable>(async () => await _Database.GetAsync<TTable>(primaryKey));
    }


    public async Task<TTable?> GetSingleItem<TTable>(object primaryKey) where TTable : class, new()
    {
        return await Execute<TTable, TTable>(async () => await _Database.FindAsync<TTable>(primaryKey));
    }

    public async Task<bool> AddItemAsync<TTable>(TTable item) where TTable : class, new()
        => await Execute<TTable, bool>(async () => await _Database.InsertAsync(item) > 0);

    public async Task<bool> UpdateItemAsync<TTable>(TTable item) where TTable : class, new()
    {
        await CreateTableIfNotExists<TTable>();
        return await _Database.UpdateAsync(item) > 0;
    }

    public async Task<bool> DeleteItemAsync<TTable>(TTable item) where TTable : class, new()
    {
        await CreateTableIfNotExists<TTable>();
        return await _Database.DeleteAsync(item) > 0;
    }

    public async Task<bool> DeleteItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new()
    {
        await CreateTableIfNotExists<TTable>();
        return await _Database.DeleteAsync<TTable>(primaryKey) > 0;
    }
    public async Task<bool> DeleteItemTable<TTable>() where TTable : class, new()
    {
        await CreateTableIfNotExists<TTable>();
        return await _Database.DeleteAllAsync<TTable>() > 0;
    }

    public async ValueTask DisposeAsync() => await _connection?.CloseAsync();

}

