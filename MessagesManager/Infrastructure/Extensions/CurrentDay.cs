using System;
using System.Collections.Generic;
using System.Text;

namespace MessagesManager.Infrastructure.Extensions
{
    public static class CurrentDay
    {
        public static string GetCurrentDay()
        {
            var date = DateTime.UtcNow.ToString();
            return date;
        }
    }
}
