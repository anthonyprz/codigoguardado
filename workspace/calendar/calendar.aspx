<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Index.aspx.vb" Inherits="Calendar.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
  <link  rel="stylesheet" href="css/style.css" />
</head>
<body>
    <form id="form2">
        <asp:Label ID="infor"
            text=""
            runat="server">
        </asp:Label>
    </form>
    <form id="form1" runat="server">
    <div>       
     <asp:Calendar ID="Calendar1"  class="calendar" runat="server" SelectionMode="DayWeekMonth" 
           ShowGridLines="True" OnSelectionChanged="Selection_Change">
     </asp:Calendar>
    </div> 
        <div class="diaSeleccionado">
             <table border="1">
         <tr style="background-color:silver">
            <th>
               Selected Dates:
            </th>    
          </tr>
           <tr> 
            <td>
               <asp:Label id="Message" 
                    Text="No dates selected." 
                    runat="server"/>                               
            </td>
         </tr>
      </table>

        </div>           
        
        <div class="GridView3">
         <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="true" CellSpacing="4" >
                    <Columns>                     
                    </Columns>
                </asp:GridView>
       </div> 
    
         <div class="contenedor-categoria">
            <table>
                <tr><td><asp:TextBox ID="TextBox1" runat="server" placeholder="Title"></asp:TextBox></td></tr>
                <tr><td><asp:TextBox ID="TextBox2" runat="server" placeholder="Link"></asp:TextBox></td></tr>
                <tr><td><asp:TextBox ID="TextBox3" runat="server" placeholder="Type"></asp:TextBox></td></tr>
                <tr><td><asp:TextBox ID="TextBox4" runat="server" placeholder="Organizer"></asp:TextBox></td></tr>
                <tr><td><asp:TextBox ID="TextBox5" runat="server" placeholder="Location"></asp:TextBox></td></tr>
                <tr><td><asp:TextBox ID="TextBox6" runat="server" placeholder="StartDate"></asp:TextBox></td></tr>
                <tr><td><asp:TextBox ID="TextBox7" runat="server" placeholder="EndDate"></asp:TextBox></td></tr>
                <tr><td><asp:TextBox ID="TextBox8" runat="server" placeholder="id_ekintza"></asp:TextBox></td></tr>
                <tr><td><asp:Button ID="Button1" runat="server" text="add" OnClick="Button1_Click" /></td></tr>
            </table>
      </div>
    </form>
</body>
</html>
