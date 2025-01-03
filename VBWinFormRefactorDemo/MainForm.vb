Imports VBWinFormRefactorDemo.Providers
Imports VBWinFormRefactorDemo.Repositories

Public Class MainForm

    Private _gadgetRepository As IGadgetRepository
    Private _widgetProvider As IWidgetProvider

    ' See the notes in Program.Main to see how these constructor arguments are provided
    Public Sub New(gadgetRepository As IGadgetRepository, widgetProvider As IWidgetProvider)
        InitializeComponent()
        _gadgetRepository = gadgetRepository
        _widgetProvider = widgetProvider
    End Sub
    Private Sub WidgetsButton_Click(sender As Object, e As EventArgs) Handles WidgetsButton.Click
        ' The widget form gets its data from a provider, which uses a repository to provide predefined results
        Dim widgetsView As New WidgetsView(_widgetProvider)
        widgetsView.Show()
    End Sub

    Private Sub GadgetsButton_Click(sender As Object, e As EventArgs) Handles GadgetsButton.Click
        ' The gadget form gets its data from a repository, which accepts a criteria object and returns results
        Dim gadgetsView As New GadgetsView(_gadgetRepository)
        gadgetsView.Show()
    End Sub

    Private Sub BlodgetsButton_Click(sender As Object, e As EventArgs) Handles BlodgetsButton.Click
        ' The blodget form gets its data directly from ADO.NET using an SQL query
        Dim blodgetsView As New BlodgetsView
        blodgetsView.Show()
    End Sub
End Class