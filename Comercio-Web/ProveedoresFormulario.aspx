<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ProveedoresFormulario.aspx.cs" Inherits="Comercio_Web.ProveedoresFormulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style>.validation{ color: red; font-size: 14px;}    </style> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlFormularioProveedor" runat="server" DefaultButton="btnGuardar" DefaultFocus="txtNombre">
        <div class="card shadow-sm border-0">
            <div class="card-body p-4">
                <h1 class="h3 mb-4"><asp:Label ID="lblTitulo" runat="server" Text="Nuevo Proveedor" /></h1>
                <div class="mb-3">
                    <asp:Label Text="Nombre" runat="server" CssClass="form-label" />
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="39"/>
                    <asp:RequiredFieldValidator CssClass="validation" ErrorMessage="Este campo no puede quedar vacio" ControlToValidate="txtNombre" runat="server" TextStyle="" />
                </div>
                <div class="mb-3">
                    <asp:Label Text="Teléfono" runat="server" CssClass="form-label" />
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" TextMode="Phone"  MaxLength="10" />
                    <asp:RequiredFieldValidator CssClass="validation" ErrorMessage="Este campo no puede quedar vacio" ControlToValidate="txtTelefono" runat="server" TextStyle="" />
                    <asp:RegularExpressionValidator CssClass="validation" ErrorMessage="Ingrese un telefono valido, debe contener 10 numeros" ControlToValidate="txtTelefono" runat="server" ValidationExpression="^\d{10}$" />
                    </div>
                <div class="mb-3">
                    <asp:Label Text="Email" runat="server" CssClass="form-label" />
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                    <asp:RequiredFieldValidator CssClass="validation" ErrorMessage="Este campo no puede quedar vacio" ControlToValidate="txtEmail" runat="server" TextStyle="" />
                    <asp:RegularExpressionValidator CssClass="validation" ErrorMessage="Ingrese un email valido" ControlToValidate="txtEmail" runat="server" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" />
                </div>
                <div class="mb-3">
                    <asp:Label Text="Cuit o Cuil" runat="server" CssClass="form-label" />
                    <asp:TextBox ID="txtCuit" runat="server" CssClass="form-control" TextMode="SingleLine"  MaxLength="13" />
                    <asp:RequiredFieldValidator CssClass="validation" ErrorMessage="Este campo no puede quedar vacio" ControlToValidate="txtCuit" runat="server" TextStyle="" />
                    <asp:RegularExpressionValidator CssClass="validation" ErrorMessage="Ingrese un Cuit valido, debe contener 11 numeros" ControlToValidate="txtCuit" runat="server" ValidationExpression="^\d{2}-?\d{8}-?\d$" />
                </div>
                <div class="d-flex gap-2">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" Visible="false" />
                    <a href="ProveedoresLista.aspx" class="btn btn-secondary">Cancelar</a>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
