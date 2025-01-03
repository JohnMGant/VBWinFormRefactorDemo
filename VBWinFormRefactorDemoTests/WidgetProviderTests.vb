Imports Moq
Imports VBWinFormRefactorDemo.Models
Imports VBWinFormRefactorDemo.Providers
Imports VBWinFormRefactorDemo.Repositories
Imports Xunit

Public Class WidgetProviderTests

    <Fact()>
    Public Sub GetPrimaryVerticalWidgets_Normal()
        ' This test verifies that the WidgetProvider is applying the correct criteria.
        ' It doesn't test that in real life the database will return the correct values.
        ' The tests we run aginst the search query builders should cover that (as far as we can go).
        ' If the WidgetProvider worked differently, perhaps by extracting all of the records and then directly filtering them,
        ' we could test that behavior directly without the need for the "testFilter" defined below.
        ' But in real life that would perform very poorly, so this approach to the provider and to the tests
        ' is a bit of a design compromise: less testable, but more performant.

        ' Create a set of test values, including some that should meet the criteria and some that shouldn't
        Dim testValues = {
            New Widget(1, "A", WidgetColor.Red, Spin.Up),
            New Widget(2, "B", WidgetColor.Yellow, Spin.Down),
            New Widget(3, "C", WidgetColor.Green, Spin.Stationary),
            New Widget(4, "D", WidgetColor.Yellow, Spin.Right),
            New Widget(5, "E", WidgetColor.Blue, Spin.Up)}

        ' Create a simplified version of how the actual database might apply the criteria to the results
        ' (if the criteria only included colors and spins)
        Dim testFilter As Func(Of IEnumerable(Of WidgetCriteria), IEnumerable(Of Widget)) =
            Function(c)
                Dim criteria = c.Single
                Return testValues.Where(Function(tv) criteria.Colors.Contains(tv.Color) AndAlso criteria.Spins.Contains(tv.Spin))
            End Function

        ' Create a mock IRepository and configure its Search method to apply function we just defined
        Dim mockRepository = New Mock(Of IWidgetRepository)
        mockRepository _
            .Setup(Function(r) r.Search(It.IsAny(Of IEnumerable(Of WidgetCriteria)))) _
            .Returns(testFilter)

        ' Instantiate a WidgetProvider using our mock IRepository instance
        ' In the real application, the dependency injection framework would insert a real WidgetRepository
        ' that would connect to the database
        Dim subject As New WidgetProvider(mockRepository.Object)

        ' Execute the method and check that the expected items are included in the results
        Dim result = subject.GetPrimaryVerticalWidgets
        Assert.Collection(
            result,
            Sub(item) Assert.Equal(1, item.WidgetId),
            Sub(item) Assert.Equal(5, item.WidgetId))
    End Sub

    <Fact()>
    Public Sub GetClearStationaryWidgets_NotImplemented()
        ' In a real-life workflow, we could create these _NotImplemented tests
        ' for any methods we plan to implement in the future, 
        ' and then replace these with real tests either before (as in TDD)
        ' or right after we implement the method.
        Dim mockRepository = New Mock(Of IWidgetRepository)
        Dim subject As New WidgetProvider(mockRepository.Object)
        Assert.Throws(Of NotImplementedException)(Function() subject.GetClearStationaryWidgets)
    End Sub

End Class
