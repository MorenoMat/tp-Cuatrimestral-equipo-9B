<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="UsuariosLista.aspx.cs" Inherits="Comercio_Web.UsuariosLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Usuarios</h1>
    <asp:GridView ID="dgvUsuarios" runat="server" DataKeyNames="IdUsuario"
        CssClass="table table-bordered table-hover" AutoGenerateColumns="false"
        OnSelectedIndexChanged="dgvUsuarios_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="Usuario" DataField="UsuarioLogin" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:CheckBoxField HeaderText="Admin" DataField="esAdmin" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✍" />
        </Columns>
    </asp:GridView>
    <a href="UsuariosFormulario.aspx" class="btn btn-primary">Agregar</a>
</asp:Content>
