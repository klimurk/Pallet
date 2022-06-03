using Pallet.Models;

namespace Pallet.Services.Managers.Interfaces;

public interface IManagerLanguage
{
    public Lang SelectedLang { get; set; }
    public List<Lang> Langs { get; set; }
    void ManageNewResource(string Namespace);
    string? ReadString(string Namespace, string Key);
}