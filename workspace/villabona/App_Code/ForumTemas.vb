Imports System.Data
Imports Microsoft.VisualBasic

Public Class ForumTemas
    Shared Function Get_Temas() As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("select foro_temas.codigo, foro_temas.codcategoria codcategoria, foro_temas.codusuario,foro_temas.nombre" & current.Idioma & " nombre, foro_temas.fecha, foro_temas.activo, foro_categorias.nombre" & current.Idioma & " nombreCategoria, AspNetUsers.UserName usuario, foro_categorias.color color")
        s.AppendLine("FROM foro_temas INNER JOIN foro_categorias ON foro_temas.codcategoria = foro_categorias.codigo INNER JOIN AspNetUsers ON foro_temas.codusuario = AspNetUsers.Id")
        Return BD.GetDataset(s.ToString())
    End Function

    Shared Function Get_tema(ByVal codigo As String) As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("select foro_temas.codcategoria codcategoria, foro_temas.codusuario codusuario,foro_temas.nombre1, foro_temas.nombre2, foro_temas.fecha fecha, foro_temas.activo activo, foro_categorias.nombre" & current.Idioma & " nombreCategoria, AspNetUsers.UserName UserName, foro_posts.comentario comentario")
        s.AppendLine("FROM foro_temas INNER JOIN foro_categorias ON foro_temas.codcategoria = foro_categorias.codigo INNER JOIN AspNetUsers ON foro_temas.codusuario = AspNetUsers.Id INNER JOIN foro_posts on foro_temas.codigo = foro_posts.codtema ")
        s.AppendLine(" WHERE foro_temas.codigo=" & codigo & " AND foro_posts.inicio='true'")
        Return BD.GetDataset(s.ToString())
    End Function

    Shared Function Get_categoriasActivo(ByVal pagina As String) As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("SELECT codigo, nombre" & current.Idioma & " nombre, color, activo ")
        s.AppendLine(" FROM " & pagina)
        s.AppendLine(" WHERE activo='true'")
        Return BD.GetDataset(s.ToString())
    End Function

    Shared Function Get_usersActivo(ByVal pagina As String) As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("SELECT Id, UserName")
        s.AppendLine(" FROM " & pagina)
        s.AppendLine(" WHERE activo='true'")
        Return BD.GetDataset(s.ToString())
    End Function

    Shared Function Update_tema(ByVal codigo As String, ByVal codigocategoria As String, ByVal codigoUsuario As String, ByVal nombre1 As String, ByVal nombre2 As String, ByVal activo As String) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("UPDATE foro_temas")
        s.AppendLine(" SET codcategoria= '" & codigocategoria & "', codusuario= '" & codigoUsuario & "', nombre1= '" & nombre1 & "', nombre2= '" & nombre2 & "', activo= '" & activo & "', fecha=GETDATE()")
        s.AppendLine(" WHERE codigo= '" & codigo & "'")
        Return BD.Update(s.ToString())
    End Function

    Shared Function Update_post(ByVal codigo As String, ByVal comentario As String) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("UPDATE foro_posts")
        s.AppendLine(" SET comentario= '" & comentario & "', fechamodificacion=GETDATE()")
        s.AppendLine(" WHERE codtema= '" & codigo & "'" & "AND inicio='true'")
        Return BD.Update(s.ToString())
    End Function

    Shared Function Insert_tema(ByVal codigo As String, ByVal codigocategoria As String, ByVal codigoUsuario As String, ByVal nombre1 As String, ByVal nombre2 As String, ByVal activo As String) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("INSERT INTO foro_temas")
        s.AppendLine("(codcategoria, codusuario, nombre1, nombre2, activo, fecha)")
        s.AppendLine(" VALUES ('" & codigocategoria & "', '" & codigoUsuario & "', '" & nombre1 & "', '" & nombre2 & "', '" & activo & "',GETDATE()) ")

        Return BD.InsertID(s.ToString())
    End Function

    Shared Function Insert_post(ByVal codtema As String, ByVal codigoUsuario As String, ByVal comentario As String, ByVal activo As String) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("INSERT INTO foro_posts")
        s.AppendLine("(codtema, codusuario, fechacreacion, comentario, activo, inicio)")
        s.AppendLine(" VALUES ('" & codtema & "', '" & codigoUsuario & "', GETDATE(), '" & comentario & "', '" & activo & "','true') ")
        Return BD.InsertID(s.ToString())
    End Function

    Shared Function Delete_tema(ByVal codigo As String) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("DELETE FROM foro_temas")
        s.AppendLine("WHERE codigo = " & codigo)
        Return BD.Eliminar(s.ToString)
    End Function
End Class
