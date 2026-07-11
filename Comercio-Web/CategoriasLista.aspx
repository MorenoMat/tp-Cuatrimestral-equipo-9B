<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CategoriasLista.aspx.cs" Inherits="Comercio_Web.CategoriasLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Categorías</h1>
    <asp:GridView ID="dgvCategorias" runat="server" DataKeyNames="IdCategoria"
        CssClass="table table-bordered table-hover" AutoGenerateColumns="false"
        OnSelectedIndexChanged="dgvCategorias_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Editar Categoria" />
        </Columns>
    </asp:GridView>
    <a href="CategoriasFormulario.aspx" class="btn btn-primary">Agregar</a>
</asp:Content>
