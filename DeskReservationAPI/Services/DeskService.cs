using System.Collections.Generic;
using System.Linq;
using DeskReservationAPI.Data;
using DeskReservationAPI.Entities;
using DeskReservationAPI.IServices;
using Microsoft.EntityFrameworkCore;

namespace DeskReservationAPI.Services
{
    public class DeskService : IDeskService
    {
        private readonly ApplicationDbContext _context;


        public DeskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Desk> GetAll()
        {
            return _context.Desks.Include(x=>x.Location);
        }

        public IEnumerable<Desk> GetAllByCity(string city)
        {
            return _context.Desks.Include(x => x.Location).Where(x=>x.Location.City==city);
        }

        public Desk GetDeskById(long id)
        {
            return _context.Desks.FirstOrDefault(x => x.Id == id);
        }

        public void ChangeDeskAvailability(Desk desk, bool available)
        {
            desk.Available = available;
            _context.SaveChanges();
        }
    }
}
