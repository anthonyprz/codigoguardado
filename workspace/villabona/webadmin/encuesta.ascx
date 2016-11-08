<%@ Control Language="VB" AutoEventWireup="false" CodeFile="encuesta.ascx.vb" Inherits="web_encuesta" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
 
<%@ Register TagPrefix="wc" TagName="ENCUESTAVOT" Src="~/webadmin/encuestavotacion.ascx" %>

<script type="text/javascript">
    function onchangea() {
        var div = document.getElementById("<%= Dcantidad.ClientID %>");
        if (document.getElementById("<%= CBtipo.ClientID %>").value != "1") {
            div.hidden = true;
        }
        else {
            div.hidden = false;
        }
    }
    function closea(sender, args) {
        document.getElementById('<%=Bcancelar.ClientID%>').click();
    }
</script>

<asp:HiddenField runat="server" ID="HFCodselect" />
<asp:HiddenField runat="server" ID="HFdelete" />


<div runat="server" id="Dform" class="form-horizontal" >
    <div class="row col">
        <h3><asp:Label runat="server" ID="Ltit" Text="<%$ Resources:lang,encuesta %>" ></asp:Label></h3>
        <div class="dostab">
        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1" SelectedIndex="0" Align="Justify">
            <Tabs>
                <telerik:RadTab runat="server" Text="Encuesta" Selected="True"></telerik:RadTab>
                <telerik:RadTab runat="server" Text="Resultado" ></telerik:RadTab> 
            </Tabs>
        </telerik:RadTabStrip>
        </div>
        <br />
        <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0">
            <telerik:RadPageView runat="server" ID="RadPageView1" CssClass="pageView1 pageView">
                <div class="form-group">
                    <asp:Label runat="server" ID="Lcategoria" AssociatedControlID="CBcategoria" CssClass="col-md-2 control-label" Text="<%$ Resources:lang,categoria %>" />
                    <div class="col-md-5">
                        <%--<asp:DropDownList runat="server" ID="CBcategoria" DataValueField="codigo" DataTextField="nombre" CssClass="form-control"></asp:DropDownList>--%>
                        <telerik:RadComboBox runat="server" ID="CBcategoria" DataValueField="codigo" DataTextField="nombre" Width="100%" ></telerik:RadComboBox>       
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" ID="Lnombre" AssociatedControlID="TBnombre1" CssClass="col-md-2 control-label" Text="<%$ Resources:lang,nombre %>" />
                    <div class="col-md-5">
                        <asp:TextBox runat="server" ID="TBnombre1" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="Vnombre1" ControlToValidate="TBnombre1" 
                            CssClass="text-danger" ErrorMessage="!!" ValidationGroup="FormValidationGroup" /> 
                    </div>
                    <div class="col-md-5">
                        <asp:TextBox runat="server" ID="TBnombre2" CssClass="form-control"></asp:TextBox> 
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" ID="Lfechas" AssociatedControlID="RadDateTimePicker1" CssClass="col-md-2 control-label" Text="<%$ Resources:lang,fechas %>" />
                    <div class="form-group">
                        <div class="col-md-3">
                            <telerik:RadDateTimePicker runat="server" ID="RadDateTimePicker1" Width="100%" AutoPostBackControl="Both">
                                <Calendar ID="Calendar1" runat="server" EnableKeyboardNavigation="true">
                                </Calendar>
                                <%--<ClientEvents OnDateSelected="OnClientDateChanged1" />--%>
                            </telerik:RadDateTimePicker>
                        </div>
                        <div class="col-md-3">
                            <telerik:RadDateTimePicker runat="server" ID="RadDateTimePicker2" Width="100%" AutoPostBackControl="Both">
                                <Calendar ID="Calendar2" runat="server" EnableKeyboardNavigation="true">
                                </Calendar>
                                <%--<ClientEvents OnDateSelected="OnClientDateChanged2" />--%>
                            </telerik:RadDateTimePicker>
                        </div>
                        <div class="col-md-2" runat="server" id="Derror" visible="false">
                            <div class="alert-danger" role="alert">
                                <%--<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>--%>
                                <asp:Label runat="server" ID="Lerror" CssClass="alert-danger" Text="!!!"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" ID="Lactivo" AssociatedControlID="CBactivo" Text="Activo" CssClass="col-md-2 control-label"/>
                    <div class="col-md-10">
                        <div class="checkbox">
                            <asp:CheckBox runat="server" ID="CBactivo" Text="Activo" Checked="true" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" ID="Lverresultado" AssociatedControlID="CBverresultado" Text="Ver resultado" CssClass="col-md-2 control-label"/>
                    <div class="col-md-10">
                        <div class="checkbox">
                            <asp:CheckBox runat="server" ID="CBverresultado" Text="Verresultado" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" ID="Ltipo" AssociatedControlID="CBtipo" CssClass="col-md-2 control-label" Text="<%$ Resources:lang,tipo %>" />
                    <div class="col-md-3">
                        <asp:DropDownList runat="server" ID="CBtipo" CssClass="form-control" onchange="onchangea()">
                            <asp:ListItem Text="Seleccionar entre varias opciones" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Valoracion por puntos" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Si / No" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div  class="col-md-6">
                        <div id="Dcantidad" runat="server">
                            <asp:Label runat="server" ID="Lcantidad" AssociatedControlID="TBcantidad" CssClass="col-md-2 control-label" Text="Cantidad" />
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="TBcantidad" TextMode="Number" CssClass="form-control input-md"></asp:TextBox>
                <%--                <telerik:RadNumericTextBox ID="RadNumericTextBox1" runat="server" ShowSpinButtons="true" MinValue="0" NumberFormat-DecimalDigits="0" ></telerik:RadNumericTextBox>--%>
                            </div>
                        </div>

                    </div>
                </div> 
                

                <div class="form-group">
                    <asp:Label runat="server" ID="Lpregunta" AssociatedControlID="TBpregunta1" CssClass="col-md-2 control-label" Text="<%$ Resources:lang,pregunta %>" />
                    <div class="col-md-5">
                        <asp:TextBox runat="server" ID="TBpregunta1" CssClass="form-control" TextMode="MultiLine"></asp:TextBox> 
                    </div>
                    <div class="col-md-5">
                        <asp:TextBox runat="server" ID="TBpregunta2" CssClass="form-control" TextMode="MultiLine"></asp:TextBox> 
                    </div>
                </div>
                <%-------------------------------%>
                <div class="form-group">
                    <asp:Label runat="server" ID="Lopciones" AssociatedControlID="RadGrid1" CssClass="col-md-2 control-label" Text="<%$ Resources:lang,opciones %>" />
                    <div class="col-md-10">          
                        <telerik:RadGrid ID="RadGrid1" runat="server" ShowHeader="false" Skin="Default" >
                            <MasterTableView AutoGenerateColumns="False" EditMode="InPlace" CommandItemDisplay="Top" DataKeyNames="codigo" InsertItemDisplay="Bottom">
                                <Columns>
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" >
                                        <ItemStyle CssClass="MyImageButton" Width="45px"/>
                                    </telerik:GridEditCommandColumn> 
                         
                                    <telerik:GridBoundColumn UniqueName="codigo" DataField="codigo" HeaderText="<%$ Resources:lang,codigo %>" Visible="false" />
                                    <%--<telerik:GridBoundColumn DataField="texto1" HeaderText="texto1" SortExpression="texto1" UniqueName="texto1" ItemStyle-Width="41%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="texto2" HeaderText="texto2" SortExpression="texto2" UniqueName="texto2" ItemStyle-Width="41%">
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridTemplateColumn UniqueName="texto1" HeaderText="texto1" DataField="texto1" SortExpression="texto1" ItemStyle-Width="41%">   
                                        <ItemTemplate> 
                                            <label><%# Eval("texto1") %></label>   
                                        </ItemTemplate>  
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="TBtexto1" TextMode="MultiLine" Width="100%" Text='<%# Eval("texto1") %>'></asp:TextBox>  
                                        </EditItemTemplate>  
                                    </telerik:GridTemplateColumn>
                        
                                    <telerik:GridTemplateColumn UniqueName="texto2" HeaderText="texto2" DataField="texto2" SortExpression="texto2" ItemStyle-Width="41%">   
                                        <ItemTemplate> 
                                            <label><%# Eval("texto2") %></label>   
                                        </ItemTemplate>  
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="TBtexto2" TextMode="MultiLine" Width="100%" Text='<%# Eval("texto2") %>'></asp:TextBox>  
                                        </EditItemTemplate>  
                                    </telerik:GridTemplateColumn>


                                    <telerik:GridTemplateColumn UniqueName="color" HeaderText="color" DataField="color" SortExpression="color" ItemStyle-Width="35px">   
                                        <ItemTemplate>  
                                            <div style='width: 16px; height: 16px; background-color: <%# Eval("color") %>'></div>  
                                        </ItemTemplate>  
                                        <EditItemTemplate>  
                                            <telerik:RadColorPicker runat="server" ShowIcon="true" ID="RadColorPicker1" SelectedColor='<%# HTML2Color(myCStr(Eval("color"))) %>' />  
                                        </EditItemTemplate>  
                                    </telerik:GridTemplateColumn>
                        
                                    <telerik:GridButtonColumn Text="Delete" CommandName="Delete" ButtonType="ImageButton">
                                        <ItemStyle Width="20px" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="RadPageView2" CssClass="pageView1 pageView">
                <wc:ENCUESTAVOT runat="server" ID="wcENCUESTAVOT" ></wc:ENCUESTAVOT>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
         
                 
    </div>
    <div class="row botoiak">
        <telerik:RadButton runat="server" ID="Bguardar" SkinID="Bguardar" ValidationGroup="FormValidationGroup"></telerik:RadButton>
        <telerik:RadButton runat="server" ID="Bcancelar" SkinID="Bcancelar" ></telerik:RadButton>
        <telerik:RadButton runat="server" ID="Bnotificacion" SkinID="Bnotificacion"></telerik:RadButton>
        <telerik:RadButton runat="server" ID="Bresetear" SkinID="Bresetear"></telerik:RadButton>
    </div>
                         
</div>