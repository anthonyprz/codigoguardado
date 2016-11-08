Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class encuestas

    Shared Function Get_Encuesta(ByVal codigo As String, ByRef dbConnection As SqlConnection) As SqlDataReader
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("SELECT codigo, codcategoria, nombre1, nombre2, pregunta1, pregunta2, inicio, fin, tipo, cantidad, activo, verresultado")
        s.AppendLine(" FROM encuestas")
        s.AppendLine(" WHERE codigo = " & codigo)
        Return BD.GetReader(s.ToString, dbConnection)
    End Function

    Shared Function Get_EncuestaLang(ByVal codigo As String, ByRef dbConnection As SqlConnection) As SqlDataReader
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("SELECT codigo, nombre" & current.Idioma & " as nombre, pregunta" & current.Idioma & " pregunta, tipo, (select count(codigo) from encuestasusuarios where codencuesta='" & codigo & "') as participantes")
        s.AppendLine(" FROM encuestas")
        s.AppendLine(" WHERE codigo = " & codigo)
        Return BD.GetReader(s.ToString, dbConnection)
    End Function

    'Shared Function Get_MisNoticias(ByVal codusuario As String) As DataSet
    '    Dim s As StringBuilder = New StringBuilder()
    '    s.AppendLine("SELECT codigo, (select nombre" & current.Idioma & " from noticiascategorias where codigo = codcategoria) categoria, foto, titulo" & current.Idioma & " titulo, subtitulo" & current.Idioma & " subtitulo, descripcion" & current.Idioma & " descripcion, fecha, activo ")
    '    s.AppendLine(" FROM noticias  ")
    '    s.AppendLine(" WHERE codusuario = '" & codusuario & "'")
    '    s.AppendLine(" ORDER BY fecha DESC")
    '    Return BD.GetDataset(s.ToString())
    'End Function

    Shared Function Get_Encuestas() As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("SELECT codigo, nombre" & current.Idioma & " nombre, inicio, fin, activo, tipo ")
        s.AppendLine(" FROM encuestas  ")
        s.AppendLine(" WHERE codID = '" & current.codID & "'")
        s.AppendLine(" ORDER BY inicio DESC")
        Return BD.GetDataset(s.ToString())
    End Function

    Shared Function Insert(ByVal codcategoria As Integer, ByVal codusuario As String, nombre1 As String, nombre2 As String, pregunta1 As String, pregunta2 As String, inicio As Date, fin As Date, tipo As Integer, cantidad As String, activo As Boolean, verresultado As Boolean) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("INSERT INTO encuestas ( codID, codusuario, codcategoria, nombre1, nombre2, pregunta1, pregunta2, inicio, fin, tipo, cantidad, activo, verresultado) ")
        s.AppendLine(" VALUES ('" & current.codID & "', '" & current.Usuario & "','" & codcategoria & "','" & nombre1 & "','" & nombre2 & "','" & pregunta1 & "','" & pregunta2 & "','" & Format(inicio, "dd/MM/yyyy HH:mm:ss") & "','" & Format(fin, "dd/MM/yyyy HH:mm:ss") & "','" & tipo & "','" & cantidad & "','" & activo & "','" & verresultado & "') ")
        Return BD.InsertID(s.ToString())
    End Function

    Shared Function Update(ByVal codigo As Integer, ByVal codcategoria As Integer, ByVal codusuario As String, nombre1 As String, nombre2 As String, pregunta1 As String, pregunta2 As String, inicio As Date, fin As Date, tipo As Integer, cantidad As Integer, activo As Boolean, verresultado As Boolean) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine(" UPDATE encuestas ")
        s.AppendLine(" SET codcategoria= '" & codcategoria & "', nombre1 = '" & nombre1 & "', nombre2 = '" & nombre2 & "', pregunta1 = '" & pregunta1 & "', pregunta2 = '" & pregunta2 & "', inicio = '" & Format(inicio, "dd/MM/yyyy HH:mm:ss") & "', fin= '" & Format(fin, "dd/MM/yyyy HH:mm:ss") & "', tipo= '" & tipo & "', cantidad= '" & cantidad & "', [activo]= '" & activo & "', verresultado= '" & verresultado & "'")
        s.AppendLine(" WHERE codigo= '" & codigo & "'")
        Return BD.Update(s.ToString())
    End Function

    Shared Function Delete_Encuestas(ByVal codigo As String) As Integer
        BD.Eliminar("DELETE FROM encuestasopciones WHERE codencuesta = " & codigo)
        BD.Eliminar("DELETE FROM encuestasusuarios WHERE codencuesta = " & codigo)
        Return BD.Eliminar("DELETE FROM encuestas WHERE codigo = " & codigo)
    End Function

