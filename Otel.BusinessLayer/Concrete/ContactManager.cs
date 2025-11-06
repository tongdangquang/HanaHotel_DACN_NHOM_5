using Otel.BusinessLayer.Abstract;
using Otel.DataAccessLayer.Abstract;
using Otel.EntityLayer.Concrete;

namespace Otel.BusinessLayer.Concrete
{
    public class ContactManager : IContactService
    {
        private readonly IContactDal _contactDal;

        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }

        public List<Contact> TGetRepliedContactsCount()
        {
            return _contactDal.GetRepliedContactsCount();
        }

        public List<Contact> TGetUnRepliedContactCount()
        {
            return _contactDal.GetUnRepliedContactCount();
        }

        public void TDelete(Contact entity)
        {
            _contactDal.Delete(entity);
        }

        public Contact TGetByID(int id)
        {
            return _contactDal.GetByID(id);
        }

        public List<Contact> TGetList()
        {
            return _contactDal.GetList();
        }

        public void TInsert(Contact entity)
        {
            _contactDal.Insert(entity);
        }

        public void TUpdate(Contact entity)
        {
            _contactDal.Update(entity);
        }

        public List<Contact> TGetSpesificCategoryContacts(int categoryId)
        {
            return _contactDal.GetSpesificCategoryContacts(categoryId);
        }

        public List<Contact> TGetContactWithCategory()
        {
            return _contactDal.GetContactWithCategory();
        }
    }
}