<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="UsuariosFormulario.aspx.cs" Inherits="Comercio_Web.UsuariosFormulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card shadow-sm border-0">
        <div class="card-body p-4">
            <h1 class="h3 mb-4"><asp:Label ID="lblTitulo" runat="server" Text="Nuevo Usuario" /></h1>
            <div class="mb-3">
                <asp:Label Text="Nombre" runat="server" CssClass="form-label" />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <asp:Label Text="Usuario" runat="server" CssClass="form-label" />
                <asp:TextBox ID="txtUsuarioLogin" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ErrorMessage="Rellena el Usuario" ControlToValidate="txtUsuarioLogin" runat="server" />
            </div>
            <div class="mb-3">
                <asp:Label Text="Contraseña" runat="server" CssClass="form-label" />
                <asp:TextBox ID="txtContraseña" runat="server" CssClass="form-control" TextMode="Password" />
                <asp:RequiredFieldValidator ErrorMessage="Rellena la Contraseña" ControlToValidate="txtContraseña" runat="server" />
            </div>
            <div class="mb-3 form-check">
                <asp:CheckBox ID="chkAdmin" runat="server" CssClass="form-check-input" />
                <asp:Label Text="Administrador" runat="server" CssClass="form-check-label" />
            </div>
            <div class="d-flex gap-2">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" Visible="false" />
                <a href="UsuariosLista.aspx" class="btn btn-secondary">Cancelar</a>
            </div>
        </div>
    </div>
</asp:Content>
