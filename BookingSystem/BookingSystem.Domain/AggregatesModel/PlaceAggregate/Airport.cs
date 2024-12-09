using BookingSystem.Domain.Exceptions;

namespace BookingSystem.Domain.AggregatesModel.PlaceAggregate
{
    public class Airport : BaseModel
    {
        /// <summary>
        /// Из общедоступных источников дата создания первого аэропорта.
        /// </summary>
        private const int FIRST_CREATED_AIRPORT = 1903;
        
        public string Name { get; private set;}
        public Country Country { get; private set;} 
        public int CreatedYear { get; private set;}

        private Airport(Guid id, string name, int createdYear, Country country)
        {
            Id = id;
            Name = name;
            CreatedYear = createdYear;
            Country = country;
        }

        public static Airport Create(Guid id, string name, int createdYear, Country country) 
        {
            if (string.IsNullOrEmpty(name))
                throw new DomainException("Имя аэропорта не может быть пустым!");

            if (createdYear < FIRST_CREATED_AIRPORT)
                throw new DomainException($"Год создания должен быть позднее {FIRST_CREATED_AIRPORT} года");

            return new Airport(id, name, createdYear, country);
        }
    }
}
