using Pallet.Database.Context;
using Pallet.Database.Entities.Log;
using Pallet.Database.Entities.OPC;
using Pallet.Database.Entities.ProfileData.Products;
using Pallet.Database.Entities.ProfileData.Profiles;
using Pallet.Database.Entities.ProfileData.Tables;
using Pallet.Database.Entities.ProfileData.Types;
using Pallet.Database.Entities.Users;

namespace Pallet.Data;

internal class DbInitializer
{
    private readonly DatabaseDB _db;
    private readonly ILogger<DbInitializer> _Logger;

    public DbInitializer(DatabaseDB db, ILogger<DbInitializer> Logger)
    {
        _db = db;
        _Logger = Logger;
    }

    public async Task InitializeAsync()
    {
        "Database Initialize".CheckStage();
        var timer = Stopwatch.StartNew();
        _Logger.LogInformation("Инициализация БД...");

        //_Logger.LogInformation("Удаление существующей БД...");
        //await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
        //_Logger.LogInformation("Удаление существующей БД выполнено за {0} мс", timer.ElapsedMilliseconds);

        //_db.Database.EnsureCreated();

        _Logger.LogInformation("Миграция БД...");

        "before migration".CheckStage();
        _db.Database.MigrateAsync().Wait();
        "Miration complete".CheckStage();

        _Logger.LogInformation("Миграция БД выполнена за {0} мс", timer.ElapsedMilliseconds);

        "Start db init data".CheckStage();

        if (!_db.SystemEvents.AsQueryable().Any())
            await InitializeSystemEvents().ConfigureAwait(false);

        "InitializeSystemEvents init complete".CheckStage();

        #region Init OPC

        if (!_db.Signals.AsQueryable().Any())
            await InitializeSignals().ConfigureAwait(false);

        "InitializeSignals init complete".CheckStage();
        if (!_db.Alarms.AsQueryable().Any())
            await InitializeAlarms().ConfigureAwait(false);

        "InitializeAlarms init complete".CheckStage();

        #endregion Init OPC

        #region Init Profiles

        if (!_db.Tables.AsQueryable().Any())
            await InitializeTables().ConfigureAwait(false);

        "InitializeTables init complete".CheckStage();
        if (!_db.Elements.AsQueryable().Any())
            await InitializeElements().ConfigureAwait(false);
        "InitializeElements init complete".CheckStage();

        if (!_db.Products.AsQueryable().Any())
            await InitializeProducts().ConfigureAwait(false);
        "InitializeProducts init complete".CheckStage();

        if (!_db.Nailers.AsQueryable().Any())
            await InitializeNailers().ConfigureAwait(false);
        "InitializeNailers init complete".CheckStage();

        if (!_db.Profiles.AsQueryable().Any())
            await InitializeProfiles().ConfigureAwait(false);
        "InitializeProfiles init complete".CheckStage();

        if (!_db.ElementPositions.AsQueryable().Any())
            await InitializeElementPositions().ConfigureAwait(false);
        "InitializeElementPositions init complete".CheckStage();

        if (!_db.Nails.AsQueryable().Any())
            await InitializeNails().ConfigureAwait(false);
        "InitializeNails init complete".CheckStage();

        if (!_db.ProfileProducts.AsQueryable().Any())
            await InitializeProfilesProducts().ConfigureAwait(false);
        "InitializeProfilesProducts init complete".CheckStage();

        #endregion Init Profiles

        #region Init System

        if (!_db.Users.AsQueryable().Any())
            await InitializeUsers().ConfigureAwait(false);
        "InitializeUsers init complete".CheckStage();

        #endregion Init System

        _Logger.LogInformation("Инициализация БД выполнена за {0} с", timer.Elapsed.TotalSeconds);

        "DB init complete".CheckStage();
    }

    #region Initialize Profile

