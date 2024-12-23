using BookingSystem.Domain.AggregatesModel.PlaneAggregate;
using BookingSystem.Domain.Exceptions;

namespace BookingSystem.Domain.AggregatesModel.TicketAggregate
{
    public class Flight : BaseModel
    {
        public string DeparturePoint { get; private set; }
        
        public string DestinationPoint { get; private set; }

        public Plane Plane { get; private set; }

        public int EmptyPlaces { get; private set; }

        public DateTime DepartureDate { get; private set; }


        private Flight(string departurePoint, string destinationPoint, Plane plane, DateTime departureDate)
        {
            DeparturePoint = departurePoint;
            DestinationPoint = destinationPoint;
            Plane = plane;
            EmptyPlaces = plane.PassengersCount;
            DepartureDate = departureDate;  
        }

        public static Flight Create(string departurePoint, string destinationPoint, Plane plane, DateTime departureDate)
        {
            if (string.IsNullOrEmpty(departurePoint))
                throw new DomainException("Пункт отправления не может быть пустым!");

            if (string.IsNullOrEmpty(destinationPoint))
                throw new DomainException("Пункт назначения не может быть пустым!");

            if (plane.PassengersCount <= 0)
                throw new DomainException("Проверьте правлиьность самолета при создание рейса!");

            if (departureDate < DateTime.UtcNow.AddDays(3) && departureDate < DateTime.UtcNow)
                throw new DomainException("Проверьте правильность выбора даты полета");

            return new Flight(departurePoint, destinationPoint, plane, departureDate);
        }

        public void TryToBuyFlight()
        {
            if (EmptyPlaces <= 0)
                throw new DomainException("Места на данный рейс закончены!");

            EmptyPlaces -= 1;
        }

        public void ChangeDepartureDate(DateTime newDepartureTime)
        {
            if (newDepartureTime < DateTime.UtcNow.AddDays(3) && newDepartureTime < DateTime.UtcNow)
                throw new DomainException("Проверьте правильность выбора даты полета");

            DepartureDate = newDepartureTime;
        }
    }
}
