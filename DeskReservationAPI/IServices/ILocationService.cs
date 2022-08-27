using System.Collections;
using System.Collections.Generic;
using DeskReservationAPI.Entities;

namespace DeskReservationAPI.IServices
{
    public interface ILocationService
    {
        IEnumerable<Location> GetAll();
        Location GetLocationById(long id);
        Location GetLocationByCity(string city);
        void AddLocation(Location location);
        void DeleteLocation(Location location);
        void AddDeskToLocation(Location location, Desk desk);
        void RemoveDeskFromLocation(Location location, long deskId);

    }
}
