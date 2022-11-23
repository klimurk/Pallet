using Pallet.Services.Models;
using System.Reflection;
using System.Resources;

namespace Pallet.Services.Language;

public class ManagerLanguage : IManagerLanguage
{
    public List<Lang> Langs { get; set; }

    public Lang SelectedLang
    {
        get => _SelectedLang;
        set
        {
            _SelectedLang = value;
            //try
            //{
            //    CultureManager.UICulture = value.Culture;
            //    //Thread.CurrentThread.CurrentCulture = value.Culture;
            //    //Thread.CurrentThread.CurrentUICulture = value.Culture;
            //    //CultureInfo.DefaultThreadCurrentCulture = value.Culture;
            //}
            //catch { }
        }
    }

    private Lang _SelectedLang;
    private readonly ResourceManager _ResourceManager;

    public ManagerLanguage()
    {
        _ResourceManager = new ResourceManager("Pallet.Resources.Stringify.Windows.LanguageWindow.LanguageWindowResource", typeof(ManagerLanguage).Assembly);
        Langs = new()
        {
            new(_ResourceManager.GetString("LangEnglish"), "Resources/Icons/great-britain-48.png", new CultureInfo("en")),
            //new(_ResourceManager.GetString("LangCzech"), "Resources/Icons/czech-republic-48.png", new CultureInfo("cs-CZ")),
            new(_ResourceManager.GetString("LangGerman"), "Resources/Icons/germany-48.png", new CultureInfo("de-DE")),
        };

        _Managers = new();
        //ManageNewResource("Information.Login.LoginInfo");
        //ManageNewResource("Information.OPC.ErrorsOPCResource");
        //ManageNewResource("Information.Users.UserInfo");
    }

    private readonly List<ResourceManager> _Managers;

    public void ManageNewResource(string Namespace)
    {
        if (_Managers.Any(s => s.BaseName == Namespace)) return;
        _Managers.Add(new("Pallet.Resources.Stringify." + Namespace, Assembly.GetExecutingAssembly()));
    }

    public string? ReadString(string Namespace, string Key) => _Managers.First(s => s.BaseName == ("Pallet.Resources.Stringify." + Namespace))?.GetString(Key);
}