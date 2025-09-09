using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSendEmail
{
    public static class EmailService
    {
        public static void SendEmail(string payLoad)
        {
            Console.WriteLine($"Email Sent: {payLoad}");
        }
    }
}
