# VB WinForms Refactor Demo

The purpose of this project is to demonstrate
how an existing Windows Forms application
that includes data access logic directly in the presentation layer
can be progressively refactored
into a more testable and maintainable program structure.
The project makes use of three similar-looking Windows Forms.
Each form displays a grid containing items read from a database.
While they look similar on the outside,
they work very differently from one another on the inside.

The items we're working with are as widgets, gadgets, and blodgets.

# Base case - Blodgets

The blodgets represent the code base before refactoring.
It's important to note that even with the other objects having been refactored,
the blodgets still work perfectly fine.

Here's what the `Load` event handler looks like for that form.
Like thousands of other WinForms applications,
it uses some kind of lightweight data access component
to retrieve some kind of generic data access object,
in this case an ADO.NET `DataTable`,
and binds the datagrid to that object.

```csharp
private void BlodgetView_Load(object sender, EventArgs e)
{
    string sql = @"
select blodget_id as ID, name as Name, size as Size, material as Material
from blodget
where size = 'Medium' or material = 'Plastic'
";
    DataTable dt = SqliteConnector.GetDataTable(sql);
    dataGridView1.DataSource = dt;
}
```

This approach has its advantages.
It's easy to write and easy to read.
And by itself, it's easy to maintain.
But when you have an entire application built this way,
it can become increasingly difficult to make changes.
It is also very resistant to testing, especially automated unit testing.

# Refactor Level 1 - Gadgets

The gadgets have been refactored to separate the data access code
from the presentation code.
We accomplished this by defining two key components:
a model class and a repository.

The model is a simple class definition
containing the same fields as the database.

The repository is essentially an abstraction over the database.
Instead of using SQL to read data directly from the database,
our form uses application code to execute a defined method on the repository.

This code example shows the form constructor and the `Load` handler.

> **NOTE:** The code samples in this README are in C#
> because Markdown code fencing supports that better than it does VB.

```csharp
public GadgetsView(IGadgetRepository repository)
{
    InitializeComponent();
    this.repository = repository;
}

private void GadgetsView_Load(object sender, EventArgs e)
{
    GadgetCriteria[] criteria = [new GadgetCriteria {
        MaxInitialDate = new DateTime(DateTime.Now.Year - 30, DateTime.Now.Month, DateTime.Now.Day),
        Moods = ["happy", "cheerful", "brave", "silly"]
    }];            
    IEnumerable<Gadget> records = repository.Search(criteria);
    dataGridView1.DataSource =
        records
        .Select(g => new { ID = g.GadgetId, g.Name, g.InitialDate, g.Mood })
        .ToList();
}
```

Understanding this code requires understanding a few concepts
commonly used in object-oriented programming.

## Dependency injection

In our blodget example, access to the database is provided by a data access module.
This module exists for the lifetime of the application
and cannot realistically be subjected to any automated tests,
nor can it be replaced with a mock version
to enable testing of components that use it.

The widget form requires, as part of its creation logic (its *constructor*),
an object to be passed in that meets the defintion of an `IGadgetRepository`.
The runtime supplies this information to the widget form
based on "factory" logic defined within the application's startup code.
This is useful for testing, as will be discussed later.

## Interfaces

In object-oriented programming, an interface is sometimes thought of
as an abstract definition of a real-world entity.
But it can also be useful to think of it as a contract.
A class that implements `IGadgetRepository` promises
that it will implement all of the methods that `IGadgetRepository` requires.

In our example above, the specific method we're expecting
is called `Search`, which takes an `IEnumerable` (enumerable collection)
of `GadgetCriteria` and returns an `IEnumerable` of `Gadget`.

> **Key point**: our Gadget form doesn't care
> what kind of `IGadgetRepository` it is constructed with,
> and it doesn't care how the repository implements its `Search` method.
> All it cares about is that it has a repository that it can search.

# Refactor Level 2 - Widgets

The gadgets are an improvement over the blodgets.
We've separated the low-level details of the data access out to a repository.
But there's still room for improvement.
The objects we're working with here are very simple,
but what if we had much more complex objects,
or more complex rules for working with them?
Our form code, which ideally should only be concerned
with displaying data to the user and accepting input from the user,
could get bogged down with messy and frequently-changing business logic.

With the widgets, we remove the need for the form to know anything
other than what type of widget it wants to display.
We accomplish this by using a provider object.

