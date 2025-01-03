Imports System.Data.Common
Imports Microsoft.Data.Sqlite

Namespace Repositories
    Public MustInherit Class RepositoryBase
        Private ReadOnly _connectionString As String
        Public Sub New(connectionString As String)
            _connectionString = connectionString
        End Sub
        Protected Function GetConnection() As DbConnection
            Return New SqliteConnection(_connectionString)
        End Function
        Protected Function GetCommand(sql As String, connection As DbConnection) As DbCommand
            Return New SqliteCommand(sql, DirectCast(connection, SqliteConnection))
        End Function
    End Class

End Namespace