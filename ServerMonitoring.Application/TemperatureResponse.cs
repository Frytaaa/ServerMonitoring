namespace ServerMonitoring.Application;

public class TemperatureResponse(int temperature)
{
    public int Temperature { get; set; }
    public TemperatureStatus Status { get; set; }
}
public enum TemperatureStatus
{
    Normal,
    Low,
    High
}