using Tinkerforge;

namespace ServerMonitoring.Application.SegmentDisplay
{
    public class SegmentDisplayWorkerService
    {
        private readonly BrickletSegmentDisplay4x7V2 _segmentDisplay;

        public SegmentDisplayWorkerService(SegmentDisplay segmentDisplay)
        {
            _segmentDisplay = segmentDisplay;
        }

        public void DisplayTemperature(double temperature)
        {
            // Convert temperature to a string with one decimal place
            string temperatureStr = temperature.ToString("F1");

            // Ensure the string fits the 4x7 display
            if (temperatureStr.Length > 4)
            {
                temperatureStr = temperatureStr.Substring(0, 4);
            }

            // Display the temperature on the Segment Display
            _segmentDisplay.SetSegments(temperatureStr.ToCharArray());
        }
    }
}
