Imports System.Xml
Imports System.Linq
Public Class WebForm1
    Inherits System.Web.UI.Page

    Private _XML As Object
    Public Property farmaciasDataSet As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CargarDatos()
    End Sub

    Private Sub CargarDatos()

        Dim dss As New DataSet()
        Dim dt As New DataTable()
        dt.Columns.Add(New DataColumn("Municipio", GetType(String)))
        dt.Columns.Add(New DataColumn("Num_Farmacias", GetType(Integer)))
        dt.Columns.Add(New DataColumn("Farmacia_ID", GetType(String)))
        dt.Columns.Add(New DataColumn("Nombre", GetType(String)))
        dt.Columns.Add(New DataColumn("Direccion", GetType(String)))
        dt.Columns.Add(New DataColumn("Poblacion", GetType(String)))
        dt.Columns.Add(New DataColumn("Telefono", GetType(String)))
        dt.Columns.Add(New DataColumn("Fax", GetType(String)))
        dt.Columns.Add(New DataColumn("Noficina", GetType(Integer)))
        dt.Columns.Add(New DataColumn("ID_interno", GetType(Integer)))
        dt.Columns.Add(New DataColumn("Horario", GetType(String)))


        Dim doc As XDocument
        doc = XDocument.Load(Server.MapPath("farmacias.xml"))

        Dim municipio = From muni In doc.Descendants("municipio")
                        Select New With
                       {
                           .idMunicipio = muni.Attribute("id").Value,
                           .Num_Farmacias = muni.Attribute("num_farmacias").Value,
                            .farmaciaID = muni.Descendants("farmacia")
                       }

        For Each muniF In municipio

            Dim dr As DataRow = dt.NewRow()

            'dr("Municipio") = muniF.idMunicipio
            'dr("Num_Farmacias") = muniF.Num_Farmacias
            'dt.Rows.Add(dr)
            'dt.Rows.Add(muniF.idMunicipio, muniF.Num_Farmacias)
            For Each farmacia In muniF.farmaciaID
                dt.Rows.Add(muniF.idMunicipio, muniF.Num_Farmacias, farmacia.Attribute("id").Value, farmacia.Element("nombre").Value, farmacia.Element("direccion").Value, farmacia.Element("poblacion").Value, farmacia.Element("telefono").Value, farmacia.Element("fax").Value, farmacia.Element("Noficina").Value, farmacia.Element("id_interno").Value, farmacia.Element("horario").Value)
            Next

        Next
        dss.Tables.Add(dt)

        Me.GridView1.DataSource = dss
        GridView1.DataBind()




    End Sub

    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        Me.CargarDatos()

    End Sub

End Class
