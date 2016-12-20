<%@ Control Language="VB" AutoEventWireup="false" CodeFile="encuestavotacion.ascx.vb" Inherits="web_encuestavotacion" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
 
<script type="text/javascript">
</script>

<asp:HiddenField runat="server" ID="HFCodselect" />
<asp:HiddenField runat="server" ID="HFtipo" />

<div runat="server" id="Dform" class="form-horizontal" >
    <div class="row">
        <h4><asp:Label runat="server" ID="Ltit" Text="<%$ Resources:lang,encuesta %>" ></asp:Label></h4>
        <%--<div class="form-group">
            <asp:Label runat="server" ID="Lnombre" AssociatedControlID="TBnombre" CssClass="col-md-2 control-label" Text="<%$ Resources:idioma,nombre %>" />
            <div class="col-md-6">
                <asp:TextBox runat="server" ID="TBnombre" CssClass="form-control"></asp:TextBox> 
            </div>
        </div>--%>
        <div class="form-group">
            <asp:Label runat="server" ID="Lparticipantes" AssociatedControlID="TBparticipantes" CssClass="col-md-2 control-label" Text="<%$ Resources:lang,participantes %>" />
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="TBparticipantes" CssClass="form-control" Enabled="false"></asp:TextBox> 
            </div>
        </div>

        <div class="form-group">
            <%--<asp:Label runat="server" ID="Label1" AssociatedControlID="" CssClass="col-md-2 control-label" Text="" />--%>
            <div class="col bordegabe" id="encuestasbotacion">
                <telerik:RadHtmlChart runat="server" ID="PieChart"  Transitions="true" Visible="false"  EnableViewState="false">
                    <ChartTitle>
                        <Appearance Align="Center" Position="Top">
                        </Appearance>
                    </ChartTitle>
                    <%--<Legend>
                        <Appearance Position="Right" Visible="true">
                        </Appearance>
                    </Legend>--%>
                    <PlotArea>
                        <Series>
                            <telerik:PieSeries StartAngle="90" >
                                <LabelsAppearance Position="OutsideEnd" DataFormatString="{0} %">
                                </LabelsAppearance>
                                <TooltipsAppearance Color="White" DataFormatString="{0} %"></TooltipsAppearance>
                                <%--<SeriesItems>
                                </SeriesItems>--%>
                            </telerik:PieSeries>
                        </Series>
                    </PlotArea>
                </telerik:RadHtmlChart>

                <telerik:RadHtmlChart runat="server" ID="ColumnChart" Visible="false"  EnableViewState="false"><%--Width="800"  Height="500" Transitions="true"--%>
                    <Appearance>
                        <FillStyle BackgroundColor="Transparent"></FillStyle>
                    </Appearance>
                    <ChartTitle>
                        <Appearance Align="Center" BackgroundColor="Transparent" Position="Top">
                        </Appearance>
                    </ChartTitle>
                    <Legend>
                        <Appearance BackgroundColor="Transparent" Position="Right" Visible="True">
                        </Appearance>
                    </Legend> 
                    <PlotArea>
                        <Series>
                            <%--<telerik:ColumnSeries Stacked="false" Gap="0" Spacing="0" ColorField="color" DataFieldY="puntos">
                                <LabelsAppearance Position="OutsideEnd"></LabelsAppearance>
                                <TooltipsAppearance Color="White"></TooltipsAppearance>
                                <Items>
                                    <telerik:SeriesItem   />
                                </Items>
                            </telerik:ColumnSeries>   --%>                         
                        </Series>
                        <XAxis><%-- DataLabelsField="texto"--%>
                            <%--<LabelsAppearance RotationAngle="75"></LabelsAppearance>--%>
                            <MajorGridLines Visible="False" />
                            <MinorGridLines Visible="False" />
                        </XAxis>
                        <YAxis>
                            <MajorGridLines Visible="False" />
                            <MinorGridLines Visible="false" />
                        </YAxis>     
 
                    </PlotArea>
                </telerik:RadHtmlChart>

                <telerik:RadHtmlChart ID="BarChart" runat="server" Visible="false" EnableViewState="false" ><%-- Width="800"--%>
                    <Legend>
                        <Appearance Visible="false">
                        </Appearance>
                    </Legend>
                    <PlotArea>
                        <Series>
                            <telerik:BarSeries Stacked="true" Gap="0.5">
                                <LabelsAppearance DataFormatString="{0} %" Position="Center"></LabelsAppearance>
                            </telerik:BarSeries>
                            <telerik:BarSeries Stacked="true" Gap="0.5">
                                <LabelsAppearance DataFormatString="{0} %" Position="Center"></LabelsAppearance>
                                <TooltipsAppearance DataFormatString="{0} %"></TooltipsAppearance>
                            </telerik:BarSeries>
                        </Series>
                        <XAxis Color="Transparent">
                            <LabelsAppearance Color="#000000"></LabelsAppearance>
                            <MinorGridLines Visible="false"></MinorGridLines>
                            <MajorGridLines Visible="false"></MajorGridLines>
                        </XAxis>
                        <YAxis  Visible="false">
                            <MinorGridLines Visible="false"></MinorGridLines>
                            <MajorGridLines Visible="false"></MajorGridLines>
                        </YAxis>
                    </PlotArea>
                </telerik:RadHtmlChart>

                <asp:Panel runat="server" ID="Ptipo2" Visible="false">
                    <div class="form-group">
                        <h5><asp:Label runat="server" ID="TBpregunta" CssClass="control-label" TextMode="MultiLine"></asp:Label></h5>
                    </div>
                    <div class="form-group">
                        <telerik:RadGrid ID="RadGrid1" runat="server" ShowHeader="false" Skin="Default">
                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="codigo">
                                <Columns>                        
                                    <telerik:GridBoundColumn UniqueName="codigo" DataField="codigo" HeaderText="<%$ Resources:lang,codigo %>" Visible="false" />
                                    <telerik:GridTemplateColumn UniqueName="color" HeaderText="color" DataField="color" SortExpression="color" ItemStyle-Width="35px">   
                                        <ItemTemplate>  
                                            <div style='width: 16px; height: 16px; background-color: <%# Eval("color") %>'></div>  
                                        </ItemTemplate>  
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="texto" HeaderText="texto" DataField="texto" SortExpression="texto" ItemStyle-Width="50%">   
                                        <ItemTemplate> 
                                            <label><%# Eval("texto") %></label>   
                                        </ItemTemplate>  
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Rating">
                                        <ItemTemplate>
                                            <%--<telerik:RadRating ID="RadRating1" runat="server" ItemCount="10" Value='<%# Convert.ToDouble(Eval("puntos")) / Convert.ToDouble(Eval("personas"))  %>'
                                                ReadOnly="true">
                                            </telerik:RadRating>--%>
                                            <div class="col-md-10" >
                                                <telerik:RadRating ID="RadRating1" runat="server" ItemCount="10" Value='<%# Eval("media")  %>'
                                                    ReadOnly="true"><%--<%# Convert.ToDouble(Eval("media"))--%>
                                                </telerik:RadRating>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:Label runat="server" ID="l" Text='<%# Eval("media") %>'></asp:Label> 
                                            </div>
                                        </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </asp:Panel>
            </div>
        </div>        
    </div>

    <%--<div class="row botoiak">
        <telerik:RadButton runat="server" ID="Bguardar" SkinID="Bguardar" Visible ="true"></telerik:RadButton>
        <telerik:RadButton runat="server" ID="Bcancelar" SkinID="Bcancelar" ></telerik:RadButton>
    </div>--%>
                         
</div>