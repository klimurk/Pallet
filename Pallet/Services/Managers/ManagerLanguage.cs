using Infralution.Localization.Wpf;
using Pallet.Models;
using Pallet.Services.Managers.Interfaces;
using System.Reflection;
using System.Resources;

namespace Pallet.Services.Managers;

public class ManagerLanguage : IManagerLanguage
{
    public List<Lang> Langs { get; set; }

    public Lang SelectedLang
    {
        get => _SelectedLang;
        set
        {
            _SelectedLang = value;
            try
            {
                CultureManager.UICulture = value.Culture;
            }
            catch { }
        }
    }

    private Lang _SelectedLang;
    private readonly ResourceManager _ResourceManager;

    public ManagerLanguage()
    {
        _ResourceManager = new ResourceManager("Pallet.Resources.Windows.LanguageWindow.LanguageWindowResource", Assembly.GetExecutingAssembly());
        Langs = new();
        Langs.Add(new(_ResourceManager.GetString("LangEnglish"), "Resources/Icons/great-britain-48.png", new CultureInfo("en")));
        Langs.Add(new(_ResourceManager.GetString("LangCzech"), "Resources/Icons/czech-republic-48.png", new CultureInfo("cs-CZ")));
        Langs.Add(new(_ResourceManager.GetString("LangGerman"), "Resources/Icons/germany-48.png", new CultureInfo("de-DE")));
        Langs.Add(new(_ResourceManager.GetString("LangRussian"), "Resources/Icons/russia-48.png", new CultureInfo("ru-RU")));
        Langs.Add(new(_ResourceManager.GetString("LangUkraine"), "Resources/Icons/ukraine-48.png", new CultureInfo("uk-UA")));
        SelectedLang = Langs[0];

        _Managers = new();
    }

    private List<ResourceManager> _Managers;
    public void ManageNewResource(string Namespace)
    {
        if (_Managers.Any(s => s.BaseName == Namespace)) return;
        _Managers.Add(new(Namespace, Assembly.GetExecutingAssembly()));
    }

    public string? ReadString(string Namespace, string Key) => _Managers.First(s => s.BaseName == Namespace)?.GetString(Key);

}