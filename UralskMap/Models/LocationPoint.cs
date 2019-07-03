using Microsoft.Maps.MapControl.WPF;

namespace UralskMap.Models
{
    public class LocationPoint
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Enums.LocationType LocationType { get; set; }
        public Position Position { get; set; }
    }
}
