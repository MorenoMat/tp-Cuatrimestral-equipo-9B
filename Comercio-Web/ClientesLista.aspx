<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ClientesLista.aspx.cs" Inherits="Comercio_Web.ClientesLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Clientes</h1>
    <asp:GridView ID="dgvClientes" runat="server" DataKeyNames="IdCliente"
        CssClass="table table-bordered table-hover" AutoGenerateColumns="false"
        OnSelectedIndexChanged="dgvClientes_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="DNI" DataField="Dni" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Email" DataField="Email" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✍" />
        </Columns>
    </asp:GridView>
    <a href="ClientesFormulario.aspx" class="btn btn-primary">Agregar</a>
</asp:Content>
