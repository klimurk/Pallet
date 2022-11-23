using Pallet.Services.Models;

namespace Pallet.Services.Language;

/// <summary>
/// Language manager.
/// </summary>
public interface IManagerLanguage
{
    /// <summary>
    /// Selected language on interface
    /// </summary>
    public Lang SelectedLang { get; set; }

    /// <summary>
    ///List of languages.
    /// </summary>
    public List<Lang> Langs { get; set; }

    /// <summary>
    /// Add new resource by namespace.
    /// </summary>
    /// <param name="Namespace">The namespace.</param>
    void ManageNewResource(string Namespace);

    /// <summary>
    /// Find and return string by key in resources.
    /// </summary>
    /// <param name="Namespace">The namespace.</param>
    /// <param name="Key">The key.</param>
    /// <returns string</returns>
    string? ReadString(string Namespace, string Key);
}