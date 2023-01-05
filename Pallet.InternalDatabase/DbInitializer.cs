using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pallet.Extensions;
using Pallet.InternalDatabase.Context;
using Pallet.InternalDatabase.Entities.Log;
using Pallet.InternalDatabase.Entities.OPC;
using Pallet.InternalDatabase.Entities.Users;

namespace Pallet.InternalDatabase;

public class InternalDbInitializer
{
    private readonly InternalDbContext _db;
    private readonly ILogger<InternalDbInitializer> _Logger;
    private readonly JObject JsonInitObject;

    public InternalDbInitializer(InternalDbContext db, ILogger<InternalDbInitializer> Logger)
    {
        _db = db;
        _Logger = Logger;
        using StreamReader reader = File.OpenText(@"InitData\OPCInit.json");
        JsonInitObject = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
    }

    public async Task InitializeAsync()
    {
        var timer = Stopwatch.StartNew();
        _Logger.LogInformation("Инициализация БД...");
        if (!_db.Database.CanConnect()) throw new Exception();
        //_Logger.LogInformation("Удаление существующей БД...");
        //_db.Database.EnsureDeleted();
        //_Logger.LogInformation("Удаление существующей БД выполнено за {0} мс", timer.ElapsedMilliseconds);
        _db.Database.EnsureCreated();

        //_Logger.LogInformation("Миграция БД...");

        //_db.Database.Migrate();

        //_Logger.LogInformation("Миграция БД выполнена за {0} мс", timer.ElapsedMilliseconds);

        if (!_db.SystemEvents.AsQueryable().Any())
            InitializeSystemEvents();

        #region Init OPC

        if (!_db.Signals.AsQueryable().Any())
            InitializeSignals();

        if (!_db.Alarms.AsQueryable().Any())
            InitializeAlarms();

        #endregion Init OPC

        #region Init System

        if (!_db.Users.AsQueryable().Any())
            InitializeUsers();

        #endregion Init System

        _Logger.LogInformation("Инициализация БД выполнена за {0} с", timer.Elapsed.TotalSeconds);
    }

    #region Initialize System

    private void InitializeUsers()
    {
        _Logger.LogInformation("Users initialize ... ");
        List<User> _Users = new()
        {
            new User
            {
                Name = "jarda.papik",
                Description = "Jarda Papík",
                RoleNum = 1,
                RegistrationTime = DateTime.Now,
                AdminRegisteredName = "Admin",
                Hashcode = "56B1DB8133D9EB398AABD376F07BF8AB5FC584EA0B8BD6A1770200CB613CA005"
            },
            new User
            {
                Name = "oleksii.klymov",
                Description = "Oleksii Klymov, BenThor",
                RoleNum = 10,
                RegistrationTime = DateTime.Now,
                AdminRegisteredName = "Admin",
                Hashcode = "A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3"
            },
            new User
            {
                Name = "pavel.stekl",
                Description = "Ing. Pavel Štekl, BenThor",
                RoleNum = 2,
                RegistrationTime = DateTime.Now,
                AdminRegisteredName = "Admin",
                Hashcode = "618D4F2B0BE8A5EE027C13B495E17C5504DE7DD296833C0D3B6D474DB1364A8B"
            }
        };
        _db.Users.AddRange(_Users);
        _db.SaveChanges();

    }

