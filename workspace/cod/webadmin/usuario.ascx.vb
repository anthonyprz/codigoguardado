Imports Telerik.Web.UI
Imports System.Data
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity
Imports System.Collections.Generic


Partial Class web_usuario
    Inherits System.Web.UI.UserControl

    'Public Delegate Sub EventHandlerParams(sender As Object, e As String)
    'Public Event on_ucSELECTED As EventHandlerParams
    'Private Sub ucSELECTED(g As String) ' 0:inicio; 1:bgrupo1; 2:bgrupo2; 3:bgrupo3; 4:batributo;5:divpunto
    '    RaiseEvent on_ucSELECTED(Me, g)
    'End Sub

    Public Event on_ucHIDE As EventHandler
    Private Sub ucHIDE()
        RaiseEvent on_ucHIDE(Me, EventArgs.Empty)
    End Sub




    Public _codusuario As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargarTextos()
        End If
    End Sub

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        'Dim adper = current.per_usuarios
        'Dim permiso = IIf(adper.admin, 2, adper.permiso)
        'If permiso = 1 Then
        '    Penabled.Enabled = False
        '    Bguardar.Visible = False
        'End If

        'TBclave.Attributes.Add("value", TBclave.Text)
        'TBclaveconfirm.Attributes.Add("value", TBclaveconfirm.Text)


        'If String.IsNullOrEmpty(HFCodusu.Value) Then
        '    HFCodusu.Value = _codusuario

        '    CargarTextos()
        '    If Not String.IsNullOrEmpty(HFCodusu.Value) Then 'Not IsNothing(HFCodusu.Value) Or
        '        RequiredVpass.Enabled = False
        '        CargarDatos()
        '        CargarPermisos()
        '    End If
        'End If

    End Sub

    Private Sub CargarTextos()
        titusuario.Text = Resources.lang.usuario.ToUpper
        lusuario.Text = Resources.lang.datos.ToUpper
        lnombre.Text = Resources.lang.nombre.ToUpper
        lusu.Text = Resources.lang.lusuario.ToUpper
        Lerror.Text = Resources.lang.enuso.ToUpper

        lemail.Text = Resources.lang.email.ToUpper
        TBemail.Attributes.Add("placeholder", "xxxx@yyyy.com")
        Vemail.ErrorMessage = Resources.lang.val_errorformato
        Lerroremail.Text = Resources.lang.enuso.ToUpper

        CBactivo.Text = Resources.lang.activo.ToUpper

        lcontrasena.Text = Resources.lang.cambiarcontrasena.ToUpper
        lclave.Text = Resources.lang.contrasena.ToUpper
        lclaveconfirm.Text = Resources.lang.claveconfirm.ToUpper

        Help.Text = Resources.lang.helpcontrasena
        Vpass.ErrorMessage = Resources.lang.helpcontrasena


        lconfiguracion.Text = Resources.lang.configuracion.ToUpper
        CBadmin.Text = Resources.lang.administrador.ToUpper

        Bcancelar.Text = Resources.lang.cancelar.ToUpper
        Bguardar.Text = Resources.lang.guardar.ToUpper

        'required testuak falta

    End Sub

    Public Sub CargarDatos(ByVal codusuario As String)
        If Not IsNothing(codusuario) Then
            Dim getAdm = New Func(Of Integer, Boolean)(Function(roleID)
                                                           Return If(roleID = 1, True, False)
                                                       End Function)
            Dim context = New ApplicationDbContext()
            Dim gesusu = context.Users.Find(codusuario)

            TBnombre.Text = gesusu.nombre
            TBapel1.Text = gesusu.apellido1
            TBapel2.Text = gesusu.apellido2
            TBdni.Text = gesusu.dni
            TBtelf.Text = gesusu.PhoneNumber

            TBusuario.Text = gesusu.UserName
            TBemail.Text = gesusu.Email

            CBactivo.Checked = CBool(gesusu.activo)
            CBadmin.Checked = getAdm(gesusu.Roles(0).RoleId)


            HFCodusu.Value = codusuario
            'HFusuario.Value = gesusu.UserName

            TBclave.Attributes.Add("value", TBclave.Text)
            TBclaveconfirm.Attributes.Add("value", TBclaveconfirm.Text)


            If Not String.IsNullOrEmpty(HFCodusu.Value) Then
                RequiredVpass.Enabled = False
            End If

        End If
    End Sub

    Protected Async Sub Bguardar_Click(sender As Object, e As EventArgs) Handles Bguardar.Click

        If Page.IsValid Then
            If Not Derror.Visible Then

                Dim manager = New UserManager()
                If String.IsNullOrEmpty(HFCodusu.Value) Then 'insert
                    Dim user = New ApplicationUser() With {.UserName = TBusuario.Text, .codID = 1, .nombre = TBnombre.Text, .apellido1 = TBapel1.Text, .apellido2 = TBapel2.Text, .dni = TBdni.Text, .PhoneNumber = TBtelf.Text,
                                                            .activo = CBool(CBactivo.Checked), .Email = TBemail.Text, .EmailConfirmed = True}


                    Dim result = manager.Create(user, TBclave.Text)

                    If result.Succeeded Then
                        manager.AddToRole(user.Id, If(CBadmin.Checked, "Admin", "User"))
                    Else
                        'ErrorMessage.Text = result.Errors.FirstOrDefault()
                    End If
                Else 'update
                    Dim context = New ApplicationDbContext()
                    Dim gesusu = context.Users.Find(HFCodusu.Value)
                    gesusu.nombre = TBnombre.Text
                    gesusu.apellido1 = TBapel1.Text
                    gesusu.apellido2 = TBapel2.Text
                    gesusu.dni = TBdni.Text
                    gesusu.PhoneNumber = TBtelf.Text

                    gesusu.UserName = TBusuario.Text
                    gesusu.Email = TBemail.Text

                    gesusu.activo = CBool(CBactivo.Checked)

                    ''Dim roleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(New ApplicationDbContext()))
                    'Dim roleStore = New RoleStore(Of IdentityRole)(New ApplicationDbContext())


                    Dim oldRoleId = gesusu.Roles.SingleOrDefault().RoleId
                    'Dim oldRoleName = roleStore.FindByIdAsync(oldRoleId)

                    ''Dim oldRoleName = DB.Roles.SingleOrDefault(Function(r) r.Id = oldRoleId).Name
                    manager.RemoveFromRole(gesusu.Id, If(oldRoleId = 1, "Admin", "User"))
                    manager.AddToRole(gesusu.Id, If(CBadmin.Checked, "Admin", "User"))

                    context.SaveChanges()

                    'Contraseña aldatzeko Async="true" jarri. Adb: <%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="usuarios.aspx.vb" Inherits="Default2" Async="true" %>
                    'Dim context As New ApplicationDbContext()
                    Dim store As New UserStore(Of ApplicationUser)(context)
                    Dim UserManager As New UserManager(Of ApplicationUser)(store)
                    Dim userId As [String] = HFCodusu.Value
                    Dim newPassword As [String] = TBclave.Text
                    Dim hashedNewPassword As [String] = UserManager.PasswordHasher.HashPassword(newPassword)
                    Dim cUser As ApplicationUser = Await store.FindByIdAsync(userId)
                    If Not String.IsNullOrEmpty(TBclave.Text) Then
                        Await store.SetPasswordHashAsync(cUser, hashedNewPassword)
                    End If
                    Await store.UpdateAsync(cUser)

                    'If HFCodusu.Value = current.Usuario Then
                    '    current.CargarConfigPermisos(current.Usuario)
                    'End If
                End If
                'Reset()
                Dim pagina = GetRouteUrl(amigables.RoutePage.ToString & current.Idioma, Nothing)
                Response.Redirect(pagina, False)
            End If
        End If
    End Sub

    Protected Sub Bcancelar_Click(sender As Object, e As EventArgs) Handles Bcancelar.Click
        Reset()
    End Sub

    Private Sub Reset()
        TBnombre.Text = String.Empty
        TBapel1.Text = String.Empty
        TBapel2.Text = String.Empty
        TBdni.Text = String.Empty
        TBtelf.Text = String.Empty
        TBusuario.Text = String.Empty
        'HFusuario.Value = String.Empty
        Derror.Visible = False
        TBemail.Text = String.Empty

        CBactivo.Checked = False
        CBadmin.Checked = False

        TBclave.Text = String.Empty
        TBclaveconfirm.Text = String.Empty

        HFCodusu.Value = String.Empty
        'HFusuario.Value = String.Empty

        ucHIDE()

    End Sub

    Protected Sub TBusuario_TextChanged(sender As Object, e As EventArgs) Handles TBusuario.TextChanged
        'Dim manager = New UserManager()
        'Dim k = manager.FindByName(TBusuario.Text)
        Dim context = New ApplicationDbContext()
        Dim gesusuUserName = ""
        Try : gesusuUserName = context.Users.Find(HFCodusu.Value).UserName : Catch : End Try

        'Dim existe = usuarios.Existe_NombreUsuario(HFusuario.Value, TBusuario.Text)
        Dim existe = usuarios.Existe_NombreUsuario(gesusuUserName, TBusuario.Text)
        If existe > 0 Then
            Derror.Visible = True
        Else
            Derror.Visible = False
        End If
    End Sub

    Private Sub TBemail_TextChanged(sender As Object, e As EventArgs) Handles TBemail.TextChanged
        Dim context = New ApplicationDbContext()
        Dim gesusuemail = ""
        Try : gesusuemail = context.Users.Find(HFCodusu.Value).Email : Catch : End Try

        Dim existe = usuarios.Existe_EmailUsuario(gesusuemail, TBemail.Text)
        If existe > 0 Then
            Derroremail.Visible = True
        Else
            Derroremail.Visible = False
        End If
    End Sub
End Class
