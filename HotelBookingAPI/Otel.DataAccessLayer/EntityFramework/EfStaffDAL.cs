using Otel.DataAccessLayer.Abstract;
using Otel.DataAccessLayer.Concrete;
using Otel.DataAccessLayer.Repositories;
using Otel.EntityLayer.Concrete;

namespace Otel.DataAccessLayer.EntityFramework
{
    public class EfStaffDAL : GenericRepository<Staff>, IStaffDal
    {
        public EfStaffDAL(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
