using System;
using System.Collections.Generic;

namespace EF.Entities
{
    public class VehicleInitialized
    {
        private HashSet<Feature> _features = new HashSet<Feature>();

        #region Constructors
        public VehicleInitialized(string name)
        {
            Name = name;
        }
        private VehicleInitialized() { }
        #endregion

        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Feature> Features { get => _features; private set => _features = new HashSet<Feature>(value); }

        public void AddFeature(Feature feature) => _features.Add(feature);

    }
}
