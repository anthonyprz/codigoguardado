
Imports System.Data
Imports System.Drawing
Imports Telerik.Web.UI

Partial Class Default2
    Inherits System.Web.UI.Page

    Dim _pagina As String = "encuestascategorias"
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Page.Theme = current.Skin
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Ltitulo.Text = Resources.lang.encuestascat.ToUpper

        If Request.Params("__EVENTTARGET") = "dobleclick" Then
            Dim args = Request.Params("__EVENTARGUMENT")
            Abrir(args)
        End If

        If Not IsPostBack Then
            Me.CargarTextos()
        End If
    End Sub

    Private Sub CargarTextos()
        Bbai.ToolTip = Resources.lang.si
        Bez.ToolTip = Resources.lang.no

        Ltit.Text = Resources.lang.categoria.ToUpper
        lnombre.Text = Resources.lang.nombre.ToUpper
        CBactivo.Text = Resources.lang.activo.ToUpper

        For x As Integer = 0 To RGlista.Columns.Count - 1
            Dim col As String = RGlista.Columns(x).UniqueName
            RGlista.Columns(x).HeaderText = GetGlobalResourceObject("lang", col.ToLower).ToString().ToUpper
        Next

        Bcancelar.Text = Resources.idioma.cancelar.ToUpper
        Bguardar.Text = Resources.idioma.guardar.ToUpper
    End Sub

    Protected Sub RGlista_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGlista.NeedDataSource
        CargarDatos()
    End Sub

    Private Sub CargarDatos()
        Dim ds = categorias.Get_Categorias(_pagina)
        If ds.Tables.Count > 0 Then
            RGlista.DataSource = ds
        End If
    End Sub

    Protected Sub RGlista_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RGlista.ItemCommand
        If e.CommandName = RadGrid.EditSelectedCommandName Then
            e.Canceled = True
            Dim codselect As String = RGlista.SelectedValue
            If Not String.IsNullOrEmpty(codselect) Then
                Abrir(codselect)
            End If
        ElseIf e.CommandName = RadGrid.InitInsertCommandName Then
            e.Canceled = True
            HFcodigo.Value = String.Empty
            TBnombre1.Text = String.Empty
            TBnombre2.Text = String.Empty
            RadColorPicker1.SelectedColor = Nothing
            Abrir(Nothing)
        ElseIf e.CommandName = RadGrid.DeleteSelectedCommandName Then
            'If RGlista.SelectedItems.Count > 0 Then
            '    Dim codselect As String
            '    Dim nombres As String = String.Empty
            '    For Each sel As GridDataItem In RGlista.SelectedItems()
            '        codselect = sel.GetDataKeyValue("codigo")
            '        If Not String.IsNullOrEmpty(codselect) Then
            '            Dim count As Integer = 0
            '            Select Case _pagina
            '                Case "agendacategorias"
            '                    count = categorias.Get_Count(codselect, "agenda")
            '                Case "encuestascategorias"
            '                    count = categorias.Get_Count(codselect, "encuestas")
            '                Case "noticiascategorias"
            '                    count = categorias.Get_Count(codselect, "noticias")
            '            End Select
            '            If count > 0 Then
            '                nombres = nombres & sel.GetDataKeyValue("nombre").ToString & "\n "
            '            Else
            '                categorias.Delete_Categoria(codselect, _pagina)
            '            End If
            '        End If
            '    Next
            '    If nombres.Length > 0 Then
            '        RadAjaxManager1.ResponseScripts.Add(String.Format("alert('" & Resources.idioma.errorborrar & "\n" & nombres & "')"))
            '    End If
            '    RGlista.DataBind()
            'End If
        ElseIf e.CommandName = RadGrid.FilterCommandName Or e.CommandName = RadGrid.RebindGridCommandName Then
            RGlista.Rebind()
        ElseIf e.CommandName = "clicklink" Then
            Dim codigo As String = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("codigo").ToString()
            'Dim codigo = (DirectCast(e.Item, GridDataItem)).GetDataKeyValue("codigo").ToString
            Abrir(codigo)
            'ElseIf e.CommandName = "ExportExcel" Then
            '    PrepararExportar()
            '    RGlista.MasterTableView.ExportToExcel()
            'ElseIf e.CommandName = "ExportWord" Then
            '    PrepararExportar()
            '    RGlista.MasterTableView.ExportToWord()
            'ElseIf e.CommandName = "ExportPdf" Then
            '    PrepararExportar()
            '    RGlista.MasterTableView.ExportToPdf()
        End If
    End Sub

    'Protected Sub PrepararExportar()
    '    RGlista.MasterTableView.GetColumn("ClientSelectColumn").Visible = False
    '    RGlista.MasterTableView.GetColumn("ex").Visible = True
    '    RGlista.MasterTableView.GetColumn("ex").HeaderText = RGlista.MasterTableView.GetColumn("nombre").HeaderText
    '    RGlista.MasterTableView.GetColumn("nombre").Visible = False

    '    RGlista.ExportSettings.FileName = Ltit.Text
    '    RGlista.ExportSettings.ExportOnlyData = True
    'End Sub

    Function showModal() As String
        Return "return showModal('" & RGlista.ClientID.ToString & "','" & Resources.lang.alertaeliminar & "','" & Bbai.ClientID.ToString & "','" & Bez.ClientID.ToString & "','" & modalDialog.ClientID.ToString & "');"
    End Function

    Protected Sub Bez_Click(sender As Object, e As EventArgs) Handles Bez.Click
        RGlista.Rebind()
        RadAjaxManager.GetCurrent(Page).ResponseScripts.Add("return hide();")
    End Sub

    Protected Sub Bbai_Click(sender As Object, e As EventArgs) Handles Bbai.Click
        RadAjaxManager.GetCurrent(Page).ResponseScripts.Add("return hide();")
        If RGlista.SelectedItems.Count > 0 Then
            Dim codselect As String
            Dim nombres As String = String.Empty
            For Each sel As GridDataItem In RGlista.SelectedItems()
                codselect = sel.GetDataKeyValue("codigo")
                If Not String.IsNullOrEmpty(codselect) Then
                    Dim count As Integer = 0
                    count = categorias.Get_Count(codselect, "encuestas")

                    If count > 0 Then
                        nombres = nombres & sel.GetDataKeyValue("nombre").ToString & "<br />"
                    Else
                        categorias.Delete_Categoria(codselect, _pagina)
                    End If
                End If
            Next
            If nombres.Length > 0 Then
                RadAjaxManager.GetCurrent(Page).ResponseScripts.Add("return showModal('','" & Resources.lang.errorborrar & "<br />" & nombres & "','" & Bbai.ClientID.ToString & "','" & Bez.ClientID.ToString & "','" & modalDialog.ClientID.ToString & "');")
            End If
            Reset()
        End If
    End Sub

    Private Sub Abrir(ByVal codselect As String)
        Pcategoria.Visible = True
        HFcodigo.Value = codselect
        If Not IsNothing(codselect) Then
            Dim dt As DataTable = categorias.Get_Categoria(codselect, _pagina).Tables(0)
            If dt.Rows.Count > 0 Then
                TBnombre1.Text = dt.Rows(0).Item("nombre1").ToString
                TBnombre2.Text = dt.Rows(0).Item("nombre2").ToString
                Try : RadColorPicker1.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0).Item("color").ToString) : Catch : End Try
                CBactivo.Checked = CBool(dt.Rows(0).Item("activo"))
            End If
        End If
    End Sub



    Private Sub Reset()
        HFcodigo.Value = String.Empty
        TBnombre1.Text = String.Empty
        TBnombre2.Text = String.Empty
        RadColorPicker1.SelectedColor = Nothing
        CBactivo.Checked = True

        RGlista.Rebind()
        Pcategoria.Visible = False
    End Sub
    Protected Sub Bcancelar_Click(sender As Object, e As EventArgs) Handles Bcancelar.Click
        Reset()
    End Sub

    Protected Sub Bguardar_Click(sender As Object, e As EventArgs) Handles Bguardar.Click
        If Page.IsValid Then
            Dim codigo As String = HFcodigo.Value
            Dim nombre2 = IIf(String.IsNullOrEmpty(Trim(TBnombre2.Text)), Trim(TBnombre1.Text), Trim(TBnombre2.Text))
            If Not String.IsNullOrEmpty(codigo) Then
                categorias.Update_Categorias(codigo, _pagina, Trim(TBnombre1.Text), nombre2, ColorTranslator.ToHtml(RadColorPicker1.SelectedColor), CBactivo.Checked)
            Else
                categorias.Insert_Categorias(_pagina, Trim(TBnombre1.Text), nombre2, ColorTranslator.ToHtml(RadColorPicker1.SelectedColor), CBactivo.Checked)
            End If
            Reset()
        End If
    End Sub

    Function enabledisable() As Boolean
        Return True
    End Function


    'Protected Sub RGlista_PreRender(sender As Object, e As EventArgs) Handles RGlista.PreRender
    '    If RGlista.PageCount - 1 < RGlista.CurrentPageIndex Then
    '        RGlista.CurrentPageIndex = RGlista.CurrentPageIndex - 1
    '        RGlista.Rebind()
    '    End If
    'End Sub


End Class
