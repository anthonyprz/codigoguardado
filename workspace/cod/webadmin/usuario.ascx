<%@ Control Language="VB" AutoEventWireup="false" CodeFile="usuario.ascx.vb" Inherits="web_usuario" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%--<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="IBcandado">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RTVPermisos" LoadingPanelID="LoadingPanel" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="IBver">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RTVPermisos" LoadingPanelID="LoadingPanel" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="IBescribir">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RTVPermisos" LoadingPanelID="LoadingPanel" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="TBusuario">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="Dusuario" LoadingPanelID="LoadingPanel" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>--%>

<asp:HiddenField runat="server" ID="HFCodusu" />

<div>
	<h2>
        <asp:Label ID="titusuario" runat="server"></asp:Label>
    </h2>
</div>
<div runat="server" id="Dform" class="form-horizontal" >
    <asp:Panel runat="server" ID="Penabled">
    <div class="row col">
        <div class="col-md-7">
            <h3><asp:Label runat="server" ID="lusuario" Text="USUARIO" ></asp:Label></h3>
            <div class="form-group">
                <asp:Label runat="server" ID="lnombre" AssociatedControlID="TBnombre" CssClass="col-md-2 control-label" Text="Nombre" />
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="TBnombre" Text="" CssClass="form-control input-md"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="Vnombre1" ControlToValidate="TBnombre" 
                            CssClass="text-danger" ErrorMessage="!!" ValidationGroup="FormValidationGroup" />
                </div>
            </div>

            <%--apellido1--%>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="TBapel1" CssClass="col-md-2 control-label"><%: Resources.idioma.apellido1.ToUpper %></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="TBapel1" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="TBapel1" ID="RequiredFieldValidator2"
                        CssClass="text-danger" ErrorMessage="El campo de nombre de usuario es obligatorio."  ValidationGroup="FormValidationGroup" />
                </div>
            </div>
            <%--apellido2--%>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="TBapel2" CssClass="col-md-2 control-label"><%: Resources.idioma.apellido2.ToUpper %></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="TBapel2" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="TBapel2" ID="RequiredFieldValidator3"
                        CssClass="text-danger" ErrorMessage="El campo de nombre de usuario es obligatorio."  ValidationGroup="FormValidationGroup" />
                </div>
            </div>
            <%--dni--%>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="TBdni" CssClass="col-md-2 control-label"><%: Resources.idioma.dni.ToUpper %></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="TBdni" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="TBdni" ID="RequiredFieldValidator4"
                        CssClass="text-danger" ErrorMessage="El campo de nombre de usuario es obligatorio."  ValidationGroup="FormValidationGroup" />
                </div>
            </div>
            <%--tlf--%>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="TBtelf" CssClass="col-md-2 control-label"><%: Resources.idioma.telefono.ToUpper %></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="TBtelf" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="TBtelf" ID="RequiredFieldValidator5"
                        CssClass="text-danger" ErrorMessage="El campo de nombre de usuario es obligatorio."  ValidationGroup="FormValidationGroup" />
                </div>
            </div>
            

            <div class="form-group" runat="server" id="Dusuario">
                <asp:Label runat="server" ID="lusu" AssociatedControlID="TBusuario" CssClass="col-md-2 control-label" Text="Usuario" />
                <div class="col-md-6">
                    <%--<asp:HiddenField runat="server" ID="HFusuario" />--%>
                    <asp:TextBox runat="server" ID="TBusuario" CssClass="form-control input-md" AutoPostBack="true"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="Vusuario" ControlToValidate="TBusuario" 
                            CssClass="text-danger" ErrorMessage="!!" ValidationGroup="FormValidationGroup" />
                </div>
                <div class="col-md-2" runat="server" id="Derror" visible="false">
                    <div class="alert-danger" role="alert">
                        <%--<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>--%>
                        <asp:Label runat="server" ID="Lerror" CssClass="alert-danger" Text="En uso"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" ID="lemail" AssociatedControlID="TBemail" CssClass="col-md-2 control-label" Text="Email" />
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="TBemail" TextMode="Email" CssClass="form-control input-md" AutoPostBack="true"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ID="Vemail" ControlToValidate="TBemail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
					    ErrorMessage="Email incorrecto" ValidationGroup="FormValidationGroup"  CssClass="text-danger" /> 
                    <asp:RequiredFieldValidator runat="server" ID="RVemail" ControlToValidate="TBemail" 
                            CssClass="text-danger" ErrorMessage="!!" ValidationGroup="FormValidationGroup" />        
                </div>
                <div class="col-md-2" runat="server" id="Derroremail" visible="false">
                    <div class="alert-danger" role="alert">
                        <%--<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>--%>
                        <asp:Label runat="server" ID="Lerroremail" CssClass="alert-danger" Text="En uso"></asp:Label>
                    </div>
                </div>
            </div>

            <h3><asp:Label runat="server" ID="lcontrasena" Text=""></asp:Label></h3>
            <div class="form-group">
                <asp:Label runat="server" ID="lclave" AssociatedControlID="TBclave" CssClass="col-md-2 control-label" Text="Clave" />
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="TBclave" CssClass="form-control" TextMode="Password" ></asp:TextBox> 
                    <p class="help-block"><asp:Label runat="server" ID="Help" Text="Luzera" ></asp:Label></p>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredVpass" ControlToValidate="TBclave" 
                            CssClass="text-danger" ErrorMessage="!!" ValidationGroup="FormValidationGroup" />
                    <asp:RegularExpressionValidator ID="Vpass" runat="server" CssClass="text-danger"   
                    ErrorMessage="Minimum 6 characters required"
                    ControlToValidate="TBclave"    
                    ValidationExpression = "^[\s\S]{6,}$" />
                    <%--ValidationExpression=".{5}.*" ValidationExpression="^[a-zA-Z0-9'@&#.\s]{6,10}$"--%>

                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" ID="lclaveconfirm" AssociatedControlID="TBclaveconfirm" CssClass="col-md-2 control-label" Text="Claveconfirm" />
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="TBclaveconfirm" Text="" CssClass="form-control" TextMode="Password"></asp:TextBox>  
                    <asp:CompareValidator runat="server" ControlToCompare="TBclave" ControlToValidate="TBclaveconfirm"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="La contraseña y la contraseña de confirmación no coinciden." />
                </div>
            </div>
        </div> 
        <div class="col-md-5">
            <h3><asp:Label runat="server" ID="lconfiguracion" Text="CONFIGURACION" ></asp:Label></h3>
            <div class="form-group">
                <div class="checkbox">
                    <asp:CheckBox runat="server" ID="CBadmin" Text="Administrador" />
                </div>
                 <div class="checkbox">
                    <asp:CheckBox runat="server" ID="CBactivo" Text="Activo" Checked="true" />
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