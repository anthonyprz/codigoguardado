Imports Microsoft.VisualBasic
Imports System.Data

Public Class categorias
    Shared Function Get_Categoria(ByVal codigo As Integer, ByVal pagina As String) As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("SELECT nombre1, nombre2, color, activo")
        s.AppendLine(" FROM " & pagina)
        s.AppendLine(" WHERE codigo=" & codigo)
        Return BD.GetDataset(s.ToString())
    End Function

    Shared Function Get_Categorias(ByVal pagina As String) As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("SELECT codigo, nombre" & current.Idioma & " nombre, color, activo ")
        s.AppendLine(" FROM " & pagina)
        s.AppendLine(" WHERE codID = '" & current.codID & "'")
        Return BD.GetDataset(s.ToString())
    End Function

    Shared Function Get_CategoriasActivo(ByVal pagina As String) As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("SELECT codigo, nombre" & current.Idioma & " nombre, color, activo ")
        s.AppendLine(" FROM " & pagina)
        s.AppendLine(" WHERE activo='true' AND codID = '" & current.codID & "'")
        Return BD.GetDataset(s.ToString())
    End Function

    Shared Function Insert_Categorias(ByVal pagina As String, ByVal nombre1 As String, ByVal nombre2 As String, ByVal color As String, ByVal activo As Boolean) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("INSERT INTO " & pagina)
        s.AppendLine("(codID, nombre1, nombre2, color, activo)")
        s.AppendLine(" VALUES ('" & current.codID & "', '" & nombre1 & "', '" & nombre2 & "', '" & color & "', '" & activo & "') ")
        Return BD.InsertID(s.ToString())
    End Function
    Shared Function Update_Categorias(ByVal codigo As String, ByVal pagina As String, ByVal nombre1 As String, ByVal nombre2 As String, ByVal color As String, ByVal activo As Boolean) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("UPDATE " & pagina)
        s.AppendLine(" SET nombre1= '" & nombre1 & "', nombre2= '" & nombre2 & "', color= '" & color & "', activo= '" & activo & "'")
        s.AppendLine(" WHERE codigo= '" & codigo & "'")
        Return BD.Update(s.ToString())
    End Function
    Shared Function Delete_Categoria(ByVal codigo As String, ByVal pagina As String) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("DELETE FROM " & pagina)
        s.AppendLine("WHERE codigo = " & codigo)
        Return BD.Eliminar(s.ToString)
    End Function

    Shared Function Get_Count(ByVal codigo As String, ByVal pagina As String) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("SELECT count(codigo) FROM " & pagina)
        s.AppendLine("WHERE codcategoria = " & codigo)
        Return BD.GetInteger(s.ToString)
    End Function


    'Shared Function Get_Categoria(ByVal codigo As Integer, ByVal pagina As Integer) As DataSet
    '    Dim s As StringBuilder = New StringBuilder()
    '    s.AppendLine("SELECT nombre1, nombre2, color")
    '    Select Case pagina
    '        Case amigables.Page.noticiascat
    '            s.AppendLine(" FROM noticiascategorias ")
    '        Case amigables.Page.agendacat
    '            s.AppendLine(" FROM agendacategorias ")
    '    End Select
    '    s.AppendLine(" WHERE codigo=" & codigo)
    '    Return BD.GetDataset(s.ToString())
    'End Function

    'Shared Function Get_Categorias(ByVal pagina As String) As DataSet
    '    Dim s As StringBuilder = New StringBuilder()
    '    s.AppendLine("SELECT codigo, nombre" & current.Idioma & " nombre, color ")

    '    Select Case pagina
    '        Case amigables.Page.noticiascat
    '            s.AppendLine(" FROM noticiascategorias ")
    '        Case amigables.Page.agendacat
    '            s.AppendLine(" FROM agendacategorias ")
    '        Case Else
    '            s.AppendLine(" FROM " & pagina)
    '    End Select
    '    s.AppendLine(" WHERE codID = '" & current.codID & "'")
    '    Return BD.GetDataset(s.ToString())
    'End Function

    'Shared Function Insert_Categorias(ByVal pagina As Integer, ByVal nombre1 As String, ByVal nombre2 As String, ByVal color As String) As Integer
    '    Dim s As StringBuilder = New StringBuilder()
    '    Select Case pagina
    '        Case amigables.Page.noticiascat
    '            s.AppendLine("INSERT INTO noticiascategorias ")
    '        Case amigables.Page.agendacat
    '            s.AppendLine("INSERT INTO agendacategorias ")
    '    End Select
    '    s.AppendLine("(codID, nombre1, nombre2, color)")
    '    s.AppendLine(" VALUES ('" & current.codID & "', '" & nombre1 & "', '" & nombre2 & "', '" & color & "') ")
    '    Return BD.Insert(s.ToString())
    'End Function
    'Shared Function Update_Categorias(ByVal codigo As Integer, ByVal pagina As Integer, ByVal nombre1 As String, ByVal nombre2 As String, ByVal color As String) As Integer
    '    Dim s As StringBuilder = New StringBuilder()
    '    Select Case pagina
    '        Case amigables.Page.noticiascat
    '            s.AppendLine("UPDATE noticiascategorias ")
    '        Case amigables.Page.agendacat
    '            s.AppendLine("UPDATE agendacategorias ")
    '    End Select
    '    s.AppendLine(" SET nombre1= '" & nombre1 & "', nombre2= '" & nombre2 & "', color= '" & color & "'")
    '    s.AppendLine(" WHERE codigo= '" & codigo & "'")
    '    Return BD.Update(s.ToString())
    'End Function
    'Shared Function Delete_Categoria(ByVal codigo As String, ByVal pagina As Integer) As Integer
    '    Dim s As StringBuilder = New StringBuilder()
    '    Select Case pagina
    '        Case amigables.Page.noticiascat
    '            s.AppendLine("DELETE FROM noticiascategorias ")
    '        Case amigables.Page.agendacat
    '            s.AppendLine("DELETE FROM agendacategorias ")
    '    End Select
    '    s.AppendLine("WHERE codigo = " & codigo)
    '    Return BD.Eliminar(s.ToString)
    'End Function

End Class
