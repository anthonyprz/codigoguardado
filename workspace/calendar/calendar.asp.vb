Imports System.Xml
Imports System.Linq
Imports System.Web.UI.WebControls
Imports System.Globalization
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.IO

Public Class Index

    Inherits System.Web.UI.Page
    Dim rdf As XNamespace = "http://purl.org/rss/1.0/"
    Dim ev As XNamespace = "http://purl.org/rss/1.0/modules/event/"
    Dim dc As XNamespace = "http://purl.org/dc/elements/1.1/"
    Dim doc As XDocument = XDocument.Load(Server.MapPath("kultur_erreserbak.xml"))

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' Let item = (From items In thexml.Descendants("items"))
        '     Let seq = (From Seq In thexml.Descendants("items").Descendants(rdf + "Seq")
        '                         Select Case Seq.Element(rdf + "li").Attribute(rdf + "resource").Value
        ')

        '(From ev In feed.Element("events").Elements("location")ev.Attribute("city").Value).Aggregate(Function(x, y)
        'Convert.ToString(x) & "<br />" & Convert.ToString(y))

        Dim channel = From thexml In doc.Descendants(rdf + "channel")
                      Select New With
                            {
                            .title = thexml.Element(rdf + "title").Value,
                            .Link = thexml.Element(rdf + "link").Value,
                            .description = thexml.Element(rdf + "description").Value
                            }
        For Each titulo In channel
            infor.Text = titulo.title + " " + titulo.Link + " " + titulo.description
        Next
    End Sub

    Sub Selection_Change(sender As Object, e As EventArgs)
        Message.Text = ""

        Dim day As DateTime
        For Each day In Calendar1.SelectedDates
            Message.Text &= day.Date.ToString("yyyy/MM/dd")
            Dim evento = From kulturerres In doc.Descendants(rdf + "item")
                         Let spl = kulturerres.Element(ev + "startdate").Value.Split(" ")
                         Where spl(0) = day.Date.ToString("yyyy/MM/dd")
                         Select New With
                                 {
                            .Title = kulturerres.Element(rdf + "title").Value,
                            .Link = kulturerres.Element(rdf + "link").Value,
                            .type = kulturerres.Element(ev + "type").Value,
                            .organizer = kulturerres.Element(ev + "organizer").Value,
                            .location = kulturerres.Element(ev + "location").Value,
                            .startdate = kulturerres.Element(ev + "startdate").Value,
                            .enddate = kulturerres.Element(ev + "enddate").Value,
                            .subject = kulturerres.Element(dc + "subject").Value,
                            .id_ekintza = kulturerres.Element(dc + "id_ekintza").Value
                                          }
            GridView3.DataSource = evento.ToList
            GridView3.DataBind()
        Next
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Dim rdf2 As XNamespace = "http: //www.w3.org/1999/02/22-rdf-syntax-ns#"

        Dim ele As XElement = XElement.Load(Server.MapPath("kultur_erreserbak.xml"))
        Dim save As XElement = XElement.Load(Server.MapPath("save.xml"))

        Dim xmlTree = New XElement(rdf + "item",
                             New XAttribute(rdf2 + "about", "http://www.arrasate-mondragon.net/"),
                                      New XElement(rdf + "title", TextBox1.Text),
                                      New XElement(rdf + "link", TextBox2.Text),
                                      New XElement(ev + "type", TextBox3.Text),
                                      New XElement(ev + "organizer", TextBox4.Text),
                                      New XElement(ev + "location", TextBox5.Text),
                                      New XElement(ev + "startdate", TextBox6.Text),
                                      New XElement(ev + "enddate", TextBox7.Text),
                                      New XElement(dc + "subject", ""),
                                      New XElement(dc + "id_ekintza", TextBox8.Text)
                             )
        ele.Add(xmlTree)
        ele.Save(Server.MapPath("kultur_erreserbak.xml"))

        save.Add(xmlTree)
        save.Save(Server.MapPath("save.xml"))
    End Sub
End Class