Imports System.Data
Imports System.Drawing
Imports Telerik.Web.UI
Partial Class TemasDelForum
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Ltitulo.Text = Resources.lang.encuestascat.ToUpper

        If Request.Params("__EVENTTARGET") = "dobleclick" Then
            Dim args = Request.Params("__EVENTARGUMENT")
            Abrir(args)
        End If

        If Not IsPostBack Then
            Me.CargarTextos()
        End If

        Dim ds = ForumTags.get_Tags()
        ' Dim i As Integer = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Dim datos As List(Of String) = New List(Of String)
            For Each row In ds.Tables(0).Rows
                Dim prueba = row("nombre")
                datos.Add(prueba)

            Next
            RadAutoCompleteBox1.DataSource = datos
        End If



    End Sub
    Private Sub CargarTextos()
        Bbai.ToolTip = Resources.lang.si
        Bez.ToolTip = Resources.lang.no
        'Ltit.Text = Resources.lang.categoria.ToUpper
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
    Function enabledisable() As Boolean
        Return True
    End Function
    Function showModal() As String
        Return "return showModal('" & RGlista.ClientID.ToString & "','" & Resources.lang.alertaeliminar & "','" & Bbai.ClientID.ToString & "','" & Bez.ClientID.ToString & "','" & modalDialog.ClientID.ToString & "');"
    End Function
    Protected Sub RGlista_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RGlista.NeedDataSource
        CargarDatos()

    End Sub
    Protected Sub autocompletar()
        Dim ds = ForumTags.get_Tags()

        If ds.Tables(0).Rows.Count > 0 Then
            For Each row In ds.Tables(0).Rows
                Dim datos As List(Of String) = row("nombre")
                RadAutoCompleteBox1.DataSource = datos
            Next
        End If
    End Sub

    Private Sub CargarDatos()
        Dim ds = ForumTemas.Get_Temas()
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
            CBcategoria.ClearSelection()
            ' CBusuario.ClearSelection()
            CBactivo.Checked = True
            'TBusuario.Text = String.Empty
            TBusername.Text = String.Empty
            TBnombre1.Text = String.Empty
            tbdate.Text = String.Empty
            TBnombre2.Text = String.Empty
            TBTexto.Content = String.Empty
            RadAutoCompleteBox1.Entries.Clear()
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

    'Private Sub CargarTags(ByVal codselect As String)
    '    Pcategoria.Visible = True
    '    HFcodigo.Value = codselect
    '    Dim ds = ForumTags.get_Relationtag(codselect)
    '    If ds.Tables.Count > 0 Then
    '        taglista.DataSource = ds
    '    End If
    'End Sub

    Private Sub Abrir(ByVal codselect As String)
        Pcategoria.Visible = True
        HFcodigo.Value = codselect
        CargarCombos()
        RadAutoCompleteBox1.Entries.Clear()
        cargarTags(codselect)
        If Not IsNothing(codselect) Then
            Dim dt As DataTable = ForumTemas.Get_tema(codselect).Tables(0)


            '   Dim dt2 = ForumTags.get_Relationtag(codselect).Tables(0)
            'If dt2.Tables.Count > 0 Then
            '    taglista.DataSource = dt2
            'End If

            '  If dt2.Rows.Count > 0 Then
            '  CBtags.SelectedValue = CInt(dt2.Rows(0).Item("codigo"))
            'End If


            If dt.Rows.Count > 0 Then
                    ' TBcodigoCategoria.Text = dt.Rows(0).Item("nombreCategoria").ToString
                    CBcategoria.SelectedValue = CInt(dt.Rows(0).Item("codcategoria"))
                    ' CBusuario.SelectedValue = dt.Rows(0).Item("codusuario")
                    TBusuario.Text = dt.Rows(0).Item("codusuario").ToString
                    TBnombre1.Text = dt.Rows(0).Item("nombre1").ToString
                    TBnombre2.Text = dt.Rows(0).Item("nombre2").ToString
                    tbdate.Text = dt.Rows(0).Item("fecha").ToString
                    TBusername.Text = dt.Rows(0).Item("UserName").ToString
                TBTexto.Content = dt.Rows(0).Item("comentario").ToString
                CBactivo.Checked = CBool(dt.Rows(0).Item("activo"))
            End If
            End If
    End Sub

    Protected Sub CargarCombos()
        Dim ds = ForumTemas.Get_categoriasActivo("foro_categorias")
        If ds.Tables.Count > 0 Then
            CBcategoria.DataSource = ds
            CBcategoria.DataBind()
        End If
    End Sub

    Protected Sub cargarTags(codigo As Integer)

        Dim ds = ForumTags.get_Relationtag(codigo)
        If ds.Tables.Count > 0 Then
            '     CBtags.DataSource = ds
            '    CBtags.DataBind()
            For Each tags In ds.Tables(0).Rows
                RadAutoCompleteBox1.Entries.Add(New AutoCompleteBoxEntry(tags("nombretag").ToString()))
            Next
            'RadAutoCompleteBox1.Entries.Insert(0, New AutoCompleteBoxEntry(ds.Tables(0).Rows(0)("nombretag").ToString()))


        End If
    End Sub

    'Protected Sub BTdelete_click(sender As Object, e As EventArgs) Handles BTdelete.Click

    '    Dim codigotag = CBtags.CheckedItems

    '    For Each tags In codigotag

    '        ForumTags.delete_relationTemaTag(tags.Value)
    '    Next
    '    ' Dim codigotag As String = CBtags.SelectedItem.Value


    'End Sub

    Private Sub Reset()

        CBcategoria.ClearSelection()
        TBnombre1.Text = String.Empty
        TBnombre2.Text = String.Empty
        TBTexto.Content = String.Empty
        ' TBtags.Text = String.Empty
        CBactivo.Checked = True
        'DatosTags.Value = String.Empty
        '  CBtags.ClearSelection()
        RGlista.Rebind()
        Pcategoria.Visible = False
        RadAutoCompleteBox1.Entries.Clear()
    End Sub
    Protected Sub Bcancelar_Click(sender As Object, e As EventArgs) Handles Bcancelar.Click
        ' CargarDatos()
        Reset()
    End Sub

    Protected Sub Bguardar_Click(sender As Object, e As EventArgs) Handles Bguardar.Click
        If Page.IsValid Then
            Dim codigo As String = HFcodigo.Value
            Dim nombre2 = IIf(String.IsNullOrEmpty(Trim(TBnombre2.Text)), Trim(TBnombre1.Text), Trim(TBnombre2.Text))
            If Not String.IsNullOrEmpty(codigo) Then
                ForumTemas.Update_tema(codigo, CBcategoria.SelectedValue, Trim(TBusuario.Text), Trim(TBnombre1.Text), nombre2, CBool(CBactivo.Checked))
                ForumTemas.Update_post(codigo, Trim(TBTexto.Content))
                creartag(codigo)
            Else
                Dim codtema As Integer = ForumTemas.Insert_tema(codigo, CBcategoria.SelectedValue, current.Usuario, Trim(TBnombre1.Text), nombre2, CBool(CBactivo.Checked))
                ForumTemas.Insert_post(codtema, current.Usuario, Trim(TBTexto.Content), CBool(CBactivo.Checked))
                creartag(codtema)


            End If
            Reset()
        End If
    End Sub

    Protected Sub creartag(codigoTema As Integer)

        If Not IsNothing(RadAutoCompleteBox1.Entries) Then

            'comprobar si se han eliminado los tag de la entrada
            Dim entrada = RadAutoCompleteBox1.Entries

            Dim consulta = ForumTags.get_Relationtag(codigoTema)
            Dim datosconsulta As List(Of String) = New List(Of String)

            If (consulta.Tables(0).Rows.Count > 0) Then
                For Each tag In consulta.Tables(0).Rows
                    Dim tagcodigo = tag("codigo")
                    Dim tagnombre = tag("nombretag")
                    datosconsulta.Add(tagnombre)
                    If (entrada.ToString.Contains(tagnombre)) Then
                        Dim ver = tagcodigo
                        For Each datos As AutoCompleteBoxEntry In RadAutoCompleteBox1.Entries
                            Try  'si el tag esta creado...

                                Dim tagcreado As DataSet = ForumTags.get_tagNombre(datos.Text)
                                Dim codigoTag = tagcreado.Tables(0).Rows(0)("codigo").ToString
                                'ForumTags.insert_relationTemaTags(codigoTema, codigoTag)

                                Try
                                    'si esto da una exception va a ir al catch exx a insertar esa relacion tema/tag nueva
                                    Dim relacionTemaTag As DataSet = ForumTags.get_RelationtagTema(codigoTag, codigoTema)
                                    Dim codtag = relacionTemaTag.Tables(0).Rows(0)("codigo").ToString
                                    ForumTags.Update_relationTematags(codtag, codigoTema, codigoTag)

                                Catch exx As Exception
                                    ForumTags.insert_relationTemaTags(codigoTema, codigoTag)
                                End Try

                            Catch ex As Exception 'no vale crear tag nuevo desde aqui----- solo desde la pantalla tag
                                'Dim codtag As Integer = ForumTags.insert_tags(datos.Text)
                                'ForumTags.insert_relationTemaTags(codigoTema, codtag)
                            End Try

                        Next
                    Else
                        ForumTags.delete_relationTemaTag(tagcodigo)
                    End If
                Next
            Else
                'comprobar si esta tag esta creado
                For Each datos As AutoCompleteBoxEntry In RadAutoCompleteBox1.Entries
                    Try  'si el tag esta creado...

                        Dim tagcreado As DataSet = ForumTags.get_tagNombre(datos.Text)
                        Dim codigoTag = tagcreado.Tables(0).Rows(0)("codigo").ToString
                        'ForumTags.insert_relationTemaTags(codigoTema, codigoTag)

                        Try
                            'si esto da una exception va a ir al catch exx a insertar esa relacion tema/tag nueva
                            Dim relacionTemaTag As DataSet = ForumTags.get_RelationtagTema(codigoTag, codigoTema)
                            Dim codtag = relacionTemaTag.Tables(0).Rows(0)("codigo").ToString
                            ForumTags.Update_relationTematags(codtag, codigoTema, codigoTag)

                        Catch exx As Exception
                            ForumTags.insert_relationTemaTags(codigoTema, codigoTag)
                        End Try

                    Catch ex As Exception 'no vale crear tag nuevo desde aqui----- solo desde la pantalla tag
                        '     Dim codtag As Integer = ForumTags.insert_tags(datos.Text)
                        '    ForumTags.insert_relationTemaTags(codigoTema, codtag)
                    End Try

                Next

            End If


            ''comprobar si esta tag esta creado
            'For Each datos As AutoCompleteBoxEntry In RadAutoCompleteBox1.Entries
            '    Try  'si el tag esta creado...

            '        Dim tagcreado As DataSet = ForumTags.get_tagNombre(datos.Text)
            '        Dim codigoTag = tagcreado.Tables(0).Rows(0)("codigo").ToString
            '        'ForumTags.insert_relationTemaTags(codigoTema, codigoTag)

            '        Try
            '            'si esto da una exception va a ir al catch exx a insertar esa relacion tema/tag nueva
            '            Dim relacionTemaTag As DataSet = ForumTags.get_RelationtagTema(codigoTag, codigoTema)
            '            Dim codtag = relacionTemaTag.Tables(0).Rows(0)("codigo").ToString
            '            ForumTags.Update_relationTematags(codtag, codigoTema, codigoTag)

            '        Catch exx As Exception
            '            ForumTags.insert_relationTemaTags(codigoTema, codigoTag)
            '        End Try

            '    Catch ex As Exception 'no vale crear tag nuevo desde aqui----- solo desde la pantalla tag
            '        Dim codtag As Integer = ForumTags.insert_tags(datos.Text)
            '        ForumTags.insert_relationTemaTags(codigoTema, codtag)
            '    End Try

            'Next
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
                    ' count = Forumtemas.Get_Count(codselect, "encuestas")

                    If count > 0 Then
                        nombres = nombres & sel.GetDataKeyValue("nombre").ToString & "<br />"
                    Else
                        ForumTemas.Delete_tema(codselect)
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
