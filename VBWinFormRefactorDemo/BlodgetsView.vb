Public Class BlodgetsView
    Private Sub BlodgetsView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' This is the most basic way of providing data to the presentation layer.
        ' It requires us to know all the implementation details of the database,
        ' and increases the risk of any changes we might make in the database
        Dim sql = "
select blodget_id as ID, name as Name, size as Size, material as Material
from blodget
where size = 'Medium' or material = 'Plastic'
"
        Dim dt = SqliteConnector.GetDataTable(sql)
        DataGridView1.DataSource = dt
    End Sub
End Class