<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm1.aspx.vb" Inherits="readXMLVB.WebForm1" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="formulario" runat="server">
    <div>
        <asp:GridView ID="GridView1" HeaderStyle-BackColor="#30CCFF" HeaderStyle-ForeColor="White" 
                RowStyle-BackColor="#EBEBEB" AlternatingRowStyle-BackColor="#CAEEFA" AlternatingRowStyle-ForeColor="#000" 
                runat="server" AutoGenerateColumns="false"  AllowPaging="True" OnPageIndexChanging="OnPageIndexChanging"
                CellPadding="4" PageSize="15" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellSpacing="2" ForeColor="Black"  >
            <Columns>
                <asp:BoundField DataField="Municipio" HeaderText="Municipio"/>
                <asp:BoundField DataField="Num_Farmacias" HeaderText="Numero de Farmacias"/>
                <asp:BoundField DataField="Farmacia_ID" HeaderText="Farmacia ID" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Direccion" HeaderText="Direccion" />
                <asp:BoundField DataField="Poblacion" HeaderText="Poblacion" />
                <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                <asp:BoundField DataField="Fax" HeaderText="Fax" />
                <asp:BoundField DataField="Noficina" HeaderText="Nª oficina" />
                <asp:BoundField DataField="ID_interno" HeaderText="Id interno" />
                <asp:BoundField DataField="Horario" HeaderText="Horario" />                             
            </Columns>            
        </asp:GridView>        
    </div>
    </form>
</body>
</html>
