using System.Collections.Generic;
using System.Linq;
using UralskMap.Models;

namespace UralskMap
{
    public class Locations
    {
        public static List<LocationPoint> GetLocations(Enums.LocationType locationType)
        {
            using (var db = new ModelContainer())
            {
                return db.Locations.Where(x => x.LocationType == locationType).ToList();
            }
        }
    }
}
