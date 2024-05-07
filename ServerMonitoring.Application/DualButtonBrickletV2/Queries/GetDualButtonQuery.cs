using MediatR;
using ServerMonitoring.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMonitoring.Application.DualButtonBrickletV2.Queries
{
    public class GetDualButtonQuery : IRequest<DualButtonResponses>;


}
