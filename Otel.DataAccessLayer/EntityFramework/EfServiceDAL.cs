using Otel.DataAccessLayer.Abstract;
using Otel.DataAccessLayer.Concrete;
using Otel.DataAccessLayer.Repositories;
using Otel.EntityLayer.Concrete;

namespace Otel.DataAccessLayer.EntityFramework
{
    public class EfServiceDAL : GenericRepository<Service>, IServiceDal
    {
        public EfServiceDAL(DataContext dataContext) : base(dataContext)
        {
        }
    }
}