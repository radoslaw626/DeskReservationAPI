using System.Collections;
using System.Collections.Generic;
using DeskReservationAPI.Entities;

namespace DeskReservationAPI.IServices
{
    public interface IDeskService
    {
        IEnumerable<Desk> GetAll();
        IEnumerable<Desk> GetAllByCity(string city);
         Desk GetDeskById(long id);
         void ChangeDeskAvailability(Desk desk, bool available);
    }
}
