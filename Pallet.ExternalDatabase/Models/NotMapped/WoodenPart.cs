namespace Pallet.ExternalDatabase.Models.NotMapped
{
    public class WoodenPart
    {
        public T3dVerpackungDetail Detail;
        public T3dVerpackung Verpackung;
 
        public WoodenPart(T3dVerpackungDetail detail, T3dVerpackung verpackung)
        {
            Detail = detail;
            Verpackung = verpackung;
        }
    }
}
