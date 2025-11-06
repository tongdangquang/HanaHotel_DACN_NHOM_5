using Otel.DataAccessLayer.Abstract;
using Otel.DataAccessLayer.Concrete;
using Otel.DataAccessLayer.Repositories;
using Otel.EntityLayer.Concrete;

namespace Otel.DataAccessLayer.EntityFramework
{
    public class EfRoomDAL : GenericRepository<Room>, IRoomDal
    {
        public EfRoomDAL(DataContext dataContext) : base(dataContext)
        {
        }
    }
}