    private async Task InitializeTables()
    {
        _Logger.LogInformation("Tables initialize ... ");
        List<Table> _Tables = new()
        {
            new Table
            {
                Name="INIT_ZERO",
                //PlaceA1Cofiguration="EMPTY",
                //PlaceA2Cofiguration="EMPTY",
                //PlaceB1Cofiguration="EMPTY",
                //PlaceB2Cofiguration="EMPTY",
                Enabled=true,
                PlaceA1OffsetX = 0,
                PlaceA1OffsetY =0,
                PlaceA2OffsetX =0,
                PlaceA2OffsetY =0,
                PlaceB1OffsetX =0,
                PlaceB1OffsetY =0,
                PlaceB2OffsetX =0,
                PlaceB2OffsetY =0,
                SideASizeX = 0,
                SideASizeY =0,
                SideBSizeX =0,
                SideBSizeY = 0,
                WorkAreaASizeX= 0,
                WorkAreaASizeY=0,
                WorkAreaBSizeX=0,
                WorkAreaBSizeY=0,
                WorkAreaAOffsetX= 0,
                WorkAreaAOffsetY= 0,
                WorkAreaBOffsetX= 0,
                WorkAreaBOffsetY= 0
            },
            new Table
            {
                Name="DEFAULT",
                //PlaceA1Cofiguration="PROD1",
                //PlaceA2Cofiguration="PREP1",
                //PlaceB1Cofiguration="PROD1",
                //PlaceB2Cofiguration="PREP1",
                Enabled=true,
                PlaceA1OffsetX = 0,
                PlaceA1OffsetY =0,
                PlaceA2OffsetX =2000,
                PlaceA2OffsetY =0,
                PlaceB1OffsetX =0,
                PlaceB1OffsetY =0,
                PlaceB2OffsetX =2000,
                PlaceB2OffsetY =0,
                SideASizeX = 3180,
                SideASizeY =1630,
                SideBSizeX =3180,
                SideBSizeY = 1630,
                WorkAreaASizeX= 3080,
                WorkAreaASizeY=1530,
                WorkAreaBSizeX=3080,
                WorkAreaBSizeY=1530,
                WorkAreaAOffsetX= 50,
                WorkAreaAOffsetY= 50,
                WorkAreaBOffsetX= 50,
                WorkAreaBOffsetY= 50
            },
            new Table
            {
                Name="EPAL_STD_1200X800",
                //PlaceA1Cofiguration="PROD1",
                //PlaceA2Cofiguration="PREP1",
                //PlaceB1Cofiguration="PROD1",
                //PlaceB2Cofiguration="PREP1",
                Enabled=true,
                PlaceA1OffsetX =0,
                PlaceA1OffsetY =0,
                PlaceA2OffsetX =2000,
                PlaceA2OffsetY =0,
                PlaceB1OffsetX =0,
                PlaceB1OffsetY =0,
                PlaceB2OffsetX =2000,
                PlaceB2OffsetY =0,
                SideASizeX = 3180,
                SideASizeY =1630,
                SideBSizeX =3180,
                SideBSizeY = 1630,
                WorkAreaASizeX= 3080,
                WorkAreaASizeY=1530,
                WorkAreaBSizeX=3080,
                WorkAreaBSizeY=1530,
                WorkAreaAOffsetX= 50,
                WorkAreaAOffsetY= 50,
                WorkAreaBOffsetX= 50,
                WorkAreaBOffsetY= 50
            },
            new Table
            {
                Name="CRATE_SEITE_1",
                //PlaceA1Cofiguration="PROD1",
                //PlaceA2Cofiguration="EMPTY",
                //PlaceB1Cofiguration="EMPTY",
                //PlaceB2Cofiguration="EMPTY",
                Enabled=false,
                PlaceA1OffsetX =0,
                PlaceA1OffsetY =0,
                PlaceA2OffsetX =0,
                PlaceA2OffsetY =0,
                PlaceB1OffsetX =0,
                PlaceB1OffsetY =0,
                PlaceB2OffsetX =0,
                PlaceB2OffsetY =0,
                SideASizeX = 3012,
                SideASizeY =1560,
                SideBSizeX =0,
                SideBSizeY = 0,
                WorkAreaASizeX= 2922,
                WorkAreaASizeY=1462,
                WorkAreaBSizeX=0,
                WorkAreaBSizeY=0,
                WorkAreaAOffsetX= 45,
                WorkAreaAOffsetY= 78,
                WorkAreaBOffsetX= 0,
                WorkAreaBOffsetY= 0
            },
            new Table
            {
                Name="CRATE_KOPF_2",
                //PlaceA1Cofiguration="PROD1",
                //PlaceA2Cofiguration="EMPTY",
                //PlaceB1Cofiguration="EMPTY",
                //PlaceB2Cofiguration="EMPTY",
                Enabled=false,
                PlaceA1OffsetX =0,
                PlaceA1OffsetY =0,
                PlaceA2OffsetX =0,
                PlaceA2OffsetY =0,
                PlaceB1OffsetX =0,
                PlaceB1OffsetY =0,
                PlaceB2OffsetX =0,
                PlaceB2OffsetY =0,
                SideASizeX = 3012,
                SideASizeY =1560,
                SideBSizeX =0,
                SideBSizeY = 0,
                WorkAreaASizeX=297.2,
                WorkAreaASizeY=146.2,
                WorkAreaBSizeX=0,
                WorkAreaBSizeY=0,
                WorkAreaAOffsetX= 20,
                WorkAreaAOffsetY= 78,
                WorkAreaBOffsetX= 0,
                WorkAreaBOffsetY= 0
            },
            new Table
            {
                Name="PAL_6_ONLY",
                //PlaceA1Cofiguration="PROD1",
                //PlaceA2Cofiguration="PREP1",
                //PlaceB1Cofiguration="PROD1",
                //PlaceB2Cofiguration="PREP1",
                Enabled=true,
                PlaceA1OffsetX =0,
                PlaceA1OffsetY =0,
                PlaceA2OffsetX =2000,
                PlaceA2OffsetY =0,
                PlaceB1OffsetX =0,
                PlaceB1OffsetY =0,
                PlaceB2OffsetX =2000,
                PlaceB2OffsetY =0,
                SideASizeX = 3180,
                SideASizeY =1630,
                SideBSizeX =3180,
                SideBSizeY = 1630,
                WorkAreaASizeX= 3080,
                WorkAreaASizeY=1530,
                WorkAreaBSizeX=3080,
                WorkAreaBSizeY=1530,
                WorkAreaAOffsetX= 50,
                WorkAreaAOffsetY= 50,
                WorkAreaBOffsetX= 50,
                WorkAreaBOffsetY= 50
            },
            new Table
            {
                Name="Test1",
                //PlaceA1Cofiguration="PROD1",
                //PlaceA2Cofiguration="PREP1",
                //PlaceB1Cofiguration="PROD1",
                //PlaceB2Cofiguration="PREP1",
                Enabled=true,
                PlaceA1OffsetX =0,
                PlaceA1OffsetY =0,
                PlaceA2OffsetX =2000,
                PlaceA2OffsetY =0,
                PlaceB1OffsetX =0,
                PlaceB1OffsetY =0,
                PlaceB2OffsetX =2000,
                PlaceB2OffsetY =0,
                SideASizeX = 2000,
                SideASizeY =1000,
                SideBSizeX =0,
                SideBSizeY = 0,
                WorkAreaASizeX= 2000,
                WorkAreaASizeY=1000,
                WorkAreaBSizeX=0,
                WorkAreaBSizeY=0,
                WorkAreaAOffsetX=0,
                WorkAreaAOffsetY=0,
                WorkAreaBOffsetX=0,
                WorkAreaBOffsetY=0
            },
        };

        await _db.Tables.AddRangeAsync(_Tables);
        await _db.SaveChangesAsync();

        "InitializeTables db init data".CheckStage();
    }

    private async Task InitializeElements()
    {
        _Logger.LogInformation("Elements initialize ... ");
        List<Element> _Elements = new()
        {
            new Element
            {
                Name="BODENRAND",
                DescriptionEn="Bottom deck lead board",
                DescriptionDe="Bodenrandbrett",
                DescriptionLocal=" Krajní přířez opěrné podlahy",
                Count=2,
                SizeX=1200,
                SizeY=100,
                SizeZ=22,
                Direction=0,
            },
            new Element
            {
                Name="DECKRAND",
                DescriptionEn="Top deck lead board",
                DescriptionDe="Deckrandbrett",
                DescriptionLocal="Krajní přířez ložné podlahy",
                Count=2,
                SizeX=1200,
                SizeY=140,
                SizeZ=22,
                Direction=0,
            },
            new Element
            {
                Name="BODENMITTEL",
                DescriptionEn="Central bottom deck board",
                DescriptionDe="Bodenmittelbrett",
                DescriptionLocal="Střední přířez opěrné podlahy",
                Count=1,
                SizeX=1200,
                SizeY=145,
                SizeZ=22,
                Direction=0,
            },
            new Element
            {
                Name="QUERBRETT",
                DescriptionEn="Stringer board",
                DescriptionDe="Querbrett (Unterzug)",
                DescriptionLocal="Svlak",
                Count=3,
                SizeX=145,
                SizeY=800,
                SizeZ=22,
                Direction=1,
            },
            new Element
            {
                Name="DECKMITTEL",
                DescriptionEn="Central top deck board",
                DescriptionDe="Deckmittelbrett",
                DescriptionLocal="Střední přířez ložné podlahy",
                Count=1,
                SizeX=1200,
                SizeY=145,
                SizeZ=22,
                Direction=0,
            },
            new Element
            {
                Name="DECKINNEN",
                DescriptionEn="Intermediate top deck board",
                DescriptionDe="Deckinnenbrett",
                DescriptionLocal="Vnitřní přířez ložné podlahy",
                Count=2,
                SizeX=1200,
                SizeY=100,
                SizeZ=22,
                Direction=0,
            },
            new Element
            {
                Name="KLOTZRAND",
                DescriptionEn="Outer skid block",
                DescriptionDe="Klotz der äußeren Kufe",
                DescriptionLocal="Krajní špalík",
                Count=6,
                SizeX=145,
                SizeY=100,
                SizeZ=78,
                Direction=0,
            },
            new Element
            {
                Name="KLOTZMITTEL",
                DescriptionEn="Center skid block",
                DescriptionDe="Klotz der mittleren Kufe",
                DescriptionLocal="Střední špalík",
                Count=3,
                SizeX=145,
                SizeY=145,
                SizeZ=78,
                Direction=0,
            },
            new Element
            {
                Name="KLOTZRAND",
                DescriptionEn="Outer skid block",
                DescriptionDe="Klotz der äußeren Kufe",
                DescriptionLocal="Krajní špalík",
                Count=6,
                SizeX=100,
                SizeY=145,
                SizeZ=78,
                Direction=1,
            },
            new Element
            {
                Name="KLOTZMITTEL",
                DescriptionEn="Center skid block",
                DescriptionDe="Klotz der mittleren Kufe",
                DescriptionLocal="Střední špalík",
                Count=3,
                SizeX=145,
                SizeY=145,
                SizeZ=78,
                Direction=1,
            },
            new Element
            {
                Name="BODENRAND",
                DescriptionEn="Bottom deck lead board",
                DescriptionDe="Bodenrandbrett",
                DescriptionLocal="Krajní přířez opěrné podlahy",
                Count=2,
                SizeX=100,
                SizeY=1200,
                SizeZ=22,
                Direction=1,
            },
            new Element
            {
                Name="BODENMITTEL",
                DescriptionEn="Central bottom deck board",
                DescriptionDe="Bodenmittelbrett",
                DescriptionLocal="Střední přířez opěrné podlahy",
                Count=1,
                SizeX=145,
                SizeY=1200,
                SizeZ=22,
                Direction=1,
            },
            new Element
            {
                Name="DECK",
                DescriptionEn="Outer deck",
                DescriptionDe="Platte aussen",
                DescriptionLocal="Vnější deska",
                Count=1,
                SizeX=2100,
                SizeY=1240,
                SizeZ=10,
                Direction=0,
            },
            new Element
            {
                Name="VERTBRETT",
                DescriptionEn="Vertical board",
                DescriptionDe="Vertikal Brett",
                DescriptionLocal="Vertikální prkno",
                Count=4,
                SizeX=150,
                SizeY=990,
                SizeZ=10,
                Direction=1,
            },
            new Element
            {
                Name="HORBRETT",
                DescriptionEn="Horizontal board",
                DescriptionDe="Horizontal Brett",
                DescriptionLocal="Horizontální prkno",
                Count=2,
                SizeX=2100,
                SizeY=125,
                SizeZ=10,
                Direction=0,
            },
            new Element
            {
                Name="DECKRAND",
                DescriptionEn="Top deck lead board",
                DescriptionDe="Deckrandbrett",
                DescriptionLocal="Krajní přířez ložné podlahy",
                Count=2,
                SizeX=125,
                SizeY=1645,
                SizeZ=36,
                Direction=1,
            },
            new Element
            {
                Name="DECKINNEN",
                DescriptionEn="Intermediate top deck board",
                DescriptionDe="Deckinnenbrett",
                DescriptionLocal="Vnitřní přířez ložné podlahy",
                Count=5,
                SizeX=125,
                SizeY=1645,
                SizeZ=36,
                Direction=1,
            },
            new Element
            {
                Name="HOLZDECKE",
                DescriptionEn="Test element",
                DescriptionDe="Test element ",
                DescriptionLocal="Test element ",
                Count=0,
                SizeX=1000,
                SizeY=100,
                SizeZ=18,
                Direction=0,
            },
            new Element
            {
                Name="UNTERDECKE",
                DescriptionEn="Untere Decke",
                DescriptionDe="Untere Decke",
                DescriptionLocal="Untere Decke",
                Count=0,
                SizeX=50,
                SizeY=50,
                SizeZ=100,
                Direction=2,
            },
            new Element
            {
                Name="NULL",
                DescriptionEn="null",
                DescriptionDe="null",
                DescriptionLocal="null",
                Count=0,
                SizeX=0,
                SizeY=0,
                SizeZ=0,
                Direction=2,
            },
        };
        await _db.Elements.AddRangeAsync(_Elements);
        await _db.SaveChangesAsync();

        "InitializeElements db init data".CheckStage();
    }

