using System;
using MediatR;
using ServerMonitoring.Application.LEDButtonBricklet.Commands;
using Tinkerforge;

namespace ServerMonitoring.Application.NFCScanner;

public class NFCService(ISender mediator)
{
    public void ReaderStateChangedCB(BrickletNFC sender, byte state, bool idle)
    {
        if(state == BrickletNFC.READER_STATE_REQUEST_TAG_ID_READY)
        {
            try
            {
                mediator.Send(new SetLEDButtonColorCommand
                {
                    Color = LEDColor.Blue
                });
                byte tagType;
                byte[] tagID;
                string tagInfo;

                sender.ReaderGetTagID(out tagType, out tagID);
                if (tagID == null && tagID.Length == 0)
                {
                    mediator.Send(new SetLEDButtonColorCommand
                    {
                        Color = LEDColor.Red
                    });
                    throw new Exception("No tag found");
                }
                tagInfo = String.Format("Found tag of type {0} with ID [", tagType);

                for (var i = 0; i < tagID.Length; i++)
                {
                    tagInfo += String.Format("0x{0:X2}", tagID[i]);

                    if (i < tagID.Length - 1)
                    {
                        tagInfo += " ";
                    }
                }

                tagInfo += "]";

                Console.WriteLine(tagInfo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        if(idle)
        {
            mediator.Send(new SetLEDButtonColorCommand
            {
                Color = LEDColor.Green
            });
            sender.ReaderRequestTagID();
        }
    }
}