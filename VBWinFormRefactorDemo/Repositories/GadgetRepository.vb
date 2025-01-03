Imports System.Globalization
Imports VBWinFormRefactorDemo.Models

Namespace Repositories

    Public Class GadgetRepository
        Inherits RepositoryBase
        Implements IGadgetRepository

        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        Public Sub Replace(key As Integer, item As Gadget) Implements IEntityRepository(Of Gadget, Integer, GadgetCriteria).Replace
            Throw New NotImplementedException()
        End Sub

        Public Sub Delete(key As Integer) Implements IEntityRepository(Of Gadget, Integer, GadgetCriteria).Delete
            Throw New NotImplementedException()
        End Sub

        Public Function GetById(key As Integer) As Gadget Implements IEntityRepository(Of Gadget, Integer, GadgetCriteria).GetById
            Throw New NotImplementedException()
        End Function

        Public Iterator Function Search(criteria As IEnumerable(Of GadgetCriteria)) As IEnumerable(Of Gadget) Implements IEntityRepository(Of Gadget, Integer, GadgetCriteria).Search
            Dim builder As New GadgetSearchQueryBuilder(criteria)
            Dim sql = builder.GetQuery
            Using cn = GetConnection()
                cn.Open()
                Using cmd = GetCommand(sql, cn)
                    Using reader = cmd.ExecuteReader
                        While reader.Read
                            Dim gadgetId = reader.GetInt32(0)
                            Dim name = reader.GetString(1)
                            Dim initialDate = Date.ParseExact(reader.GetString(2), "yyyy-MM-dd", CultureInfo.CurrentCulture)
                            Dim mood = reader.GetString(3)
                            Yield New Gadget(gadgetId, name, initialDate, mood)
                        End While
                    End Using
                End Using
            End Using
        End Function

        Public Function Create(item As Gadget) As Integer Implements IEntityRepository(Of Gadget, Integer, GadgetCriteria).Create
            Throw New NotImplementedException()
        End Function
    End Class

End Namespace