Imports System.Data
Imports System.Drawing
Imports Telerik.Web.UI
Partial Class CategoriasDelForum
    Inherits System.Web.UI.Page
    ' Dim _pagina As String = "foro_categorias"
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
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Page.Theme = current.Skin
    End Sub
    Protected Sub RGlista_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGlista.NeedDataSource
        CargarDatos()
    End Sub

    Private Sub CargarDatos()
        Dim ds = ForumCategorias.Get_Categorias()
        If ds.Tables.Count > 0 Then
            RGlista.DataSource = ds
        End If
    End Sub
    Function enabledisable() As Boolean
        Return True
    End Function

    Function showModal() As String
        Return "return showModal('" & RGlista.ClientID.ToString & "','" & Resources.lang.alertaeliminar & "','" & Bbai.ClientID.ToString & "','" & Bez.ClientID.ToString & "','" & modalDialog.ClientID.ToString & "');"
    End Function

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
            TBdescripcion1.Text = String.Empty
            TBdescripcion2.Text = String.Empty
            CBactivo.Checked = True
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
        If Not IsNothing(codselect) Then
            Dim dt As DataTable = ForumCategorias.Get_Categoria(codselect).Tables(0)
            If dt.Rows.Count > 0 Then
                TBnombre1.Text = dt.Rows(0).Item("nombre1").ToString
                TBnombre2.Text = dt.Rows(0).Item("nombre2").ToString
                TBdescripcion1.Text = dt.Rows(0).Item("descripcion1").ToString
                TBdescripcion2.Text = dt.Rows(0).Item("descripcion2").ToString
                'Try : RadColorPicker1.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0).Item("color").ToString) : Catch : End Try
                CBactivo.Checked = CBool(dt.Rows(0).Item("activo"))
            End If
        End If
    End Sub

    Protected Sub Bguardar_Click(sender As Object, e As EventArgs) Handles Bguardar.Click
        If Page.IsValid Then
            Dim codigo As String = HFcodigo.Value
            Dim nombre2 = IIf(String.IsNullOrEmpty(Trim(TBnombre2.Text)), Trim(TBnombre1.Text), Trim(TBnombre2.Text))
            If Not String.IsNullOrEmpty(codigo) Then
                ForumCategorias.Update_Categorias(codigo, Trim(TBnombre1.Text), nombre2, TBdescripcion1.Text, TBdescripcion2.Text)
            Else
                ForumCategorias.Insert_Categorias(Trim(TBnombre1.Text), nombre2, TBdescripcion1.Text, TBdescripcion2.Text)
            End If
            Reset()
        End If
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
                    count = ForumCategorias.Get_Count(codselect, "encuestas")

                    If count > 0 Then
                        nombres = nombres & sel.GetDataKeyValue("nombre").ToString & "<br />"
                    Else
                        ForumCategorias.Delete_Categoria(codselect)
                    End If
                End If
            Next
            If nombres.Length > 0 Then
                RadAjaxManager.GetCurrent(Page).ResponseScripts.Add("return showModal('','" & Resources.lang.errorborrar & "<br />" & nombres & "','" & Bbai.ClientID.ToString & "','" & Bez.ClientID.ToString & "','" & modalDialog.ClientID.ToString & "');")
            End If
            Reset()
        End If
    End Sub
    Private Sub Reset()
        HFcodigo.Value = String.Empty
        TBnombre1.Text = String.Empty
        TBnombre2.Text = String.Empty
        TBdescripcion1.Text = String.Empty
        CBactivo.Checked = True

        RGlista.Rebind()
        Pcategoria.Visible = False
    End Sub

    Protected Sub Bcancelar_Click(sender As Object, e As EventArgs) Handles Bcancelar.Click
        ' CargarDatos()
        Reset()
    End Sub
End Class
