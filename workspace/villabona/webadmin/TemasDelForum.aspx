<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="TemasDelForum.aspx.vb" Inherits="TemasDelForum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">
            function RowDblClick(sender, eventArgs) {
                var codigo = eventArgs.getDataKeyValue("codigo");
                __doPostBack('dobleclick', codigo);
            }
            var count = 0;

            window.onload = countcero()
            function countcero() {
                count == 0;

            }

           <%-- function search(ele) {
                var autoComplete = $find("<%= RadAutoCompleteBox1.ClientID %>");

                if (event.keyCode == 13) {

                    alert(autoComplete.get_text());

                    var hiddenDiv = document.getElementById("<%= DatosTags.ClientID %>");
                    hiddenDiv = document.getElementById("<%= DatosTags.ClientID %>").value += autoComplete.get_text();


                    var element = document.getElementById("tagss");
                    var parrafo = document.createElement("p");
                    var contenido = document.createTextNode(autoComplete.get_text());
                    parrafo.appendChild(contenido);
                    element.appendChild(parrafo)
                    // parrafo.appendChild(contenido);
                    // document.body.appendChild(parrafo);

                    if (autoComplete != "") {
                        autoComplete.SelectedItems = null;
                    }

                }
            }

            function stopRKey(evt) {
                var evt = (evt) ? evt : ((event) ? event : null);
                var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
                if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
            }
            document.onkeypress = stopRKey;
