<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="encuestascat.aspx.vb" Inherits="Default2" %>
<%--<%@ Register TagPrefix="wc" TagName="CAT" Src="~/web/categorias.ascx" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <%--<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="LoadingPanel">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RGlista">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="Pcategoria" />
                <telerik:AjaxUpdatedControl ControlID="HFcodigo" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="Bcancelar">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="Pcategoria" />
                <telerik:AjaxUpdatedControl ControlID="HFcodigo" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="Bguardar">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RGlista" />
                <telerik:AjaxUpdatedControl ControlID="Pcategoria" />
                <telerik:AjaxUpdatedControl ControlID="HFcodigo" />
            </UpdatedControls>
        </telerik:AjaxSetting>

         <telerik:AjaxSetting AjaxControlID="Ppage">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="Ppage" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
    <ClientEvents OnRequestStart="mngRequestStarted" />
</telerik:RadAjaxManager>--%>

<%--<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <link href='<%= Page.ClientScript.GetWebResourceUrl(RadColorPicker1.GetType() , "Telerik.Web.UI.Skins.ColorPicker.css") %>' rel="stylesheet" type="text/css" />
    <link href='<%= Page.ClientScript.GetWebResourceUrl(RadColorPicker1.GetType() , "Telerik.Web.UI.Skins.Silk.ColorPicker.Silk.css") %>' rel="stylesheet" type="text/css" />
</telerik:RadCodeBlock> --%>

<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
    <script type="text/javascript">
        //function mngRequestStarted(ajaxManager, eventArgs) {
        //    if (eventArgs.EventTarget.indexOf("Bexcel") >= 0 ||
        //        eventArgs.EventTarget.indexOf("Bpdf") >= 0 ||
        //        eventArgs.EventTarget.indexOf("Bword") >= 0) {
        //        eventArgs.EnableAjax = false;
        //    }
        //}
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
            <MasterTableView DataKeyNames="codigo, nombre" ClientDataKeyNames="codigo">
              <%-- ↓↓botones etidar, nuevo, elimiar, actualizar↓↓ --%>
                  <CommandItemTemplate>                
                    <div class="menu_editar">
                        <asp:LinkButton runat="server" ID="Beditatu" CssClass="btnEditatu" CommandName="EditSelected" Text="<%$ Resources:lang,editar %>" Visible='<%# RGLista.EditIndexes.Count = 0 %>'></asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton runat="server" ID="Bberria" CssClass="btnBerria" CommandName="InitInsert" Text="<%$ Resources:lang,nuevo %>" Visible='<%# enabledisable()%>'></asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton runat="server" ID="Bezabatu" CssClass="btnEzabatu" CommandName="DeleteSelected" Text="<%$ Resources:lang,eliminar %>" Visible='<%# enabledisable() %>' OnClientClick='<%# showModal()%>'></asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton runat="server" ID="Beguneratu" CssClass="btnEguneratu" CommandName="RebindGrid" Text="<%$ Resources:lang,actualizar %>" ></asp:LinkButton>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" ItemStyle-Width="40px">
                    </telerik:GridClientSelectColumn>
                    <telerik:GridBoundColumn UniqueName="codigo" DataField="codigo" HeaderText="<%$ Resources:lang,codigo %>" visible="false"/>
                    <telerik:GridBoundColumn  UniqueName="ex" DataField="nombre" Visible="false"/>
                    <telerik:GridTemplateColumn UniqueName="nombre" DataField="nombre" HeaderText="<%$ Resources:lang,nombre %>" AllowFiltering="true"
                        FilterControlWidth="90%">
                        <ItemTemplate>
                            <asp:LinkButton ID="HLnombre" runat="server" Text ='<%# Eval("nombre") %>' CommandName= "clicklink"></asp:LinkButton>
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
                            <telerik:RadColorPicker ID="RadColorPicker1" runat="server" ShowIcon="true" ></telerik:RadColorPicker>
                        </div>
                        <div class="col-md-2">
                            <div class="controls">
                                <asp:CheckBox runat="server" ID="CBactivo" />
                            </div>
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

