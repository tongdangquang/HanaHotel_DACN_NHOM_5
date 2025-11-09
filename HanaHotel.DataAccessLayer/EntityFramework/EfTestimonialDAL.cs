using HanaHotel.DataAccessLayer.Abstract;
using HanaHotel.DataAccessLayer.Concrete;
using HanaHotel.DataAccessLayer.Repositories;
using HanaHotel.EntityLayer.Concrete;

namespace HanaHotel.DataAccessLayer.EntityFramework
{
    public class EfTestimonialDAL : GenericRepository<Testimonial>, ITestimonialDal
    {
        public EfTestimonialDAL(DataContext dataContext) : base(dataContext)
        {
        }
    }
}

