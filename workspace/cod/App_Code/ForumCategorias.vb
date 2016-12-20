Imports System.Data
Imports System.Text
Imports Microsoft.VisualBasic

Public Class ForumCategorias
    Shared Function Get_Categorias() As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("SELECT codigo, nombre1, nombre2, descripcion1, descripcion2, fecha, activo")
        s.AppendLine(" FROM foro_categorias")
        s.AppendLine(" WHERE codID = '" & current.codID & "'")
        Return BD.GetDataset(s.ToString())
    End Function
    Shared Function Get_Categoria(ByVal codigo As Integer) As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("SELECT nombre1, nombre2, descripcion1, descripcion2, fecha, activo")
        s.AppendLine(" FROM foro_categorias")
        s.AppendLine(" WHERE codigo=" & codigo)
        Return BD.GetDataset(s.ToString())
    End Function
    Shared Function Update_Categorias(ByVal codigo As String, ByVal nombre1 As String, ByVal nombre2 As String, ByVal descripcion1 As String, ByVal descripcion2 As String) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("UPDATE foro_categorias")
        s.AppendLine(" SET nombre1= '" & nombre1 & "', nombre2= '" & nombre2 & "', descripcion1= '" & descripcion1 & "', descripcion2= '" & descripcion2 & "', fecha=GETDATE()")
        s.AppendLine(" WHERE codigo= '" & codigo & "'")
        Return BD.Update(s.ToString())
    End Function
    Shared Function Insert_Categorias(ByVal nombre1 As String, ByVal nombre2 As String, ByVal descripcion1 As String, ByVal descripcion2 As String) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("INSERT INTO foro_categorias")
        s.AppendLine("(codID, nombre1, nombre2, descripcion1, descripcion2, fecha)")
        s.AppendLine(" VALUES ('" & current.codID & "', '" & nombre1 & "', '" & nombre2 & "', '" & descripcion1 & "', '" & descripcion2 & "',GETDATE()) ")
        Return BD.InsertID(s.ToString())
    End Function

    Shared Function Get_Count(ByVal codigo As String, ByVal pagina As String) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("SELECT count(codigo) FROM foro_categorias ")
        s.AppendLine("WHERE codcategoria = " & codigo)
        Return BD.GetInteger(s.ToString)
    End Function

    Shared Function Delete_Categoria(ByVal codigo As String) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("DELETE FROM foro_categorias")
        s.AppendLine("WHERE codigo = " & codigo)
        Return BD.Eliminar(s.ToString)
    End Function
End Class
