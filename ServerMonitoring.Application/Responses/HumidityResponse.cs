namespace ServerMonitoring.Application.Responses;

public class HumidityResponse
{
    public double Humidity { get; init; }
    public HumidityStatus Status { get; init; }
}

public enum HumidityStatus
{
    Low,
    Normal,
    High
}