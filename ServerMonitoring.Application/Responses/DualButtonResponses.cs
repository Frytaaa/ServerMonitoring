using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMonitoring.Application.Responses
{
    public class DualButtonResponses
    {
        public int DualButton { get; init; }
        public DualButtonStatus Status { get; set; }
    }

    public enum DualButtonStatus
    {
        None,
        Right,
        Left
    }
}
