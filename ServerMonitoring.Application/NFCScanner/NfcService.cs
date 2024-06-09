using System;
using Tinkerforge;

namespace ServerMonitoring.Application.NfcScanner;

public static class NfcService
{
    public static void ReaderStateChangedCB(BrickletNFC sender, byte state, bool idle)
    {
        if (state == BrickletNFC.READER_STATE_REQUEST_TAG_ID_READY)
        {
            try
            {
                byte tagType;
                byte[] tagID;
                string tagInfo;

                sender.ReaderGetTagID(out tagType, out tagID);
                if (tagID == null && tagID.Length == 0)
                {
                    throw new Exception("No tag found");
                }

                tagInfo = $"Found tag of type {tagType} with ID [";

                for (var i = 0; i < tagID.Length; i++)
                {
                    tagInfo += $"0x{tagID[i]:X2}";

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

        if (idle)
        {
            sender.ReaderRequestTagID();
        }
    }
}