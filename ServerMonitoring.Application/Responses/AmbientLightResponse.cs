namespace ServerMonitoring.Application.Responses
{
    public class AmbientLightResponse
    {
        public double AmbientLight { get; init; }
        public AmbientLightStatus Status { get; init; }
    }

    public enum AmbientLightStatus
    {
        Low,
        Normal,
        High
    }
}