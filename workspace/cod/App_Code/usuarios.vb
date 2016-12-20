Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class usuarios

    Shared Function Get_Ciudad() As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("SELECT codigo, nombre ")
        s.AppendLine(" FROM ciudad ")
        Return BD.GetDataset(s.ToString())
    End Function

    Shared Function Get_Idiomas() As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("SELECT  codigo, nombre FROM idiomas")
        Return BD.GetDataset(s.ToString())
    End Function

    Shared Function get_PermisosUsuario(ByVal codusu As String) As DataTable
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("SELECT * ")
        s.AppendLine("FROM usuariopermisos ")
        s.AppendLine("WHERE codusu = '" & codusu & "'")
        Return BD.GetDatatable(s.ToString())
    End Function
    Shared Function get_PermisosUsuario(ByVal codusu As String, ByRef dbConnection As SqlConnection) As SqlDataReader
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("SELECT * ")
        s.AppendLine("FROM usuariopermisos ")
        s.AppendLine("WHERE codusu = '" & codusu & "'")
        Return BD.GetReader(s.ToString(), dbConnection)
    End Function
    Shared Function get_ConfiPermisosUsuario(ByVal codusu As String, ByRef dbConnection As SqlConnection) As SqlDataReader
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("select c.historial configincidencias,c.mensajeria, confirmarincidencia, mostrarusuario, ")
        s.AppendLine(" case when c.envioemail='true' then (case when u.admin <>'true' then u.envioemail else 'true' end) else 'false' end envioemail,  ")
        s.AppendLine(" u.admin,  u.configuracion, u.gestionusuarios,u.incidencias,u.formularios, u.agenda, u.noticias ")
        s.AppendLine("FROM configuracion as c, usuariopermisos as u ")
        s.AppendLine("WHERE codusu = '" & codusu & "' AND codID='" & current.codID & "'")
        Return BD.GetReader(s.ToString(), dbConnection)
    End Function

    Shared Function Update_Permisos(ByVal codusu As String, ByVal admin As Boolean, ByVal envioemail As Boolean, _
                                   ByVal configuracion As Integer, ByVal gestionusuarios As Integer, ByVal incidencias As Integer, _
                                   ByVal formularios As Integer, ByVal agenda As Integer, ByVal noticias As Integer) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine(" UPDATE [usuariopermisos] ")
        s.AppendLine(" SET [admin]= '" & admin & "',[envioemail]= '" & envioemail & _
                     "',[configuracion]= '" & configuracion & "',[gestionusuarios]= '" & gestionusuarios & "',[incidencias]= '" & incidencias & _
                     "',[formularios]= '" & formularios & "',[agenda]= '" & agenda & "',[noticias]= '" & noticias & "'")
        s.AppendLine(" WHERE codusu= '" & codusu & "'")
        Return BD.Update(s.ToString())
    End Function

    Shared Function Insert_Permisos(ByVal codusu As String, ByVal admin As Boolean, ByVal envioemail As Boolean, _
                                   ByVal configuracion As Integer, ByVal gestionusuarios As Integer, ByVal incidencias As Integer, _
                                   ByVal formularios As Integer, ByVal agenda As Integer, ByVal noticias As Integer) As Integer ', ByVal mensajeria As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("INSERT INTO usuariopermisos ([codusu], [admin ], [envioemail], [configuracion], [gestionusuarios], [incidencias], [formularios], [agenda], [noticias])")
        s.AppendLine("VALUES ('" & codusu & "','" & admin & "','" & envioemail & "','" & _
                     configuracion & "','" & gestionusuarios & "','" & incidencias & "','" & formularios & "','" & agenda & "','" & noticias & "')") '& "','" & mensajeria 
        Return BD.Insert(s.ToString())
    End Function

    Shared Function Init_Permisos(ByVal codusu As String) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("INSERT INTO usuariopermisos ([codusu], [admin ], [envioemail], [configuracion], [gestionusuarios], [incidencias], [formularios], [agenda], [noticias])")
        s.AppendLine("VALUES ('" & codusu & "','True','False','0','0','0','0','0','0')")
        Return BD.Insert(s.ToString())
    End Function

    Shared Function Delete_UsuarioPermisos(ByVal codusu As String) As Integer
        Return BD.Eliminar("DELETE [usuariopermisos] WHERE codusu= '" & codusu & "'")
    End Function

    Shared Function Existe_NombreUsuario(ByVal nombreusuarioOriginal As String, ByVal nombreusuario As String) As Integer
        Dim s As New StringBuilder
        s.AppendLine("SELECT count(ID) FROM AspNetUsers WHERE UserName= '" & nombreusuario & "'")
        If Not String.IsNullOrEmpty(nombreusuarioOriginal) Then
            s.AppendLine(" AND UserName <> '" & nombreusuarioOriginal & "'")
        End If
        Return BD.GetInteger(s.ToString)
    End Function
    Shared Function Existe_EmailUsuario(ByVal nombreusuarioOriginal As String, ByVal nombreusuario As String) As Integer
        Dim s As New StringBuilder
        s.AppendLine("SELECT count(ID) FROM AspNetUsers WHERE Email= '" & nombreusuario & "'")
        If Not String.IsNullOrEmpty(nombreusuarioOriginal) Then
            s.AppendLine(" AND Email <> '" & nombreusuarioOriginal & "'")
        End If
        Return BD.GetInteger(s.ToString)
    End Function

    Shared Function Get_Count(ByVal codigo As String) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("select sum(total) from ( ")
        s.AppendLine(" select count(*) as total from mensajes_recibidos ")
        s.AppendLine(" where codemisor = '" & codigo & "'")
        s.AppendLine(" union ")
        s.AppendLine(" select count(*)  as total from mensajes_enviados ")
        s.AppendLine(" where codreceptor = '" & codigo & "') as t ")
        Return BD.GetInteger(s.ToString())
    End Function

    Shared Function Delete_Usuario(ByVal codusu As String) As Integer
        Dim s As StringBuilder = New StringBuilder
        s.AppendLine("DELETE mensajes_recibidos WHERE codreceptor= '" & codusu & "'")
        s.AppendLine("; DELETE mensajes_enviados WHERE codemisor= '" & codusu & "'")
        s.AppendLine("; DELETE departamentosusuarios WHERE codusuario= '" & codusu & "'")
        s.AppendLine("; DELETE [usuariopermisos] WHERE codusu= '" & codusu & "'")
        Return BD.Eliminar(s.ToString)
    End Function

End Class
