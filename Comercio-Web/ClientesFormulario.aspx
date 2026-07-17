<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ClientesFormulario.aspx.cs" Inherits="Comercio_Web.ClientesFormulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style> .validation{ color:red; font-size:15px;} </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlFormularioCliente" runat="server" DefaultButton="btnGuardar" DefaultFocus="txtDni">
        <div class="card shadow-sm border-0">
            <div class="card-body p-4">
                <h1 class="h3 mb-4"><asp:Label ID="lblTitulo" runat="server" Text="Nuevo Cliente" /></h1>
                <div class="mb-3">
                    <asp:Label Text="DNI" runat="server" CssClass="form-label" />
                    <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" TextMode="Number" MaxLength="8" />
                    <asp:RequiredFieldValidator CssClass="validation" ErrorMessage="Este campo no puede quedar vacio" ControlToValidate="txtDni" runat="server" TextStyle="" />
                    <asp:RegularExpressionValidator CssClass="validation" ErrorMessage="Ingrese un DNI valido, debe contener 8 numeros" ControlToValidate="txtDni" runat="server" ValidationExpression="^\d{8}$" />
                </div>
                <div class="mb-3">
                    <asp:Label Text="Nombre" runat="server" CssClass="form-label" />
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="50" />
                    <asp:RequiredFieldValidator CssClass="validation" ErrorMessage="Este campo no puede quedar vacio" ControlToValidate="txtNombre" runat="server" textMode="string" maxlength="30" />
                    <asp:RegularExpressionValidator CssClass="validation" ErrorMessage="Ingrese un nombre valido, solo letras y espacios" ControlToValidate="txtNombre" runat="server" ValidationExpression="^[a-zA-Z\s]+$" />
                </div>
                <div class="mb-3">
                    <asp:Label Text="Email" runat="server" CssClass="form-label" />
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                    <asp:RequiredFieldValidator CssClass="validation" ErrorMessage="Este campo no puede quedar vacio" ControlToValidate="txtEmail" runat="server" TextStyle="" />
                    <asp:RegularExpressionValidator CssClass="validation" ErrorMessage="Ingrese un email valido" ControlToValidate="txtEmail" runat="server" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" />
                </div>
                <div class="d-flex gap-2">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" Visible="false" />
                    <a href="ClientesLista.aspx" class="btn btn-secondary">Cancelar</a>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
