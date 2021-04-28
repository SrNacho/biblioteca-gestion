Imports MySql.Data.MySqlClient
Public Class modificacion
    Dim conexion As New MySqlConnection
    Dim mods As Modulos = New Modulos()
    Dim llenarlistbox As MySqlCommand
    Dim llenarlist As MySqlDataReader
    Dim nom_libro As String
    Dim sql As MySqlCommand
    Dim stock As Integer
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = String.Empty Then
            MsgBox("Falta nombre del libro")
        Else
            If TextBox2.Text <> "" Then
                mods.alta("UPDATE `libro` SET `nombre_libro` = '" & TextBox1.Text.ToUpper & "', `stock` = '" & TextBox2.Text & "' WHERE `libro`.`nombre_libro` = '" & nom_libro & "'") 'pasa los datos al metodo modificar
                MsgBox("Libro actualizado")
                Me.Close()
                mods.relomodi()
            Else
                MsgBox("Falta el stock")
            End If
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        nom_libro = ListBox1.GetItemText(ListBox1.SelectedItem)
        TextBox1.Text = nom_libro
        Try
            conexion.Open()
            sql = New MySqlCommand("select stock from libro where nombre_libro='" & nom_libro & "'", conexion)
            stock = Convert.ToInt32(sql.ExecuteScalar)
            TextBox2.Text = stock
            conexion.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub modificacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try ' intenta hacer el codigo de abajo, si algo sale mal, el catch atrapa el error y lo muestra en pantalla
            conexion.ConnectionString = "server=localhost;userid=root;password=;database=biblioteca" 'string de conexion
            conexion.Open() ' se abre la conexión
            llenarlistbox = New MySqlCommand("SELECT nombre_libro FROM LIBRO ORDER BY nombre_libro ASC", conexion)
            Using llenarlist = llenarlistbox.ExecuteReader  'de acá hasta el using es para llenar la listbox con los libros
                While llenarlist.Read
                    ListBox1.Items.Add(CStr(llenarlist("nombre_libro")))
                End While
            End Using
            conexion.Close()
        Catch ex As Exception 'atrapa los errores
            MsgBox(ex.Message)
        End Try
    End Sub
End Class