using System;
using System.Collections.Generic;
using System.Text;

namespace MessagesManager.Infrastructure.Extensions
{
    public static class Base64Decoder
    {
        public static string FromBase64(this string encodedString)
        {
            byte[] data = Convert.FromBase64String(encodedString);
            string decodedString = Encoding.UTF8.GetString(data);
            return decodedString;
        }
    }
}
