Imports VBWinFormRefactorDemo.Models
Imports VBWinFormRefactorDemo.Repositories
Imports Xunit

Public Class GadgetSearchQueryBuilderTests

    <Fact>
    Public Sub GetQuery_NameOnly()
        Dim criteria = {
            New GadgetCriteria With {
                .NamePattern = "TestGadget%"
            }}
        Dim subject = New GadgetSearchQueryBuilder(criteria)
        Dim expected = "select * from gadget where name like 'TestGadget%'"
        Dim actual = subject.GetQuery
        Assert.Equal(expected, actual)
    End Sub

    <Fact>
    Public Sub GetQuery_MoodOnly()
        Dim criteria = {
            New GadgetCriteria With {
                .Moods = {"gloomy", "cheerful"}
            }}
        Dim subject = New GadgetSearchQueryBuilder(criteria)
        Dim expected = "select * from gadget where mood in ('gloomy','cheerful')"
        Dim actual = subject.GetQuery
        Assert.Equal(expected, actual)
    End Sub

    <Fact>
    Public Sub GetQuery_MinDateOnly()
        Dim criteria = {
            New GadgetCriteria With {
                .MinInitialDate = New Date(2001, 1, 1)
            }}
        Dim subject = New GadgetSearchQueryBuilder(criteria)
        Dim expected = "select * from gadget where initial_date >= '2001-01-01'"
        Dim actual = subject.GetQuery
        Assert.Equal(expected, actual)
    End Sub

    <Fact()>
    Public Sub GetQuery_MaxDateOnly()
        Dim criteria = {
            New GadgetCriteria With {
                .MaxInitialDate = New Date(2015, 12, 31)
            }}
        Dim subject = New GadgetSearchQueryBuilder(criteria)
        Dim expected = "select * from gadget where initial_date <= '2015-12-31'"
        Dim actual = subject.GetQuery
        Assert.Equal(expected, actual)
    End Sub

    <Fact()>
    Public Sub GetQuery_MultiAnd()
        Dim criteria = {
           New GadgetCriteria With {
               .NamePattern = "TestGadget%",
               .Moods = {"gloomy", "cheerful"},
               .MinInitialDate = New Date(2001, 1, 1),
               .MaxInitialDate = New Date(2015, 12, 31)
           }}
        Dim subject = New GadgetSearchQueryBuilder(criteria)
        Dim expected = "select * from gadget where name like 'TestGadget%' and initial_date >= '2001-01-01' and initial_date <= '2015-12-31' and mood in ('gloomy','cheerful')"
        Dim actual = subject.GetQuery
        Assert.Equal(expected, actual)
    End Sub

    <Fact()>
    Public Sub GetQuery_MultiOr()
        Dim criteria = {
          New GadgetCriteria With {
              .NamePattern = "TestGadget%",
              .Moods = {"gloomy", "cheerful"}
          },
          New GadgetCriteria With {
              .MaxInitialDate = New Date(2015, 12, 31)
          }}
        Dim subject = New GadgetSearchQueryBuilder(criteria)
        Dim expected = "select * from gadget where (name like 'TestGadget%' and mood in ('gloomy','cheerful')) or (initial_date <= '2015-12-31')"
        Dim actual = subject.GetQuery
        Assert.Equal(expected, actual)
    End Sub

End Class
