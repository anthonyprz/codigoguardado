Imports Telerik.Web.UI
Imports System.Data
Imports Microsoft.AspNet.Identity.EntityFramework
Imports System.Data.SqlClient
Imports Newtonsoft.Json
Imports System.Drawing


Partial Class web_encuestavotacion
    Inherits System.Web.UI.UserControl

    Public _codselect As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargarTextos()
        End If
    End Sub

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If String.IsNullOrEmpty(HFCodselect.Value) Then
            HFCodselect.Value = _codselect
        End If
        CargarDatos()
        CargarGrafico()
    End Sub


    Private Sub CargarTextos()
        'Ltit.Text = Resources.idioma.encuesta.ToUpper
        'Lnombre.Text = Resources.idioma.nombre.ToUpper

        'Bcancelar.Text = Resources.idioma.cancelar.ToUpper
        'Bguardar.Text = Resources.idioma.guardar.ToUpper

        Lparticipantes.Text = Resources.lang.participantes.ToUpper
    End Sub

    Private Sub CargarDatos()
        If Not IsNothing(HFCodselect.Value) AndAlso Not String.IsNullOrEmpty(HFCodselect.Value) Then
            Dim dbConnection As SqlConnection = current.SqlConnection
            Dim reader As SqlDataReader = encuestas.Get_EncuestaLang(HFCodselect.Value, dbConnection)
            Try
                If reader.Read Then
                    'TBnombre.Text = reader("nombre").ToString
                    Ltit.Text = reader("nombre").ToString
                    TBpregunta.Text = reader("pregunta").ToString
                    HFtipo.Value = reader("tipo").ToString

                    TBparticipantes.Text = reader("participantes").ToString

                    PieChart.ChartTitle.Text = reader("pregunta").ToString
                    ColumnChart.ChartTitle.Text = reader("pregunta").ToString
                    BarChart.ChartTitle.Text = reader("pregunta").ToString
                End If
            Catch ex As Exception
            Finally
                reader.Close()
                dbConnection.Close()
            End Try

            RadGrid1.DataSource = encuestas.Get_OpcionesLang(HFCodselect.Value).Tables(0)
            RadGrid1.DataBind()
        End If
    End Sub

    Private Sub CargarGrafico()
        If Not IsNothing(HFtipo.Value) AndAlso Not String.IsNullOrEmpty(HFtipo.Value) Then
            Dim dt As DataTable = RadGrid1.DataSource
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then

                Select Case CInt(HFtipo.Value)
                    Case 1
                        PieChart.Visible = True
                        ColumnChart.Visible = True
                        Ptipo2.Visible = False
                        BarChart.Visible = False

                        If Not IsNothing(dt) Then
                            'tipo  = 1 PIE
                            Dim puntostotal = dt.AsEnumerable().Sum(Function(dr) dr.Field(Of Integer)("puntos"))
                            Dim puntos As Integer = 0
                            Dim porcen As Decimal = 0

                            Dim chartData As New PieSeries
                            For Each row As DataRow In dt.Rows
                                puntos = CInt(row("puntos"))
                                Try : porcen = (puntos * 100) / puntostotal : Catch : porcen = 0 : End Try

                                Dim item As New SeriesItem()
                                item.Name = row("texto")
                                item.YValue = Decimal.Round(porcen, 2)
                                item.BackgroundColor = ColorTranslator.FromHtml(row("color"))
                                chartData.Items.Add(item)
                            Next
                            PieChart.PlotArea.Series.Add(chartData)
                        End If

                        'tipo = 1 COLUMN
                        For Each row As DataRow In dt.Rows
                            Dim columserie As New ColumnSeries
                            columserie.Name = row("texto")
                            columserie.Appearance.FillStyle.BackgroundColor = ColorTranslator.FromHtml(row("color"))
                            columserie.Gap = 0
                            columserie.Spacing = 0.20000000000000001

                            Dim item As New SeriesItem()
                            item.Name = row("texto")
                            item.YValue = CInt(row("puntos"))
                            item.BackgroundColor = ColorTranslator.FromHtml(row("color"))
                            columserie.Items.Add(item)
                            'columserie.LabelsAppearance.DataFormatString = "{0} " & Resources.idioma.voto_s
                            columserie.TooltipsAppearance.DataFormatString = "{0} " & Resources.lang.voto_s & "<br />" & row("texto")
                            ColumnChart.PlotArea.Series.Add(columserie)
                        Next
                        'ColumnChart.DataSource = dt
                        'ColumnChart.DataBind()

                        'Dim column = DirectCast(ColumnChart.PlotArea.Series.Item(0), ColumnSeries)
                        'column.LabelsAppearance.DataFormatString = "{0} " & Resources.idioma.voto_s
                        'column.TooltipsAppearance.DataFormatString = "{0} " & Resources.idioma.voto_s
                    Case 2
                        PieChart.Visible = False
                        ColumnChart.Visible = False
                        Ptipo2.Visible = True
                        BarChart.Visible = False

                    Case 3
                        PieChart.Visible = False
                        ColumnChart.Visible = False
                        Ptipo2.Visible = False
                        BarChart.Visible = True
                        'tipo=3 SI/NO
                        BarChart.ViewStateMode = UI.ViewStateMode.Disabled
                        Dim kop = dt.Rows.Count
                        BarChart.Height = kop * 120

                        For Each row As DataRow In dt.Rows
                            BarChart.PlotArea.XAxis.Items.Add(row("texto"))

                            Dim si As Integer = CInt(row("si"))
                            Dim no As Integer = CInt(row("no"))
                            Dim siporcen As Decimal = 0
                            Try : siporcen = (si * 100) / (si + no) : Catch : siporcen = 0 : End Try

                            Dim itemsi As New SeriesItem()
                            itemsi.Name = row("texto")
                            itemsi.YValue = Decimal.Round(siporcen, 2)
                            itemsi.BackgroundColor = ColorTranslator.FromHtml(row("color"))

                            Dim itemno As New SeriesItem()
                            itemno.Name = row("texto")
                            itemno.YValue = Decimal.Round(100 - siporcen, 2)
                            itemno.BackgroundColor = ColorTranslator.FromHtml("#828282")

                            'series.TooltipsAppearance.DataFormatString = "{0} " & Resources.idioma.voto_s & "<br />" & row("texto")

                            BarChart.PlotArea.Series(0).Items.Add(itemsi)
                            BarChart.PlotArea.Series(1).Items.Add(itemno)
                        Next
                End Select
            Else
                PieChart.Visible = False
                ColumnChart.Visible = False
                Ptipo2.Visible = False
                BarChart.Visible = False
            End If
        End If
    End Sub

    Public Sub Reset()
        'Dim Pusu As Control = Me.FindControl("Dform")
        'For Each ctl As Control In Pusu.Controls
        '    If TypeOf ctl Is TextBox Then
        '        CType(ctl, TextBox).Text = String.Empty
        '    ElseIf TypeOf ctl Is CheckBox Then
        '        CType(ctl, CheckBox).Checked = False
        '    End If
        'Next

        _codselect = Nothing
        HFCodselect.Value = String.Empty
        HFtipo.Value = String.Empty
        TBparticipantes.Text = String.Empty

        TBpregunta.Text = String.Empty


        PieChart.Visible = False
        ColumnChart.Visible = False
        Ptipo2.Visible = False
        BarChart.Visible = False
    End Sub

    'Protected Sub Bcancelar_Click(sender As Object, e As EventArgs) Handles Bcancelar.Click
    '    Reset()
    'End Sub

    'Protected Sub Bguardar_Click(sender As Object, e As EventArgs) Handles Bguardar.Click

    '    Reset()
    'End Sub

    'Protected Sub ColumnChart_Load(sender As Object, e As EventArgs) Handles ColumnChart.Load
    '    ColumnChart.DataSource = encuestas.Get_OpcionesLang(13).Tables(0)
    '    ColumnChart.DataBind()

    'End Sub
End Class
