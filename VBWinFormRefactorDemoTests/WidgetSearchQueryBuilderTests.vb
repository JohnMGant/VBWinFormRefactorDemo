Imports VBWinFormRefactorDemo.Models
Imports VBWinFormRefactorDemo.Repositories
Imports Xunit

Public Class WidgetSearchQueryBuilderTests

    <Fact>
    Public Sub GetQuery_NameOnly()
        Dim criteria = {
            New WidgetCriteria With {
                .NamePattern = "TestWidget%"
            }}
        Dim subject = New WidgetSearchQueryBuilder(criteria)
        Dim expected = "select * from widget where name like 'TestWidget%'"
        Dim actual = subject.GetQuery
        Assert.Equal(expected, actual)
    End Sub

    <Fact>
    Public Sub GetQuery_ColorOnly()
        Dim criteria = {
            New WidgetCriteria With {
                .Colors = {WidgetColor.Red, WidgetColor.Green, WidgetColor.Blue}
            }}
        Dim subject = New WidgetSearchQueryBuilder(criteria)
        Dim expected = "select * from widget where color in ('Red','Green','Blue')"
        Dim actual = subject.GetQuery
        Assert.Equal(expected, actual)
    End Sub

    <Fact>
    Public Sub GetQuery_SpinOnly()
        Dim criteria = {
            New WidgetCriteria With {
                .Spins = {Spin.Up, Spin.Down}
            }}
        Dim subject = New WidgetSearchQueryBuilder(criteria)
        Dim expected = "select * from widget where spin in ('Up','Down')"
        Dim actual = subject.GetQuery
        Assert.Equal(expected, actual)
    End Sub

    <Fact()>
    Public Sub GetQuery_MultiAnd()
        Dim criteria = {
           New WidgetCriteria With {
               .NamePattern = "TestWidget%",
               .Colors = {WidgetColor.Red, WidgetColor.Green, WidgetColor.Blue},
               .Spins = {Spin.Up, Spin.Down}
           }}
        Dim subject = New WidgetSearchQueryBuilder(criteria)
        Dim expected = "select * from widget where name like 'TestWidget%' and color in ('Red','Green','Blue') and spin in ('Up','Down')"
        Dim actual = subject.GetQuery
        Assert.Equal(expected, actual)
    End Sub

    <Fact()>
    Public Sub GetQuery_MultiOr()
        Dim criteria = {
            New WidgetCriteria With {
               .NamePattern = "TestWidget%",
               .Spins = {Spin.Up, Spin.Down}
           },
           New WidgetCriteria With {
               .Colors = {WidgetColor.Red, WidgetColor.Green, WidgetColor.Blue}
           }}
        Dim subject = New WidgetSearchQueryBuilder(criteria)
        Dim expected = "select * from widget where (name like 'TestWidget%' and spin in ('Up','Down')) or (color in ('Red','Green','Blue'))"
        Dim actual = subject.GetQuery
        Assert.Equal(expected, actual)
    End Sub

End Class