    private async Task InitializeProducts()
    {
        _Logger.LogInformation("Products initialize ... ");
        List<Product> _Products = new()
        {
            new Product
            {
                Name="EPAL_STD_1200X800",
                DescriptionEn="Standard EUR-pallet 1200x800x144 mm.",
                DescriptionDe="Standard EUR-Pallete 1200x800x144 mm.",
                DescriptionLocal="Standardní EUR paleta 1200x800x144 mm.",
                //Step1=true,
                //Step2=true,
                Size1X=1200,
                Size1Y=800,
                Size1Z=144,
                Size2X=365,
                Size2Y=1200,
                Size2Z=100,
                Preset=true,
                Type=0,
                Prod=0
            },
            new Product
            {
                Name="Legs_EPAL_STD",
                DescriptionEn="Legs Standard EUR-pallet 1200x800x144 mm.",
                DescriptionDe="Legs Standard EUR-Pallete 1200x800x144 mm.",
                DescriptionLocal="Legs Standardní EUR paleta 1200x800x144 mm.",
                //Step1=true,
                //Step2=true,
                Size1X=1200,
                Size1Y=800,
                Size1Z=144,
                Size2X=365,
                Size2Y=1200,
                Size2Z=100,
                Preset=true,
                Type=0,
                Prod=0
            },
            new Product
            {
                Name="CRATE_SEITE_1",
                DescriptionEn="Crate Robot 'Seite 1'",
                DescriptionDe="Crate Robot 'Seite 1'",
                DescriptionLocal="Crate Robot 'Seite 1'",
                //Step1=true,
                //Step2=false,
                Size1X=1,
                Size1Y=1,
                Size1Z=1,
                Size2X=1,
                Size2Y=1,
                Size2Z=1,
                Preset=true,
                Type=0,
                Prod=0
            },
            new Product
            {
                Name="CRATE_KOPF_2",
                DescriptionEn="Crate Robot 'Kopf 2'",
                DescriptionDe="Crate Robot 'Kopf 2'",
                DescriptionLocal="Crate Robot 'Kopf 2'",
                //Step1=true,
                //Step2=false,
                Size1X=1,
                Size1Y=1,
                Size1Z=1,
                Size2X=1,
                Size2Y=1,
                Size2Z=1,
                Preset=true,
                Type=0,
                Prod=0
            },
            new Product
            {
                Name="PAL_NR_6",
                DescriptionEn="Pallet No. 6",
                DescriptionDe="Pallete Nr. 6",
                DescriptionLocal="Paleta č. 6",
                //Step1=true,
                //Step2=true,
                Size1X=1645,
                Size1Y=1645,
                Size1Z=136,
                Size2X=1645,
                Size2Y=1645,
                Size2Z=172,
                Preset=false,
                Type=0,
                Prod=0
            },
        };
        await _db.Products.AddRangeAsync(_Products);
        await _db.SaveChangesAsync();

        "InitializeProducts db init data".CheckStage();
    }

    private async Task InitializeNailers()
    {
        _Logger.LogInformation("Nailers initialize ... ");
        List<Nailer> _Nailers = new()
        {
            new Nailer
            {
                InternalID=1,
                Name="N_3x60",
                DescriptionEn="Nailer 3x60",
                DescriptionDe="Nagelmaschine 3x60",
                DescriptionLocal="Hřebíkovačka 3x60",
                Dock=1,
                Lenght=60,
                Width=3,
                Size=22,
                Color=4294956800,
                NailSize=10
            },
            new Nailer
            {
                InternalID=2,
                Name="N_4x150",
                DescriptionEn="Nailer 4x150",
                DescriptionDe="Nagelmaschine 4x150",
                DescriptionLocal="Hřebíkovačka 4x150",
                Dock=2,
                Lenght=150,
                Width=4,
                Size=28,
                Color=4278255615,
                NailSize=20
            },
            new Nailer
            {
                InternalID=3,
                Name="N_5x200",
                DescriptionEn="Nailer 5x200",
                DescriptionDe="Nagelmaschine 5x200",
                DescriptionLocal="Hřebíkovačka 5x200",
                Dock=3,
                Lenght=200,
                Width=5,
                Size=32,
                Color=4256842341,
                NailSize=30
            },
            new Nailer
            {
                InternalID=4,
                Name="N_3x60",
                DescriptionEn="Nailer 3x60",
                DescriptionDe="Nagelmaschine 3x60",
                DescriptionLocal="Hřebíkovačka 3x60",
                Dock=4,
                Lenght=60,
                Width=3,
                Size=22,
                Color=4294956800,
                NailSize=10
            },
            new Nailer
            {
                InternalID=5,
                Name="N_4x150",
                DescriptionEn="Nailer 4x150",
                DescriptionDe="Nagelmaschine 4x150",
                DescriptionLocal="Hřebíkovačka 4x150",
                Dock=5,
                Lenght=150,
                Width=4,
                Size=28,
                Color=4278255615,
                NailSize=20
            },
            new Nailer
            {
                InternalID=6,
                Name="N_5x200",
                DescriptionEn="Nailer 5x200",
                DescriptionDe="Nagelmaschine 5x200",
                DescriptionLocal="Hřebíkovačka 5x200",
                Dock=6,
                Lenght=200,
                Width=5,
                Size=32,
                Color=4256842341,
                NailSize=30
            },
        };
        try
        {
            await _db.Nailers.AddRangeAsync(_Nailers);
            await _db.SaveChangesAsync();
        }
        catch (Exception e) { e.ExceptionToString(); }

        "InitializeNailers db init data".CheckStage();
    }

