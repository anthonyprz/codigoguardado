<%@ Page Title="Forum Categorias" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Forumcategorias.aspx.vb" Inherits="CategoriasDelForum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
    <script type="text/javascript">
        function RowDblClick(sender, eventArgs) {
            var codigo = eventArgs.getDataKeyValue("ID");
            __doPostBack('dobleclick', codigo);
        }
    </script>
</telerik:RadCodeBlock>
    <div class="form-horizontal" > 
    <asp:Panel runat="server" ID="modalDialog">
        <span id="sErroresLoginList" style="visibility: hidden;" class="errors errCestaList"> 
            <span id="texto"></span>
            <asp:Button runat="server" ID="Bbai" CssClass="confirm_bai" />
            <asp:Button runat="server" ID="Bez" CssClass="confirm_ez" />
        </span>
    </asp:Panel>
    <%-- titulo --%>
    <div>
	    <h2>
            <asp:Label ID="Ltitulo" runat="server" Text=""></asp:Label>
        </h2>
	</div>
    <div>
        <telerik:RadGrid ID="RGlista" runat="server"  SkinID="Grid" OnNeedDataSource="RGlista_NeedDataSource">
            <MasterTableView DataKeyNames="codigo, nombre1" ClientDataKeyNames="codigo">
              <%-- ↓↓botones etidar, nuevo, elimiar, actualizar↓↓ --%>
                  <CommandItemTemplate>                
                    <div class="menu_editar">
                        <asp:LinkButton runat="server" ID="Beditatu" CssClass="btnEditatu" CommandName="EditSelected" Text="<%$ Resources:lang,editar %>" Visible='<%# RGLista.EditIndexes.Count = 0 %>'></asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton runat="server" ID="Bberria" CssClass="btnBerria" CommandName="InitInsert" Text="<%$ Resources:lang,nuevo %>" Visible='<%# enabledisable()%>'></asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton runat="server" ID="Bezabatu" CssClass="btnEzabatu" CommandName="DeleteSelected" Text="<%$ Resources:lang,eliminar %>" Visible='<%# enabledisable() %>' OnClientClick='<%# showModal()%>'></asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton runat="server" ID="Beguneratu" CssClass="btnEguneratu" CommandName="RebindGrid" Text="<%$ Resources:lang,actualizar %>" ></asp:LinkButton>
                    </div>
                </CommandItemTemplate>
                <%-- ↑↑botones etidar, nuevo, elimiar, actualizar↑↑ --%>
                <%-- ↓↓ nombre de la parte gris de las  columnas ↓↓ --%>
                <Columns>
                    <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" ItemStyle-Width="40px">
                    </telerik:GridClientSelectColumn>
                    <telerik:GridBoundColumn UniqueName="codigo" DataField="codigo" HeaderText="<%$ Resources:lang,codigo %>" visible="false"/>
                    <telerik:GridBoundColumn  UniqueName="ex" DataField="nombre1" Visible="false"/>                  
                    <%-- ↑↑ nombre de la parte gris de las  columnas  ↑↑ --%>
                    <telerik:GridTemplateColumn UniqueName="nombre" DataField="nombre1" HeaderText="<%$ Resources:lang,nombre %>"  AllowFiltering="true"
                        FilterControlWidth="90%">
                        <ItemTemplate>
                            <asp:LinkButton ID="HLnombre" runat="server" Text ='<%# Eval("nombre1") %>' CommandName= "clicklink"></asp:LinkButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridCheckBoxColumn DataField="activo" HeaderText="activo" SortExpression="activo" UniqueName="activo" HeaderStyle-Width="100" >
                    </telerik:GridCheckBoxColumn>                          
                </Columns>
                
            </MasterTableView>
            <ClientSettings>
                <ClientEvents OnRowDblClick="RowDblClick" />
            </ClientSettings>
        </telerik:RadGrid>
    </div>

    <asp:HiddenField runat="server" ID="HFcodigo" />
    
        <%-- para editar despues de hacer doble click --%>
    <asp:Panel runat="server" ID="Pcategoria" Visible="false">
        <div class="col" id="kol" runat="server">
            <asp:Panel runat="server" ID="Penabled">
                <h3><asp:Label runat="server" ID="Ltit"></asp:Label></h3>
                <div class="form-group">
                    <asp:Label runat="server" ID="lnombre" AssociatedControlID="TBnombre1" CssClass="col-md-1 control-label" Text="Nombre" />
                    <div class="col-md-11">
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="TBnombre1" Text="" CssClass="form-control input-md"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="Vnombre1" ControlToValidate="TBnombre1" 
                                CssClass="text-danger" ErrorMessage="!!" ValidationGroup="FormValidationGroup" />                  
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="TBnombre2" Text="" CssClass="form-control input-md"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="Vnombre2" ControlToValidate="TBnombre2" 
                                CssClass="text-danger" ErrorMessage="!!" ValidationGroup="FormValidationGroup" />
                        </div>
                        <div class="col-md-2">                        
                            <%--<telerik:RadColorPicker ID="RadColorPicker1" runat="server" ShowIcon="true" ></telerik:RadColorPicker>--%>
                         <%--    <asp:TextBox runat="server" ID="TBdescripcion1" Text="" CssClass="form-control input-md"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="TBdescripcion1" 
                                CssClass="text-danger" ErrorMessage="!!" ValidationGroup="FormValidationGroup" />--%>
                        </div>
                        <div class="col-md-2">
                            <div class="controls">
                                <asp:CheckBox runat="server" ID="CBactivo" />
                            </div>
                        </div>
                    </div>
                </div>
               <div class="form-group">
                    <asp:Label runat="server" ID="Label1" AssociatedControlID="TBnombre1" CssClass="col-md-1 control-label" Text="Descripcion" />
                    <div class="col-md-11">                       
                        <div class="col-md-4">      
                             <%--<telerik:RadColorPicker ID="RadColorPicker1" runat="server" ShowIcon="true" ></telerik:RadColorPicker>--%>
                            <asp:TextBox  TextMode="multiline" Columns="50" Rows="5" runat="server" ID="TBdescripcion1" Text="" CssClass="form-control input-md"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="TBdescripcion1" 
                                CssClass="text-danger" ErrorMessage="!!" ValidationGroup="FormValidationGroup" />                           
                        </div>  
                        <div class="col-md-4">      
                             <%--<telerik:RadColorPicker ID="RadColorPicker1" runat="server" ShowIcon="true" ></telerik:RadColorPicker>--%>
                            <asp:TextBox TextMode="multiline" Columns="50" Rows="5" runat="server" ID="TBdescripcion2" Text="" CssClass="form-control input-md"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="TBdescripcion2" 
                                CssClass="text-danger" ErrorMessage="!!" ValidationGroup="FormValidationGroup" />                           
                        </div>                            
                          
                    </div>
                </div>
            </asp:Panel>
            <div class="row botoiak">
                <telerik:RadButton runat="server" ID="Bguardar" SkinID="Bguardar" ValidationGroup="FormValidationGroup"></telerik:RadButton>
                <telerik:RadButton runat="server" ID="Bcancelar" SkinID="Bcancelar" ></telerik:RadButton>
            </div>
        </div>
    </asp:Panel>
     
</div>
</asp:Content>

