Namespace Models
    Public Class Gadget
        Public Sub New(gadgetId As Integer, name As String, initialDate As Date, mood As String)
            Me.GadgetId = gadgetId
            Me.Name = name
            Me.InitialDate = initialDate
            Me.Mood = mood
        End Sub
        Public Property GadgetId As Integer
        Public Property Name As String
        Public Property InitialDate As Date
        Public Property Mood As String
    End Class

End Namespace