Imports System.IO
Imports Microsoft.Data.Sqlite

Module SqliteConnector

    Public Function GetConnectionString() As String
        Return "Data Source=vb_winform_refactor_demo.db"
    End Function

    Public Function GetConnection() As SqliteConnection
        Return New SqliteConnection(GetConnectionString)
    End Function

    Public Function GetDataTable(sql As String) As DataTable
        Using cn = GetConnection()
            cn.Open()
            Dim dt As New DataTable
            Using cmd As New SqliteCommand(sql, cn)
                Using reader = cmd.ExecuteReader
                    dt.Load(reader)
                    Return dt
                End Using
            End Using
        End Using
    End Function

    Public Sub Execute(sql As String)
        Using cn = GetConnection()
            cn.Open()
            Using cmd As New SqliteCommand(sql, cn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub DropDatabase()
        File.Delete("vb_winform_refactor_demo.db")
    End Sub

End Module
