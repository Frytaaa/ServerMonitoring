namespace ServerMonitoring.Application.Responses;

public class HumidityResponse(int humidity)
{
    public double Humidity { get; set; }
    public HumidityStatus Status { get; set; }
}
public enum HumidityStatus
{
    Normal,
    Low,
    High
}