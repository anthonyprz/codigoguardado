Imports Owin
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.Google
Imports Microsoft.Owin

Partial Public Class Startup
    ' Para obtener más información sobre la configuración de la autenticación, visite http://go.microsoft.com/fwlink/?LinkId=301883
    Public Sub ConfigureAuth(app As IAppBuilder)

        'amigables.DefinirRutas()


        ' Habilitar la aplicación para que use una cookie para almacenar la información del usuario que inició sesión
        ' y almacenar también información acerca de un usuario que inicie sesión con un proveedor de inicio de sesión de un tercero.
        ' Es obligatorio si la aplicación permite a los usuarios iniciar sesión
        app.UseCookieAuthentication(New CookieAuthenticationOptions() With {
        .AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        .SlidingExpiration = True,
        .ExpireTimeSpan = TimeSpan.FromMinutes(30),
        .LoginPath = New PathString("/Account/Login")})

        'app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie)

        ' Quitar las marcas de comentario de las líneas siguientes para habilitar el inicio de sesión con proveedores de inicio de sesión de terceros
        'app.UseMicrosoftAccountAuthentication(
        '    clientId:= "",
        '    clientSecret:= "")



        'app.UseTwitterAuthentication(
        '   consumerKey:="3uVtQukoPpAc20jMiKTYQnwO4",
        '   consumerSecret:="MX05XzE53II5OBy8Qogbi62INEvd87nMYYKW4LZiTc0qCqHmHW")

        'app.UseFacebookAuthentication(
        '   appId:="141871209508247",
        '   appSecret:="72da2acab706deb8e6b254213ea64794")


        'app.UseGoogleAuthentication(New GoogleOAuth2AuthenticationOptions() With {
        '   .ClientId = "335813118645-qocah04uqjnvoj5g9mpqjlk7kt7qm5qq.apps.googleusercontent.com",
        '   .ClientSecret = "-H46zNsk_EsM4meUhCeimfRm",
        '   .CallbackPath = New PathString("/Account/RegisterExternalLogin")})


        'Dim options = New GoogleOAuth2AuthenticationOptions() With {
        '    .ClientId = "335813118645-qocah04uqjnvoj5g9mpqjlk7kt7qm5qq.apps.googleusercontent.com",
        '    .ClientSecret = "-H46zNsk_EsM4meUhCeimfRm",
        '    .Provider = New GoogleOAuth2AuthenticationProvider() With {
        '    .OnAuthenticated = Function(context)
        '                           ' Retrieve the OAuth access token to store for subsequent API calls
        '                           Dim accessToken As String = context.AccessToken

        '                           ' Retrieve the name of the user in Google
        '                           Dim googleName As String = context.Name

        '                           ' Retrieve the user's email address
        '                           Dim googleEmailAddress As String = context.Email

        '                           ' You can even retrieve the full JSON-serialized user
        '                           Dim serializedUser = context.User

        '                       End Function
        '                       }
        '}
        'app.UseGoogleAuthentication(options)



    End Sub
End Class