    private void InitializeSystemEvents()
    {
        _Logger.LogInformation("SystemEvents initialize ... ");
        List<SystemEvent> _SystemEvent = new()
        {
            new SystemEvent
            {
                Name = "Login",
                DescriptionEn="Login",
                DescriptionDe="Login",
                DescriptionLocal="Login",
                Device="System"
            },
            new SystemEvent
            {
                Name = "Logout",
                DescriptionEn="Logout",
                DescriptionDe="Logout",
                DescriptionLocal="Logout",
                Device="System"
            },
            new SystemEvent
            {
                Name = "ActivateProfile",
                DescriptionEn="ActivateProfile",
                DescriptionDe="ActivateProfile",
                DescriptionLocal="ActivateProfile",
                Device="System"
            },
            new SystemEvent
            {
                Name = "DeactivateProfile",
                DescriptionEn="DeactivateProfile",
                DescriptionDe="DeactivateProfile",
                DescriptionLocal="DeactivateProfile",
                Device="System"
            },
            new SystemEvent
            {
                Name = "ChooseNailer",
                DescriptionEn="ChooseNailer",
                DescriptionDe="ChooseNailer",
                DescriptionLocal="ChooseNailer",
                Device="System"
            },
            new SystemEvent
            {
                Name = "ActivateProfile",
                DescriptionEn="ActivateProfile",
                DescriptionDe="ActivateProfile",
                DescriptionLocal="ActivateProfile",
                Device="System"
            },
            new SystemEvent
            {
                Name = "OPCConnected",
                DescriptionEn="OPC Connected",
                DescriptionDe="OPC Connected",
                DescriptionLocal="OPC Connected",
                Device="OPC Connection"
            },
            new SystemEvent
            {
                Name = "OPCDisconnected",
                DescriptionEn="OPC Disconnected",
                DescriptionDe="OPC Disconnected",
                DescriptionLocal="OPC Disconnected",
                Device="OPC Connection"
            },
            new SystemEvent
            {
                Name = "OPCError_ServersNotFinded",
                DescriptionEn="Servers not finded",
                DescriptionDe="Servers not finded",
                DescriptionLocal="Servers not finded",
                Device="OPC Connection"
            },
            new SystemEvent
            {
                Name = "OPCError_Header",
                DescriptionEn="OPC Connection",
                DescriptionDe="OPC Connection",
                DescriptionLocal="OPC Connection",
                Device="OPC Connection"
            },
            new SystemEvent
            {
                Name = "OPCError_ConnectionFailed",
                DescriptionEn="Connection failed",
                DescriptionDe="Connection failed",
                DescriptionLocal="Connection failed",
                Device="OPC Connection"
            },
            new SystemEvent
            {
                Name = "OPCError_ReadingFailed",
                DescriptionEn="Reading Failed",
                DescriptionDe="Reading failed",
                DescriptionLocal="Reading failed",
                Device="OPC Connection"
            },
            new SystemEvent
            {
                Name = "StartApp",
                DescriptionEn="Start Application",
                DescriptionDe="Start Application",
                DescriptionLocal="Start Application",
                Device="System"
            },
            new SystemEvent
            {
                Name = "ExitApp",
                DescriptionEn="Exit Application",
                DescriptionDe="Exit Application",
                DescriptionLocal="Exit Application",
                Device="System"
            }
        };
        _db.SystemEvents.AddRange(_SystemEvent);
        _db.SaveChanges();

    }

    #endregion Initialize System

    #region Initialize OPC

