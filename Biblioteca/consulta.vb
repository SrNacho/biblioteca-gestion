Imports MySql.Data.MySqlClient
Public Class consulta
    Dim conexion As New MySqlConnection
    Dim adapt As MySqlDataAdapter
    Dim datable As DataTable
    Dim sql As String
    Dim comando As MySqlCommand

    Private Sub consulta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            conexion.ConnectionString = "server=localhost;userid=root;password=;database=biblioteca" 'string de conexion
            conexion.Open() ' se abre la conexión
            If TextBox1.Text = "" Then
                sql = "select nombre_libro,nombre_alumno,dni_alumno from alumno,libro where libro.id_libro=alumno.id_libro" 'consulta sql para buscar
                adapt = New MySqlDataAdapter(sql, conexion) 'clase para llenar el dataset 
                datable = New DataTable 'representa una tabla
                adapt.Fill(datable) 'llena la tabla
                DataGridView1.DataSource = datable 'usa los datos de datable para la datagridview
                conexion.Close()
            Else
                sql = "select nombre_libro,nombre_alumno,dni_alumno from alumno,libro where dni_alumno='" & TextBox1.Text & "' and libro.id_libro=alumno.id_libro" 'consulta sql para buscar
                adapt = New MySqlDataAdapter(sql, conexion)  'clase para llenar el dataset 
                datable = New DataTable 'representa una tabla
                adapt.Fill(datable) 'llena la tabla
                DataGridView1.DataSource = datable 'usa los datos de datable para la datagridview
                conexion.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class