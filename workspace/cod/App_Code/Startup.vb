Imports Microsoft.Owin
Imports Owin

Imports System.Web.Routing

<Assembly: OwinStartupAttribute(GetType(Startup))>

Partial Public Class Startup
    Public Sub Configuration(app As IAppBuilder)
        ConfigureAuth(app)
    End Sub

End Class
