Imports Telerik.Web.UI
Imports System.Data
Imports Microsoft.AspNet.Identity.EntityFramework
Imports System.Data.SqlClient
Imports Newtonsoft.Json
Imports System.Drawing

Partial Class web_encuesta
    Inherits System.Web.UI.UserControl


    Public Event on_ucHIDE As EventHandler
    Private Sub ucHIDE()
        RaiseEvent on_ucHIDE(Me, EventArgs.Empty)
    End Sub

    Public _codselect As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim adper = current.per_formularios
        'Dim permiso = IIf(adper.admin, 2, adper.permiso)
        'If permiso = 1 Then
        '    RadPageView1.Enabled = False
        '    RadPageView2.Enabled = False
        '    Bguardar.Visible = False
        '    Bnotificacion.Visible = False
        '    Bresetear.Visible = False
        'End If

        If Not Page.IsPostBack Then
            CargarTextos()
            'CargarCombos()
            CargarGrid(1)
            RadGrid1.DataBind()
        End If
    End Sub

    'Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
    '    If String.IsNullOrEmpty(HFCodselect.Value) Then
    '        HFCodselect.Value = _codselect
    '        wcENCUESTAVOT._codselect = _codselect
    '        CargarDatos()
    '    End If
    'End Sub

    Protected Sub CargarCombos()
        Dim ds = categorias.Get_CategoriasActivo("encuestascategorias")
        If ds.Tables.Count > 0 Then
            CBcategoria.DataSource = ds
            CBcategoria.DataBind()
        End If
    End Sub
    Protected Sub CargarCombosActivo()
        Dim ds = categorias.Get_CategoriasActivo("encuestascategorias")
        If ds.Tables.Count > 0 Then
            CBcategoria.DataSource = ds
            CBcategoria.DataBind()
        End If
    End Sub

    Private Sub CargarTextos()

        RadTabStrip1.Tabs(0).Text = Resources.lang.encuesta.ToUpper
        RadTabStrip1.Tabs(1).Text = Resources.lang.votacion.ToUpper

        Ltit.Text = Resources.lang.encuesta.ToUpper
        Lcategoria.Text = Resources.lang.categoria.ToUpper
        Lfechas.Text = Resources.lang.fecha.ToUpper
        Lnombre.Text = Resources.lang.nombre.ToUpper
        Lactivo.Text = Resources.lang.activo.ToUpper
        CBactivo.Text = Resources.lang.activo.ToUpper
        Ltipo.Text = Resources.lang.tipo.ToUpper
        CBtipo.Items(0).Text = Resources.lang.seleccionarentrevariasopciones
        CBtipo.Items(1).Text = Resources.lang.valoracionporpuntos
        CBtipo.Items(2).Text = Resources.lang.sino
        Lcantidad.Text = Resources.lang.cantidad.ToUpper
        Lpregunta.Text = Resources.lang.pregunta.ToUpper
        Lopciones.Text = Resources.lang.opciones.ToUpper
        Lverresultado.Text = Resources.lang.verresultado.ToUpper
        CBverresultado.Text = Resources.lang.verresultado.ToUpper

        Bcancelar.Text = Resources.idioma.cancelar.ToUpper
        Bguardar.Text = Resources.idioma.guardar.ToUpper
        Bnotificacion.Text = Resources.lang.enviarnotificacion.ToUpper
        Bresetear.Text = Resources.lang.resetearvotacion.ToUpper

    End Sub

    Public Sub CargarDatos(ByVal codselect As String)
        HFCodselect.Value = codselect
        If Not IsNothing(codselect) AndAlso Not String.IsNullOrEmpty(codselect) AndAlso codselect > 0 Then
            CargarCombos()
            Dim dbConnection = current.SqlConnection
            Dim reader As SqlDataReader = encuestas.Get_Encuesta(codselect, dbConnection)
            Try
                If reader.Read Then
                    CBcategoria.SelectedValue = CInt(reader("codcategoria"))
                    TBnombre1.Text = reader("nombre1").ToString
                    TBnombre2.Text = reader("nombre2").ToString
                    TBpregunta1.Text = reader("pregunta1").ToString
                    TBpregunta2.Text = reader("pregunta2").ToString
                    RadDateTimePicker1.SelectedDate = CDate(reader("inicio"))
                    RadDateTimePicker2.SelectedDate = CDate(reader("fin"))
                    CBtipo.SelectedValue = CInt(reader("tipo"))
                    Dim k = CInt(reader("tipo"))
                    If CInt(reader("tipo")) <> 1 Then
                        Dcantidad.Attributes.Add("hidden", "")
                    Else
                        Dcantidad.Attributes.Remove("hidden")
                    End If
                    TBcantidad.Text = CInt(reader("cantidad"))
                    CBactivo.Checked = CBool(reader("activo"))
                    CBverresultado.Checked = CBool(reader("verresultado"))
                End If
            Catch ex As Exception
            Finally
                reader.Close()
                dbConnection.Close()
            End Try

            Dim dt As DataTable = encuestas.Get_Opciones(codselect).Tables(0)

            ViewState("dt") = dt
            CargarGrid()
            RadGrid1.DataBind()
        Else
            RadDateTimePicker1.SelectedDate = Today
            RadDateTimePicker2.SelectedDate = Today
            CargarCombosActivo()
        End If
    End Sub

    Private Sub Reset()
        'Dim Pusu As Control = Me.FindControl("Dform")
        'For Each ctl As Control In Pusu.Controls
        '    If TypeOf ctl Is TextBox Then
        '        CType(ctl, TextBox).Text = String.Empty
        '    ElseIf TypeOf ctl Is CheckBox Then
        '        CType(ctl, CheckBox).Checked = False
        '    End If
        'Next
        RadTabStrip1.SelectedIndex = 0
        RadMultiPage1.SelectedIndex = 0

        CBcategoria.ClearSelection()
        TBnombre1.Text = String.Empty
        TBnombre2.Text = String.Empty
        RadDateTimePicker1.Clear()
        RadDateTimePicker2.Clear()
        Derror.Visible = False

        CBactivo.Checked = False
        CBverresultado.Checked = False
        CBtipo.ClearSelection()

        TBcantidad.Text = 1
        TBpregunta1.Text = String.Empty
        TBpregunta2.Text = String.Empty

        _codselect = Nothing
        HFCodselect.Value = String.Empty
        HFdelete.Value = String.Empty


        CargarGrid(1)
        RadGrid1.DataBind()

        wcENCUESTAVOT.Reset()

        ucHIDE()

    End Sub

    Protected Sub Bcancelar_Click(sender As Object, e As EventArgs) Handles Bcancelar.Click
        Reset()
    End Sub

    Protected Sub Bguardar_Click(sender As Object, e As EventArgs) Handles Bguardar.Click
        If Page.IsValid Then
            Guardar()
            Reset()
        End If
    End Sub
    Protected Sub Guardar()
        Dim inicio = RadDateTimePicker1.SelectedDate
        Dim fin = CDate(RadDateTimePicker2.SelectedDate)

        Dim nombre2 = IIf(String.IsNullOrEmpty(Trim(TBnombre2.Text)), Trim(TBnombre1.Text), Trim(TBnombre2.Text))

        If String.IsNullOrEmpty(HFCodselect.Value) Or HFCodselect.Value = "0" Then 'insert
            Dim cod = encuestas.Insert(CBcategoria.SelectedValue, current.Usuario, Trim(TBnombre1.Text), nombre2, TBpregunta1.Text, TBpregunta2.Text, inicio, fin, CBtipo.SelectedValue, TBcantidad.Text, CBool(CBactivo.Checked), CBool(CBverresultado.Checked))
            HFCodselect.Value = cod
        Else 'update
            encuestas.Update(HFCodselect.Value, CBcategoria.SelectedValue, current.Usuario, Trim(TBnombre1.Text), nombre2, TBpregunta1.Text, TBpregunta2.Text, inicio, fin, CBtipo.SelectedValue, TBcantidad.Text, CBool(CBactivo.Checked), CBool(CBverresultado.Checked))
        End If
        Dim dt As DataTable = TryCast(ViewState("dt"), DataTable)
        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                Select Case row.RowState
                    Case DataRowState.Added
                        encuestas.Insert_Opciones(HFCodselect.Value, row("texto1"), row("texto2"), row("color"))
                    Case DataRowState.Modified
                        encuestas.Update_Opciones(row("codigo"), HFCodselect.Value, row("texto1"), row("texto2"), row("color"))
                End Select
            Next
        End If
        If Not String.IsNullOrEmpty(HFdelete.Value) Then
            Dim codigos = HFdelete.Value.Split(";")
            For Each cod In codigos
                If Not String.IsNullOrEmpty(cod) AndAlso cod > 0 Then
                    encuestas.Delete_Opciones(cod)
                End If
            Next
        End If
    End Sub
    Protected Sub CargarGrid(Optional ByVal inicio As Integer = 0)
        If inicio = 1 Then
            Dim dt As New DataTable()
            dt.Columns.AddRange(New DataColumn(3) {New DataColumn("codigo"), New DataColumn("texto1"), New DataColumn("texto2"), New DataColumn("color")})
            ViewState("dt") = dt
        End If
        RadGrid1.DataSource = TryCast(ViewState("dt"), DataTable)
    End Sub

    Protected Sub RadGrid1_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid1.ItemCommand
        'RadGrid1.EditIndex = -1 ????
        Select Case e.CommandName
            Case RadGrid.EditCommandName
                CargarGrid()
            Case RadGrid.UpdateCommandName
                Dim dataitem = DirectCast(e.Item, GridDataItem)
                Dim codigo = dataitem.GetDataKeyValue("codigo").ToString

                Dim rowindex = e.Item.ItemIndex

                Dim item As GridEditableItem = TryCast(e.Item, GridEditableItem)
                'Dim texto1 = TryCast(item.EditManager.GetColumnEditor("texto1"), GridTextBoxColumnEditor).Text
                'Dim texto2 = TryCast(item.EditManager.GetColumnEditor("texto2"), GridTextBoxColumnEditor).Text

                Dim texto1 = CType(item.FindControl("TBtexto1"), TextBox).Text
                Dim texto2 = CType(item.FindControl("TBtexto2"), TextBox).Text

                Dim color = ColorTranslator.ToHtml(CType(item.FindControl("RadColorPicker1"), RadColorPicker).SelectedColor)

                Dim dt As DataTable = TryCast(ViewState("dt"), DataTable)
                dt.Rows(rowindex)("texto1") = texto1
                dt.Rows(rowindex)("texto2") = texto2
                dt.Rows(rowindex)("color") = color
                ViewState("dt") = dt
                CargarGrid()
            Case RadGrid.CancelCommandName
                CargarGrid()

            Case RadGrid.InitInsertCommandName
                CargarGrid()

            Case RadGrid.PerformInsertCommandName
                Dim item As GridEditableItem = TryCast(e.Item, GridEditableItem)
                'Dim texto1 = TryCast(item.EditManager.GetColumnEditor("texto1"), GridTextBoxColumnEditor).Text
                'Dim texto2 = TryCast(item.EditManager.GetColumnEditor("texto2"), GridTextBoxColumnEditor).Text

                Dim texto1 = CType(item.FindControl("TBtexto1"), TextBox).Text
                Dim texto2 = CType(item.FindControl("TBtexto2"), TextBox).Text

                Dim color = ColorTranslator.ToHtml(CType(item.FindControl("RadColorPicker1"), RadColorPicker).SelectedColor)

                Dim dt As DataTable = TryCast(ViewState("dt"), DataTable)
                dt.Rows.Add(0, texto1, texto2, color)
                CargarGrid()
            Case RadGrid.DeleteCommandName
                Dim dataitem = DirectCast(e.Item, GridDataItem)
                Dim codigo = dataitem.GetDataKeyValue("codigo").ToString
                HFdelete.Value = HFdelete.Value & codigo & ";"
                Dim rowindex = e.Item.ItemIndex
                Dim dt As DataTable = TryCast(ViewState("dt"), DataTable)
                'dt.Rows(rowindex).Delete()
                dt.Rows.Remove(dt.Rows(rowindex))
                ViewState("dt") = dt
                CargarGrid()
        End Select
    End Sub

    Function HTML2Color(ByVal hex As String) As Color
        Return ColorTranslator.FromHtml(hex)
    End Function

    Function myCStr(ByVal dbstring As Object) As String
        If IsDBNull(dbstring) Then
            Return ("#ffffff")
        Else
            Return CStr(dbstring)
        End If
    End Function

    Protected Sub Bresetear_Click(sender As Object, e As EventArgs) Handles Bresetear.Click
        If HFCodselect.Value > 0 Then
            Dim codopcion As Integer
            Dim dt As DataTable = encuestas.Get_Opciones(HFCodselect.Value).Tables(0)
            For Each row In dt.Rows
                codopcion = row("codigo")
                encuestas.Update_Votacion(codopcion)
            Next
            encuestas.Delete_Usuarios(HFCodselect.Value)
        End If
    End Sub

    Protected Sub Bnotificacion_Click(sender As Object, e As EventArgs) Handles Bnotificacion.Click
        Guardar()

        Dim ip = System.Net.Dns.GetHostAddresses(Request.Url.Host)(0).ToString()
        If ip.Length < 7 Then ip = "37.187.250.215"

        Dim window1 As New RadWindow()
        'window1.NavigateUrl = "http://151.80.129.32/node/helloworld/hello.js" & "?titulo=4&mensaje=" & TBnombre1.Text & "/" & TBnombre2.Text & "&codigo=" & HFCodselect.Value
        window1.NavigateUrl = "http://" & ip & "/appayto/noti.js" & "?codID=" & current.codID & "&titulo=4&mensaje=" & TBnombre1.Text & "/" & TBnombre2.Text & "&codigo=" & HFCodselect.Value
        window1.VisibleOnPageLoad = True
        window1.ID = "RadWindow1"
        window1.Width = 500
        window1.Height = 300
        window1.Modal = True
        window1.Behaviors = WindowBehaviors.Close
        window1.VisibleStatusbar = False
        window1.DestroyOnClose = True
        window1.OnClientClose = "closea"

        Me.Dform.Controls.Add(window1)

    End Sub

    Protected Sub RadDateTimePicker1_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs) Handles RadDateTimePicker1.SelectedDateChanged
        If RadDateTimePicker1.SelectedDate > RadDateTimePicker2.SelectedDate Then
            RadDateTimePicker2.SelectedDate = CDate(RadDateTimePicker1.SelectedDate).AddDays(1)
        End If
    End Sub

    Protected Sub RadDateTimePicker2_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs) Handles RadDateTimePicker2.SelectedDateChanged
        If RadDateTimePicker1.SelectedDate > RadDateTimePicker2.SelectedDate Then
            Derror.Visible = True
        Else
            Derror.Visible = False
        End If
    End Sub
End Class
