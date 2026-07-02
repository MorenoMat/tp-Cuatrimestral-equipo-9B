<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Comercio_Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid mt-4">
        <div class="row g-3">

            <%-- Primer tercio: Alertas de stock --%>
            <div class="col-md-4">
                <div class="card h-100">
                    <div class="card-header card-header-stock">
                        <h5 class="mb-0">&#9888; Alertas de Stock</h5>
                    </div>
                    <div class="card-body">
                        <asp:Panel ID="pnlAlertas" runat="server">
                        </asp:Panel>
                    </div>
                </div>
            </div>

            <%-- Segundo tercio --%>
            <div class="col-md-4">
                <div class="card h-100">
                    <div class="card-header">
                        <h5 class="mb-0">Sección 2</h5>
                    </div>
                    <div class="card-body d-flex flex-column gap-2">
                        <button type="button" class="btn btn-primary">Botón 1</button>
                        <button type="button" class="btn btn-secondary">Botón 2</button>
                        <button type="button" class="btn btn-success">Botón 3</button>
                    </div>
                </div>
            </div>

            <%-- Tercer tercio --%>
            <div class="col-md-4">
                <div class="card h-100">
                    <div class="card-header">
                        <h5 class="mb-0">Sección 3</h5>
                    </div>
                    <div class="card-body d-flex flex-column gap-2">
                        <button type="button" class="btn btn-info">Botón A</button>
                        <button type="button" class="btn btn-warning">Botón B</button>
                        <button type="button" class="btn btn-danger">Botón C</button>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>