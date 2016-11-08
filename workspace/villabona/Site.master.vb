Imports System.Collections.Generic
Imports System.Data
Imports System.Security.Claims
Imports System.Security.Principal
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Telerik.Web.UI

Partial Public Class SiteMaster
    Inherits MasterPage
    Private Const AntiXsrfTokenKey As String = "__AntiXsrfToken"
    Private Const AntiXsrfUserNameKey As String = "__AntiXsrfUserName"
    Private _antiXsrfTokenValue As String

    Protected Sub Page_Init(sender As Object, e As EventArgs)
        ' El código siguiente ayuda a proteger frente a ataques XSRF
        Dim requestCookie = Request.Cookies(AntiXsrfTokenKey)
        Dim requestCookieGuidValue As Guid
        If requestCookie IsNot Nothing AndAlso Guid.TryParse(requestCookie.Value, requestCookieGuidValue) Then
            ' Utilizar el token Anti-XSRF de la cookie
            _antiXsrfTokenValue = requestCookie.Value
            Page.ViewStateUserKey = _antiXsrfTokenValue
        Else
            ' Generar un nuevo token Anti-XSRF y guardarlo en la cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N")
            Page.ViewStateUserKey = _antiXsrfTokenValue

            Dim responseCookie = New HttpCookie(AntiXsrfTokenKey) With {
                .HttpOnly = True,
                .Value = _antiXsrfTokenValue
            }
            If FormsAuthentication.RequireSSL AndAlso Request.IsSecureConnection Then
                responseCookie.Secure = True
            End If
            Response.Cookies.[Set](responseCookie)
        End If

        AddHandler Page.PreLoad, AddressOf master_Page_PreLoad

        current.InitializeCulture()

    End Sub

    Protected Sub master_Page_PreLoad(sender As Object, e As EventArgs)
        If Not IsPostBack Then
            ' Establecer token Anti-XSRF
            ViewState(AntiXsrfTokenKey) = Page.ViewStateUserKey
            ViewState(AntiXsrfUserNameKey) = If(Context.User.Identity.Name, [String].Empty)

            'InitAuthenticated()

        Else
            ' Validar el token Anti-XSRF
            If DirectCast(ViewState(AntiXsrfTokenKey), String) <> _antiXsrfTokenValue OrElse DirectCast(ViewState(AntiXsrfUserNameKey), String) <> (If(Context.User.Identity.Name, [String].Empty)) Then
                Throw New InvalidOperationException("Error de validación del token Anti-XSRF.")
            End If
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs)
        Cargar_Textos()
        'If Not IsPostBack Then
        InitAuthenticated()

        'End If


        'If Context.User.Identity.IsAuthenticated Then
        '    CargarCombos()
        '    If Not Page.IsPostBack Then
        '        CargarTextos()
        '        CargarURL()
        '        ''CargarConfiguracion()
        '    End If
        '    'HLabout.NavigateUrl = GetRouteUrl("abouta1", New With {Key .id = "nnn"})
        'Else
        '    Dim pagina = GetRouteUrl(amigables.Page.login.ToString & current.Idiomas.Español, Nothing)
        '    Response.Redirect(pagina)
        'End If
    End Sub

    Protected Sub InitAuthenticated()
        If Context.User.Identity.IsAuthenticated Then
            Dim manager = New UserManager()
            Dim confirmado = manager.IsEmailConfirmed(Context.User.Identity.GetUserId)
            If confirmado Then
                'Dim k = manager.GetRoles(Context.User.Identity.GetUserId)
                'If HFlogged.Value = 0 Then
                '    HFlogged.Value = 1
                'End If
                Dim inrole = Context.User.IsInRole("Admin")

                If inrole Then
                    Using ob = DirectCast(LoginView1.FindControl("PHadmin"), PlaceHolder)
                        ob.Visible = True
                        'ob.NavigateUrl = GetRouteUrl(amigables.Page.kudeaketa.ToString & current.Idioma, Nothing) ' "~/kudeaketa/Default.aspx" 
                    End Using
                End If

                'CargarTextos()
                CargarURL()
                'CargarConfiguracion()
                'CargarPermisos()

                Select Case amigables.RoutePage 'pag
                    Case amigables.Page.usuarios, amigables.Page.encuestascat, amigables.Page.encuestasadm
                        If Not inrole Then
                            Dim pagina = GetRouteUrl(amigables.Page.default.ToString & current.Idioma, Nothing)
                            Response.Redirect(pagina)
                        End If

                    Case Else
                        Exit Select
                End Select
            Else
                'ZURE KONTUA KONFIRMATU BEHAR DUZU
                Response.Write("<script language=javascript>alert('" & Resources.idioma.confirmaremail.ToUpper & "')</script>")

            End If
        Else
            'Dim pagina = GetRouteUrl(amigables.Page.login.ToString & current.Idiomas.Español, Nothing)
            'Response.Redirect(pagina)
        End If

    End Sub

    Protected Sub Unnamed_LoggingOut(sender As Object, e As LoginCancelEventArgs)

        Context.GetOwinContext().Authentication.SignOut()
        Session.Abandon()

        'Session.Remove("facebooktoken")

        'FormsAuthentication.SignOut()
        'If (Not Request.Cookies("kukiexterno") Is Nothing) Then
        '    Dim myCookie As HttpCookie
        '    myCookie = New HttpCookie("kukiexterno")
        '    myCookie.Expires = DateTime.Now.AddDays(-1D)
        '    Response.Cookies.Add(myCookie)
        'End If

    End Sub

    Private Sub Cargar_Textos()
        '    TBbuscar.Attributes.Add("placeholder", Resources.idioma.buscar)

    End Sub
    Private Sub CargarURL()
        'ADMIN
        DirectCast(LoginView1.FindControl("HLusuarios"), HyperLink).NavigateUrl = GetRouteUrl(amigables.Page.usuarios.ToString & current.Idioma, Nothing)
        DirectCast(LoginView1.FindControl("HLcategorias"), HyperLink).NavigateUrl = GetRouteUrl(amigables.Page.encuestascat.ToString & current.Idioma, Nothing)
        DirectCast(LoginView1.FindControl("HLencuestasadm"), HyperLink).NavigateUrl = GetRouteUrl(amigables.Page.encuestasadm.ToString & current.Idioma, Nothing)
        'Forum
        DirectCast(LoginView1.FindControl("HLforumcategorias"), HyperLink).NavigateUrl = GetRouteUrl(amigables.Page.forumcategorias.ToString & current.Idioma, Nothing)
        DirectCast(LoginView1.FindControl("HLforumtemas"), HyperLink).NavigateUrl = GetRouteUrl(amigables.Page.forumtemas.ToString & current.Idioma, Nothing)
        DirectCast(LoginView1.FindControl("HLforumtags"), HyperLink).NavigateUrl = GetRouteUrl(amigables.Page.forumtags.ToString & current.Idioma, Nothing)

        'USER
        DirectCast(LoginView1.FindControl("HLencuetas"), HyperLink).NavigateUrl = GetRouteUrl(amigables.Page.usuarios.ToString & current.Idioma, Nothing)
        'DirectCast(LoginView1.FindControl("HLforo"), HyperLink).NavigateUrl = GetRouteUrl(amigables.Page.usuarios.ToString & current.Idioma, Nothing)



        Using ob = DirectCast(LoginView1.FindControl("HLuser"), HyperLink)
            ob.Text = Context.User.Identity.GetUserName()
        End Using
        Using ob = DirectCast(LoginView1.FindControl("HLconfiguracion"), HyperLink)
            'ob.Text = Resources.idioma.configuracion
            ob.Text = Resources.idioma.administrarcuenta
            ob.NavigateUrl = GetRouteUrl(amigables.Page.manage.ToString & current.Idioma, Nothing) '"~/Account/Manage"
        End Using

        Using ob = DirectCast(LoginView1.FindControl("Logout"), LoginStatus)
            ob.LogoutText = Resources.idioma.cerrarsesion
        End Using
    End Sub

End Class
