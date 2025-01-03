Imports VBWinFormRefactorDemo.Models
Imports VBWinFormRefactorDemo.Repositories

Public Class GadgetsView
    Private ReadOnly _repository As IGadgetRepository

    ' See the notes in Program.Main to see how this constructor argument is provided
    Public Sub New(repository As IGadgetRepository)
        InitializeComponent()
        _repository = repository
    End Sub

    Private Sub GadgetsView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Compared with the Widgets View, this is a less advanced level of abstraction.
        ' Using the repository keeps us from having to deal with SQL directly,
        ' or even necessarily know what kind of database we're using,
        ' but it still requires us to work with the repository component at a relatively low level,
        ' which also limits our ability to test this logic
        Dim thirtyYearsAgo = New Date(Now.Year - 0, Now.Month, Now.Day)
        Dim goodMoods = {"happy", "cheerful", "brave", "silly"}
        Dim criteria = {
            New GadgetCriteria With {
                .MaxInitialDate = thirtyYearsAgo,
                .Moods = goodMoods
            }
        }
        Dim records = _repository.Search(criteria)
        DataGridView1.DataSource = records.Select(Function(g) New With {.ID = g.GadgetId, g.Name, g.InitialDate, g.Mood}).ToList
    End Sub
End Class