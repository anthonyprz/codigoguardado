<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="usuarios.aspx.vb" Inherits="Default2" Async="true" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register TagPrefix="wc" TagName="USU" Src="~/webadmin/usuario.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

<%--<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="LoadingPanel">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RGlista">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="Plista" />
                <telerik:AjaxUpdatedControl ControlID="Pdatos" />
                <telerik:AjaxUpdatedControl ControlID="HFcodigo"  />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="Bcancelar">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="Plista"  />
                <telerik:AjaxUpdatedControl ControlID="Pdatos"  />
                <telerik:AjaxUpdatedControl ControlID="HFcodigo"  />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="Bguardar">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="Plista"  />
                <telerik:AjaxUpdatedControl ControlID="Pdatos" />
                <telerik:AjaxUpdatedControl ControlID="HFcodigo" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="IBcandado">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RTVPermisos"  />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="IBver">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RTVPermisos" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="IBescribir">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RTVPermisos" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>--%>

     <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
         <script type="text/javascript">
             function RowDblClick(sender, eventArgs) {
                 var codigo = eventArgs.getDataKeyValue("ID");
                 __doPostBack('dobleclick', codigo);
             }
        </script>
    </telerik:RadCodeBlock>
    
    <asp:Panel runat="server" ID="Plista">
        <div>
	        <h2>
                <asp:Label ID="lusuarios" runat="server"></asp:Label>
            </h2>
	    </div>
        <div>
            <telerik:RadGrid ID="RGlista" runat="server" SkinID="Grid" OnNeedDataSource="RGlista_NeedDataSource">
                <MasterTableView DataKeyNames="ID,nombre" ClientDataKeyNames="ID">
                    <CommandItemTemplate>
                        <div class="menu_editar">
                            <asp:LinkButton runat="server" ID="Beditatu" CssClass="btnEditatu" CommandName="EditSelected" Text="<%$ Resources:lang,editar %>" Visible='<%# RGLista.EditIndexes.Count = 0 %>'></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="Bberria" CssClass="btnBerria" CommandName="InitInsert" Text="<%$ Resources:lang,nuevo %>" Visible='<%# enabledisable() %>'></asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="Bezabatu" CssClass="btnEzabatu" CommandName="DeleteSelected" Text="<%$ Resources:lang,eliminar %>" Visible='<%# enabledisable() %>' OnClientClick='<%# showModal()%>'></asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="Beguneratu" CssClass="btnEguneratu" CommandName="RebindGrid" Text="<%$ Resources:lang,actualizar %>"></asp:LinkButton>
                        </div>
                    </CommandItemTemplate>
                    <Columns>
                        <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" ItemStyle-Width="40px">
                        </telerik:GridClientSelectColumn>
                        <telerik:GridBoundColumn UniqueName="ID" DataField="ID" HeaderText="<%$ Resources:lang,codigo %>" visible="false" />
                        <telerik:GridTemplateColumn UniqueName="nombre" DataField="nombre" HeaderText="<%$ Resources:idioma,nombre %>"
                            AllowFiltering="true" HeaderStyle-Width="40%" FilterControlWidth="90%" >
                            <ItemTemplate>
                                <asp:LinkButton ID="HLnombre" runat="server" Text ='<%# Eval("nombre") %>' CommandName= "clicklink"></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>  
                        <telerik:GridBoundColumn UniqueName="usuario" DataField="UserName" HeaderText="<%$ Resources:lang,usuario %>"
                            HeaderStyle-Width="30%" FilterControlWidth="90%"  />
                        <telerik:GridBoundColumn UniqueName="Email" DataField="Email" HeaderText="<%$ Resources:lang,email %>"
                            HeaderStyle-Width="30%" FilterControlWidth="90%"  />
                        <telerik:GridCheckBoxColumn DataField="activo" HeaderText="activo" SortExpression="activo" UniqueName="activo" HeaderStyle-Width="100" >
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridCheckBoxColumn DataField="admin" HeaderText="admin" SortExpression="admin" UniqueName="admin" HeaderStyle-Width="100" >
                        </telerik:GridCheckBoxColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <ClientEvents OnRowDblClick="RowDblClick" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>

        <asp:Panel runat="server" ID="modalDialog">
            <span id="sErroresLoginList" style="visibility: hidden;" class="errors errCestaList"> 
                <span id="texto"></span>
                <asp:Button runat="server" ID="Bbai" CssClass="confirm_bai" />
                <asp:Button runat="server" ID="Bez" CssClass="confirm_ez" />
            </span>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel runat="server" ID="Pdatos" Visible="false">
        <wc:USU runat="server" ID="wcUSU"></wc:USU>
    </asp:Panel>

</asp:Content>

