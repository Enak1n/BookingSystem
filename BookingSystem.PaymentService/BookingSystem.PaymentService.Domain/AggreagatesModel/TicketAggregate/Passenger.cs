
using BookingSystem.PaymentService.Domain.AggregatesModel;
using BookingSystem.PaymentService.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace BookingSystem.PaymentService.Domain.TicketAggregate
{
    public class Passenger : ValueObject
    {
        private const string EMAIL_REGEX = @"^[-\w.]+@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,4}$";

        public string Name { get; }
        public string Surname { get; }
        public string? Patronymic { get; }
        public string Email { get; }

        private Passenger(string name, string surname, string? patronymic, string email)        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Email = email;
        }

        public static Passenger Create(string name, string surname, string? patronymic, string email)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname))
                throw new DomainException("Проверьте правильность введеных данных!");

            if (!Regex.IsMatch(email, EMAIL_REGEX))
                throw new DomainException("Проверьте правильность введеных данных!");

            return new Passenger(name, surname, patronymic, email);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Surname;
            yield return Patronymic;
            yield return Email;
        }
    }
}
