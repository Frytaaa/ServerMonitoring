using MediatR;
using ServerMonitoring.Application.NFCScanner.Queries;
using ServerMonitoring.Application.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace ServerMonitoring.Application.NFCScanner.Handler
{
    public class GetNFCScannerQueryHandler(Tinkerforge.BrickletNFC device) : IRequestHandler<GetNFCScannerQuery, NFCScannerResponse>
    {
        public Task<NFCScannerResponse> Handle(GetNFCScannerQuery _, CancellationToken cancellationToken)
        {
            var response = new NFCScannerResponse();

            device.ReaderGetState(out byte state, out bool idle);

            if (idle)
            {
                response = new NFCScannerResponse { Status = NFCScannerStatus.Ready };
            }
            else if (state == Tinkerforge.BrickletNFC.READER_STATE_REQUEST_TAG_ID_READY)
            {
                device.ReaderGetTagID(out byte tagType, out byte[] tagID);
                if (tagID != null && tagID.Length > 0)
                {

                    response = new NFCScannerResponse { Status = NFCScannerStatus.OK };
                }
                else
                {
                    response = new NFCScannerResponse { Status = NFCScannerStatus.NotOk };
                }
            }
            else
            {
                response = new NFCScannerResponse { Status = NFCScannerStatus.NotOk };
            }

            return Task.FromResult(response);
        }
    }
}
