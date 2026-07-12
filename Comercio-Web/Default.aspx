<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Comercio_Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .home-hero-title { font-weight: 700; }
        .home-kpi-card {
            border: 1px solid #dee2e6;
            border-radius: .75rem;
            transition: transform .15s ease, box-shadow .15s ease;
        }
        .home-kpi-card:hover {
            transform: translateY(-2px);
            box-shadow: 0 .5rem 1rem rgba(0,0,0,.08);
        }
        .home-kpi-icon {
            width: 44px;
            height: 44px;
            border-radius: .5rem;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            font-size: 1.25rem;
            font-weight: 700;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mb-4">
        <h1 class="h2 mb-1 home-hero-title">
            <asp:Label ID="lblSaludo" runat="server" />
        </h1>
        <p class="text-muted mb-0">Aquí tienes un resumen de la actividad comercial de hoy.</p>
    </div>

    <div class="row g-3">
        <div class="col-12 col-md-6 col-xl-3">
            <button type="button" class="btn w-100 text-start p-0 border-0 bg-transparent">
                <div class="card home-kpi-card h-100">
                    <div class="card-body p-3">
                        <div class="d-flex align-items-center justify-content-between mb-3">
                            <span class="home-kpi-icon" style="background:#e8eeff;color:#2f62ff;">💵</span>
                            <span class="badge text-bg-success-subtle text-success">+12.5%</span>
                        </div>
                        <div class="text-uppercase text-muted small fw-semibold">Ventas del día</div>
                        <div class="fs-3 fw-semibold">$42,850.00</div>
                    </div>
                </div>
            </button>
        </div>

        <div class="col-12 col-md-6 col-xl-3">
            <button type="button" class="btn w-100 text-start p-0 border-0 bg-transparent">
                <div class="card home-kpi-card h-100">
                    <div class="card-body p-3">
                        <div class="mb-3">
                            <span class="home-kpi-icon" style="background:#eef1f5;color:#6c757d;">📋</span>
                        </div>
                        <div class="text-uppercase text-muted small fw-semibold">Compras pendientes</div>
                        <div class="fs-3 fw-semibold">14 órdenes</div>
                    </div>
                </div>
            </button>
        </div>

        <div class="col-12 col-md-6 col-xl-3">
            <button type="button" class="btn w-100 text-start p-0 border-0 bg-transparent">
                <div class="card home-kpi-card h-100">
                    <div class="card-body p-3">
                        <div class="mb-3">
                            <span class="home-kpi-icon" style="background:#fff3cd;color:#8a6d3b;">⚠</span>
                        </div>
                        <div class="text-uppercase text-muted small fw-semibold">Alertas de stock</div>
                        <div class="fs-3 fw-semibold text-danger">8 productos bajos</div>
                    </div>
                </div>
            </button>
        </div>

        <div class="col-12 col-md-6 col-xl-3">
            <button type="button" class="btn w-100 text-start p-0 border-0 bg-transparent">
                <div class="card home-kpi-card h-100">
                    <div class="card-body p-3">
                        <div class="mb-3">
                            <span class="home-kpi-icon" style="background:#ede9ff;color:#5b4abf;">👤</span>
                        </div>
                        <div class="text-uppercase text-muted small fw-semibold">Clientes nuevos</div>
                        <div class="fs-3 fw-semibold">24 registrados</div>
                    </div>
                </div>
            </button>
        </div>
    </div>
</asp:Content>