    private void InitializeSignals()
    {
        _Logger.LogInformation("Signals initialize ... ");
        List<Signal> _Signals = new();
        foreach (var obj in JsonInitObject["Signals"].ToList())
        {
            _Signals.Add(new Signal
            {
                Name = (string)obj["Name"],
                Address = (string)obj["Address"],
                DescriptionEn = (string)obj["DescriptionEn"],
                DescriptionDe = (string)obj["DescriptionDe"],
                DescriptionLocal = (string)obj["DescriptionLocal"],
                Device = (string)obj["Device"]
            });
        }

        //List<Signal> _Signals = new()
        //{
        //    new Signal
        //    {
        //        Name = "DatenAnforderung",
        //        Address = "ns=3;s=\"status\".\"PC_IN\".\"DatenAnforderung\"",
        //        DescriptionEn="Data request",
        //        DescriptionDe="DatenAnforderung",
        //        DescriptionLocal="Žádost o údaje",
        //        Device="PLC"
        //    },
        //    new Signal
        //    {
        //        Name = "JobFertig",
        //        Address = "ns=3;s=\"status\".\"PC_IN\".\"JobFertig\"",
        //        DescriptionEn="Job Done",
        //        DescriptionDe="Job Fertig",
        //        DescriptionLocal="Práce Dokončena",
        //        Device="PLC"
        //    },
        //    new Signal
        //    {
        //        Name = "AktuellDaten_niO",
        //        Address = "ns=3;s=\"status\".\"PC_IN\".\"AktuellDaten_niO\"",
        //        DescriptionEn="Data is actual",
        //        DescriptionDe="Daten sind aktuell",
        //        DescriptionLocal="Data jsou aktuální",
        //        Device="PLC"
        //    },
        //    new Signal
        //    {
        //        Name = "Stoerung",
        //        Address = "ns=3;s=\"status\".\"PC_IN\".\"Stoerung\"",
        //        DescriptionEn="Failure",
        //        DescriptionDe="Stoerung",
        //        DescriptionLocal="porucha",
        //        Device="PLC"
        //    },

        //    new Signal
        //    {
        //        Name = "DatenBereit",
        //        Address = "ns=3;s=\"status\".\"PC_OUT\".\"DatenBereit\"",
        //        DescriptionEn="Data Is Ready",
        //        DescriptionDe="Daten Bereit",
        //        DescriptionLocal="Data Připravena",
        //        Device="PLC"
        //    },
        //    new Signal
        //    {
        //        Name = "JobQuittierung",
        //        Address = "ns=3;s=\"status\".\"PC_OUT\".\"JobQuittierung\"",
        //        DescriptionEn="On kvit we delete dates",
        //        DescriptionDe="Auf kvit löschen wir Daten",
        //        DescriptionLocal="Na kvit smažeme data",
        //        Device="PLC"
        //    },
        //    new Signal
        //    {
        //        Name = "Anforderung_JobHalt",
        //        Address = "ns=3;s=\"status\".\"PC_OUT\".\"Anforderung_JobHalt\"",
        //        DescriptionEn="Finish the last nail and stop",
        //        DescriptionDe="Den letzten Nagel fertigstellen und anhalten",
        //        DescriptionLocal="Dokončit poslední hřebík a zastavit",
        //        Device="PLC"
        //    },
        //    new Signal
        //    {
        //        Name = "Anforderung_JobEnd",
        //        Address = "ns=3;s=\"status\".\"PC_OUT\".\"Anforderung_JobEnd\"",
        //        DescriptionEn="",
        //        DescriptionDe="",
        //        DescriptionLocal="",
        //        Device="PLC"
        //    },

        //    new Signal
        //    {
        //        Name = "Vizu_AutoStart",
        //        Address = "ns=3;s=\"Vizu_AutoStart\"",
        //        DescriptionEn="AutoMode Set",
        //        DescriptionDe="AutoMode Set",
        //        DescriptionLocal="AutoMode Set",
        //        Device="System"
        //    },
        //    new Signal
        //    {
        //        Name = "Vizu_Autostop",
        //        Address = "ns=3;s=\"Vizu_Autostop\"",
        //        DescriptionEn="HandMode Set",
        //        DescriptionDe="HandMode Set",
        //        DescriptionLocal="HandMode Set",
        //        Device=""
        //    },
        //    new Signal
        //    {
        //        Name = "MOD_Auto",
        //        Address = "ns=3;s=\"MOD_Auto\"",
        //        DescriptionEn="AutoMode Get",
        //        DescriptionDe="AutoMode Get",
        //        DescriptionLocal="AutoMode Get",
        //        Device="System"
        //    },
        //    new Signal
        //    {
        //        Name = "MOD_Hand",
        //        Address = "ns=3;s=\"MOD_Hand\"",
        //        DescriptionEn="HandMode",
        //        DescriptionDe="HandMode",
        //        DescriptionLocal="HandMode",
        //        Device=""
        //    },
        //    new Signal
        //    {
        //        Name = "OP1_Acknowledge",
        //        Address = "ns=3;s=\"OP1_Acknowledge\"",
        //        DescriptionEn="Acknowledgement",
        //        DescriptionDe="Quittierung",
        //        DescriptionLocal="Potvdit Poruchu",
        //        Device="System"
        //    },
        //    new Signal
        //    {
        //        Name = "F_Quitt",
        //        Address = "ns=3;s=\"F_Quitt\"",
        //        DescriptionEn="Depasivation",
        //        DescriptionDe="Depasivation",
        //        DescriptionLocal="Depasivace",
        //        Device=""
        //    },
        //    new Signal
        //    {
        //        Name = "R01_Prg101",
        //        Address = "ns=3;s=\"90_HMI_DB\".\"R01_Prg101\".\"HAnf\"",
        //        DescriptionEn="Robot prg 101",
        //        DescriptionDe="Robot prg 101",
        //        DescriptionLocal="Robot prg 101",
        //        Device=""
        //    },
        //    new Signal
        //    {
        //        Name = "R01_Ohne_Production",
        //        Address = "ns=3;s=\"90_HMI_DB\".\"R01_Ohne_Production\".\"HAnf\"",
        //        DescriptionEn="Without production",
        //        DescriptionDe="Without production",
        //        DescriptionLocal="Without production",
        //        Device=""
        //    },
        //    new Signal
        //    {
        //        Name = "R01_Ohne_Shooting",
        //        Address = "ns=3;s=\"90_HMI_DB\".\"R01_Ohne_Shooting\".\"HAnf\"",
        //        DescriptionEn="Without shooting",
        //        DescriptionDe="Without shooting",
        //        DescriptionLocal="Without shooting",
        //        Device=""
        //    },
        //    new Signal
        //    {
        //        Name = "R01_PauseProduction",
        //        Address = "ns=3;s=\"90_HMI_DB\".\"R01_PauseProduction\".\"HAnf\"",
        //        DescriptionEn="Robot pause production",
        //        DescriptionDe="Robot pause production",
        //        DescriptionLocal="Robot pause production",
        //        Device=""
        //    },
        //    new Signal
        //    {
        //        Name = "R01_Abort",
        //        Address = "ns=3;s=\"90_HMI_DB\".\"R01_Abort\".\"HAnf\"",
        //        DescriptionEn="Robot abort work",
        //        DescriptionDe="Robot abort work",
        //        DescriptionLocal="Robot abort work",
        //        Device=""
        //    }

        //};
        _db.Signals.AddRange(_Signals);
        _db.SaveChanges();

    }

