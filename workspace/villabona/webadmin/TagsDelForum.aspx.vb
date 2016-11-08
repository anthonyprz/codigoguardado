Imports System.Data
Imports System.Drawing
Imports Telerik.Web.UI
Partial Class TagsDelForum
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Ltitulo.Text = Resources.lang.encuestascat.ToUpper

        If Request.Params("__EVENTTARGET") = "dobleclick" Then
            Dim args = Request.Params("__EVENTARGUMENT")
            '   Abrir(args)
        End If

        If Not IsPostBack Then
            '  Me.CargarTextos()
        End If
    End Sub
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Page.Theme = current.Skin
    End Sub
    Function enabledisable() As Boolean
        Return True
    End Function
    Function showModal() As String
        Return "return showModal('" & RGlista.ClientID.ToString & "','" & Resources.lang.alertaeliminar & "','" & Bbai.ClientID.ToString & "','" & Bez.ClientID.ToString & "','" & modalDialog.ClientID.ToString & "');"
    End Function
    Protected Sub RGlista_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGlista.NeedDataSource
        CargarDatos()
    End Sub
    Private Sub CargarDatos()
        Dim ds = ForumTags.Get_Tags()
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
            TBnombre.Text = String.Empty
            'RadColorPicker1.SelectedColor = Nothing
            Abrir(Nothing)
        ElseIf e.CommandName = RadGrid.DeleteSelectedCommandName Then
        ElseIf e.CommandName = RadGrid.FilterCommandName Or e.CommandName = RadGrid.RebindGridCommandName Then
            RGlista.Rebind()
        ElseIf e.CommandName = "clicklink" Then
            Dim codigo As String = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("codigo").ToString()
            Abrir(codigo)

        End If
    End Sub

    Private Sub Abrir(ByVal codselect As String)
        Pcategoria.Visible = True
        HFcodigo.Value = codselect
        'CargarCombos()
        If Not IsNothing(codselect) Then
            Dim dt As DataTable = ForumTags.Get_tag(codselect).Tables(0)
            If dt.Rows.Count > 0 Then
                ' CBusuario.SelectedValue = dt.Rows(0).Item("codusuario")
                TBnombre.Text = dt.Rows(0).Item("nombre").ToString
            End If
        End If
    End Sub

    Protected Sub Bguardar_Click(sender As Object, e As EventArgs) Handles Bguardar.Click
        If Page.IsValid Then
            Dim codigo As String = HFcodigo.Value
            'Dim nombre2 = IIf(String.IsNullOrEmpty(Trim(TBnombre2.Text)), Trim(TBnombre1.Text), Trim(TBnombre2.Text))
            If Not String.IsNullOrEmpty(codigo) Then
                ForumTags.Update_tags(codigo, Trim(TBnombre.Text))
                'ForumTemas.Update_post(codigo, Trim(TBTexto.Content))
            Else
                '   Dim codtema As Integer = ForumTemas.Insert_tema(codigo, CBcategoria.SelectedValue, current.Usuario, Trim(TBnombre1.Text), nombre2, CBool(CBactivo.Checked))
                '  ForumTemas.Insert_post(codtema, current.Usuario, Trim(TBTexto.Content), CBool(CBactivo.Checked))
            End If
            Reset()
        End If
    End Sub
    Protected Sub Bcancelar_Click(sender As Object, e As EventArgs) Handles Bcancelar.Click
        ' CargarDatos()
        Reset()
    End Sub
    Private Sub Reset()
        TBnombre.Text = String.Empty
        RGlista.Rebind()
        Pcategoria.Visible = False

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
                    ' count = Forumtemas.Get_Count(codselect, "encuestas")

                    If count > 0 Then
                        nombres = nombres & sel.GetDataKeyValue("nombre").ToString & "<br />"
                    Else
                        ForumTags.Delete_tags(codselect)
                    End If
                End If
            Next
            If nombres.Length > 0 Then
                RadAjaxManager.GetCurrent(Page).ResponseScripts.Add("return showModal('','" & Resources.lang.errorborrar & "<br />" & nombres & "','" & Bbai.ClientID.ToString & "','" & Bez.ClientID.ToString & "','" & modalDialog.ClientID.ToString & "');")
            End If
            Reset()
        End If
    End Sub
    Protected Sub Bez_Click(sender As Object, e As EventArgs) Handles Bez.Click
        RGlista.Rebind()
        RadAjaxManager.GetCurrent(Page).ResponseScripts.Add("return hide();")
    End Sub

End Class
