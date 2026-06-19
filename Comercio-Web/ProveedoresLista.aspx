<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ProveedoresLista.aspx.cs" Inherits="Comercio_Web.ProveedoresLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Proveedores</h1>
    <asp:GridView ID="dgvProveedores" runat="server" DataKeyNames="IdProveedor"
        CssClass="table table-bordered table-hover" AutoGenerateColumns="false"
        OnSelectedIndexChanged="dgvProveedores_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
            <asp:BoundField HeaderText="Email" DataField="Email" />
            <asp:BoundField HeaderText="CUIT o CUIL" DataField="Cuit" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Editar Proveedores" />
        </Columns>
    </asp:GridView>
    <a href="ProveedoresFormulario.aspx" class="btn btn-primary">Agregar</a>
</asp:Content>
