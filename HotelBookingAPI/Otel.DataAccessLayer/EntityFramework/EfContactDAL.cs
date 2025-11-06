using Microsoft.EntityFrameworkCore;
using Otel.DataAccessLayer.Abstract;
using Otel.DataAccessLayer.Concrete;
using Otel.DataAccessLayer.Repositories;
using Otel.EntityLayer.Concrete;

namespace Otel.DataAccessLayer.EntityFramework
{
    public class EfContactDAL : GenericRepository<Contact>, IContactDal
    {
        private readonly DataContext _context;

        public EfContactDAL(DataContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }

        public List<Contact> GetRepliedContactsCount()
        {
            return [.. _context.Set<Contact>().Where(item => item.IsReplied)];
        }

        public List<Contact> GetSpesificCategoryContacts(int categoryId)
        {
            return [.. _context.Set<Contact>().Where(item => item.MessageCategoryId == categoryId)];
        }

        public List<Contact> GetUnRepliedContactCount()
        {
            return [.. _context.Set<Contact>().Where(item => !item.IsReplied)];

        }


        public new List<Contact> GetContactWithCategory()
        {
            return [.. _context.Set<Contact>().Include(c => c.MessageCategory)];
        }

    }
}
