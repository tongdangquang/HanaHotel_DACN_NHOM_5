namespace Otel.WebUI.DTOs.RoomDTO
{
    public class AddRoomDTO
    {
        public required string RoomNumber { get; set; }
        public required string RoomCoverImage { get; set; }
        public required int Price { get; set; }
        public required string Title { get; set; }
        public required string BedCount { get; set; }
        public required string BathCount { get; set; }
        public required string Wifi { get; set; }
        public required string Description { get; set; }
    }
}
