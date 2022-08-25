using System.Collections.Generic;
using DeskReservationAPI.Entities;
using DeskReservationAPI.IServices;

namespace DeskReservationAPI.Services
{
    public class LocationService : ILocationService
    {
        public IEnumerable<Location> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Location> GetDesksOfLocation(long locationId)
        {
            throw new System.NotImplementedException();
        }

        public void AddLocation(Location location)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveLocation(Location location)
        {
            throw new System.NotImplementedException();
        }
    }
}
