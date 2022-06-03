using Pallet.Database.Context;
using Pallet.Database.Entities.OPC;
using Pallet.Database.Entities.Users;

namespace Pallet.Data;

internal class DbInitializer
{
    private readonly DatabaseDB _db;
    private readonly ILogger<DbInitializer> _Logger;

    private List<Signal> _Signals;
    private List<Alarm> _Alarms;

    //private List<Profile> _Profiles;
    //private List<Nailer> _NailTypes;
    private List<User> _Users;

    public DbInitializer(
        DatabaseDB db, ILogger<DbInitializer> Logger
        )
    {
        _db = db;
        _Logger = Logger;
    }

    //!!
    public async Task InitializeAsync()
    {
        var timer = Stopwatch.StartNew();
        _Logger.LogInformation("Инициализация БД...");

        //_Logger.LogInformation("Удаление существующей БД...");
        //await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
        //_Logger.LogInformation("Удаление существующей БД выполнено за {0} мс", timer.ElapsedMilliseconds);

        //_db.Database.EnsureCreated();

        _Logger.LogInformation("Миграция БД...");
        try
        {
            _db.Database.MigrateAsync().Wait();
        }
        catch { }
        _Logger.LogInformation("Миграция БД выполнена за {0} мс", timer.ElapsedMilliseconds);

        //if (!_db.Profiles.AsQueryable().Any())
        //    await InitializeProfiles().ConfigureAwait(false);

        if (!_db.Signals.AsQueryable().Any())
            await InitializeSignals().ConfigureAwait(false);

        if (!_db.Alarms.AsQueryable().Any())
            await InitializeAlarms().ConfigureAwait(false);

        if (!_db.Users.AsQueryable().Any())
            await InitializeUsers().ConfigureAwait(false);

        _Logger.LogInformation("Инициализация БД выполнена за {0} с", timer.Elapsed.TotalSeconds);
    }

    private async Task InitializeUsers()
    {
        _Logger.LogInformation("Profiles initialize ... ");
        _Users = new();
        _Users.Add(new User
        {
            Name = "jarda.papik",
            Description = "Jarda Papík",
            RoleNum = 1,
            Hashcode = "56B1DB8133D9EB398AABD376F07BF8AB5FC584EA0B8BD6A1770200CB613CA005"
        });
        _Users.Add(new User
        {
            Name = "oleksii.klymov",
            Description = "Oleksii Klymov, BenThor",
            RoleNum = 10,
            Hashcode = "A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3"
        });
        _Users.Add(new User
        {
            Name = "pavel.stekl",
            Description = "Ing. Pavel Štekl, BenThor",
            RoleNum = 2,
            Hashcode = "618D4F2B0BE8A5EE027C13B495E17C5504DE7DD296833C0D3B6D474DB1364A8B"
        });

        await _db.Users.AddRangeAsync(_Users);
        await _db.SaveChangesAsync();
    }

    private async Task InitializeSignals()
    {
        _Logger.LogInformation("Signals initialize ... ");
        _Signals = new();
        _Signals.Add(new Signal
        {
            Name = "DatenAnforderung",
            Address = "ns=3;s=\"status\".\"PC_IN\".\"DatenAnforderung\""
        });
        _Signals.Add(new Signal
        {
            Name = "JobFertig",
            Address = "ns=3;s=\"status\".\"PC_IN\".\"JobFertig\""
        });
        _Signals.Add(new Signal
        {
            Name = "JobQuittierung",
            Address = "ns=3;s=\"status\".\"PC_OUT\".\"JobQuittierung\""
        });
        _Signals.Add(new Signal
        {
            Name = "M_Auto",
            Address = "ns=3;s=\"M_Auto\""
        });
        _Signals.Add(new Signal
        {
            Name = "M_Halt",
            Address = "ns=3;s=\"M_Halt\""
        });
        _Signals.Add(new Signal
        {
            Name = "F_Quitt",
            Address = "ns=3;s=\"F_Quitt\""
        });
        _Signals.Add(new Signal
        {
            Name = "M_nail",
            Address = "ns=3;s=\"M_nail\""
        });
        _Signals.Add(new Signal
        {
            Name = "M_Links",
            Address = "ns=3;s=\"M_Links\""
        });
        _Signals.Add(new Signal
        {
            Name = "M_rechts",
            Address = "ns=3;s=\"M_rechts\""
        });
        _Signals.Add(new Signal
        {
            Name = "M_start",
            Address = "ns=3;s=\"M_start\""
        });
        _Signals.Add(new Signal
        {
            Name = "M_Robot_stoe",
            Address = "ns=3;s=\"M_Robot_stoe\""
        });
        _Signals.Add(new Signal
        {
            Name = "M_shooting",
            Address = "ns=3;s=\"M_shooting\""
        });
        _Signals.Add(new Signal
        {
            Name = "M_LampTeste",
            Address = "ns=3;s=\"M_LampTeste\""
        });
        await _db.Signals.AddRangeAsync(_Signals);
        await _db.SaveChangesAsync();
    }

