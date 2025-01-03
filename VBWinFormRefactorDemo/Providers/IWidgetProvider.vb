Imports VBWinFormRefactorDemo.Models

Namespace Providers
    Public Interface IWidgetProvider
        Function GetPrimaryVerticalWidgets() As IEnumerable(Of Widget)
        Function GetAllWidgets() As IEnumerable(Of Widget)
        Function GetClearStationaryWidgets() As IEnumerable(Of Widget)
    End Interface

End Namespace
