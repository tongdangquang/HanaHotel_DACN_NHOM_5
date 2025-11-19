using HanaHotel.BusinessLayer.Abstract;
using HanaHotel.EntityLayer.Concrete;

namespace HanaHotel.BusinessLayer.Concrete
{
	public class PromotionService : IPromotionService
	{
		void IGenericService<Promotion>.TDelete(Promotion entity)
		{
			throw new NotImplementedException();
		}

		Promotion IGenericService<Promotion>.TGetByID(int id)
		{
			throw new NotImplementedException();
		}

		List<Promotion> IGenericService<Promotion>.TGetList()
		{
			throw new NotImplementedException();
		}

		void IGenericService<Promotion>.TInsert(Promotion entity)
		{
			throw new NotImplementedException();
		}

		void IGenericService<Promotion>.TUpdate(Promotion entity)
		{
			throw new NotImplementedException();
		}
	}
}
