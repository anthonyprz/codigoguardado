<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="TagsDelForum.aspx.vb" Inherits="TagsDelForum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">
            function RowDblClick(sender, eventArgs) {
                var codigo = eventArgs.getDataKeyValue("codigo");
                __doPostBack('dobleclick', codigo);
            }
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
                <MasterTableView DataKeyNames="codigo, nombre" ClientDataKeyNames="codigo">
                    <%-- ↓↓botones etidar, nuevo, elimiar, actualizar↓↓ --%>
                    <CommandItemTemplate>
                        <div class="menu_editar">
                            <asp:LinkButton runat="server" ID="Beditatu" CssClass="btnEditatu" CommandName="EditSelected" Text="<%$ Resources:lang,editar %>" Visible='<%# RGLista.EditIndexes.Count = 0 %>'></asp:LinkButton>&nbsp;&nbsp;
                        <%--<asp:LinkButton runat="server" ID="Bberria" CssClass="btnBerria" CommandName="InitInsert" Text="<%$ Resources:lang,nuevo %>" Visible='<%# enabledisable()%>'></asp:LinkButton>&nbsp;&nbsp;--%>
                        <asp:LinkButton runat="server" ID="Bezabatu" CssClass="btnEzabatu" CommandName="DeleteSelected" Text="<%$ Resources:lang,eliminar %>" Visible='<%# enabledisable() %>' OnClientClick='<%# showModal()%>'></asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton runat="server" ID="Beguneratu" CssClass="btnEguneratu" CommandName="RebindGrid" Text="<%$ Resources:lang,actualizar %>"></asp:LinkButton>
                        </div>
                    </CommandItemTemplate>
                    <%-- ↑↑botones etidar, nuevo, elimiar, actualizar↑↑ --%>

                    <%-- ↓↓ agrupar con el nombre de la tema ↓↓--%>
                    <%--<GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldAlias=":" FieldName="nombretema"></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="nombretema"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>--%>
                    <%-- ↑↑ agrupar con el nombre de la tema ↑↑--%>
                    <Columns>
                        <telerik:GridBoundColumn DataField="codigo" DataType="System.Int32" Visible="false"
                            HeaderText="codigo" ReadOnly="True" SortExpression="codigo" UniqueName="codigo">
                        </telerik:GridBoundColumn>                 
                       <%-- <telerik:GridBoundColumn DataField="nombretema" Visible="false"
                            HeaderText="nombreCategoria" ReadOnly="True" SortExpression="nombretema" UniqueName="tema">
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridTemplateColumn UniqueName="nombre" DataField="nombre" HeaderText="<%$ Resources:lang,nombre %>" AllowFiltering="true"
                            FilterControlWidth="90%">
                            <ItemTemplate>
                                <asp:LinkButton ID="HLnombretag" runat="server" Text='<%# Eval("nombre") %>' CommandName="clicklink"></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                     <%--   <telerik:GridCheckBoxColumn DataField="activo" HeaderText="activo" SortExpression="activo"
                            UniqueName="activo" />--%>
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
                    <h3>editar tags</h3>
                    <div class="form-group">
                        <asp:Label runat="server" ID="nombre" AssociatedControlID="TBnombre" CssClass="col-md-1 control-label" Text="nombre" />
                        <div class="col-md-11">                           
                            <div class="col-md-4">
                                <%--   <telerik:RadComboBox runat="server" ID="CBusuario" DataValueField="Id" DataTextField="UserName" IsEnabled="False" Width="100%"></telerik:RadComboBox>--%>
                               <%-- <asp:TextBox runat="server" ID="TBusuario" Text="" Visible="false" DataValueField="Id" CssClass="form-control input-md" ReadOnly="true"></asp:TextBox>--%>
                                <asp:TextBox runat="server" ID="TBnombre" Text="" Visible="true" DataValueField="UserName" DataTextField="nombre" CssClass="form-control input-md" ></asp:TextBox>
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
