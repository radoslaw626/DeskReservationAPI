using System;

namespace DeskReservationAPI.Helpers
{
    public class DateParser
    {
        public static bool BeAValidDate(string value)
        {
            DateTime date;
            return DateTime.TryParse(value, out date);
        }
    }
}