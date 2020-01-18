using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Entities
{
    public class Vehicle
    {
        private HashSet<Feature> _features;

        #region Constructors
        public Vehicle(string name)
        {
            Name = name;
            _features = new HashSet<Feature>();
        }
        private Vehicle() { }
        #endregion

        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Feature> Features { get => _features; private set => _features = new HashSet<Feature>(value); }

        public void AddFeature(Feature feature) => _features.Add(feature);

    }
    public class Feature : ValueObject
    {
        #region Constructors
        public Feature(int id)
        {
            Id = id;
        }
        private Feature() { }
        #endregion
        public int Id { get; set; }
        //public string Name { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            var otherValue = obj as ValueObject;

            if (otherValue == null)
                return false;

            var valueObject = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    return HashCode.Combine(current, obj);
                });
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }
    }
}