    private void InitializeAlarms()
    {
        _Logger.LogInformation("Alarms initialize ... ");
        List<Alarm> _Alarms = new();
        foreach (var obj in JsonInitObject["Alarms"].ToList())
        {
            _Alarms.Add(new Alarm
            {
                NR = (int)obj["NR"],
                Device = (string)obj["Device"],
                Inverted = (bool)obj["Inverted"],
                Priority = (string)obj["Priority"],
                StopType = (string)obj["StopType"],
                DescriptionEn = (string)obj["DescriptionEn"],
                DescriptionDe = (string)obj["DescriptionDe"],
                DescriptionLocal = (string)obj["DescriptionLocal"],
                Name = (string)obj["Name"],
                Address = (string)obj["Address"]
            });
        }
        //List<Alarm> _Alarms = new()
        //{
        //    new Alarm
        //    {
        //        NR = 1,
        //        Device = "Station",
        //        Inverted = false,
        //        Priority = "S",
        //        StopType = "T",
        //        DescriptionEn = "Not Automatic Mode",
        //        DescriptionDe = "Ohne Automatik",
        //        DescriptionLocal = "Není v automatickém režimu",
        //        Name = "N_auto",
        //        Address = "ns=3;s=\"Stoe_DB\".\"N_auto\""
        //    },
        //    new Alarm
        //    {
        //        NR = 2,
        //        Device = "Station",
        //        Inverted = false,
        //        Priority = "S",
        //        StopType = "O",
        //        DescriptionEn = "Stop",
        //        DescriptionDe = "Halt",
        //        DescriptionLocal = "Stop",
        //        Name = "Halt",
        //        Address = "ns=3;s=\"Stoe_DB\".\"Halt\""
        //    },
        //    new Alarm
        //    {
        //        NR = 3,
        //        Device = "Station",
        //        Inverted = false,
        //        Priority = "S",
        //        StopType = "O",
        //        DescriptionEn = "Emergency Stop",
        //        DescriptionDe = "Not-Aus",
        //        DescriptionLocal = "Nouzový stop",
        //        Name = "EmStop",
        //        Address = "ns=3;s=\"Stoe_DB\".\"EmStop\""
        //    },
        //    new Alarm
        //    {
        //        NR = 4,
        //        Device = "Robot",
        //        Inverted = false,
        //        Priority = "S",
        //        StopType = "T",
        //        DescriptionEn = "Robot - System Fault",
        //        DescriptionDe = "Roboter - Systemfehler",
        //        DescriptionLocal = "Robot - systémová porucha",
        //        Name = "R_systemFault",
        //        Address = "ns=3;s=\"Stoe_DB\".\"R_sytemFault\""
        //    },
        //    new Alarm
        //    {
        //        NR = 5,
        //        Device = "Robot",
        //        Inverted = false,
        //        Priority = "S",
        //        StopType = "O",
        //        DescriptionEn = "Robot - Emergency Stop",
        //        DescriptionDe = "Roboter - Not - Aus",
        //        DescriptionLocal = "Robot - Nouzový stop",
        //        Name = "R_EmStop",
        //        Address = "ns=3;s=\"Stoe_DB\".\"R_EmStop\""
        //    },
        //    new Alarm
        //    {
        //        NR = 6,
        //        Device = "Station",
        //        Inverted = false,
        //        Priority = "S",
        //        StopType = "T",
        //        DescriptionEn = "General Fault",
        //        DescriptionDe = "Gesamtfehler",
        //        DescriptionLocal = "Celková porucha",
        //        Name = "Gesamtfehler",
        //        Address = "ns=3;s=\"Stoe_DB\".\"Gesamtfehler\""
        //    },
        //    new Alarm
        //    {
        //        NR = 7,
        //        Device = "Station",
        //        Inverted = false,
        //        Priority = "S",
        //        StopType = "T",
        //        DescriptionEn = "General BS",
        //        DescriptionDe = "BS-Gesamt",
        //        DescriptionLocal = "Celková BS",
        //        Name = "BS_gesamt",
        //        Address = "ns=3;s=\"Stoe_DB\".\"BS_gesamt\""
        //    },
        //    new Alarm
        //    {
        //        NR = 8,
        //        Device = "Robot",
        //        Inverted = false,
        //        Priority = "S",
        //        StopType = "T",
        //        DescriptionEn = "Robot - Not Auto",
        //        DescriptionDe = "Roboter -Ohne Automatik",
        //        DescriptionLocal = "Robot -Není v automatice",
        //        Name = "N_R_Auto",
        //        Address = "ns=3;s=\"Stoe_DB\".\"N_R_Auto\""
        //    },
        //    new Alarm
        //    {
        //        NR = 9,
        //        Device = "Station",
        //        Inverted = false,
        //        Priority = "Z",
        //        StopType = "-",
        //        DescriptionEn = "Motor Off",
        //        DescriptionDe = "Motor aus",
        //        DescriptionLocal = "Motor vypnut",
        //        Name = "MotOffState",
        //        Address = "ns=3;s=\"Stoe_DB\".\"MotOffState\""
        //    },
        //    new Alarm
        //    {
        //        NR = 10,
        //        Device = "Station",
        //        Inverted = false,
        //        Priority = "S",
        //        StopType = "T",
        //        DescriptionEn = "Motor Not On",
        //        DescriptionDe = "Motor nicht ein",
        //        DescriptionLocal = "Motor neběží",
        //        Name = "N_MotOnState",
        //        Address = "ns=3;s=\"Stoe_DB\".\"N_MotOnState\""
        //    },
        //    new Alarm
        //    {
        //        NR = 11,
        //        Device = "Station",
        //        Inverted = false,
        //        Priority = "M",
        //        StopType = "-",
        //        DescriptionEn = "PC Not Connected",
        //        DescriptionDe = "PC nicht angebunden",
        //        DescriptionLocal = "PC nepřipojeno",
        //        Name = "PC_not_connected",
        //        Address = "ns=3;s=\"Stoe_DB\".\"PC_not_connected\""
        //    },
        //    new Alarm
        //    {
        //        NR = 12,
        //        Device = "Station",
        //        Inverted = false,
        //        Priority = "S",
        //        StopType = "O",
        //        DescriptionEn = "Emergency Stop",
        //        DescriptionDe = "Not - Aus",
        //        DescriptionLocal = "Nouzový stop",
        //        Name = "Not_halt",
        //        Address = "ns=3;s=\"Stoe_DB\".\"Not_halt\""
        //    }
        //};
        _db.Alarms.AddRange(_Alarms);
        _db.SaveChanges();

    }

    #endregion Initialize OPC
}