using System.ComponentModel.DataAnnotations;

namespace HanaHotel.EntityLayer.Concrete
{
	public class ServiceDetail
	{
		[Key]
		public int Id { get; set; }
		public int RoomId { get; set; }
		public int ServiceId { get; set; }
	}
}
