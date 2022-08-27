using System.Linq;
using DeskReservationAPI.Data;
using DeskReservationAPI.Entities;
using DeskReservationAPI.IServices;

namespace DeskReservationAPI.Services
{
    public class DeskService : IDeskService
    {
        private readonly ApplicationDbContext _context;

        public DeskService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Desk GetDeskById(long id)
        {
            return _context.Desks.FirstOrDefault(x => x.Id == id);
        }
    }
}