```csharp
public WidgetView(IWidgetProvider provider)
{
    InitializeComponent();
    this.provider = provider;
}

private void WidgetView_Load(object sender, EventArgs e)
{
    dataGridView1.DataSource = provider.GetPrimaryVerticalWidgets();
}
```

Instead of an `IWidgetRepository`,
this form's constructor requires an `IWidgetProvider`.
The `IWidgetRepository` still exists, and is used by the `IWidgetProvider`,
but the provider gives an additional layer of abstraction.
We don't need to know exactly how to formulate
the criteria required by the repository.
The provider takes care of that.

This is not simply a convenience, however.
The provider pattern improves our ability to create automated unit tests
where we can meaningfully test the business logic
involved in selecting and transforming the data to be displayed.

Understanding this involves another concept, automated unit testing.

## Automated unit testing

There are various levels of testing applied to any software system.
Nearly any system will undergo user acceptance testing,
in which human testers use the software in a pre-production environment,
making sure that it works as expected before releasing it to production.

Many systems also make use of automated functional testing,
in which specialized tools execute fully-built application components,
verifying the application's behavior against predefined inputs.

An additional type of test that can be very useful during development
is the automated unit test.
Rather than testing compiled application components,
unit tests test the functionality of smaller units of code
within the application.

Of course, in a working application, components depend on other components.
For example, the widget form depends on a widget provider,
which, in turn, depends on a widget repository.
And the widget repository itself has dependencies:
it makes use of a the SQLite client library for .NET,
which, in turn, makes use of an actual SQLite database.
It's not practical to run automated tests against the actual database.
And there's really no need to test the SQLite client code provided by Microsoft.
That's where dependency injection comes in.

The `WidgetProvider` class doesn't require an actual repository.
It only requires a class that implements `IWidgetRepository`.
In the real application, we configure the dependency injection framework
to create an instance of `WidgetRepository`
and provide that to the `WidgetProvider` constructor.
But in an automated unit test, we instead use a framework called `Moq`
to construct a mock repository.

```csharp
[Fact]
public void GetPrimaryVerticalWidgets_Normal()
{
    Widget[] testValues = [
        new Widget(1, "A", WidgetColor.Red, Spin.Up),
        new Widget(2, "B", WidgetColor.Yellow, Spin.Down),
        new Widget(3, "C", WidgetColor.Green, Spin.Stationary),
        new Widget(4, "D", WidgetColor.Yellow, Spin.Right),
        new Widget(5, "E", WidgetColor.Blue, Spin.Up),
    ];
    Func<IEnumerable<WidgetCriteria>, IEnumerable<Widget>> testFilter = c =>
    {
        WidgetCriteria criteria = c.Single();
        return testValues.Where(
          tv => (criteria.Colors?.Contains(tv.Color) ?? true) 
                  && (criteria.Spins?.Contains(tv.Spin) ?? true));
    };
    Mock<IWidgetRepository> mockRepository = new Mock<IWidgetRepository>();
    mockRepository
        .Setup(r => r.Search(It.IsAny<IEnumerable<WidgetCriteria>>()))
        .Returns(testFilter);
    WidgetProvider subject = new WidgetProvider(mockRepository.Object);
    IEnumerable<Widget> result = subject.GetPrimaryVerticalWidgets();
    Assert.Collection(
        result,
        item => Assert.Equal(1, item.WidgetId),
        item => Assert.Equal(5, item.WidgetId));
}
```

We configure the `Search` method of the mock repository
to imitate the data retrieval method of a real repository.
So we're not testing the repository itself (there's not much to test there);
instead, we're testing that the provider will provide the correct criteria
to the repository and will handle the returned results correctly.

# Summary

We see in this demo project how we can safely transform an application
to make it easier to maintain
by gradually refactoring the data access code to make it more modular.

- Business entities are defined in code as **model** components
- Low-level database code is encapsulated by a **repository**
- Complex logic for retrieving and transforming data is wrapped in a **provider**

We accomplish this by using some key development techniques:
- **Interfaces** to define the requirements of our components without specifying
  the implementation details
- **Dependency injection** to separate our application logic from the services
  it depends on
- **Automated unit testing** and **mocks** to test our individual components
  while ignoring dependencies we can't or don't need to test

All of this can be done incrementally, feature by feature, 
keeping the application fully operational
without requiring a major up-front investment or lengthy IT initiative.
