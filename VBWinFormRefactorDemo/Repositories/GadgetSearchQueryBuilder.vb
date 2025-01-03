Imports System.Text
Imports VBWinFormRefactorDemo.Models

Namespace Repositories

    Public Class GadgetSearchQueryBuilder
        Private _criteria As IEnumerable(Of GadgetCriteria)
        Public Sub New(criteria As IEnumerable(Of GadgetCriteria))
            _criteria = criteria
        End Sub
        Public Function GetQuery() As String
            Dim sb As New StringBuilder
            sb.Append("select * ")
            sb.Append("from gadget ")
            If _criteria.Any Then
                sb.Append("where ")
            End If
            Dim criteriaStrings As New List(Of String)
            For Each criteriaItem In _criteria
                Dim criteriaItemStrings As New List(Of String)
                If Not String.IsNullOrEmpty(criteriaItem.NamePattern) Then
                    criteriaItemStrings.Add($"name like '{criteriaItem.NamePattern}'")
                End If
                If criteriaItem.MinInitialDate IsNot Nothing Then
                    criteriaItemStrings.Add($"initial_date >= '{criteriaItem.MinInitialDate:yyyy-MM-dd}'")
                End If
                If criteriaItem.MaxInitialDate IsNot Nothing Then
                    criteriaItemStrings.Add($"initial_date <= '{criteriaItem.MaxInitialDate:yyyy-MM-dd}'")
                End If
                If criteriaItem.Moods IsNot Nothing AndAlso criteriaItem.Moods.Any Then
                    criteriaItemStrings.Add($"mood in ({String.Join(","c, criteriaItem.Moods.QuoteArrayItems())})")
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