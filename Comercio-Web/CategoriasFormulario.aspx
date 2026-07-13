                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          <%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CategoriasFormulario.aspx.cs" Inherits="Comercio_Web.CategoriasFormulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .validation{ color: red; font-size: 12px }
    </style>
    <asp:Panel ID="pnlFormularioCategoria" runat="server" DefaultButton="btnGuardar" DefaultFocus="txtDescripcion">
        <div class="card shadow-sm border-0">
            <div class="card-body p-4">
                <h1 class="h3 mb-4"><asp:Label ID="lblTitulo" runat="server" Text="Nueva Categoría" /></h1>
                <div class="mb-3">
                    <asp:Label Text="Descripción" runat="server" CssClass="form-label" />
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator CssClass="validation" ErrorMessage="Este campo no puede quedar vacio" ControlToValidate="txtDescripcion" runat="server" TextStyle=""/>
                </div>
                <div class="d-flex gap-2">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" Visible="false" />
                    <a href="CategoriasLista.aspx" class="btn btn-secondary">Cancelar</a>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
