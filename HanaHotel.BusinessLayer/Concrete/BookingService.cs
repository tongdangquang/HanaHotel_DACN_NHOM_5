using HanaHotel.BusinessLayer.Abstract;
using HanaHotel.DataAccessLayer.Abstract;
using HanaHotel.EntityLayer.Concrete;

namespace HanaHotel.BusinessLayer.Concrete
{
    public class BookingService : IBookingService
    {
        private readonly IBookingDal _bookingDal;

        public BookingService(IBookingDal bookingDal)
        {
            _bookingDal = bookingDal;
        }

        public void TDelete(Booking entity)
        {
            _bookingDal.Delete(entity);
        }

        public Booking TGetByID(int id)
        {
            return _bookingDal.GetByID(id);
        }

        public List<Booking> TGetList()
        {
            return _bookingDal.GetList();
        }

        public void TInsert(Booking entity)
        {
            _bookingDal.Insert(entity);
        }

        public void TUpdate(Booking entity)
        {
            _bookingDal.Update(entity);
        }
    }
}