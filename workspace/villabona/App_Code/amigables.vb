Imports Microsoft.VisualBasic
Imports System.Web.Routing
Imports System.Globalization
Imports Owin

Public Class amigables

    Shared ReadOnly Property RoutePage As Page ' current.Idiomas
        Get
            Try
                'Return HttpContext.Current.Request.RequestContext.RouteData.Values("pagina")
                'Return HttpContext.Current.Request.RequestContext.RouteData.DataTokens.Item("pagina")

                Dim pagina = HttpContext.Current.Request.RequestContext.RouteData.DataTokens("pagina")
                Return If(pagina = Nothing, Page.default, pagina)
            Catch
                Return Page.default
            End Try
        End Get
    End Property


    Public Enum Page
        log

        [default]
        home

        confirm
        forgot
        manage
        register
        resetpass
        resetpassconfirm

        'ADMIN
        usuarios
        encuestascat
        encuestasadm
        'forum
        forumcategorias
        forumtemas
        forumtags

        'USER
        encuestas

    End Enum

    Shared Sub DefinirRutas()
        'With RouteTable.Routes()
        '    For Each item In System.Enum.GetValues(GetType(current.Idiomas))
        '        Dim idioma As String = item.ToString
        '        Dim i = CInt(item)
        '        Dim iasd = CInt(item)
        '        Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo(idioma)
        '        .MapPageRoute(Page.default.ToString.ToLower & i, idioma & "/" & Resources.idioma._default, "~/Default.aspx", True, Nothing, Nothing, New Routing.RouteValueDictionary(New With {.pagina = Page.default, .idioma = i}))
        '    Next
        'End With

        With System.Web.Routing.RouteTable.Routes()
            For i As current.Idiomas = current.Idiomas.ES To current.Idiomas.EU

                Dim idioma As String = i.ToString.ToLower

                Select Case i
                    Case current.Idiomas.EU
                        Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("EU")
                    Case current.Idiomas.ES
                        Threading.Thread.CurrentThread.CurrentUICulture = New CultureInfo("ES")
                End Select

                .MapPageRoute(Page.log.ToString.ToLower & i, idioma & "/" & CorregirTexto(Resources.idioma.login), "~/log.aspx", True, Nothing, Nothing, New Routing.RouteValueDictionary(New With {.pagina = Page.log, .idioma = i}))

                .MapPageRoute(Page.default.ToString.ToLower & i, idioma & "/" & CorregirTexto(Resources.idioma._default), "~/Default.aspx", True, Nothing, Nothing, New Routing.RouteValueDictionary(New With {.pagina = Page.default, .idioma = i}))

                .MapPageRoute(Page.confirm.ToString.ToLower & i, idioma & "/" & CorregirTexto(Resources.idioma.confirmacion), "~/Account/Confirm.aspx", True, Nothing, Nothing, New Routing.RouteValueDictionary(New With {.pagina = Page.confirm, .idioma = i}))
                .MapPageRoute(Page.forgot.ToString.ToLower & i, idioma & "/" & CorregirTexto(Resources.idioma.forgot), "~/Account/Forgot.aspx", True, Nothing, Nothing, New Routing.RouteValueDictionary(New With {.pagina = Page.forgot, .idioma = i}))
                .MapPageRoute(Page.manage.ToString.ToLower & i, idioma & "/" & CorregirTexto(Resources.idioma.administrar), "~/Account/Manage.aspx", True, Nothing, Nothing, New Routing.RouteValueDictionary(New With {.pagina = Page.manage, .idioma = i}))
                .MapPageRoute(Page.register.ToString.ToLower & i, idioma & "/" & CorregirTexto(Resources.idioma.registro), "~/Account/Register.aspx", True, Nothing, Nothing, New Routing.RouteValueDictionary(New With {.pagina = Page.register, .idioma = i}))
                .MapPageRoute(Page.resetpass.ToString.ToLower & i, idioma & "/" & CorregirTexto(Resources.idioma.restablecercontrasena), "~/Account/ResetPassword.aspx", True, Nothing, Nothing, New Routing.RouteValueDictionary(New With {.pagina = Page.resetpass, .idioma = i}))
                .MapPageRoute(Page.resetpassconfirm.ToString.ToLower & i, idioma & "/" & CorregirTexto(Resources.idioma.restablecercontrasenaconfirmacion), "~/Account/ResetPasswordConfirmation.aspx", True, Nothing, Nothing, New Routing.RouteValueDictionary(New With {.pagina = Page.resetpassconfirm, .idioma = i}))

                .MapPageRoute(Page.usuarios.ToString.ToLower & i, idioma & "/" & CorregirTexto(Resources.lang.usuarios), "~/webadmin/usuarios.aspx", True, Nothing, Nothing, New Routing.RouteValueDictionary(New With {.pagina = Page.usuarios, .idioma = i}))

                .MapPageRoute(Page.encuestascat.ToString.ToLower & i, idioma & "/" & CorregirTexto(Resources.lang.encuestascat), "~/webadmin/encuestascat.aspx", True, Nothing, Nothing, New Routing.RouteValueDictionary(New With {.pagina = Page.encuestascat, .idioma = i}))
                .MapPageRoute(Page.encuestasadm.ToString.ToLower & i, idioma & "/" & CorregirTexto(Resources.lang.encuestas), "~/webadmin/encuestas.aspx", True, Nothing, Nothing, New Routing.RouteValueDictionary(New With {.pagina = Page.encuestasadm, .idioma = i}))

                'forum
                .MapPageRoute(Page.forumcategorias.ToString.ToLower & i, idioma & "/forum_categorias", "~/webadmin/Forumcategorias.aspx", True, Nothing, Nothing, New Routing.RouteValueDictionary(New With {.pagina = Page.encuestasadm, .idioma = i}))
                .MapPageRoute(Page.forumtemas.ToString.ToLower & i, idioma & "/forum_temas", "~/webadmin/TemasDelForum.aspx", True, Nothing, Nothing, New Routing.RouteValueDictionary(New With {.pagina = Page.encuestasadm, .idioma = i}))
                .MapPageRoute(Page.forumtags.ToString.ToLower & i, idioma & "/forum_tags", "~/webadmin/TagsDelForum.aspx", True, Nothing, Nothing, New Routing.RouteValueDictionary(New With {.pagina = Page.encuestasadm, .idioma = i}))

                '.MapPageRoute(Page.mensajeria.ToString.ToLower & i, idioma & "/" & CorregirTexto(Resources.idioma.mensajeria), "~/web/mensajes.aspx", True, Nothing, Nothing, New Routing.RouteValueDictionary(New With {.pagina = Page.mensajeria, .idioma = i}))
                ''.MapPageRoute(Page.mensajenuevo.ToString.ToLower & i, idioma & "/" & CorregirTexto(Resources.idioma.mensajenuevo), "~/web/mensajenuevo.aspx", True, Nothing, Nothing, New Routing.RouteValueDictionary(New With {.pagina = Page.mensajenuevo, .idioma = i}))
                '.MapPageRoute(Page.buscarusuarios.ToString.ToLower & i, idioma & "/" & "Buscar_usuarios", "~/web/buscarusuarios.aspx", True, Nothing, Nothing, New Routing.RouteValueDictionary(New With {.pagina = Page.buscarusuarios, .idioma = i}))

            Next
        End With
    End Sub


    Shared Function CorregirTexto(ByVal text As String) As String
        Dim stFormD As String = text.Normalize(NormalizationForm.FormD)
        Dim sb As New StringBuilder()

        For ich As Integer = 0 To stFormD.Length - 1
            Dim uc As UnicodeCategory = CharUnicodeInfo.GetUnicodeCategory(stFormD(ich))
            If uc <> UnicodeCategory.NonSpacingMark Then
                sb.Append(stFormD(ich))
            End If
        Next
        Return (sb.ToString().Normalize(NormalizationForm.FormC)).Replace(" ", "_")
    End Function

    Shared Function ReplaceText(ByVal text As String) As String
        Return Trim$(text.Replace("-", "_").Replace("/", "_").Replace("&", "Y").Replace("+", "_").Replace(".", "").Replace(":", "_").Replace(Chr(34), "''"))
    End Function


End Class
