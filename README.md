two almost identical entities `VehicleInitialized` and `Vehicle`. they own a collection of `Feature`, in the form of `private HashSet`. Worth to mention that the `Feature` type `overrides Equals()` and using `Feature.Id` for equality comparison.

`VehicleInitialized` has the `HashSet` initialized at declaration: `private HashSet<Feature> _features = new HashSet<Feature>();`
but `Vehicle` entity however, has its `HashSet<Feature> _features;` initialized only in a public constructor with parameters, that is not used by ef core.

both entities are tracked by `HashsetContext`.

when pulling a saved instance of `VehicleInitialized` off the `HashsetContext`, and attempting to add features that are already added before persisting the entity won't do anything. `HashSet.Add()` will return false.
on the other hand, when doing same thing with `Vehicle` entity, will add the duplicate features and return true.

the reason is [ef core will initialize a hashSet with reference equality](https://github.com/dotnet/efcore/issues/18898#issuecomment-555723062), hence the `overriden Equals()` will be ignore. now obviously the work around is to manually instantiate the HashSet at declare, [with potential ef core malfunction](https://github.com/dotnet/efcore/issues/18898#issuecomment-570355117), and losing the protection advantage of not initializing collections in declaration [as advocated by jon p smith](https://www.thereformedprogrammer.net/creating-domain-driven-design-entity-classes-with-entity-framework-core/#3-handling-aggregates-the-reviews-collection-property) and i qoute: 
>I purposely don’t initialise the _reviews field in the private parameterless constructor that EF Core uses when loading the entity.

of course the better approach is to use a List implementation and maintain the no-duplicates-allowed logic manually.
