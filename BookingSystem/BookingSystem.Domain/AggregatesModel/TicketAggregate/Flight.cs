using BookingSystem.Domain.AggregatesModel.PlaceAggregate;
using BookingSystem.Domain.AggregatesModel.PlaneAggregate;
using BookingSystem.Domain.Exceptions;
using Newtonsoft.Json;

namespace BookingSystem.Domain.AggregatesModel.TicketAggregate
{
    public class Flight : BaseModel
    {
        public string NumberOfTheFlight { get; private set; }
        public string DeparturePoint { get; private set; }

        public string DestinationPoint { get; private set; }

        public Plane Plane { get; private set; }

        public int EmptyPlaces { get; private set; }

        public DateTime DepartureDate { get; private set; }

        public Airport DepartureAirport { get; private set; }
        public Airport DestinatioAirport { get; private set; }


        private Flight(string departurePoint, string destinationPoint, Plane plane, DateTime departureDate,
            Airport departureAirport, Airport destinationAirport,
            string numberOfTheFlight)
        {
            DeparturePoint = departurePoint;
            DestinationPoint = destinationPoint;
            Plane = plane;
            EmptyPlaces = plane.PassengersCount;
            DepartureDate = departureDate;
            DepartureAirport = departureAirport;
            DestinatioAirport = destinationAirport;
            NumberOfTheFlight = numberOfTheFlight;
        }

        public static Flight Create(string departurePoint, string destinationPoint, Plane plane, DateTime departureDate,
            Airport departureAirport, Airport destinationAirport, string numberOfTheFlight)
        {
            if (string.IsNullOrEmpty(departurePoint))
                throw new DomainException("Пункт отправления не может быть пустым!");

            if (string.IsNullOrEmpty(destinationPoint))
                throw new DomainException("Пункт назначения не может быть пустым!");

            if (plane.PassengersCount <= 0)
                throw new DomainException("Проверьте правлиьность самолета при создание рейса!");

            if (departureDate < DateTime.UtcNow.AddDays(3) && departureDate < DateTime.UtcNow)
                throw new DomainException("Проверьте правильность выбора даты полета");

            return new Flight(departurePoint, destinationPoint, plane, departureDate, departureAirport, destinationAirport, numberOfTheFlight);
        }

        public void ChangeDepartureDate(DateTime newDepartureTime)
        {
            if (newDepartureTime < DateTime.UtcNow.AddDays(3) && newDepartureTime < DateTime.UtcNow)
                throw new DomainException("Проверьте правильность выбора даты полета");

            DepartureDate = newDepartureTime;
        }

        public void TakeASeat()
        {
            EmptyPlaces -= 1;
        }
    }
}
