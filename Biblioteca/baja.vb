Imports MySql.Data.MySqlClient
Public Class baja
    Dim llenarlistbox As MySqlCommand
    Dim llenarlist As MySqlDataReader
    Dim conexion As New MySqlConnection
    Dim nom_libro As String
    Dim mods As Modulos = New Modulos()
    Private Sub baja_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try ' intenta hacer el codigo de abajo, si algo sale mal, el catch atrapa el error y lo muestra en pantalla
            conexion.ConnectionString = "server=localhost;userid=root;password=;database=biblioteca" 'string de conexion
            conexion.Open() ' se abre la conexión
            llenarlistbox = New MySqlCommand("SELECT nombre_libro FROM LIBRO ORDER BY nombre_libro ASC", conexion) 'de acá hasta el END USING es para llenar la listbox con los libros
            Using llenarlist = llenarlistbox.ExecuteReader
                While llenarlist.Read
                    ListBox1.Items.Add(CStr(llenarlist("nombre_libro")))
                End While
            End Using
            conexion.Close()
        Catch ex As Exception 'atrapa los errores
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        nom_libro = ListBox1.GetItemText(ListBox1.SelectedItem)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        mods.baja(nom_libro)
    End Sub
End Class