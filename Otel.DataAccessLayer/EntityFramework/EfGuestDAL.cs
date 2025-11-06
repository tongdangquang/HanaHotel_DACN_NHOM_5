using Otel.DataAccessLayer.Abstract;
using Otel.DataAccessLayer.Concrete;
using Otel.DataAccessLayer.Repositories;
using Otel.EntityLayer.Concrete;

namespace Otel.DataAccessLayer.EntityFramework
{
    public class EfGuestDAL : GenericRepository<Guest>, IGuestDal
    {
        public EfGuestDAL(DataContext dataContext) : base(dataContext)
        {
        }
    }
}