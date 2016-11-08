<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="encuestas.aspx.vb" Inherits="Default2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register TagPrefix="wc" TagName="ENCUESTA" Src="~/webadmin/encuesta.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
  
<%--<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadGrid1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="LoadingPanel" />
             </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RadDateTimePicker1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadDateTimePicker1" LoadingPanelID="LoadingPanel" />
                <telerik:AjaxUpdatedControl ControlID="RadDateTimePicker2" LoadingPanelID="LoadingPanel" />
                <telerik:AjaxUpdatedControl ControlID="Derror" LoadingPanelID="LoadingPanel" />
             </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RadDateTimePicker2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadDateTimePicker2" LoadingPanelID="LoadingPanel" />
                <telerik:AjaxUpdatedControl ControlID="Derror" LoadingPanelID="LoadingPanel" />
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
                <asp:Label ID="lencuestas" runat="server"></asp:Label>
            </h2>
	    </div>
        <div>
            <telerik:RadGrid ID="RGlista" runat="server" SkinID="Grid" OnNeedDataSource="RGlista_NeedDataSource">
                <MasterTableView DataKeyNames="codigo" ClientDataKeyNames="codigo">
                    <CommandItemTemplate>
                        <div class="menu_editar">
                            <asp:LinkButton runat="server" ID="Beditatu" CssClass="btnEditatu" CommandName="EditSelected" Text="<%$ Resources:lang,editar %>" Visible='<%# RGLista.EditIndexes.Count = 0 %>'></asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="Bberria" CssClass="btnBerria" CommandName="InitInsert" Text="<%$ Resources:lang,nuevo %>" Visible='<%# enabledisable()%>'></asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="Bezabatu" CssClass="btnEzabatu" CommandName="DeleteSelected" Text="<%$ Resources:lang,eliminar %>" Visible='<%# enabledisable()%>' OnClientClick='<%# showModal()%>'></asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="Beguneratu" CssClass="btnEguneratu" CommandName="RebindGrid" Text="<%$ Resources:lang,actualizar %>"></asp:LinkButton>
                        </div>
                    </CommandItemTemplate>
                    <Columns>
                        <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" ItemStyle-Width="40px">
                        </telerik:GridClientSelectColumn>
                        <telerik:GridBoundColumn UniqueName="codigo" DataField="codigo" HeaderText="<%$ Resources:lang,codigo %>" visible = "false" />
                        <telerik:GridTemplateColumn UniqueName="nombre" DataField="nombre" HeaderText="<%$ Resources:lang,nombre %>"
                            AllowFiltering="true" FilterControlWidth="90%" >
                            <ItemTemplate>
                                <asp:LinkButton ID="HLnombre" runat="server" Text ='<%# Eval("nombre") %>' CommandName= "clicklink"></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridDateTimeColumn DataField="inicio" HeaderText="inicio" SortExpression="inicio" UniqueName="inicio" DataFormatString="{0:d}" HeaderStyle-Width="150" >
                        </telerik:GridDateTimeColumn>
                        <telerik:GridDateTimeColumn DataField="fin" HeaderText="fin" SortExpression="fin" UniqueName="fin" DataFormatString="{0:d}" HeaderStyle-Width="150" >
                        </telerik:GridDateTimeColumn>
                        <telerik:GridCheckBoxColumn DataField="activo" HeaderText="activo" SortExpression="activo" UniqueName="activo" HeaderStyle-Width="100" >
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
            <wc:ENCUESTA runat="server" ID="wcENCUESTA"></wc:ENCUESTA>     
    </asp:Panel>

</asp:Content>

