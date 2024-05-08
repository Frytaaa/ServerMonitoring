using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMonitoring.Application.Responses
{
    public class NFCScannerResponse
    {
        public double NFCScanner { get; init; }
        public NFCScannerStatus Status { get; int; }
    }

    public enum NFCScannerStatus
    {
        Ready,
        OK,
        NotOk
    }
}
