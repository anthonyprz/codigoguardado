Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.Owin.Security
Imports Microsoft.AspNet.Identity





Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports System.Web.Routing



' Para añadir datos de perfil del usuario añada más propiedades a su clase de usuario. Visite http://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
Public Class ApplicationUser
    Inherits IdentityUser

    'Public Function GenerateUserIdentity(manager As ApplicationUserManager) As ClaimsIdentity
    '    ' Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    '    Dim userIdentity = manager.CreateIdentity(Me, DefaultAuthenticationTypes.ApplicationCookie)
    '    ' Add custom user claims here
    '    Return userIdentity
    'End Function

    'Public Function GenerateUserIdentityAsync(manager As ApplicationUserManager) As Task(Of ClaimsIdentity)
    '    Return Task.FromResult(GenerateUserIdentity(manager))
    'End Function
    Private m_codID As Integer
    Private m_dni As String
    Private m_nombre As String
    Private m_apellido1 As String
    Private m_apellido2 As String
    Private m_emailapp As String
    Private m_activo As Boolean
    Private m_imagen As String

    Public Property codID() As Integer
        Get
            Return m_codID
        End Get
        Set(value As Integer)
            m_codID = value
        End Set
    End Property
    Public Property dni() As String
        Get
            Return m_dni
        End Get
        Set(value As String)
            m_dni = value
        End Set
    End Property

    Public Property nombre() As String
        Get
            Return m_nombre
        End Get
        Set(value As String)
            m_nombre = value
        End Set
    End Property
    Public Property apellido1() As String
        Get
            Return m_apellido1
        End Get
        Set(value As String)
            m_apellido1 = value
        End Set
    End Property
    Public Property apellido2() As String
        Get
            Return m_apellido2
        End Get
        Set(value As String)
            m_apellido2 = value
        End Set
    End Property
    Public Property emailapp() As String
        Get
            Return m_emailapp
        End Get
        Set(value As String)
            m_emailapp = value
        End Set
    End Property
    Public Property activo() As Boolean
        Get
            Return m_activo
        End Get
        Set(value As Boolean)
            m_activo = value
        End Set
    End Property
    Public Property imagen() As String
        Get
            Return m_imagen
        End Get
        Set(value As String)
            m_imagen = value
        End Set
    End Property
End Class

Public Class ApplicationDbContext
    Inherits IdentityDbContext(Of ApplicationUser)
    Public Sub New()
        MyBase.New("DefaultConnection")
    End Sub
End Class

#Region "Helpers"
Public Class UserManager
    Inherits UserManager(Of ApplicationUser)
    Public Sub New()
        MyBase.New(New UserStore(Of ApplicationUser)(New ApplicationDbContext()))
    End Sub