    private async Task InitializeProfiles()
    {
        _Logger.LogInformation("Profiles initialize ... ");
        List<Profile> _Profiles = new()
        {
            new Profile
            {
                Name= "Crate Robot 'Kopf 2'",
                DescriptionEn = "Crate Robot 'Kopf 2'",
                DescriptionDe = "Profil pro Crate Robot 'Kopf 2'",
                DescriptionLocal = "Profil pro Crate Robot 'Kopf 2'",
                DateCreate=DateTime.Parse("2022-04-01 14:09:00.000"),
                DateLastModified=DateTime.Parse("2022 - 04 - 01 14:09:00.0000000"),
                DateLastUse=DateTime.Parse("2022 - 06 - 06 10:18:00.4399684"),
                Author="Ing.Pavel Štekl, BenThor",
                Table= _db.Tables.First(t=>t.Name == "CRATE_KOPF_2")
            },
            new Profile
            {
                Name = "EPAL_STD_1200X800",
                DescriptionEn = "Standard EUR-pallet 1200x800x144 mm.",
                DescriptionDe = "Standard EUR-Pallete 1200x800x144 mm.",
                DescriptionLocal = "Standardní EUR paleta 1200x800x144 mm.",
                DateCreate =DateTime.Parse("2022-04-01 14:06:00.0000000"),
                DateLastModified=DateTime.Parse("2022 - 04 - 01 14:06:00.0000000"),
                DateLastUse=DateTime.Parse("2022 - 06 - 08 15:08:42.2669720"),
                Author="Ing.Pavel Štekl, BenThor",
                Table= _db.Tables.First(t=>t.Name == "EPAL_STD_1200X800")
            },
            new Profile
            {
                Name = "CRATE_SEITE_1",
                DescriptionEn = "Crate Robot 'Seite 1'",
                DescriptionDe = "Crate Robot 'Seite 1'",
                DescriptionLocal = "Crate Robot 'Seite 1'",
                DateCreate =DateTime.Parse("2022-04-01 14:08:00.0000000"),
                DateLastModified=DateTime.Parse("2022 - 04 - 01 14:08:00.0000000"),
                DateLastUse=DateTime.Parse("2022 - 06 - 06 14:46:07.7020755"),
                Author="Ing.Pavel Štekl, BenThor",
                Table= _db.Tables.First(t=>t.Name=="CRATE_SEITE_1")
            },
            new Profile
            {
                Name = "PAL_6_ONLY",
                DescriptionEn = "Paleta č. 6 na obou stranách",
                DescriptionDe = "Paleta č. 6 na obou stranách",
                DescriptionLocal = "Paleta č. 6 na obou stranách",
                DateCreate =DateTime.Parse("2022-04-14 15:44:30.0000000"),
                DateLastModified=DateTime.Parse("2022 - 04 - 14 15:44:30.0000000"),
                DateLastUse=DateTime.Parse("2022 - 05 - 31 16:40:40.7018054"),
                Author="Ing.Pavel Štekl, BenThor",
                Table= _db.Tables.First(t=>t.Name=="PAL_6_ONLY")
            },
        };
        foreach (var table in _db.Tables.ToList())
            table.Profiles = _Profiles.Where(p => p.Table.ID == table.ID).ToList();

        await _db.Profiles.AddRangeAsync(_Profiles);
        await _db.SaveChangesAsync();

        "InitializeProfiles db init data".CheckStage();
    }

