using Microsoft.Maps.MapControl.WPF;

namespace UralskMap.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Enums.LocationType LocationType { get; set; }
        public Microsoft.Maps.MapControl.WPF.Location Position { get; set; }
    }
}
