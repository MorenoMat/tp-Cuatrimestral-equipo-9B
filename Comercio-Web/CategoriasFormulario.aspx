<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CategoriasFormulario.aspx.cs" Inherits="Comercio_Web.CategoriasFormulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1><asp:Label ID="lblTitulo" runat="server" Text="Nueva Categoría" /></h1>
    <div class="mb-3">
        <asp:Label Text="Descripción" runat="server" CssClass="form-label" />
        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" />
    </div>
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger ms-2" OnClick="btnEliminar_Click" Visible="false" />
    <a href="CategoriasLista.aspx" class="btn btn-secondary ms-2">Cancelar</a>
</asp:Content>
