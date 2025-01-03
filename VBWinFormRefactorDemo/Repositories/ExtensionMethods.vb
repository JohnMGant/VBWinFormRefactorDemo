Imports System.Runtime.CompilerServices
Imports System.Text

Namespace Repositories

    Module ExtensionMethods

        <Extension()>
        Public Function SnakeCaseToPascalCase(snakeCaseString As String) As String
            If snakeCaseString.All(Function(c) c = "_"c OrElse Char.IsLower(c) OrElse Char.IsDigit(c)) Then
                Throw New ArgumentException("Not in snake_case", NameOf(snakeCaseString))
            End If
            Dim parts = snakeCaseString.Split("_"c)
            If parts.Length = 0 Then
                Throw New ArgumentException("Failed to split on ""_""", NameOf(snakeCaseString))
            End If
            Dim sb As New StringBuilder
            For Each part In parts
                sb.Append(Char.ToUpper(part(0)))
                sb.Append(part.Substring(1))
            Next
            Return sb.ToString
        End Function

        <Extension()>
        Public Iterator Function QuoteArrayItems(Of T)(items As IEnumerable(Of T)) As IEnumerable(Of String)
            For Each item In items
                If item Is Nothing Then
                    Throw New ArgumentException("Null values not allowed", NameOf(item))
                End If
                Dim str = item.ToString
                Dim escaped = str.Replace("'", "''")
                Yield $"'{escaped}'"
            Next
        End Function
    End Module

End Namespace