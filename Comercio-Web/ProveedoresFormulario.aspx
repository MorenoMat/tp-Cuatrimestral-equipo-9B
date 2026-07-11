<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ProveedoresFormulario.aspx.cs" Inherits="Comercio_Web.ProveedoresFormulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style>.validation{ color: red; font-size: 14px;}    </style> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1><asp:Label ID="lblTitulo" runat="server" Text="Nuevo Proveedor" /></h1>
    <div class="mb-3">
        <asp:Label Text="Nombre" runat="server" CssClass="form-label" />
        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator CssClass="validation" ErrorMessage="Este campo no puede quedar vacio" ControlToValidate="txtNombre" runat="server"  TextStyle=""/> 
    </div>
    <div class="mb-3">
        <asp:Label Text="Teléfono" runat="server" CssClass="form-label" />
        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" TextMode="Phone" />
        <asp:RequiredFieldValidator CssClass="validation" ErrorMessage="Este campo no puede quedar vacio" ControlToValidate="txtTelefono" runat="server"  TextStyle=""/> 
    </div>
    <div class="mb-3">
        <asp:Label Text="Email" runat="server" CssClass="form-label" />
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
        <asp:RequiredFieldValidator CssClass="validation" ErrorMessage="Este campo no puede quedar vacio" ControlToValidate="txtEmail" runat="server"  TextStyle=""/> 
    </div>
       <div class="mb-3">
       <asp:Label Text="Cuit o Cuil" runat="server" CssClass="form-label" />
       <asp:TextBox ID="txtCuit" runat="server" CssClass="form-control"     TextMode="SingleLine" MaxLength="13"/>
       <asp:RequiredFieldValidator CssClass="validation" ErrorMessage="Este campo no puede quedar vacio" ControlToValidate="txtCuit" runat="server"  TextStyle=""/> 
   </div>
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger ms-2" OnClick="btnEliminar_Click" Visible="false" />
    <a href="ProveedoresLista.aspx" class="btn btn-secondary ms-2">Cancelar</a>
</asp:Content>
