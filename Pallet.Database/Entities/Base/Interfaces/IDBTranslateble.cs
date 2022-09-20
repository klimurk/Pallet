namespace Pallet.Database.Entities.Base.Interfaces;

public interface IDBTranslateble
{
    public string DescriptionEn { get; set; }
    public string DescriptionDe { get; set; }
    public string DescriptionLocal { get; set; }
}