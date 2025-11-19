using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanaHotel.EntityLayer.Concrete
{
	public class PromotionDetail
	{
		public int Id { get; set; }	
		public int PromotionId { get; set; }
		public int RoomId { get; set; }
	}
}
