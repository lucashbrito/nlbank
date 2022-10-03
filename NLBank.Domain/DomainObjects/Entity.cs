namespace NLBank.Domain.DomainObjects
{
    public abstract class Entity : IEquatable<Entity>
    {
        public Guid Id { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Entity;

            return Equals(other);
        }

        public bool Equals(Entity other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (string.IsNullOrEmpty(Id.ToString()) || string.IsNullOrEmpty(other.Id.ToString()))
                return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"{GetType().Name} ID={Id}";
        }
    }
}
