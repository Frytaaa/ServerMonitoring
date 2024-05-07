namespace ServerMonitoring.Application.Responses
{
    public class LEDButtonResponse
    {
        public byte LEDButton { get; init; }
        public LEDButtonStatus Status { get; init; }
    }

    public enum LEDButtonStatus
    {
        Green,
        Red,
        Blue
    }
}