Namespace Models
    Public Class Widget
        Public Sub New(widgetId As Integer, name As String, color As WidgetColor, spin As Spin)
            Me.WidgetId = widgetId
            Me.Name = name
            Me.Color = color
            Me.Spin = spin
        End Sub
        Public Property WidgetId As Integer
        Public Property Name As String
        Public Property Color As WidgetColor
        Public Property Spin As Spin
    End Class
End Namespace