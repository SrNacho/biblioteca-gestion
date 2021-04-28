Imports MySql.Data.MySqlClient
Public Class Modulos
    Dim Conexion As New MySqlConnection

    Public Sub alta(ByVal str As String)
        Dim sql As MySqlCommand
        Try
            Conexion.ConnectionString = "server=localhost;userid=root;password=;database=biblioteca"
            Conexion.Open()
            sql = New MySqlCommand(str, Conexion)
            sql.ExecuteNonQuery() 'ejecuta la consulta de arriba sin que devuelva resultado
            Conexion.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub baja(ByVal libro As String)
        Dim sql As MySqlCommand
        Dim sqlborrar As MySqlCommand
        Try
            Conexion.ConnectionString = "server=localhost;userid=root;password=;database=biblioteca"
            Conexion.Open()
            sql = New MySqlCommand("select id_libro from libro where nombre_libro='" & libro & "'", Conexion)
            Dim id As Integer = Convert.ToInt32(sql.ExecuteScalar) 'ejecuta la consulta de arriba y la convierte a entero
            sqlborrar = New MySqlCommand("DELETE FROM alumno WHERE id_libro = '" & id & "'", Conexion)
            sqlborrar.ExecuteNonQuery() 'ejecuta la consulta de arriba para borrar al alumno que tenga el libro
            sqlborrar = New MySqlCommand("DELETE FROM libro where id_libro='" & id & "'", Conexion)
            sqlborrar.ExecuteNonQuery() 'ejecuta la consulta de arriba para borrar al libro
            MsgBox("Libro borrado con exito")
            Conexion.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub modificar(ByVal libro As String, ByVal estado As String, ByVal nombre As String)
        Dim sql As MySqlCommand
        Try
            Conexion.ConnectionString = "server=localhost;userid=root;password=;database=biblioteca"
            Conexion.Open()
            sql = New MySqlCommand("select id_libro from libro where nombre_libro='" & libro & "'", Conexion)
            Dim id As Integer = Convert.ToInt32(sql.ExecuteScalar) 'ejecuta la consulta de arriba y la convierte a entero
            sql = New MySqlCommand("UPDATE libro SET estado_libro = '" & estado & "', nombre_libro='" & nombre & "' WHERE id_libro = '" & id & "'", Conexion)
            sql.ExecuteNonQuery() 'actualiza el estado y nombre del libro
            MsgBox("Libro actualizado con exito")
            Conexion.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub relo()
        altaalumno.Show()
    End Sub
    Public Sub relomodi()
        modificacion.Show()
    End Sub
End Class