#Region "encuestasopcinones"

    Shared Function Get_Opciones(ByVal codencuesta As Integer) As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("SELECT codigo, texto1, texto2, color, puntos, si, no ")
        s.AppendLine(" FROM encuestasopciones  ")
        s.AppendLine(" WHERE codencuesta= '" & codencuesta & "'")
        Return BD.GetDataset(s.ToString())
    End Function

    Shared Function Get_OpcionesLang(ByVal codencuesta As Integer) As DataSet
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("select codigo, texto, color,puntos, si, no, personas, case when  personas=0 then 0 else CONVERT(DECIMAL(10,2),CONVERT(decimal(10,2), puntos)/personas)  end as media from ( ")
        s.AppendLine("SELECT codigo, texto" & current.Idioma & " as texto, color, puntos, si, no, (select count(codigo) from encuestasusuarios where codencuesta = " & codencuesta & " ) as personas ")
        s.AppendLine(" FROM encuestasopciones  ")
        s.AppendLine(" WHERE codencuesta= '" & codencuesta & "'")
        s.AppendLine(" ) as t ")
        Return BD.GetDataset(s.ToString())
    End Function
    '  OR
    'SELECT encuestasopciones.codigo, texto1, texto2, color, puntos, si, no , count(encuestasusuarios.codigo) as perso
    'FROM encuestasopciones left outer join encuestasusuarios on encuestasopciones.codencuesta=encuestasusuarios.codencuesta
    'WHERE encuestasopciones.codencuesta= '9'
    'group by encuestasopciones.codigo, texto1, texto2, color, puntos, si ,no



    Shared Function Insert_Opciones(ByVal codencuesta As Integer, texto1 As String, texto2 As String, color As String) As Integer ', puntos As Integer, si As Integer, no As Integer) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine("INSERT INTO encuestasopciones ( codencuesta, texto1, texto2, color, puntos, si, no) ")
        s.AppendLine(" VALUES ('" & codencuesta & "', '" & texto1 & "','" & texto2 & "','" & color & "','" & 0 & "','" & 0 & "','" & 0 & "') ")
        Return BD.InsertID(s.ToString())
    End Function

    Shared Function Update_Opciones(ByVal codigo As Integer, ByVal codencuesta As Integer, texto1 As String, texto2 As String, color As String) As Integer ', puntos As Integer, si As Integer, no As Integer) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine(" UPDATE encuestasopciones ")
        s.AppendLine(" SET codencuesta= '" & codencuesta & "', texto1 = '" & texto1 & "', texto2 = '" & texto2 & "', color = '" & color & "'")
        s.AppendLine(" WHERE codigo= '" & codigo & "'")
        Return BD.Update(s.ToString())
    End Function

    Shared Function Delete_Opciones(ByVal codigo As String) As Integer
        Return BD.Eliminar("DELETE FROM encuestasopciones WHERE codigo = " & codigo)
    End Function



    Shared Function Update_Votacion(ByVal codigo As Integer) As Integer
        Dim s As StringBuilder = New StringBuilder()
        s.AppendLine(" UPDATE encuestasopciones ")
        s.AppendLine(" SET puntos = '0', si = '0', no = '0'")
        s.AppendLine(" WHERE codigo= '" & codigo & "'")
        Return BD.Update(s.ToString())
    End Function
    Shared Function Delete_Usuarios(ByVal codencuesta As String) As Integer
        Return BD.Eliminar("DELETE FROM encuestasusuarios WHERE codencuesta = " & codencuesta)
    End Function

#End Region


End Class
