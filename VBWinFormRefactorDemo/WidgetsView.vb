Imports VBWinFormRefactorDemo.Providers

Public Class WidgetsView

    Private ReadOnly _provider As IWidgetProvider

    ' See the notes in Program.Main to see how this constructor argument is provided
    Public Sub New(provider As IWidgetProvider)
        InitializeComponent()
        _provider = provider
    End Sub
    Private Sub WidgetsView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' The WidgetProvider defines a set of specific results,
        ' so the form doesn't need to know anything about the database at all
        ' This is the most advanced form of abstraction presented in this demo project
        DataGridView1.DataSource =
            _provider.GetPrimaryVerticalWidgets _
            .Select(Function(w) New With {.ID = w.WidgetId, w.Name, w.Color, w.Spin}) _
            .ToList
    End Sub
End Class