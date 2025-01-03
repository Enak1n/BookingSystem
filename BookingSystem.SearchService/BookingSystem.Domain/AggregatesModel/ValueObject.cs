namespace BookingSystem.Domain.AggregatesModel
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            var valueObject = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents().Aggregate(default(int), (hashcode, value) =>
                HashCode.Combine(hashcode, value.GetHashCode()));
        }

        public static bool operator ==(ValueObject firstObject, ValueObject secondObject)
        {
            if (firstObject is null && secondObject is null)
                return true;

            if (firstObject is null || secondObject is null)
                return false;

            return firstObject.Equals(secondObject);
        }

        public static bool operator !=(ValueObject firstObject, ValueObject secondObject)
        {
            return !(firstObject == secondObject);
        }
    }
}
