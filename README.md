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
database in a transaction. The `IDbSet<T>` properties are the repositories.

It's also testable, since the properties on the `DbContext` are interfaces.

A problem is that you have to reference `EntityFramework.dll` everywhere,
since public members on the context are of type `IDbSet<T>`. This exposes the
entire application to the specific version of Entity Framework you're using.
Admittedly this is not so severe; in fact, it's mostly a "cosmetic" issue.
However, this is an issue if when upgrading to EF6 (after EF changed its
namespace), this pattern ensures that changes need to be made across your
entire project.


Service Repository
-------------------

I consider this an anti-pattern. I believe this is more-or-less the pattern Rob
Conery references in his blog post [Repositories on top UnitOfWork are a bad
idea][conery-repositories].

In the Service Repository pattern (the "Suppository" pattern, if you will), the
`Repository` object is requested directly by consumers. The repository also
contains some business logic, therefore I would call this a service. Hence, the
name.

This has the problems Rob Conery describes:

* It encourages you to add lots of `GetStudentByX`-like methods to your
  repository because callers have no access to the underlying `IQueryable`.

* If you put `SaveChanges()` on the repository, it encourages someone to call
  `SaveChanges()` who doesn't know everything that the `DbContext` has been
  used for. Will that `SaveChanges()` commit other things? Who knows!

* If you hide the `SaveChanges()` method from callers, only the repository can
  perform an atomic operation. Your caller can only do the operations your
  repository has provided. To make a larger operation atomic, you have to shove
  more and more business logic into the data access layer.

[conery-repositories]: http://www.wekeroad.com/2014/03/04/repositories-and-unitofwork-are-not-a-good-idea/


IUnitOfWork
-----------

This is my preferred pattern. This adds an extra interface to your `DbContext`
which abstracts the "unit-of-worky" part of the DbContext. The application code
only references the interface.

The interface members are `IRepository<T>` objects, so the upside is that other
assemblies don't have to reference `EntityFramework.dll`. All ORM-specific code
is contained within the `Repository<T>` and `StudentContext` objects.

The downside is that you now have a `Repository<T>` to maintain. Luckly, all
the methods are straightforward to implement. The Repository object isn't
testable, but it isn't meant to be. It's just an object that proxies requests
onto `DbSet`s, there's not much there to test.

If your data access layer is in the same assembly as the service layer, then
this pattern doesn't have much benefit over the EFContext pattern for most
cases. The `IRepository<T>` is slightly easier to mock/fake than `IDbSet<T>`,
but that's all. If your data access layer is separated into its own assembly,
then this pattern makes it clear how to access the data, doesn't require extra
assembly references, and gives the caller an `IQueryable` interface to the
data.
