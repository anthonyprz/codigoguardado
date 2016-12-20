Imports System.Data
Imports Microsoft.VisualBasic

Public Class ForumTags
    Shared Function get_Tags() As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("select nombre, codigo")
        s.AppendLine("from foro_tags")
        Return BD.GetDataset(s.ToString())
    End Function
    Shared Function get_tag(ByVal codigo As String) As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("select nombre, codigo")
        s.AppendLine("from foro_tags")
        s.AppendLine("where codigo=" & codigo)
        Return BD.GetDataset(s.ToString())
    End Function

    Shared Function get_tagNombre(ByVal nombre As String) As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("select nombre, codigo")
        s.AppendLine("from foro_tags")
        s.AppendLine("where nombre='" & nombre & "'")
        Return BD.GetDataset(s.ToString())
    End Function


    Shared Function Update_tags(ByVal codigo As String, ByVal nombre As String) As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("UPDATE foro_tags")
        s.AppendLine("SET nombre='" & nombre & "'")
        s.AppendLine("where codigo=" & codigo)
        Return BD.GetDataset(s.ToString())
    End Function

    Shared Function Delete_tags(ByVal codigo As String) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("DELETE FROM foro_tags")
        s.AppendLine("WHERE codigo =" & codigo)
        Return BD.Eliminar(s.ToString)
    End Function

    Shared Function insert_tags(ByVal nombre As String) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("INSERT INTO foro_tags")
        s.AppendLine("(nombre, codID)")
        s.AppendLine("values ('" & nombre & "','1')")
        Return BD.InsertID(s.ToString())
    End Function

    Shared Function insert_relationTemaTags(ByVal codtema As Integer, ByVal codtag As Integer) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("INSERT INTO foro_temas_tags")
        s.AppendLine("(codtema, codtag)")
        s.AppendLine("values ('" & codtema & "','" & codtag & "')")
        Return BD.InsertID(s.ToString())
    End Function

    Shared Function get_Relationtag(ByVal codigo As Integer) As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("select foro_tags.nombre nombretag, foro_tags.codigo codigo")
        s.AppendLine("from foro_tags INNER JOIN foro_temas_tags ON foro_tags.codigo = foro_temas_tags.codtag INNER JOIN foro_temas on foro_temas_tags.codtema = foro_temas.codigo")
        s.AppendLine("where foro_temas.codigo=" & codigo)
        Return BD.GetDataset(s.ToString())
    End Function


    Shared Function get_RelationtagTema(ByVal codigotag As Integer, ByVal codigotema As Integer) As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("select foro_tags.nombre nombretag, foro_tags.codigo codigo")
        s.AppendLine("from foro_tags INNER JOIN foro_temas_tags ON foro_tags.codigo = foro_temas_tags.codtag INNER JOIN foro_temas on foro_temas_tags.codtema = foro_temas.codigo")
        s.AppendLine("where foro_temas.codigo=" & codigotema & " AND foro_tags.codigo =" & codigotag)
        Return BD.GetDataset(s.ToString())
    End Function

    Shared Function delete_relationTemaTag(ByVal codigo As Integer) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("DELETE FROM foro_temas_tags")
        s.AppendLine("WHERE codtag = " & codigo)
        Return BD.Eliminar(s.ToString)
    End Function
    Shared Function Update_relationTematags(ByVal codigo As Integer, ByVal codtema As Integer, ByVal codtag As Integer) As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("UPDATE foro_temas_tags")
        s.AppendLine("SET codtema=" & codtema & ", codtag=" & codtag)
        s.AppendLine("where codigo=" & codigo)
        Return BD.GetDataset(s.ToString())
    End Function

End Class
