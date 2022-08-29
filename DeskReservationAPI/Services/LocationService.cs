using System.Collections.Generic;
using System.Linq;
using DeskReservationAPI.Data;
using DeskReservationAPI.Entities;
using DeskReservationAPI.IServices;
using Microsoft.EntityFrameworkCore;

namespace DeskReservationAPI.Services
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _context;

        public LocationService(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Location> GetAll()
        {
            return _context.Locations.Include(x => x.Desks);
        }

        public Location GetLocationById(long id)
        {
            return _context.Locations.Include(x => x.Desks).FirstOrDefault(x => x.Id == id);
        }

        public Location GetLocationByCity(string city)
        {
            return _context.Locations.Include(x => x.Desks).FirstOrDefault(x => x.City == city);
        }

        public void AddLocation(Location location)
        {
            _context.Locations.Add(location);
            _context.SaveChanges();
        }

        public void DeleteLocation(Location location)
        {
            _context.Locations.Remove(location);
            _context.SaveChanges();
        }

        public void AddDeskToLocation(Location location, Desk desk)
        {
            location.Desks.Add(desk);
            _context.SaveChanges();
        }

        public void RemoveDeskFromLocation(Location location, Desk desk)
        {
            location.Desks.Remove(desk);
            _context.Desks.Remove(desk);
            _context.SaveChanges();
        }
    }
}
