namespace Pallet.Services.UserDialog
{
    public class MessageToSnack
    {
        public string Content { get; set; } = "";
        public MessageToSnackLevel Level { get; set; } = 0;
        public TimeSpan? Duration { get; set; } = null; // if null it will use the default duration of material design -> 3s
        public bool WithCloseButton { get; set; } = true;
    }

    public enum MessageToSnackLevel
    {
        NoLevel = 0,
        Error = 1,
        Warning = 2,
        Info = 3,
        Success = 4,
    }
}