--%>

        </script>

    </telerik:RadCodeBlock>

    <div class="form-horizontal">
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
            <telerik:RadGrid ID="RGlista" runat="server" SkinID="Grid" OnNeedDataSource="RGlista_NeedDataSource">
                <MasterTableView DataKeyNames="codigo, codcategoria" ClientDataKeyNames="codigo">
                    <%-- ↓↓botones etidar, nuevo, elimiar, actualizar↓↓ --%>
                    <CommandItemTemplate>
                        <div class="menu_editar">
                            <asp:LinkButton runat="server" ID="Beditatu" CssClass="btnEditatu" CommandName="EditSelected" Text="<%$ Resources:lang,editar %>" Visible='<%# RGLista.EditIndexes.Count = 0 %>'></asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton runat="server" ID="Bberria" CssClass="btnBerria" CommandName="InitInsert" Text="<%$ Resources:lang,nuevo %>" Visible='<%# enabledisable()%>'></asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton runat="server" ID="Bezabatu" CssClass="btnEzabatu" CommandName="DeleteSelected" Text="<%$ Resources:lang,eliminar %>" Visible='<%# enabledisable() %>' OnClientClick='<%# showModal()%>'></asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton runat="server" ID="Beguneratu" CssClass="btnEguneratu" CommandName="RebindGrid" Text="<%$ Resources:lang,actualizar %>"></asp:LinkButton>
                        </div>
                    </CommandItemTemplate>
                    <%-- ↑↑botones etidar, nuevo, elimiar, actualizar↑↑ --%>

                    <%-- ↓↓ agrupar con el nombre de la categoria ↓↓--%>
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldAlias=":" FieldName="nombreCategoria"></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="nombreCategoria"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <%-- ↑↑ agrupar con el nombre de la categoria ↑↑--%>


                    <%--    <CommandItemSettings  ShowExportToWordButton="true" ShowExportToExcelButton="true" ShowExportToPdfButton="true" 
                ShowExportToCsvButton="true" ExportToCsvText="CSV" ExportToExcelText="Excel" ExportToPdfText="PDF" ExportToWordText="Word"/>--%>
                    <Columns>
                        <telerik:GridBoundColumn DataField="codigo" DataType="System.Int32" Visible="false"
                            HeaderText="codigo" ReadOnly="True" SortExpression="codigo" UniqueName="codigo">
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridBoundColumn DataField="nombre" 
                            HeaderText="nombre" ReadOnly="True" SortExpression="nombre" UniqueName="nombre" >
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn DataField="nombreCategoria" Visible="false"
                            HeaderText="nombreCategoria" ReadOnly="True" SortExpression="nombreCategoria" UniqueName="Categoria">
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridBoundColumn DataField="nombre"
                            HeaderText="nombre" ReadOnly="True" SortExpression="nombre" UniqueName="nombre">
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridTemplateColumn UniqueName="nombre" DataField="nombre" HeaderText="<%$ Resources:lang,nombre %>" AllowFiltering="true"
                            FilterControlWidth="90%">
                            <ItemTemplate>
                                <asp:LinkButton ID="HLnombre" runat="server" Text='<%# Eval("nombre") %>' CommandName="clicklink"></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridCheckBoxColumn DataField="activo" HeaderText="activo" SortExpression="activo"
                            UniqueName="activo" />
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
                    <%--<h3><asp:Label runat="server" ID="Ltit"></asp:Label></h3>--%>
                    <h3>editar tema</h3>
                    <div class="form-group">
                        <asp:Label runat="server" ID="lnombre" AssociatedControlID="TBnombre1" CssClass="col-md-1 control-label" Text="Categoria y usuario" />
                        <div class="col-md-11">
                            <div class="col-md-4">
                                <telerik:RadComboBox runat="server" ID="CBcategoria" DataValueField="codigo" DataTextField="nombre" Width="100%"></telerik:RadComboBox>
                            </div>
                            <div class="col-md-4">
                                <%--   <telerik:RadComboBox runat="server" ID="CBusuario" DataValueField="Id" DataTextField="UserName" IsEnabled="False" Width="100%"></telerik:RadComboBox>--%>
                                <asp:TextBox runat="server" ID="TBusuario" Text="" Visible="false" DataValueField="Id" CssClass="form-control input-md" ReadOnly="true"></asp:TextBox>
                                <asp:TextBox runat="server" ID="TBusername" Text="" Visible="true" DataValueField="UserName" DataTextField="UserName" CssClass="form-control input-md" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-2">
                                <div class="controls">
                                    <asp:CheckBox runat="server" ID="CBactivo" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label1" AssociatedControlID="TBnombre1" CssClass="col-md-1 control-label" Text="Nombre" />
                        <div class="col-md-11">
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="TBnombre1" Text="" CssClass="form-control input-md"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="TBnombre1"
                                    CssClass="text-danger" ErrorMessage="!!" ValidationGroup="FormValidationGroup" />
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="TBnombre2" Text="" CssClass="form-control input-md"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="TBnombre2"
                                    CssClass="text-danger" ErrorMessage="!!" ValidationGroup="FormValidationGroup" />
                            </div>
                            <div class="col-md-2">
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label2" AssociatedControlID="tbdate" CssClass="col-md-1 control-label" Text="date" />
                        <div class="col-md-11">
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="tbdate" Text="" CssClass="form-control input-md" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label3" AssociatedControlID="RadAutoCompleteBox1" CssClass="col-md-1 control-label" Text="tags" />
                        <div class="col-md-7">
                            
                               <%-- <div id="tagss">
                                    <asp:HiddenField runat="server" ID="DatosTags" />--%>
                                    <%-- <asp:TextBox  runat="server" ID="conjuntodetags" BorderStyle="None" CssClass="form-control input-md" BorderColor="White"> </asp:TextBox>--%>
                                    <%--  <asp:LinkButton runat="server" ID="tag" CssClass=""  Text="" Visible='<%# RGLista.EditIndexes.Count = 0 %>'></asp:LinkButton>&nbsp;&nbsp;--%>
                               <%-- </div>--%>


                                <%-- <asp:TextBox runat="server" ID="TBtags" CssClass="form-control input-md" onkeydown="search(this)"></asp:TextBox>--%>

                               <%-- <telerik:RadAutoCompleteBox RenderMode="Lightweight" runat="server" ID="RadAutoCompleteBox1" EmptyMessage="Please type here"
                                    OnNeedDataSource="autocompletar" InputType="Text" Width="500" AllowCustomEntry="True" CssClass="form-control input-md" onkeydown="search(this)">
                                </telerik:RadAutoCompleteBox>--%>

                                <div class="demo-container size-narrow">
                                    <telerik:RadAutoCompleteBox RenderMode="Lightweight" ID="RadAutoCompleteBox1" runat="server" Width="100%"
                                        DropDownHeight="200"  OnNeedDataSource="autocompletar" EmptyMessage="Select Company Name" 
                                        AllowCustomEntry="False">
                                        
                                    </telerik:RadAutoCompleteBox>
                                </div>


                                <telerik:RadAjaxManagerProxy ID="RadAjaxManager1" runat="server">
                                    <AjaxSettings>
                                        <telerik:AjaxSetting AjaxControlID="ConfigurationPanel1">
                                            <UpdatedControls>
                                                <telerik:AjaxUpdatedControl ControlID="RadAutoCompleteBox1" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>
                                    </AjaxSettings>
                                </telerik:RadAjaxManagerProxy>

                           
   
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="Texto" AssociatedControlID="TBTexto" CssClass="col-md-1 control-label" Text="Post" />
                        <div class="col-md-11">
                            <div class="col-md-8">
                                <%-- <asp:TextBox  TextMode="multiline" Columns="50" Rows="5" runat="server" ID="TBTexto" Text="" CssClass="form-control input-md"></asp:TextBox>--%>
                                <%--  <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="true" />--%>
                                <telerik:RadEditor runat="server" ID="TBTexto" RenderMode="Lightweight" Width="800px">
                                </telerik:RadEditor>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="TBTexto"
                                    CssClass="text-danger" ErrorMessage="!!" ValidationGroup="FormValidationGroup" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div class="row botoiak">
                    <telerik:RadButton runat="server" ID="Bguardar" SkinID="Bguardar" ValidationGroup="FormValidationGroup"></telerik:RadButton>
                    <telerik:RadButton runat="server" ID="Bcancelar" SkinID="Bcancelar"></telerik:RadButton>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

