namespace Pallet.Models.Interfaces;

public interface IAlarm : ISignal
{
    public int NR { get; set; }
    public string Device { get; set; }
    public string Priority { get; set; }
    public string StopType { get; set; }
    public bool Inverted { get; set; }
    public string Alarmtext1 { get; set; }
    public string Alarmtext2 { get; set; }
    public string Alarmtext3 { get; set; }
}