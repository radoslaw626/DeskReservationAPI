using System.Collections;
using System.Collections.Generic;
using DeskReservationAPI.Entities;

namespace DeskReservationAPI.IServices
{
    public interface ILocationService
    {
        IEnumerable<Location> GetAll();
        IEnumerable<Location> GetDesksOfLocation(long locationId);
        void AddLocation(Location location);
        void RemoveLocation(Location location);

    }
}