    private async Task InitializeElementPositions()
    {
        _Logger.LogInformation("ElementPositions initialize ... ");
        List<ElementPosition> _ElementPositions = new()
        {
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==1),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=1,
                PosX=1,
                PosY=0,
                PosZ=0,
                Layer=1,
                OutLN=true,
                ////Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==1),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=2,
                PosX=0,
                PosY=700,
                PosZ=0,
                Layer=1,
                OutLN=true,
                ////Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==2),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=1,
                PosX=0,
                PosY=0,
                PosZ=122,
                Layer=4,
                OutLN=false,
                ////Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==2),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=2,
                PosX=0,
                PosY=655,
                PosZ=122,
                Layer=4,
                OutLN=false,
                ////Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==3),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=1,
                PosX=0,
                PosY=327.5,
                PosZ=0,
                Layer=1,
                OutLN=false,
                ////Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==4),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=1,
                PosX=0,
                PosY=0,
                PosZ=100,
                Layer=3,
                OutLN=false,
                ////Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==4),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=2,
                PosX=527.5,
                PosY=0,
                PosZ=100,
                Layer=3,
                OutLN=false,
                ////Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==4),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=3,
                PosX=1055,
                PosY=0,
                PosZ=100,
                Layer=3,
                OutLN=false,
                ////Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==5),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=1,
                PosX=0,
                PosY=327.5,
                PosZ=122,
                Layer=4,
                OutLN=false,
                ////Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==6),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=1,
                PosX=0,
                PosY=187.5,
                PosZ=122,
                Layer=4,
                OutLN=false,
                ////Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==6),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=2,
                PosX=0,
                PosY=512.5,
                PosZ=122,
                Layer=4,
                OutLN=false,
                ////Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==7),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=1,
                PosX=0,
                PosY=0,
                PosZ=22,
                Layer=2,
                OutLN=true,
                ////Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==7),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=2,
                PosX=527.5,
                PosY=0,
                PosZ=22,
                Layer=2,
                OutLN=true,
                ////Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==7),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=3,
                PosX=1055,
                PosY=0,
                PosZ=22,
                Layer=2,
                OutLN=true,
                ////Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==7),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=4,
                PosX=0,
                PosY=700,
                PosZ=22,
                Layer=2,
                OutLN=true,
                ////Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==7),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=5,
                PosX=527.5,
                PosY=700,
                PosZ=22,
                Layer=2,
                OutLN=true,
                ////Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==7),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=6,
                PosX=1055,
                PosY=700,
                PosZ=22,
                Layer=2,
                OutLN=true,
                //Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==8),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=1,
                PosX=0,
                PosY=327.5,
                PosZ=22,
                Layer=2,
                OutLN=true,
                //Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==8),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=2,
                PosX=527.5,
                PosY=327.5,
                PosZ=22,
                Layer=2,
                OutLN=true,
                //Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==8),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
                PosID=3,
                PosX=1055,
                PosY=327.5,
                PosZ=22,
                Layer=2,
                OutLN=true,
                //Side=1
            },

            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==9),
                Product=_db.Products.First(e=>e.Name=="Legs_EPAL_STD"),
                PosID=1,
                PosX=0,
                PosY=0,
                PosZ=0,
                Layer=1,
                OutLN=true,
                //Side=2
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==9),
                Product=_db.Products.First(e=>e.Name=="Legs_EPAL_STD"),
                PosID=2,
                PosX=0,
                PosY=527.5,
                PosZ=0,
                Layer=1,
                OutLN=true,
                //Side=2
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==9),
                Product=_db.Products.First(e=>e.Name=="Legs_EPAL_STD"),
                PosID=3,
                PosX=0,
                PosY=1055,
                PosZ=0,
                Layer=1,
                OutLN=true,
                //Side=2
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==9),
                Product=_db.Products.First(e=>e.Name=="Legs_EPAL_STD"),
                PosID=4,
                PosX=265,
                PosY=0,
                PosZ=0,
                Layer=1,
                OutLN=true,
                //Side=2
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==9),
                Product=_db.Products.First(e=>e.Name=="Legs_EPAL_STD"),
                PosID=5,
                PosX=265,
                PosY=527.5,
                PosZ=0,
                Layer=1,
                OutLN=true,
                //Side=2
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==9),
                Product=_db.Products.First(e=>e.Name=="Legs_EPAL_STD"),
                PosID=6,
                PosX=265,
                PosY=1055,
                PosZ=0,
                Layer=1,
                OutLN=true,
                //Side=2
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==10),
                Product=_db.Products.First(e=>e.Name=="Legs_EPAL_STD"),
                PosID=1,
                PosX=110,
                PosY=0,
                PosZ=0,
                Layer=1,
                OutLN=true,
                //Side=2
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==10),
                Product=_db.Products.First(e=>e.Name=="Legs_EPAL_STD"),
                PosID=2,
                PosX=110,
                PosY=527.5,
                PosZ=0,
                Layer=1,
                OutLN=true,
                //Side=2
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==10),
                Product=_db.Products.First(e=>e.Name=="Legs_EPAL_STD"),
                PosID=3,
                PosX=110,
                PosY=1055,
                PosZ=0,
                Layer=1,
                OutLN=true,
                //Side=2
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==11),
                Product=_db.Products.First(e=>e.Name=="Legs_EPAL_STD"),
                PosID=1,
                PosX=0,
                PosY=0,
                PosZ=78,
                Layer=2,
                OutLN=false,
                //Side=2
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==11),
                Product=_db.Products.First(e=>e.Name=="Legs_EPAL_STD"),
                PosID=2,
                PosX=265,
                PosY=0,
                PosZ=78,
                Layer=2,
                OutLN=false,
                //Side=2
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==12),
                Product=_db.Products.First(e=>e.Name=="Legs_EPAL_STD"),
                PosID=1,
                PosX=110,
                PosY=0,
                PosZ=78,
                Layer=2,
                OutLN=false,
                //Side=2
            },

            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==1),
                Product=_db.Products.First(e=>e.Name=="CRATE_SEITE_1"),
                PosID=1,
                PosX=0,
                PosY=0,
                PosZ=0,
                Layer=1,
                OutLN=true,
                //Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==2),
                Product=_db.Products.First(e=>e.Name=="CRATE_SEITE_1"),
                PosID=1,
                PosX=0,
                PosY=125,
                PosZ=0,
                Layer=4,
                OutLN=false,
                //Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==2),
                Product=_db.Products.First(e=>e.Name=="CRATE_SEITE_1"),
                PosID=2,
                PosX=650,
                PosY=125,
                PosZ=0,
                Layer=4,
                OutLN=false,
                //Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==2),
                Product=_db.Products.First(e=>e.Name=="CRATE_SEITE_1"),
                PosID=3,
                PosX=1300,
                PosY=125,
                PosZ=0,
                Layer=4,
                OutLN=false,
                //Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==3),
                Product=_db.Products.First(e=>e.Name=="CRATE_SEITE_1"),
                PosID=1,
                PosX=0,
                PosY=0,
                PosZ=0,
                Layer=1,
                OutLN=false,
                //Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==3),
                Product=_db.Products.First(e=>e.Name=="CRATE_SEITE_1"),
                PosID=2,
                PosX=0,
                PosY=1115,
                PosZ=0,
                Layer=1,
                OutLN=false,
                //Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==2),
                Product=_db.Products.First(e=>e.Name=="CRATE_SEITE_1"),
                PosID=4,
                PosX=1950,
                PosY=125,
                PosZ=0,
                Layer=4,
                OutLN=false,
                //Side=1
            },

            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==16),
                Product=_db.Products.First(e=>e.Name=="PAL_NR_6"),
                PosID=1,
                PosX=0,
                PosY=0,
                PosZ=0,
                Layer=1,
                OutLN=false,
                //Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==16),
                Product=_db.Products.First(e=>e.Name=="PAL_NR_6"),
                PosID=2,
                PosX=0,
                PosY=1520,
                PosZ=156,
                Layer=1,
                OutLN=false,
                //Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==17),
                Product=_db.Products.First(e=>e.Name=="PAL_NR_6"),
                PosID=1,
                PosX=0,
                PosY=205,
                PosZ=0,
                Layer=2,
                OutLN=false,
                //Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==17),
                Product=_db.Products.First(e=>e.Name=="PAL_NR_6"),
                PosID=2,
                PosX=0,
                PosY=482.5,
                PosZ=0,
                Layer=2,
                OutLN=false,
                //Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==17),
                Product=_db.Products.First(e=>e.Name=="PAL_NR_6"),
                PosID=3,
                PosX=0,
                PosY=760,
                PosZ=0,
                Layer=2,
                OutLN=false,
                //Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==17),
                Product=_db.Products.First(e=>e.Name=="PAL_NR_6"),
                PosID=4,
                PosX=0,
                PosY=1037.5,
                PosZ=0,
                Layer=2,
                OutLN=false,
                //Side=1
            },
            new ElementPosition
            {
                Element=_db.Elements.First(e=>e.ID==17),
                Product=_db.Products.First(e=>e.Name=="PAL_NR_6"),
                PosID=5,
                PosX=0,
                PosY=1315,
                PosZ=0,
                Layer=2,
                OutLN=false,
                //Side=1
            },
        };
        foreach (var element in _db.Elements.ToList())
            element.Positions = _ElementPositions.Where(s => s.Element.ID == element.ID).ToList();
        foreach (var product in _db.Products.ToList())
            product.Elements = _ElementPositions.Where(s => s.Product.ID == product.ID).ToList();

        await _db.ElementPositions.AddRangeAsync(_ElementPositions);
        await _db.SaveChangesAsync();

        "InitializeElementPositions db init data".CheckStage();
    }

    private async Task InitializeNails()
    {
        _Logger.LogInformation("Profiles initialize ... ");
        List<Nail> _Nails = new()
        {
            new Nail
            {
                NailID=1,
                PosX=80,
                PosY=400,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=2,
                PosX=80,
                PosY=250,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=3,
                PosX=80,
                PosY=640,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=4,
                PosX=80,
                PosY=760,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=5,
                PosX=80,
                PosY=880,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=6,
                PosX=80,
                PosY=1000,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=7,
                PosX=20,
                PosY=940,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=8,
                PosX=20,
                PosY=820,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=9,
                PosX=20,
                PosY=700,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=10,
                PosX=20,
                PosY=580,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=11,
                PosX=20,
                PosY=460,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=12,
                PosX=20,
                PosY=340,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=13,
                PosX=20,
                PosY=220,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=14,
                PosX=730,
                PosY=160,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=15,
                PosX=730,
                PosY=280,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=16,
                PosX=730,
                PosY=400,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=17,
                PosX=730,
                PosY=520,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=18,
                PosX=730,
                PosY=640,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=19,
                PosX=730,
                PosY=760,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=20,
                PosX=730,
                PosY=880,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=21,
                PosX=730,
                PosY=1000,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=22,
                PosX=670,
                PosY=940,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=23,
                PosX=670,
                PosY=820,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=24,
                PosX=670,
                PosY=700,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=25,
                PosX=670,
                PosY=580,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=26,
                PosX=670,
                PosY=460,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=27,
                PosX=670,
                PosY=340,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=28,
                PosX=670,
                PosY=220,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=29,
                PosX=1380,
                PosY=160,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=30,
                PosX=1380,
                PosY=280,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=31,
                PosX=1380,
                PosY=400,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=32,
                PosX=1380,
                PosY=520,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=33,
                PosX=1380,
                PosY=640,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=34,
                PosX=1380,
                PosY=760,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=35,
                PosX=1380,
                PosY=880,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=36,
                PosX=1380,
                PosY=1000,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=37,
                PosX=1320,
                PosY=940,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=38,
                PosX=1320,
                PosY=820,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=39,
                PosX=1320,
                PosY=700,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=40,
                PosX=1320,
                PosY=580,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=41,
                PosX=1320,
                PosY=460,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=42,
                PosX=1320,
                PosY=340,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=43,
                PosX=1320,
                PosY=220,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=44,
                PosX=2030,
                PosY=160,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=45,
                PosX=2030,
                PosY=280,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=46,
                PosX=2030,
                PosY=400,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=47,
                PosX=2030,
                PosY=640,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=48,
                PosX=2030,
                PosY=760,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=49,
                PosX=2030,
                PosY=880,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=50,
                PosX=2030,
                PosY=1000,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=51,
                PosX=1970,
                PosY=940,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=52,
                PosX=1970,
                PosY=820,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=53,
                PosX=1970,
                PosY=700,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=54,
                PosX=1970,
                PosY=580,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=55,
                PosX=1970,
                PosY=460,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=56,
                PosX=1970,
                PosY=340,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=57,
                PosX=1970,
                PosY=220,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=58,
                PosX=1930,
                PosY=1200,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=59,
                PosX=1810,
                PosY=1200,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=60,
                PosX=1690,
                PosY=1200,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=61,
                PosX=1570,
                PosY=1200,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=62,
                PosX=1450,
                PosY=1200,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=63,
                PosX=1330,
                PosY=1200,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=64,
                PosX=1210,
                PosY=1200,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=65,
                PosX=1090,
                PosY=1200,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=66,
                PosX=970,
                PosY=1200,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=67,
                PosX=850,
                PosY=1200,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=68,
                PosX=730,
                PosY=1200,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=69,
                PosX=610,
                PosY=1200,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=70,
                PosX=490,
                PosY=1200,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=71,
                PosX=370,
                PosY=1200,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=72,
                PosX=250,
                PosY=1200,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=73,
                PosX=130,
                PosY=1200,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=74,
                PosX=60,
                PosY=1200,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=75,
                PosX=1990,
                PosY=1140,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=76,
                PosX=1870,
                PosY=1140,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=77,
                PosX=1750,
                PosY=1140,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=78,
                PosX=1630,
                PosY=1140,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=79,
                PosX=1510,
                PosY=1140,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=80,
                PosX=1390,
                PosY=1140,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=81,
                PosX=1270,
                PosY=1140,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=82,
                PosX=1150,
                PosY=1140,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=83,
                PosX=1030,
                PosY=1140,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=84,
                PosX=910,
                PosY=1140,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=85,
                PosX=790,
                PosY=1140,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=86,
                PosX=670,
                PosY=1140,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=87,
                PosX=550,
                PosY=1140,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=88,
                PosX=430,
                PosY=1140,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=89,
                PosX=310,
                PosY=1140,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=90,
                PosX=190,
                PosY=1140,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
            new Nail
            {
                NailID=91,
                PosX=70,
                PosY=1140,
                PosZ=36,
                Angle1=0,
                Angle2=0,
                NailFix=false,
                MoveNextType=0,
                Alt=0,
                Mode=0,
                Nailer=_db.Nailers.First(e=>e.Name=="N_4x150"),
                Product=_db.Products.First(e=>e.Name=="EPAL_STD_1200X800"),
            },
        };
        foreach (var nailer in _db.Nailers.ToList())
            nailer.Nails = _db.Nails.Where(s => s.Nailer.ID == nailer.ID).ToList();
        foreach (var product in _db.Products.ToList())
            product.Nails = _db.Nails.Where(s => s.Product.ID == product.ID).ToList();

        await _db.Nails.AddRangeAsync(_Nails);
        await _db.SaveChangesAsync();

        "InitializeNails db init data".CheckStage();
    }

    private async Task InitializeProfilesProducts()
    {
        _Logger.LogInformation("ProfilesProducts initialize ... ");
        List<ProfileProducts> _ProfileProducts = new()
        {
            new ProfileProducts
            {
                Position =1,
                ProfileId =_db.Profiles.First(p=>p.Name=="Crate Robot 'Kopf 2'").ID,
                Profile = _db.Profiles.First(p=>p.Name=="Crate Robot 'Kopf 2'"),
                ProductId = _db.Products.First(p=>p.Name=="EPAL_STD_1200X800").ID,
                Product = _db.Products.First(p=>p.Name=="EPAL_STD_1200X800")
            },
            new ProfileProducts
            {
                Position = 2,
                ProfileId =_db.Profiles.First(p=>p.Name=="Crate Robot 'Kopf 2'").ID,
                Profile = _db.Profiles.First(p=>p.Name=="Crate Robot 'Kopf 2'"),
                ProductId = _db.Products.First(p=>p.Name=="EPAL_STD_1200X800").ID,
                Product = _db.Products.First(p=>p.Name=="EPAL_STD_1200X800")
            },
            new ProfileProducts
            {
                Position = 3,
                ProfileId =_db.Profiles.First(p=>p.Name=="Crate Robot 'Kopf 2'").ID,
                Profile = _db.Profiles.First(p=>p.Name=="Crate Robot 'Kopf 2'"),
                ProductId = _db.Products.First(p=>p.Name=="EPAL_STD_1200X800").ID,
                Product = _db.Products.First(p=>p.Name=="EPAL_STD_1200X800")
            },
            new ProfileProducts
            {
                Position = 4,
                ProfileId =_db.Profiles.First(p=>p.Name=="Crate Robot 'Kopf 2'").ID,
                Profile = _db.Profiles.First(p=>p.Name=="Crate Robot 'Kopf 2'"),
                ProductId = _db.Products.First(p=>p.Name=="EPAL_STD_1200X800").ID,
                Product = _db.Products.First(p=>p.Name=="EPAL_STD_1200X800")
            },
            new ProfileProducts
            {
                Position =1,
                ProfileId =_db.Profiles.First(p=>p.Name=="EPAL_STD_1200X800").ID,
                Profile = _db.Profiles.First(p=>p.Name=="EPAL_STD_1200X800"),
                ProductId = _db.Products.First(p=>p.Name=="EPAL_STD_1200X800").ID,
                Product = _db.Products.First(p=>p.Name=="EPAL_STD_1200X800")
            },
            new ProfileProducts
            {
                Position = 2,
                ProfileId =_db.Profiles.First(p=>p.Name=="EPAL_STD_1200X800").ID,
                Profile = _db.Profiles.First(p=>p.Name=="EPAL_STD_1200X800"),
                ProductId = _db.Products.First(p=>p.Name=="Legs_EPAL_STD").ID,
                Product = _db.Products.First(p=>p.Name=="Legs_EPAL_STD")
            },
            new ProfileProducts
            {
                Position = 3,
                ProfileId =_db.Profiles.First(p=>p.Name=="EPAL_STD_1200X800").ID,
                Profile = _db.Profiles.First(p=>p.Name=="EPAL_STD_1200X800"),
                ProductId = _db.Products.First(p=>p.Name=="EPAL_STD_1200X800").ID,
                Product = _db.Products.First(p=>p.Name=="EPAL_STD_1200X800")
            },
            new ProfileProducts
            {
                Position = 4,
                ProfileId =_db.Profiles.First(p=>p.Name=="EPAL_STD_1200X800").ID,
                Profile = _db.Profiles.First(p=>p.Name=="EPAL_STD_1200X800"),
                ProductId = _db.Products.First(p=>p.Name=="Legs_EPAL_STD").ID,
                Product = _db.Products.First(p=>p.Name=="Legs_EPAL_STD")
            },
        };
        foreach (var profile in _db.Profiles)
            profile.ProfileProducts = _ProfileProducts.Where(t => t.Profile.ID == profile.ID).ToList();
        foreach (var product in _db.Products)
            product.ProfileProducts = _ProfileProducts.Where(t => t.Product.ID == product.ID).ToList();

        await _db.ProfileProducts.AddRangeAsync(_ProfileProducts);
        await _db.SaveChangesAsync();

        "InitializeProfilesProducts db init data".CheckStage();
    }

    #endregion Initialize Profile

    #region Initialize System

    private async Task InitializeUsers()
    {
        _Logger.LogInformation("Users initialize ... ");
        List<User> _Users = new()
        {
            new User
            {
                Name = "jarda.papik",
                Description = "Jarda Papík",
                RoleNum = 1,
                Hashcode = "56B1DB8133D9EB398AABD376F07BF8AB5FC584EA0B8BD6A1770200CB613CA005"
            },
            new User
            {
                Name = "oleksii.klymov",
                Description = "Oleksii Klymov, BenThor",
                RoleNum = 10,
                Hashcode = "A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3"
            },
            new User
            {
                Name = "pavel.stekl",
                Description = "Ing. Pavel Štekl, BenThor",
                RoleNum = 2,
                Hashcode = "618D4F2B0BE8A5EE027C13B495E17C5504DE7DD296833C0D3B6D474DB1364A8B"
            }
        };
        await _db.Users.AddRangeAsync(_Users);
        await _db.SaveChangesAsync();

        "InitializeUsers db init data".CheckStage();
    }

    private async Task InitializeSystemEvents()
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
        await _db.SystemEvents.AddRangeAsync(_SystemEvent);
        await _db.SaveChangesAsync();

        "InitializeSystemEvents db init data".CheckStage();
    }

    #endregion Initialize System

    #region Initialize OPC

    private async Task InitializeSignals()
    {
        _Logger.LogInformation("Signals initialize ... ");
        List<Signal> _Signals = new()
        {
            new Signal
            {
                Name = "DatenAnforderung",
                Address = "ns=3;s=\"status\".\"PC_IN\".\"DatenAnforderung\"",
                DescriptionEn="Data request",
                DescriptionDe="DatenAnforderung",
                DescriptionLocal="Žádost o údaje",
                Device="PLC"
            },
            new Signal
            {
                Name = "JobFertig",
                Address = "ns=3;s=\"status\".\"PC_IN\".\"JobFertig\"",
                DescriptionEn="Job Done",
                DescriptionDe="Job Fertig",
                DescriptionLocal="Práce Dokončena",
                Device="PLC"
            },
            new Signal
            {
                Name = "AktuellDaten_niO",
                Address = "ns=3;s=\"status\".\"PC_IN\".\"AktuellDaten_niO\"",
                DescriptionEn="Data is actual",
                DescriptionDe="Daten sind aktuell",
                DescriptionLocal="Data jsou aktuální",
                Device="PLC"
            },
            new Signal
            {
                Name = "Stoerung",
                Address = "ns=3;s=\"status\".\"PC_IN\".\"Stoerung\"",
                DescriptionEn="Failure",
                DescriptionDe="Stoerung",
                DescriptionLocal="porucha",
                Device="PLC"
            },

            new Signal
            {
                Name = "DatenBereit",
                Address = "ns=3;s=\"status\".\"PC_OUT\".\"DatenBereit\"",
                DescriptionEn="Data Is Ready",
                DescriptionDe="Daten Bereit",
                DescriptionLocal="Data Připravena",
                Device="PLC"
            },
            new Signal
            {
                Name = "JobQuittierung",
                Address = "ns=3;s=\"status\".\"PC_OUT\".\"JobQuittierung\"",
                DescriptionEn="On kvit we delete dates",
                DescriptionDe="Auf kvit löschen wir Daten",
                DescriptionLocal="Na kvit smažeme data",
                Device="PLC"
            },
            new Signal
            {
                Name = "Anforderung_JobHalt",
                Address = "ns=3;s=\"status\".\"PC_OUT\".\"Anforderung_JobHalt\"",
                DescriptionEn="Finish the last nail and stop",
                DescriptionDe="Den letzten Nagel fertigstellen und anhalten",
                DescriptionLocal="Dokončit poslední hřebík a zastavit",
                Device="PLC"
            },
            new Signal
            {
                Name = "Anforderung_JobEnd",
                Address = "ns=3;s=\"status\".\"PC_OUT\".\"Anforderung_JobEnd\"",
                DescriptionEn="",
                DescriptionDe="",
                DescriptionLocal="",
                Device="PLC"
            },

            new Signal
            {
                Name = "Vizu_AutoStart",
                Address = "ns=3;s=\"Vizu_AutoStart\"",
                DescriptionEn="AutoMode Set",
                DescriptionDe="AutoMode Set",
                DescriptionLocal="AutoMode Set",
                Device="System"
            },
            new Signal
            {
                Name = "Vizu_Autostop",
                Address = "ns=3;s=\"Vizu_Autostop\"",
                DescriptionEn="HandMode Set",
                DescriptionDe="HandMode Set",
                DescriptionLocal="HandMode Set",
                Device=""
            },
            new Signal
            {
                Name = "MOD_Auto",
                Address = "ns=3;s=\"MOD_Auto\"",
                DescriptionEn="AutoMode Get",
                DescriptionDe="AutoMode Get",
                DescriptionLocal="AutoMode Get",
                Device="System"
            },
            new Signal
            {
                Name = "MOD_Hand",
                Address = "ns=3;s=\"MOD_Hand\"",
                DescriptionEn="HandMode",
                DescriptionDe="HandMode",
                DescriptionLocal="HandMode",
                Device=""
            },
            new Signal
            {
                Name = "OP1_Acknowledge",
                Address = "ns=3;s=\"OP1_Acknowledge\"",
                DescriptionEn="Acknowledgement",
                DescriptionDe="Quittierung",
                DescriptionLocal="Potvdit Poruchu",
                Device="System"
            },
            new Signal
            {
                Name = "F_Quitt",
                Address = "ns=3;s=\"F_Quitt\"",
                DescriptionEn="Depasivation",
                DescriptionDe="Depasivation",
                DescriptionLocal="Depasivace",
                Device=""
            },
            new Signal
            {
                Name = "R01_Prg101",
                Address = "ns=3;s=\"90_HMI_DB\".\"R01_Prg101\".\"HAnf\"",
                DescriptionEn="Robot prg 101",
                DescriptionDe="Robot prg 101",
                DescriptionLocal="Robot prg 101",
                Device=""
            },
            new Signal
            {
                Name = "R01_Ohne_Production",
                Address = "ns=3;s=\"90_HMI_DB\".\"R01_Ohne_Production\".\"HAnf\"",
                DescriptionEn="Without production",
                DescriptionDe="Without production",
                DescriptionLocal="Without production",
                Device=""
            },
            new Signal
            {
                Name = "R01_Ohne_Shooting",
                Address = "ns=3;s=\"90_HMI_DB\".\"R01_Ohne_Shooting\".\"HAnf\"",
                DescriptionEn="Without shooting",
                DescriptionDe="Without shooting",
                DescriptionLocal="Without shooting",
                Device=""
            },
            new Signal
            {
                Name = "R01_PauseProduction",
                Address = "ns=3;s=\"90_HMI_DB\".\"R01_PauseProduction\".\"HAnf\"",
                DescriptionEn="Robot pause production",
                DescriptionDe="Robot pause production",
                DescriptionLocal="Robot pause production",
                Device=""
            },
            new Signal
            {
                Name = "R01_Abort",
                Address = "ns=3;s=\"90_HMI_DB\".\"R01_Abort\".\"HAnf\"",
                DescriptionEn="Robot abort work",
                DescriptionDe="Robot abort work",
                DescriptionLocal="Robot abort work",
                Device=""
            }
            //new Signal
            //{
            //    Name = "M_Links",
            //    Address = "ns=3;s=\"M_Links\"",
            //    DescriptionEn="Left",
            //    DescriptionDe="Links",
            //    DescriptionLocal="Vlevo",
            //    Device="Robot"
            //},
            //new Signal
            //{
            //    Name = "M_rechts",
            //    Address = "ns=3;s=\"M_rechts\"",
            //    DescriptionEn="Right",
            //    DescriptionDe="Recht",
            //    DescriptionLocal="Právo",
            //    Device="Robot"
            //},
            //new Signal
            //{
            //    Name = "M_start",
            //    Address = "ns=3;s=\"M_start\"",
            //    DescriptionEn="Start work",
            //    DescriptionDe="Beginnen Sie mit der Arbeit",
            //    DescriptionLocal="Začatek prace",
            //    Device="System"
            //},
            //new Signal
            //{
            //    Name = "M_Robot_stoe",
            //    Address = "ns=3;s=\"M_Robot_stoe\"",
            //    DescriptionEn="Robot Malfunctions",
            //    DescriptionDe="Robot Störungen",
            //    DescriptionLocal="Robot Poruchy",
            //    Device="Robot"
            //},
            //new Signal
            //{
            //    Name = "M_shooting",
            //    Address = "ns=3;s=\"M_shooting\"",
            //    DescriptionEn="Nail shoot",
            //    DescriptionDe="Nagel schießen",
            //    DescriptionLocal="Hřebík střílet",
            //    Device="Robot"
            //},
            //new Signal
            //{
            //    Name = "M_LampTeste",
            //    Address = "ns=3;s=\"M_LampTeste\"",
            //    DescriptionEn="Lamp test",
            //    DescriptionDe="Lampentest",
            //    DescriptionLocal="Zkouška lampy",
            //    Device="System"
            //}
        };
        await _db.Signals.AddRangeAsync(_Signals);
        await _db.SaveChangesAsync();

        "InitializeSignals db init data".CheckStage();
    }

    private async Task InitializeAlarms()
    {
        _Logger.LogInformation("Alarms initialize ... ");

        List<Alarm> _Alarms = new()
        {
            new Alarm
            {
                NR = 1,
                Device = "Station",
                Inverted = false,
                Priority = "S",
                StopType = "T",
                DescriptionEn = "Not Automatic Mode",
                DescriptionDe = "Ohne Automatik",
                DescriptionLocal = "Není v automatickém režimu",
                Name = "N_auto",
                Address = "ns=3;s=\"Stoe_DB\".\"N_auto\""
            },
            new Alarm
            {
                NR = 2,
                Device = "Station",
                Inverted = false,
                Priority = "S",
                StopType = "O",
                DescriptionEn = "Stop",
                DescriptionDe = "Halt",
                DescriptionLocal = "Stop",
                Name = "Halt",
                Address = "ns=3;s=\"Stoe_DB\".\"Halt\""
            },
            new Alarm
            {
                NR = 3,
                Device = "Station",
                Inverted = false,
                Priority = "S",
                StopType = "O",
                DescriptionEn = "Emergency Stop",
                DescriptionDe = "Not-Aus",
                DescriptionLocal = "Nouzový stop",
                Name = "EmStop",
                Address = "ns=3;s=\"Stoe_DB\".\"EmStop\""
            },
            new Alarm
            {
                NR = 4,
                Device = "Robot",
                Inverted = false,
                Priority = "S",
                StopType = "T",
                DescriptionEn = "Robot - System Fault",
                DescriptionDe = "Roboter - Systemfehler",
                DescriptionLocal = "Robot - systémová porucha",
                Name = "R_systemFault",
                Address = "ns=3;s=\"Stoe_DB\".\"R_sytemFault\""
            },
            new Alarm
            {
                NR = 5,
                Device = "Robot",
                Inverted = false,
                Priority = "S",
                StopType = "O",
                DescriptionEn = "Robot - Emergency Stop",
                DescriptionDe = "Roboter - Not - Aus",
                DescriptionLocal = "Robot - Nouzový stop",
                Name = "R_EmStop",
                Address = "ns=3;s=\"Stoe_DB\".\"R_EmStop\""
            },
            new Alarm
            {
                NR = 6,
                Device = "Station",
                Inverted = false,
                Priority = "S",
                StopType = "T",
                DescriptionEn = "General Fault",
                DescriptionDe = "Gesamtfehler",
                DescriptionLocal = "Celková porucha",
                Name = "Gesamtfehler",
                Address = "ns=3;s=\"Stoe_DB\".\"Gesamtfehler\""
            },
            new Alarm
            {
                NR = 7,
                Device = "Station",
                Inverted = false,
                Priority = "S",
                StopType = "T",
                DescriptionEn = "General BS",
                DescriptionDe = "BS-Gesamt",
                DescriptionLocal = "Celková BS",
                Name = "BS_gesamt",
                Address = "ns=3;s=\"Stoe_DB\".\"BS_gesamt\""
            },
            new Alarm
            {
                NR = 8,
                Device = "Robot",
                Inverted = false,
                Priority = "S",
                StopType = "T",
                DescriptionEn = "Robot - Not Auto",
                DescriptionDe = "Roboter -Ohne Automatik",
                DescriptionLocal = "Robot -Není v automatice",
                Name = "N_R_Auto",
                Address = "ns=3;s=\"Stoe_DB\".\"N_R_Auto\""
            },
            new Alarm
            {
                NR = 9,
                Device = "Station",
                Inverted = false,
                Priority = "Z",
                StopType = "-",
                DescriptionEn = "Motor Off",
                DescriptionDe = "Motor aus",
                DescriptionLocal = "Motor vypnut",
                Name = "MotOffState",
                Address = "ns=3;s=\"Stoe_DB\".\"MotOffState\""
            },
            new Alarm
            {
                NR = 10,
                Device = "Station",
                Inverted = false,
                Priority = "S",
                StopType = "T",
                DescriptionEn = "Motor Not On",
                DescriptionDe = "Motor nicht ein",
                DescriptionLocal = "Motor neběží",
                Name = "N_MotOnState",
                Address = "ns=3;s=\"Stoe_DB\".\"N_MotOnState\""
            },
            new Alarm
            {
                NR = 11,
                Device = "Station",
                Inverted = false,
                Priority = "M",
                StopType = "-",
                DescriptionEn = "PC Not Connected",
                DescriptionDe = "PC nicht angebunden",
                DescriptionLocal = "PC nepřipojeno",
                Name = "PC_not_connected",
                Address = "ns=3;s=\"Stoe_DB\".\"PC_not_connected\""
            },
            new Alarm
            {
                NR = 12,
                Device = "Station",
                Inverted = false,
                Priority = "S",
                StopType = "O",
                DescriptionEn = "Emergency Stop",
                DescriptionDe = "Not - Aus",
                DescriptionLocal = "Nouzový stop",
                Name = "Not_halt",
                Address = "ns=3;s=\"Stoe_DB\".\"Not_halt\""
            }
        };
        await _db.Alarms.AddRangeAsync(_Alarms);
        await _db.SaveChangesAsync();

        "InitializeAlarms db init data".CheckStage();
    }

    #endregion Initialize OPC
}