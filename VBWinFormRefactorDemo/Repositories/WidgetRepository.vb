Imports VBWinFormRefactorDemo.Models

Namespace Repositories

    Public Class WidgetRepository
        Inherits RepositoryBase
        Implements IWidgetRepository

        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        Public Sub Replace(key As Integer, item As Widget) Implements IEntityRepository(Of Widget, Integer, WidgetCriteria).Replace
            Throw New NotImplementedException()
        End Sub

        Public Sub Delete(key As Integer) Implements IEntityRepository(Of Widget, Integer, WidgetCriteria).Delete
            Throw New NotImplementedException()
        End Sub

        Public Function GetById(key As Integer) As Widget Implements IEntityRepository(Of Widget, Integer, WidgetCriteria).GetById
            Throw New NotImplementedException()
        End Function

        Public Iterator Function Search(criteria As IEnumerable(Of WidgetCriteria)) As IEnumerable(Of Widget) Implements IEntityRepository(Of Widget, Integer, WidgetCriteria).Search
            Dim builder As New WidgetSearchQueryBuilder(criteria)
            Dim sql = builder.GetQuery
            Using cn = GetConnection()
                cn.Open()
                Using cmd = GetCommand(sql, cn)
                    Using reader = cmd.ExecuteReader
                        While reader.Read
                            Dim widgetId = reader.GetInt32(0)
                            Dim name = reader.GetString(1)
                            Dim color = DirectCast([Enum].Parse(GetType(WidgetColor), reader.GetString(2)), WidgetColor)
                            Dim spin = DirectCast([Enum].Parse(GetType(Spin), reader.GetString(3)), Spin)
                            Yield New Widget(widgetId, name, color, spin)
                        End While
                    End Using
                End Using
            End Using
        End Function

        Public Function Create(item As Widget) As Integer Implements IEntityRepository(Of Widget, Integer, WidgetCriteria).Create
            Throw New NotImplementedException()
        End Function
    End Class

End Namespace