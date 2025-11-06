using Otel.DataAccessLayer.Abstract;
using Otel.DataAccessLayer.Concrete;
using Otel.DataAccessLayer.Repositories;
using Otel.EntityLayer.Concrete;

namespace Otel.DataAccessLayer.EntityFramework
{
    public class EfBookingDAL : GenericRepository<Booking>, IBookingDal
    {
        public EfBookingDAL(DataContext dataContext) : base(dataContext)
        {
        }
    }
}