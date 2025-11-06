using Otel.DataAccessLayer.Abstract;
using Otel.DataAccessLayer.Concrete;
using Otel.DataAccessLayer.Repositories;
using Otel.EntityLayer.Concrete;

namespace Otel.DataAccessLayer.EntityFramework
{
    public class EfAboutDAL : GenericRepository<About>, IAboutDal
    {
        public EfAboutDAL(DataContext dataContext) : base(dataContext)
        {
        }
    }
}