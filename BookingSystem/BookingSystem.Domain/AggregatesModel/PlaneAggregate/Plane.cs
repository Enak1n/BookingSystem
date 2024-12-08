using BookingSystem.Domain.Exceptions;

namespace BookingSystem.Domain.AggregatesModel.PlaneAggregate
{
    public class Plane : BaseModel
    {
        public string Model { get; private set; }

        public int YearOfCreation { get; private set; }

        public int PassengersCount { get; private set; }

        private Plane(Guid id, string model, int yearOfCreation, DateTimeOffset dateOfCreation, int passengersCount)
        {
            Id = id;
            Model = model;
            YearOfCreation = yearOfCreation;
            CreatedDate = dateOfCreation;
            PassengersCount = passengersCount;
        }

        public static Plane Create(Guid id, string model, int yearOfCreation, DateTimeOffset dateOfCreation, int passengersCount)
        {
            if(string.IsNullOrEmpty(model)) 
                throw new DomainException("Модель самолето не может быть пустой!");

            if (yearOfCreation < 2000)
                throw new DomainException("Самолет слишком старый для использования в современных реалиях!");

            return new Plane(id, model, yearOfCreation, dateOfCreation, passengersCount);
        }
    }
}
