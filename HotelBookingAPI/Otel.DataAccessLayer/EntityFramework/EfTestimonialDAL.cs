using Otel.DataAccessLayer.Abstract;
using Otel.DataAccessLayer.Concrete;
using Otel.DataAccessLayer.Repositories;
using Otel.EntityLayer.Concrete;

namespace Otel.DataAccessLayer.EntityFramework
{
    public class EfTestimonialDAL : GenericRepository<Testimonial>, ITestimonialDal
    {
        public EfTestimonialDAL(DataContext dataContext) : base(dataContext)
        {
        }
    }
}

