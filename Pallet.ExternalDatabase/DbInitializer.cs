using Newtonsoft.Json.Linq;
using Pallet.ExternalDatabase.Context;

namespace Pallet.ExternalDatabase;

public class ExternalDbInitializer
{
    private readonly ExternalDbContext _db;
    private readonly ILogger<ExternalDbInitializer> _Logger;
    private readonly JObject JsonInitObject;

    public ExternalDbInitializer(ExternalDbContext db, ILogger<ExternalDbInitializer> Logger)
    {
        _db = db;
        _Logger = Logger;
    }

    public async Task InitializeAsync()
    {
        //_Logger.LogInformation("Удаление существующей БД выполнено за {0} мс", timer.ElapsedMilliseconds);
        if (!_db.Database.CanConnect()) throw new Exception();
    }
}