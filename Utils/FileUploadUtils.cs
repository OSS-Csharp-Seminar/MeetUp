using System.Drawing;
using Microsoft.AspNetCore.Mvc;

namespace MeetUp.Utils;

public class FileUploadUtils
{
    public static byte[] FileToBytes(IFormFile file)
    {
        var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);

            // Upload the file if less than 2 MB
            if (memoryStream.Length < 2097152)
            {
                return memoryStream.ToArray();
            }
        return new byte[0];
        // TODO: Throw exception
    }
}