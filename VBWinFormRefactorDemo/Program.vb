Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting
Imports VBWinFormRefactorDemo.Providers
Imports VBWinFormRefactorDemo.Repositories

Module Program
    Public Sub Main()
        LoadDatabase()
        Dim host = CreateHostBuilder.Build
        Dim serviceProvider = host.Services
        Application.Run(serviceProvider.GetRequiredService(Of MainForm))
    End Sub

    'Configure the dependency injection framework
    'When a class (including forms) specifies one of these services in its constructor (Sub New), 
    'the application will use this configuration to determine how to provide that service
    'There are three main types used here:
    ' 1. interface-factory, where a requested interface, such as IWidgetRepository, is provided by the factory method specified (the lambda function)
    ' 2. interface-class, where a requested interface, such as IWidgetProvider, is provided by means of a class that implements it (WidgetProvider)
    ' 3. simple class, where a class is requested and provided, as in MainForm
    Private Function CreateHostBuilder() As IHostBuilder
        Dim connectionString = SqliteConnector.GetConnectionString
        Dim config =
            Sub(services As ServiceCollection)
                services.AddTransient(Of IWidgetRepository)(Function(provider) New WidgetRepository(connectionString))
                services.AddTransient(Of IGadgetRepository)(Function(provider) New GadgetRepository(connectionString))
                services.AddTransient(Of IWidgetProvider, WidgetProvider)
                services.AddTransient(Of MainForm)
            End Sub
        Return Host.CreateDefaultBuilder().ConfigureServices(config)

    End Function

    'Load Sqlite database with dummy data, refreshed on each run
    Private Sub LoadDatabase()
        SqliteConnector.DropDatabase()
        Dim initSQL = "
create table widget (
    widget_id integer not null primary key autoincrement,
    name text not null,
    color text not null,
    spin text not null
);
insert into widget (name, color, spin) values ('Alice', 'Red', 'Up');
insert into widget (name, color, spin) values ('Bill', 'Blue', 'Left');
insert into widget (name, color, spin) values ('Cindy', 'Blue', 'Up');
insert into widget (name, color, spin) values ('David', 'Green', 'Down');
insert into widget (name, color, spin) values ('Eleanor', 'Yellow', 'Down');
insert into widget (name, color, spin) values ('Frank', 'Green', 'Right');
insert into widget (name, color, spin) values ('Gertrude', 'Red', 'Down');

create table gadget (
    gadget_id integer not null primary key autoincrement,
    name text not null,
    initial_date text not null,
    mood text not null
);
insert into gadget (name, initial_date, mood) values ('Aardvark', '1970-01-01', 'happy');
insert into gadget (name, initial_date, mood) values ('Bear', '1988-06-12', 'sad');
insert into gadget (name, initial_date, mood) values ('Cat', '1940-11-05', 'happy');
insert into gadget (name, initial_date, mood) values ('Dog', '2005-03-02', 'silly');
insert into gadget (name, initial_date, mood) values ('Elephant', '1999-04-18', 'brave');
insert into gadget (name, initial_date, mood) values ('Frog', '1952-10-19', 'gloomy');
insert into gadget (name, initial_date, mood) values ('Giraffe', '1968-07-07', 'cheerful');
insert into gadget (name, initial_date, mood) values ('Hyena', '2003-02-02', 'downhearted');
insert into gadget (name, initial_date, mood) values ('Iguana', '1991-08-23', 'morose');
insert into gadget (name, initial_date, mood) values ('Jackal', '1989-09-29', 'happy');

create table blodget (
    blodget_id integer not null primary key autoincrement,
    name text not null,
    size text not null, 
    material text not null
);

insert into blodget (name, size, material) values ('Alpha', 'Small', 'Wood');
insert into blodget (name, size, material) values ('Bravo', 'Medium', 'Metal');
insert into blodget (name, size, material) values ('Charlie', 'Large', 'Plastic');
insert into blodget (name, size, material) values ('Delta', 'Small', 'Metal');
insert into blodget (name, size, material) values ('Echo', 'Medium', 'Wood');
insert into blodget (name, size, material) values ('Foxtrot', 'Large', 'Plastic');
insert into blodget (name, size, material) values ('Golf', 'Small', 'Wood');"
        SqliteConnector.Execute(initSQL)
    End Sub

End Module