    private async Task InitializeAlarms()
    {
        _Logger.LogInformation("Alarms initialize ... ");

        _Alarms = new();
        _Alarms.Add(new Alarm
        {
            NR = 1,
            Device = "Station",
            Inverted = false,
            Priority = "S",
            StopType = "T",
            Alarmtext1 = "Not Automatic Mode",
            Alarmtext2 = "Ohne Automatik",
            Alarmtext3 = "Není v automatickém režimu",
            Name = "N_auto",
            Address = "ns=3;s=\"Stoe_DB\".\"N_auto\""
        });
        _Alarms.Add(new Alarm
        {
            NR = 2,
            Device = "Station",
            Inverted = false,
            Priority = "S",
            StopType = "O",
            Alarmtext1 = "Stop",
            Alarmtext2 = "Halt",
            Alarmtext3 = "Stop",
            Name = "Halt",
            Address = "ns=3;s=\"Stoe_DB\".\"Halt\""
        });
        _Alarms.Add(new Alarm
        {
            NR = 3,
            Device = "Station",
            Inverted = false,
            Priority = "S",
            StopType = "O",
            Alarmtext1 = "Emergency Stop",
            Alarmtext2 = "Not-Aus",
            Alarmtext3 = "Nouzový stop",
            Name = "EmStop",
            Address = "ns=3;s=\"Stoe_DB\".\"EmStop\""
        });
        _Alarms.Add(new Alarm
        {
            NR = 4,
            Device = "Robot",
            Inverted = false,
            Priority = "S",
            StopType = "T",
            Alarmtext1 = "Robot - System Fault",
            Alarmtext2 = "Roboter - Systemfehler",
            Alarmtext3 = "Robot - systémová porucha",
            Name = "R_systemFault",
            Address = "ns=3;s=\"Stoe_DB\".\"R_sytemFault\""
        });
        _Alarms.Add(new Alarm
        {
            NR = 5,
            Device = "Robot",
            Inverted = false,
            Priority = "S",
            StopType = "O",
            Alarmtext1 = "Robot - Emergency Stop",
            Alarmtext2 = "Roboter - Not - Aus",
            Alarmtext3 = "Robot - Nouzový stop",
            Name = "R_EmStop",
            Address = "ns=3;s=\"Stoe_DB\".\"R_EmStop\""
        });
        _Alarms.Add(new Alarm
        {
            NR = 6,
            Device = "Station",
            Inverted = false,
            Priority = "S",
            StopType = "T",
            Alarmtext1 = "General Fault",
            Alarmtext2 = "Gesamtfehler",
            Alarmtext3 = "Celková porucha",
            Name = "Gesamtfehler",
            Address = "ns=3;s=\"Stoe_DB\".\"Gesamtfehler\""
        });
        _Alarms.Add(new Alarm
        {
            NR = 7,
            Device = "Station",
            Inverted = false,
            Priority = "S",
            StopType = "T",
            Alarmtext1 = "General BS",
            Alarmtext2 = "BS-Gesamt",
            Alarmtext3 = "Celková BS",
            Name = "BS_gesamt",
            Address = "ns=3;s=\"Stoe_DB\".\"BS_gesamt\""
        });
        _Alarms.Add(new Alarm
        {
            NR = 8,
            Device = "Robot",
            Inverted = false,
            Priority = "S",
            StopType = "T",
            Alarmtext1 = "Robot - Not Auto",
            Alarmtext2 = "Roboter -Ohne Automatik",
            Alarmtext3 = "Robot -Není v automatice",
            Name = "N_R_Auto",
            Address = "ns=3;s=\"Stoe_DB\".\"N_R_Auto\""
        });
        _Alarms.Add(new Alarm
        {
            NR = 9,
            Device = "Station",
            Inverted = false,
            Priority = "Z",
            StopType = "-",
            Alarmtext1 = "Motor Off",
            Alarmtext2 = "Motor aus",
            Alarmtext3 = "Motor vypnut",
            Name = "MotOffState",
            Address = "ns=3;s=\"Stoe_DB\".\"MotOffState\""
        });
        _Alarms.Add(new Alarm
        {
            NR = 10,
            Device = "Station",
            Inverted = false,
            Priority = "S",
            StopType = "T",
            Alarmtext1 = "Motor Not On",
            Alarmtext2 = "Motor nicht ein",
            Alarmtext3 = "Motor neběží",
            Name = "N_MotOnState",
            Address = "ns=3;s=\"Stoe_DB\".\"N_MotOnState\""
        });
        _Alarms.Add(new Alarm
        {
            NR = 11,
            Device = "Station",
            Inverted = false,
            Priority = "M",
            StopType = "-",
            Alarmtext1 = "PC Not Connected",
            Alarmtext2 = "PC nicht angebunden",
            Alarmtext3 = "PC nepřipojeno",
            Name = "PC_not_connected",
            Address = "ns=3;s=\"Stoe_DB\".\"PC_not_connected\""
        });
        _Alarms.Add(new Alarm
        {
            NR = 12,
            Device = "Station",
            Inverted = false,
            Priority = "S",
            StopType = "O",
            Alarmtext1 = "Emergency Stop",
            Alarmtext2 = "Not - Aus",
            Alarmtext3 = "Nouzový stop",
            Name = "Not_halt",
            Address = "ns=3;s=\"Stoe_DB\".\"Not_halt\""
        });
        //_Alarms.Add(new Alarm
        //{
        //    NR = 13,
        //    Device = "Station",
        //    Inverted = true,
        //    Priority = "Z",
        //    StopType = "-",
        //    Alarmtext1 = "Automatic Mode",
        //    Alarmtext2 = "Automatik",
        //    Alarmtext3 = "Automatický režim",
        //    Name = "Auto",
        //    Address = "ns=3;s=\"Stoe_DB\".\"N_auto\""
        //});

        await _db.Alarms.AddRangeAsync(_Alarms);
        await _db.SaveChangesAsync();
    }
}