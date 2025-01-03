using BookingSystem.Domain.Exceptions;

namespace BookingSystem.Domain.AggregatesModel.PlaceAggregate
{
    public class Country : BaseModel
    {
        public string Name { get; private set; }

        public string Code { get; private set; }

        private Country(Guid id, string name, string code)
        {
            Id = id;
            Name = name;
            Code = code;
        }

        public static Country Create(Guid id, string name, string code)
        {
            if (string.IsNullOrEmpty(name))
                throw new DomainException("Наименование страны не может быть пустым!");

            if (string.IsNullOrEmpty(code))
                throw new DomainException("Код страны не может быть пустым!");

            return new Country(id, name, code.ToUpper());
        }
    }
}
