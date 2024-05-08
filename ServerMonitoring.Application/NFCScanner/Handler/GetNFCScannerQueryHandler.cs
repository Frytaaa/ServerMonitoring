using MediatR;
using ServerMonitoring.Application.NFCScanner.Queries;
using ServerMonitoring.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerMonitoring.Application.NFCScanner.Handler
{
    public class GetNFCScannerQueryHandler(Tinkerforge.BrickletNFC device) : IRequestHandler<GetNFCScannerQuery, NFCScannerResponse>
    {
        public Task<NFCScannerResponse> Handle(GetNFCScannerQuery request, CancellationToken cancellationToken)
        {
            var response = new NFCScannerResponse();

            device.ReaderGetState(out byte state, out bool idle);

            if (idle)
            {
                response.Status = NFCScannerStatus.Ready;
            }
            else if (state == Tinkerforge.BrickletNFC.READER_STATE_REQUEST_TAG_ID_READY)
            {
                device.ReaderGetTagID(out byte tagType, out byte[] tagID);
                if (tagID != null && tagID.Length > 0)
                {
                    response.NFCScanner = BitConverter.ToDouble(tagID, 0);
                    response.Status = NFCScannerStatus.OK;
                }
                else
                {
                    response.Status = NFCScannerStatus.NotOk;
                }
            }
            else
            {
                response.Status = NFCScannerStatus.NotOk;
            }

            return Task.FromResult(response);
        }
    }
}
