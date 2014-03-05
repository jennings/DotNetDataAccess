.NET Data Access Examples
==========================

This repository contains examples of database access methods for .NET projects.

There are different definitions for these terms out there; these are the
definitions I use:

* **Repository**: Provides access to a store of entities. The repository
  provides basic CRUD operations, but does not perform business logic other
  than simple helper methods.

  Repositories are usually `IQueryable`.

* **Unit of work**: A unit-of-work allows code to perform multiple operations
  atomically. The unit of work provides access to repositores; any repositories
  accessed through the unit of work will not commit changes until the UOW's
  `SaveChanges()` method is called.

* **Service**: Encapsulates business logic into a set of discrete operations.
  A service's method may query a database, call a web service, and write a file
  to disk, all of which is to perform some "operation" in the problem domain.
  Consumers of the service should not be interested in how the service is
  implemented (if the method of data access switches from OData to REST calls,
  consumers of the service should not be affected).


EFContext
----------

This project uses Entity Framework's `DbContext` directly. Examples of Entity
Framework usually depict this architecture.

The good thing about this architecture is that it's simple. Since the DbContext
is already a unit-of-work object, no extra plumbing is required to update the
database in a transaction.

It's also testable, since the properties on the `DbContext` are interfaces.

A problem is that you have to reference `EntityFramework.dll` everywhere,
since public members on the context are of type `IDbSet<T>`. This exposes the
entire application to the specific version of Entity Framework you're using.
Admittedly this is not so severe; in fact, it's mostly a "cosmetic" issue.
However, this is an issue if when upgrading to EF6 (after EF changed its
namespace), this pattern ensures that changes need to be made across your
entire project.


UnitOfWork
-----------

This adds an extra interface to your `DbContext`. The application code only
references the interface.

The interface members are `IRepository` objects, so the upside is that other
assemblies don't have to reference `EntityFramework.dll`. All ORM-specific code
is contained within the `Repository<T>` and `StudentContext` objects.

The downside is that you now have a `Repository<T>` to maintain. Luckly, all
the methods are straightforward to implement. The Repository object isn't
testable, but it isn't meant to be. It's just an object that proxies requests
onto `DbSet`s, there's not much there to test.
