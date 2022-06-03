namespace Pallet.Models;

public class Lang
{
    public string IconURI { get; set; }
    public string Name { get; set; }
    public CultureInfo Culture { get; set; }

    public Lang(string Name, string IconURI, CultureInfo Culture)
    {
        this.Name = Name;
        this.IconURI = IconURI;
        this.Culture = Culture;
    }
}