using DeskReservationAPI.Entities;

namespace DeskReservationAPI.IServices
{
    public interface IDeskService
    {
         Desk GetDeskById(long id);
    }
}
