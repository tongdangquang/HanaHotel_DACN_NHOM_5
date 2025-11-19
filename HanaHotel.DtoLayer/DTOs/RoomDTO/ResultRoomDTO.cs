using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanaHotel.DtoLayer.DTOs.RoomDTO
{
	public class ResultRoomDTO
	{
		public int Id { get; set; }
		public string RoomName { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		// trả về nhiều đường dẫn ảnh cho mỗi phòng
		public List<string> ImagePaths { get; set; } = new List<string>();
	}
}
