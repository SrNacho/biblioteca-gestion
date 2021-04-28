Imports MySql.Data.MySqlClient
Public Class altalibro
    Dim mods As Modulos = New Modulos()
    Dim sql As MySqlCommand
    Dim Conexion As New MySqlConnection
    Dim libroexiste As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Conexion.ConnectionString = "server=localhost;userid=root;password=;database=biblioteca"
            Conexion.Open()
            sql = New MySqlCommand("select count(id_libro) from libro where nombre_libro ='" & TextBox1.Text.ToUpper & "'", Conexion)
            libroexiste = Convert.ToInt32(sql.ExecuteScalar)
                If libroexiste = 0 Then
                    mods.alta("insert into libro(nombre_libro,stock) values('" & TextBox1.Text.ToUpper & "','" & TextBox2.Text & "')") 'pasa el nombre al metodo altalibro
                    MsgBox("Libro insertado con exito")
                Else
                    MsgBox("el libro ya existe")
                End If
                Conexion.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class