Imports System.Text
Imports VBWinFormRefactorDemo.Models

Namespace Repositories

    Public Class WidgetSearchQueryBuilder
        Private _criteria As IEnumerable(Of WidgetCriteria)
        Public Sub New(criteria As IEnumerable(Of WidgetCriteria))
            _criteria = criteria
        End Sub
        Public Function GetQuery() As String
            Dim sb As New StringBuilder
            sb.Append("select * ")
            sb.Append("from widget ")
            If _criteria.Any Then
                sb.Append("where ")
            End If
            Dim criteriaStrings As New List(Of String)
            For Each criteriaItem In _criteria
                Dim criteriaItemStrings As New List(Of String)
                If Not String.IsNullOrEmpty(criteriaItem.NamePattern) Then
                    criteriaItemStrings.Add($"name like '{criteriaItem.NamePattern}'")
                End If
                If criteriaItem.Colors IsNot Nothing AndAlso criteriaItem.Colors.Any Then
                    criteriaItemStrings.Add($"color in ({String.Join(","c, criteriaItem.Colors.QuoteArrayItems())})")
                End If
                If criteriaItem.Spins IsNot Nothing AndAlso criteriaItem.Spins.Any Then
                    criteriaItemStrings.Add($"spin in ({String.Join(","c, criteriaItem.Spins.QuoteArrayItems())})")
                End If
                criteriaStrings.Add(String.Join(" and ", criteriaItemStrings))
            Next
            If criteriaStrings.Count = 1 Then
                sb.Append(criteriaStrings.Single)
            Else
                Dim withParens = criteriaStrings.Select(Function(s) $"({s})")
                sb.Append(String.Join(" or ", withParens))
            End If
            Return sb.ToString
        End Function

    End Class

End Namespace