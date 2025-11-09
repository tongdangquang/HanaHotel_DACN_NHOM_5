using HanaHotel.DataAccessLayer.Abstract;
using HanaHotel.DataAccessLayer.Concrete;
using HanaHotel.DataAccessLayer.Repositories;
using HanaHotel.EntityLayer.Concrete;

namespace HanaHotel.DataAccessLayer.EntityFramework
{
    public class EfSubscribeDAL : GenericRepository<Subscribe>, ISubscribeDal
    {
        public EfSubscribeDAL(DataContext dataContext) : base(dataContext)
        {

        }
    }
}


