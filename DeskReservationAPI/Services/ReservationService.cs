using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using DeskReservationAPI.Data;
using DeskReservationAPI.Entities;
using DeskReservationAPI.IServices;
using Microsoft.EntityFrameworkCore;

namespace DeskReservationAPI.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        public void ChangeDesk(Reservation reservation, Desk newDesk)
        {
            reservation.Desk = newDesk;
            _context.SaveChanges();
        }

        public double CheckCurrentReservationsLeftTime(ApplicationUser user)
        {
            var reservations = _context.Reservations.Where(x => x.User == user).Where(x=>x.EndDate>DateTime.Now);
            double sumOfReservationTimeLeft = 0;
            foreach (var reservation in reservations)
            {
                sumOfReservationTimeLeft += ((reservation.EndDate - reservation.StartDate).TotalDays);
            }
            return 7-sumOfReservationTimeLeft;
        }

        public bool CheckIfDeskIsReserved(Desk desk, DateTime startDate, DateTime endDate)
        {
            var reservations = _context.Reservations.Where(x => x.Desk == desk);
            foreach (var reservation in reservations)
            {
                if (startDate >= reservation.StartDate && endDate <= reservation.EndDate)
                    return true;
            }
            return false;
        }

        public IEnumerable<Reservation> GetAllReservationsOfDesk(Desk desk)
        {
            return _context.Reservations.Include(x=>x.Desk).Where(x => x.Desk == desk);
        }

        public bool CheckIfDeskIsAvailable(Desk desk)
        {
            if (desk.Available == true)
                return true;
            return false;
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _context.Reservations.Include(x => x.Desk).Include(x => x.User);
        }

        public Reservation GetReservationById(long reservationId)
        {
            return _context.Reservations.FirstOrDefault(x => x.Id == reservationId);
        }
    }
}