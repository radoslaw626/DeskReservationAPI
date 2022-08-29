using System;
using System.Collections.Generic;
using DeskReservationAPI.Entities;

namespace DeskReservationAPI.IServices
{
    public interface IReservationService
    {
        void AddReservation(Reservation reservation);
        void ChangeDesk(Reservation reservation, Desk newDesk);
        double CheckCurrentReservationsLeftTime(ApplicationUser user);
        bool CheckIfDeskIsReserved(Desk desk, DateTime startDate, DateTime endDate);
        IEnumerable<Reservation> GetAllReservationsOfDesk(Desk desk);
        bool CheckIfDeskIsAvailable(Desk desk);
        IEnumerable<Reservation> GetAllReservations();
        Reservation GetReservationById(long reservationId);
    }
}