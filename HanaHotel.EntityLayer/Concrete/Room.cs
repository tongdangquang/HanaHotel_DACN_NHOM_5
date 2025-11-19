using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HanaHotel.EntityLayer.Concrete
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string RoomName { get; set; }
        public RoomStatus Status { get; set; }
        public string Description { get; set; }
        public double Size { get; set; }
		public double Price { get; set; }
        public string BedCount { get; set; }
    }

    public enum RoomStatus
    {
        Available = 0,
        Reserved = 1,
        Occupied = 2,
        Maintenance = 3
	}
}


