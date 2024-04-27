namespace ServerMonitoring.Application.Responses;

public class TemperatureResponse
{
    public int Temperature { get; init; }
    public TemperatureStatus Status { get; init; }
}

public enum TemperatureStatus
{
    Low,
    Normal,
    High,
    Critical
}