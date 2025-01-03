Namespace Models
    Public Class GadgetCriteria
        Public Property NamePattern As String
        Public Property MinInitialDate As Date?
        Public Property MaxInitialDate As Date?
        Public Property Moods As IEnumerable(Of String)
    End Class
End Namespace