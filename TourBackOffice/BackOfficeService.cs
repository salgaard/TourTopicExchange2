using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourBackOffice
{
    public static class BackOfficeService
    {
        public static void CancelBooking(string payLoad)
        {
            Console.WriteLine($"Booking Cancelled: {payLoad}");
        }

        public static void CreateBooking(string payLoad)
        {
            Console.WriteLine($"Booking Created: {payLoad}");
        }
    }
}
