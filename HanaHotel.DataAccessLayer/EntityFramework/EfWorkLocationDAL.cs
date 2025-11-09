using HanaHotel.DataAccessLayer.Abstract;
using HanaHotel.DataAccessLayer.Concrete;
using HanaHotel.DataAccessLayer.Repositories;
using HanaHotel.EntityLayer.Concrete;

namespace HanaHotel.DataAccessLayer.EntityFramework
{
    public class EfWorkLocationDAL : GenericRepository<WorkLocation>, IWorkLocationDal
    {
        public EfWorkLocationDAL(DataContext dataContext) : base(dataContext)
        {
        }
    }
}