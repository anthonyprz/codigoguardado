Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Globalization
Imports System.Threading.Thread
Imports System.Web.UI.Page
Imports System.Data
Imports System.Web.Routing
Imports System.Data.SqlClient


Public Class current

    Public Enum Idiomas
        ES = 1
        EU = 2
    End Enum

    Shared Sub InitializeCulture()
        Try
            Select Case Idioma
                Case Idiomas.EU
                    Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("EU")
                    Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("EU")
                Case Idiomas.ES
                    Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ES")
                    Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("ES")
                Case Else
                    Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("EU")
                    Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("EU")
            End Select
        Catch ex As Exception
            Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("EU")
            Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("EU")
        End Try
    End Sub

    Shared Sub IniciarCulture(ByVal idioma As Idiomas)
        Try
            Select Case idioma
                Case Idiomas.EU
                    Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("EU")
                    Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("EU")
                Case Idiomas.ES
                    Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ES")
                    Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("ES")
                Case Else
                    Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("EU")
                    Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("EU")
            End Select
        Catch ex As Exception
            Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("EU")
            Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("EU")
        End Try
    End Sub

    Shared ReadOnly Property Idioma As Idiomas
        Get
            Try
                Dim lang = HttpContext.Current.Request.RequestContext.RouteData.DataTokens.Item("idioma")
                Return If(lang = 0, Idiomas.EU, lang)
            Catch
                Return Idiomas.EU
            End Try
        End Get
    End Property

    Shared ReadOnly Property SqlConnection As SqlConnection
        Get
            Return New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnection").ToString)
        End Get
    End Property
    Shared ReadOnly Property Usuario As String
        Get
            Return HttpContext.Current.User.Identity.GetUserId() '"89a1261c-cc2d-4966-9bf0-4c220097361f" 
        End Get
    End Property

    Shared ReadOnly Property Path_Avatar As String
        Get
            Return "/idcontent/" & current.codID & "/avatar/"
        End Get
    End Property



    Shared WriteOnly Property set_codID() As Integer
        Set(value As Integer)
            HttpContext.Current.Session("codID") = value
        End Set
    End Property
    Shared ReadOnly Property codID As String
        Get
            If String.IsNullOrEmpty(HttpContext.Current.Session("codID")) Then
                HttpContext.Current.Session("codID") = "1"
            End If
            Return HttpContext.Current.Session("codID")
        End Get
    End Property
    Shared ReadOnly Property Skin As String
        Get
            If String.IsNullOrEmpty(HttpContext.Current.Session("Skin")) Then
                HttpContext.Current.Session("Skin") = "Simple"
            End If
            Return HttpContext.Current.Session("Skin")
        End Get
    End Property



    '    'Shared Property Idioma As current.Idiomas
    '    '    'Get
    '    '    '    Try
    '    '    '        'If DirectCast(HttpContext.Current.Request.RequestContext.RouteData.Route, System.Web.Routing.Route).Url.Split("/")(0) = Idiomas.Euskara.ToString.ToLower.Substring(0, 2) OrElse DirectCast(HttpContext.Current.Request.RequestContext.RouteData.Route, System.Web.Routing.Route).Url.Split("/")(1) = Idiomas.Euskara.ToString.ToLower.Substring(0, 2) Then
    '    '    '        '    Return Idiomas.Euskara
    '    '    '        'Else
    '    '    '        '    Return Idiomas.Español
    '    '    '        'End If
    '    '    '        'Dim lang = HttpContext.Current.Request.RequestContext.RouteData.Values("idioma")
    '    '    '        Dim lang = HttpContext.Current.Request.RequestContext.RouteData.DataTokens.Item("idioma")
    '    '    '        Return IIf(lang = 0, 1, lang)
    '    '    '    Catch
    '    '    '        Return Idiomas.Español
    '    '    '    End Try
    '    '    'End Get

    '    '    Get

    '    '        Try

    '    '            If DirectCast(HttpContext.Current.Request.RequestContext.RouteData.Route, System.Web.Routing.Route).Url.Split("/")(0) = Idiomas.Español.ToString.ToLower.Substring(0, 2) OrElse DirectCast(HttpContext.Current.Request.RequestContext.RouteData.Route, System.Web.Routing.Route).Url.Split("/")(1) = Idiomas.Español.ToString.ToLower.Substring(0, 2) Then
    '    '                Return Idiomas.Español
    '    '            Else
    '    '                Return Idiomas.Euskara
    '    '            End If
    '    '        Catch

    '    '            'Return HttpContext.Current.Session("idioma")
    '    '            Return Idiomas.Español
    '    '        End Try
    '    '    End Get



    '    '    'Get
    '    '    '    If String.IsNullOrEmpty(HttpContext.Current.Session("idioma")) Then
    '    '    '        HttpContext.Current.Session("idioma") = -1
    '    '    '    End If
    '    '    '    Return HttpContext.Current.Session("idioma")
    '    '    'End Get

    '    '    Set(ByVal value As current.Idiomas)
    '    '        HttpContext.Current.Session("idioma") = value
    '    '    End Set
    '    'End Property

End Class