End Class
Public Class IdentityHelper
    'Se utilizan para XSRF al vincular inicios de sesión externos
    Public Const XsrfKey As String = "xsrfKey"

    Public Shared Sub SignIn(manager As UserManager, user As ApplicationUser, isPersistent As Boolean)
        Dim authenticationManager As IAuthenticationManager = HttpContext.Current.GetOwinContext().Authentication
        authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie)
        Dim identity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie)
        authenticationManager.SignIn(New AuthenticationProperties() With {.IsPersistent = isPersistent}, identity)
    End Sub

    Public Const ProviderNameKey As String = "providerName"
    Public Shared Function GetProviderNameFromRequest(request As HttpRequest) As String
        Return request(ProviderNameKey)
    End Function

    Private Shared Function IsLocalUrl(url As String) As Boolean
        Return Not String.IsNullOrEmpty(url) AndAlso ((url(0) = "/"c AndAlso (url.Length = 1 OrElse (url(1) <> "/"c AndAlso url(1) <> "\"c))) OrElse (url.Length > 1 AndAlso url(0) = "~"c AndAlso url(1) = "/"c))
    End Function

    Public Shared Sub RedirectToReturnUrl(returnUrl As String, response As HttpResponse)
        If Not [String].IsNullOrEmpty(returnUrl) AndAlso IsLocalUrl(returnUrl) Then
            response.Redirect(returnUrl)
        Else
            response.Redirect("~/")
        End If
    End Sub


    Public Const CodeKey As String = "code"
    Public Const UserIdKey As String = "userId"
    Public Shared Function GetUserConfirmationRedirectUrl(code As String, userId As String, request As HttpRequest) As String
        ' absoluteUri = "/Account/Confirm?" + CodeKey + "=" + HttpUtility.UrlEncode(code) + "&" + UserIdKey + "=" + HttpUtility.UrlEncode(userId)
        Dim absoluteUri = GetRouteUrl(amigables.Page.confirm.ToString & current.Idioma, Nothing) + "?" + CodeKey + "=" + HttpUtility.UrlEncode(code) + "&" + UserIdKey + "=" + HttpUtility.UrlEncode(userId)
        Return New Uri(request.Url, absoluteUri).AbsoluteUri.ToString()
    End Function


    Public Shared Function GetCodeFromRequest(request As HttpRequest) As String
        Return request.QueryString(CodeKey)
    End Function
    Public Shared Function GetUserIdFromRequest(request As HttpRequest) As String
        Return HttpUtility.UrlDecode(request.QueryString(UserIdKey))
    End Function

    Public Shared Function GetResetPasswordRedirectUrl(code As String, request As HttpRequest) As String
        'Dim absoluteUri = "/Account/ResetPassword?" + CodeKey + "=" + HttpUtility.UrlEncode(code)
        Dim absoluteUri = GetRouteUrl(amigables.Page.resetpass.ToString & current.Idioma, Nothing) + "?" + CodeKey + "=" + HttpUtility.UrlEncode(code)
        Return New Uri(request.Url, absoluteUri).AbsoluteUri.ToString()
    End Function


    Public Shared Function GetRouteUrl(routeName As String, routeParameters As Object) As String
        Dim dict = New RouteValueDictionary(routeParameters)
        Dim data = RouteTable.Routes.GetVirtualPath(HttpContext.Current.Request.RequestContext, routeName, dict)
        If data IsNot Nothing Then
            Return data.VirtualPath
        End If
        Return Nothing
    End Function


End Class

#End Region






'Public Class EmailService
'    Implements IIdentityMessageService
'    Public Function SendAsync(message As IdentityMessage) As Task Implements IIdentityMessageService.SendAsync
'        ' Plug in your email service here to send an email.
'        Return Task.FromResult(0)
'    End Function
'End Class

'Public Class SmsService
'    Implements IIdentityMessageService
'    Public Function SendAsync(message As IdentityMessage) As Task Implements IIdentityMessageService.SendAsync
'        ' Plug in your SMS service here to send a text message.
'        Return Task.FromResult(0)
'    End Function
'End Class

' Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
'Public Class ApplicationUserManager
'    Inherits UserManager(Of ApplicationUser)
'    Public Sub New(store As IUserStore(Of ApplicationUser))
'        MyBase.New(store)
'    End Sub

'    Public Shared Function Create(options As IdentityFactoryOptions(Of ApplicationUserManager), context As IOwinContext) As ApplicationUserManager
'        Dim manager = New ApplicationUserManager(New UserStore(Of ApplicationUser)(context.[Get](Of ApplicationDbContext)()))
'        ' Configure validation logic for usernames
'        manager.UserValidator = New UserValidator(Of ApplicationUser)(manager) With {
'          .AllowOnlyAlphanumericUserNames = False,
'          .RequireUniqueEmail = True
'        }

'        ' Configure validation logic for passwords
'        manager.PasswordValidator = New PasswordValidator() With {
'          .RequiredLength = 6,
'          .RequireNonLetterOrDigit = True,
'          .RequireDigit = True,
'          .RequireLowercase = True,
'          .RequireUppercase = True
'        }
'        ' Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user. 
'        ' You can write your own provider and plug in here.
'        manager.RegisterTwoFactorProvider("Phone Code", New PhoneNumberTokenProvider(Of ApplicationUser)() With {
'          .MessageFormat = "Your security code is {0}"
'        })
'        manager.RegisterTwoFactorProvider("Email Code", New EmailTokenProvider(Of ApplicationUser)() With {
'          .Subject = "Security Code",
'          .BodyFormat = "Your security code is {0}"
'        })

'        ' Configure user lockout defaults
'        manager.UserLockoutEnabledByDefault = True
'        manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5)
'        manager.MaxFailedAccessAttemptsBeforeLockout = 5

'        manager.EmailService = New EmailService()
'        manager.SmsService = New SmsService()
'        Dim dataProtectionProvider = options.DataProtectionProvider
'        If dataProtectionProvider IsNot Nothing Then
'            manager.UserTokenProvider = New DataProtectorTokenProvider(Of ApplicationUser)(dataProtectionProvider.Create("ASP.NET Identity"))
'        End If
'        Return manager
'    End Function
'End Class

'Public Class ApplicationSignInManager
'    Inherits SignInManager(Of ApplicationUser, String)
'    Public Sub New(userManager As ApplicationUserManager, authenticationManager As IAuthenticationManager)
'        MyBase.New(userManager, authenticationManager)
'    End Sub

'    Public Overrides Function CreateUserIdentityAsync(user As ApplicationUser) As Task(Of ClaimsIdentity)
'        Return user.GenerateUserIdentityAsync(DirectCast(UserManager, ApplicationUserManager))
'    End Function

'    Public Shared Function Create(options As IdentityFactoryOptions(Of ApplicationSignInManager), context As IOwinContext) As ApplicationSignInManager
'        Return New ApplicationSignInManager(context.GetUserManager(Of ApplicationUserManager)(), context.Authentication)
'    End Function
'End Class