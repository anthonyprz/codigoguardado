Imports System.Data
Imports Telerik.Web.UI

Partial Class Default2
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Page.Theme = current.Skin
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
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

        lencuestas.Text = Resources.lang.encuestas.ToUpper
        For x As Integer = 0 To RGlista.Columns.Count - 1
            Dim col As String = RGlista.Columns(x).UniqueName
            RGlista.Columns(x).HeaderText = GetGlobalResourceObject("lang", col.ToLower).ToString().ToUpper
        Next
    End Sub

    Protected Sub RGlista_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGlista.NeedDataSource
        CargarDatos()
    End Sub
    Private Sub CargarDatos()
        Dim ds As DataSet = encuestas.Get_Encuestas()
        If ds.Tables.Count > 0 Then
            RGlista.DataSource = ds
        End If
    End Sub

    Protected Sub RGlista_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RGlista.ItemCommand
        If e.CommandName = RadGrid.EditSelectedCommandName Then
            e.Canceled = True
            Dim codselect As String = RGlista.SelectedValue
            If Not String.IsNullOrEmpty(codselect) Then
                Abrir(RGlista.SelectedValue)
            End If
        ElseIf e.CommandName = RadGrid.InitInsertCommandName Then
            e.Canceled = True
            Abrir(0)
        ElseIf e.CommandName = RadGrid.DeleteSelectedCommandName Then
            'If RGlista.SelectedItems.Count > 0 Then
            '    Dim codselect As String
            '    For Each sel As GridDataItem In RGlista.SelectedItems()
            '        codselect = sel.GetDataKeyValue("codigo")
            '        If Not String.IsNullOrEmpty(codselect) Then
            '            encuestas.Delete_Encuestas(codselect)
            '        End If
            '    Next
            '    'CargarDatos()
            '    RGlista.DataBind()
            'End If
        ElseIf e.CommandName = RadGrid.FilterCommandName Or e.CommandName = RadGrid.RebindGridCommandName Then
            RGlista.Rebind()
        ElseIf e.CommandName = "clicklink" Then
           Dim codigo As String = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("codigo").ToString()
            'Dim k = (DirectCast(e.Item, GridDataItem)).GetDataKeyValue("ID").ToString
            Abrir(codigo)
        End If
    End Sub
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
            For Each sel As GridDataItem In RGlista.SelectedItems()
                codselect = sel.GetDataKeyValue("codigo")
                If Not String.IsNullOrEmpty(codselect) Then
                    encuestas.Delete_Encuestas(codselect)
                End If
            Next
            'CargarDatos()
            RGlista.Rebind()
        End If
    End Sub

    Private Sub Abrir(ByVal codselect As String)
        Plista.Visible = False
        Pdatos.Visible = True

        'wcENCUESTA._codselect = codselect
        wcENCUESTA.CargarDatos(codselect)
    End Sub

    Function enabledisable() As Boolean
        Return True
    End Function

    Private Sub ucHide(ByVal sender As Object, ByVal e As EventArgs) Handles wcENCUESTA.on_ucHIDE
        RGlista.Rebind()
        Pdatos.Visible = False
        Plista.Visible = True
    End Sub


    'Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
    '    If Not enabledisable() Then
    '        Dim cmdItem As GridItem = RGlista.MasterTableView.GetItems(GridItemType.CommandItem)(0)
    '        Dim bedit As LinkButton = TryCast(cmdItem.FindControl("Beditatu"), LinkButton)
    '        bedit.Text = Resources.idioma.ver
    '        bedit.CssClass = "btnVer"
    '    End If
    'End Sub

    'Protected Sub RGlista_PreRender(sender As Object, e As EventArgs) Handles RGlista.PreRender
    '    If RGlista.PageCount - 1 < RGlista.CurrentPageIndex Then
    '        RGlista.CurrentPageIndex = RGlista.CurrentPageIndex - 1
    '        RGlista.Rebind()
    '    End If
    'End Sub

End Class
