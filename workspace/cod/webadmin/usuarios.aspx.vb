Imports System.Data
Imports Telerik.Web.UI


Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports System
Imports System.Collections.Generic

Partial Class Default2
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Page.Theme = current.Skin
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim inrole = Context.User.IsInRole("Admin")

        If Request.Params("__EVENTTARGET") = "dobleclick" Then
            Dim args = Request.Params("__EVENTARGUMENT")
            AbrirUsuario(args)
        End If

        If Not IsPostBack Then
            CargarTextos()
        End If

    End Sub

    Private Sub CargarTextos()
        Bbai.ToolTip = Resources.lang.si
        Bez.ToolTip = Resources.lang.no

        lusuarios.Text = Resources.lang.usuarios.ToUpper
        For x As Integer = 0 To RGlista.Columns.Count - 1
            Dim col As String = RGlista.Columns(x).UniqueName
            RGlista.Columns(x).HeaderText = GetGlobalResourceObject("lang", col.ToLower).ToString().ToUpper
        Next
    End Sub

    Protected Sub RGlista_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGlista.NeedDataSource
        CargarDatos()
    End Sub

    Private Sub CargarDatos()

        Dim getAdm = New Func(Of Integer, Boolean)(Function(roleID)
                                                       Return If(roleID = 1, True, False)
                                                   End Function)
        Dim context = New ApplicationDbContext()
        'Dim users = context.Users.Where(Function(x) x.Roles.[Select](Function(y) y.RoleId).Contains(1)).ToList()
        Dim ds = context.Users.ToList()

        Dim dscod = From c In ds Where c.codID = current.codID Select id = c.Id, nombre = c.nombre + " " + c.apellido1 + " " + c.apellido2, UserName = c.UserName,
                                                                   Email = c.Email, activo = c.activo, rolea = c.Roles(0).RoleId, admin = getAdm(c.Roles(0).RoleId)
        If dscod.Count > 0 Then
            RGlista.DataSource = dscod
        End If
    End Sub

    Protected Sub RGlista_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RGlista.ItemCommand
        If e.CommandName = RadGrid.EditSelectedCommandName Then
            e.Canceled = True
            Dim codusu As String = RGlista.SelectedValue
            If Not String.IsNullOrEmpty(codusu) Then
                AbrirUsuario(codusu)
            End If
        ElseIf e.CommandName = RadGrid.InitInsertCommandName Then
            e.Canceled = True
            AbrirUsuario(Nothing)
        ElseIf e.CommandName = RadGrid.DeleteSelectedCommandName Then
            'If RGlista.SelectedItems.Count > 0 Then
            '    Dim codusu As String
            '    Dim nombres As String = String.Empty
            '    Dim context = New ApplicationDbContext()
            '    For Each sel As GridDataItem In RGlista.SelectedItems()
            '        codusu = sel.GetDataKeyValue("ID")

            '        Dim count As Integer = usuarios.Get_Count(codusu)

            '        If count > 0 Then
            '            nombres = nombres & sel.GetDataKeyValue("nombre").ToString & "<br />"
            '        Else
            '            usuarios.Delete_Usuario(codusu)
            '            Dim gesusu = context.Users.Find(codusu)
            '            context.Users.Remove(gesusu)
            '            context.SaveChanges()
            '            'usuarios.Delete_UsuarioPermisos(codusu)
            '        End If
            '    Next
            '    If nombres.Length > 0 Then
            '        RadAjaxManager1.ResponseScripts.Add("return showModal('','" & Resources.idioma.errorborrar & "<br />" & nombres & "','" & Bbai.ClientID.ToString & "','" & Bez.ClientID.ToString & "');")
            '    End If
            '    'CargarDatos()
            '    RGlista.DataBind()
            'End If
        ElseIf e.CommandName = RadGrid.FilterCommandName Or e.CommandName = RadGrid.RebindGridCommandName Then
            RGlista.Rebind()
        ElseIf e.CommandName = "clicklink" Then
            Dim codigo As String = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()
            'Dim k = (DirectCast(e.Item, GridDataItem)).GetDataKeyValue("ID").ToString
            AbrirUsuario(codigo)
        End If
    End Sub

    Function showModal() As String
        Return "return showModal('" & RGlista.ClientID.ToString & "','" & Resources.lang.alertaeliminarencadena & "','" & Bbai.ClientID.ToString & "','" & Bez.ClientID.ToString & "','" & modalDialog.ClientID.ToString & "');"
    End Function


    Protected Sub Bez_Click(sender As Object, e As EventArgs) Handles Bez.Click
        RGlista.Rebind()
        RadAjaxManager.GetCurrent(Page).ResponseScripts.Add("return hide();")
    End Sub

    Protected Sub Bbai_Click(sender As Object, e As EventArgs) Handles Bbai.Click
        RadAjaxManager.GetCurrent(Page).ResponseScripts.Add("return hide();")

        If RGlista.SelectedItems.Count > 0 Then
            Dim codusu As String
            Dim nombres As String = String.Empty
            Dim context = New ApplicationDbContext()
            For Each sel As GridDataItem In RGlista.SelectedItems()
                codusu = sel.GetDataKeyValue("ID")
                'Dim count As Integer = usuarios.Get_Count(codusu)
                'If count > 0 Then
                '    nombres = nombres & sel.GetDataKeyValue("nombre").ToString & "<br />"
                'Else
                Dim gesusu = context.Users.Find(codusu)
                    context.Users.Remove(gesusu)
                    context.SaveChanges()
                'End If
            Next
            If nombres.Length > 0 Then
                RadAjaxManager.GetCurrent(Page).ResponseScripts.Add("return showModal('','" & Resources.lang.errorborrar & "<br />" & nombres & "','" & Bbai.ClientID.ToString & "','" & Bez.ClientID.ToString & "','" & modalDialog.ClientID.ToString & "');")
            End If
            RGlista.Rebind()
        End If
    End Sub

    Private Sub AbrirUsuario(ByVal codusuario As String)
        Plista.Visible = False
        Pdatos.Visible = True

        'wcUSU._codusuario = codusuario
        wcUSU.CargarDatos(codusuario)
    End Sub

    Function enabledisable() As Boolean
        Return True
    End Function


    Private Sub ucHide(ByVal sender As Object, ByVal e As EventArgs) Handles wcUSU.on_ucHIDE
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
