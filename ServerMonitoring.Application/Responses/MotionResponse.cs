namespace ServerMonitoring.Application.Responses;

public class MotionResponse(bool motion)
{
    public bool { get; set;}
    public MotionStatus Status { get; set; }
}
public enum TemperatureStatus
{
    Active,
    NotActive,
}