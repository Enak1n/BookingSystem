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

        private Flight(string departurePoint, string destinationPoint, Plane plane)
        {
            DeparturePoint = departurePoint;
            DestinationPoint = destinationPoint;
            Plane = plane;
            EmptyPlaces = plane.PassengersCount;
        }

        public static Flight Create(string departurePoint, string destinationPoint, Plane plane)
        {
            if (string.IsNullOrEmpty(departurePoint))
                throw new DomainException("Пункт отправления не может быть пустым!");

            if (string.IsNullOrEmpty(destinationPoint))
                throw new DomainException("Пункт назначения не может быть пустым!");

            if (plane.PassengersCount <= 0)
                throw new DomainException("Проверьте правлиьность самолета при создание рейса!");

            return new Flight(departurePoint, destinationPoint, plane);
        }

        public void TryToBuyFlight()
        {
            if (EmptyPlaces <= 0)
                throw new DomainException("Места на данный рейс закончены!");

            EmptyPlaces -= 1;
        }
    }
}
