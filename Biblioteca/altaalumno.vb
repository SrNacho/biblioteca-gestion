Imports MySql.Data.MySqlClient
Public Class altaalumno
    Dim llenarlistbox As MySqlCommand
    Dim llenarlist As MySqlDataReader
    Dim con As New MySqlConnection
    Dim mods As Modulos = New Modulos()
    Dim fillboxsql As MySqlCommand
    Dim listreader As MySqlDataReader
    Dim nom_libro As String
    Dim sql As MySqlCommand
    Dim libro As String
    Dim librazo As String
    Dim id_libro As Integer
    Dim cantidad As Integer
    Dim idlibrosinstock As Integer
    Dim librosinstock As String
    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub altaalumno_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try ' intenta hacer el codigo de abajo, si algo sale mal, el catch atrapa el error y lo muestra en pantalla
            con.ConnectionString = "server=localhost;userid=root;password=;database=biblioteca" 'string de conexion
            con.Open() ' se abre la conexión
            fillboxsql = New MySqlCommand("SELECT distinct nombre_libro from libro where stock > 0", con) 'esto de acá hasta en loop es para llenar el listview
            listreader = fillboxsql.ExecuteReader(CommandBehavior.CloseConnection)
            ListView1.Items.Clear()
            Dim x As ListViewItem
            Do While listreader.Read = True
                x = New ListViewItem(listreader("nombre_libro").ToString)
                ListView1.Items.Add(x)
            Loop
            con.Close()
        Catch ex As Exception 'atrapa los errores
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim sql As MySqlCommand
        If libro <> "" Then
            con.Open()
            Try
                For i = 0 To ListView2.Items.Count - 1
                    librazo = ListView2.Items(i).Text
                    sql = New MySqlCommand("select id_libro from libro where nombre_libro='" & librazo & "'", con)
                    id_libro = Convert.ToInt32(sql.ExecuteScalar)
                    sql = New MySqlCommand("select stock from libro where id_libro='" & id_libro & "'", con)
                    cantidad = Convert.ToInt32(sql.ExecuteScalar) - 1
                    If cantidad >= 0 Then
                        mods.alta("INSERT INTO `alumno` (`id_libro`, `nombre_alumno`,dni_alumno) VALUES ('" & id_libro & "', '" & TextBox1.Text.ToUpper & "','" & TextBox2.Text & "')")
                    End If
                    mods.alta("UPDATE `libro` SET `stock` = '" & cantidad & "' WHERE `libro`.`id_libro` = '" & id_libro & "'")
                Next
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            sql = New MySqlCommand("select count(stock) from libro where id_libro=id_libro and stock < 0", con)
            cantidad = Convert.ToInt32(sql.ExecuteScalar)
            For i = 1 To cantidad
                sql = New MySqlCommand("select id_libro from libro where id_libro=id_libro and stock < 0", con)
                idlibrosinstock = Convert.ToInt32(sql.ExecuteScalar)
                sql = New MySqlCommand("select stock from libro where id_libro='" & idlibrosinstock & "'", con)
                cantidad = Convert.ToInt32(sql.ExecuteScalar)
                sql = New MySqlCommand("select nombre_libro from libro where id_libro='" & idlibrosinstock & "'", con)
                librosinstock = Convert.ToString(sql.ExecuteScalar)
                mods.alta("UPDATE `libro` SET `stock` = 0 WHERE `libro`.`id_libro` = '" & idlibrosinstock & "'")
                MsgBox("No se han podido prestar " + cantidad.ToString + " libros con el nombre '" + librosinstock.ToString + "'")
            Next
            con.Close()
            Me.Close()
            mods.relo()
            MsgBox("Alumno insertado")
            'Try
            '    con.Open()
            '    sql = New MySqlCommand("select id_libro from libro where nombre_libro='" & nom_libro & "'", con) 'busca la id del libro
            '    Dim id As Integer = Convert.ToInt32(sql.ExecuteScalar)
            '    mods.alta("insert into alumno(id_libro,nombre_alumno,dni_alumno) values('" & id & "','" & TextBox1.Text & "', '" & TextBox2.Text & "')")
            '    mods.alta("UPDATE libro SET estado_libro = 'ocupado' WHERE id_libro = '" & id & "'")
            '    MsgBox("Alumno insertado")
            '    Me.Close()
            '    mods.relo()
            '    con.Close()
            'Catch ex As Exception

            'End Try
        Else
            MsgBox("Debe seleccionar un libro primero")
        End If
    End Sub


    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.FocusedItem.Selected = True Then 'if para evitar errores en el evento selectindexchanged

            libro = ListView1.SelectedItems(0).Text
            ListView2.Items.Add(libro)
        End If
    End Sub

    Private Sub ListView2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView2.SelectedIndexChanged
        Dim x As String
        For i = ListView2.Items.Count - 1 To 0 Step -1
            x = ListView2.Items(i).ToString
            If ListView2.Items(i).Selected Then
                ListView2.Items(i).Remove()
            End If
        Next
    End Sub
End Class