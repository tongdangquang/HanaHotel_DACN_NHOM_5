using HanaHotel.EntityLayer.Concrete;

namespace HanaHotel.WebUI.DTOs.RoomDTO
{
    public class ResultRoomDTO
    {
        public int Id { get; set; }

        public required string RoomName { get; set; }

        public RoomStatus Status { get; set; }

        public required string Description { get; set; }

        public double Size { get; set; }

        public double Price { get; set; }

        public required string BedCount { get; set; }

		public List<string> ImagePaths { get; set; } = new List<string>();
	}
}
