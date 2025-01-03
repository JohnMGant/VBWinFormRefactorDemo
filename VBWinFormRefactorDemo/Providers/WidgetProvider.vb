Imports VBWinFormRefactorDemo.Models
Imports VBWinFormRefactorDemo.Repositories

Namespace Providers

    Public Class WidgetProvider
        Implements IWidgetProvider

        Private ReadOnly _repository As IWidgetRepository
        Public Sub New(repository As IWidgetRepository)
            _repository = repository
        End Sub

        Public Function GetPrimaryVerticalWidgets() As IEnumerable(Of Widget) Implements IWidgetProvider.GetPrimaryVerticalWidgets
            Dim primaryColors = {WidgetColor.Red, WidgetColor.Blue, WidgetColor.Green}
            Dim verticalSpins = {Spin.Up, Spin.Down}
            Dim criteria As WidgetCriteria() = {
                New WidgetCriteria With {
                    .Colors = primaryColors,
                    .Spins = verticalSpins
                }
            }
            Dim results = _repository.Search(criteria)
            Return results
        End Function

        Public Function GetAllWidgets() As IEnumerable(Of Widget) Implements IWidgetProvider.GetAllWidgets
            Throw New NotImplementedException()
        End Function

        Public Function GetClearStationaryWidgets() As IEnumerable(Of Widget) Implements IWidgetProvider.GetClearStationaryWidgets
            Throw New NotImplementedException()
        End Function
    End Class

End Namespace