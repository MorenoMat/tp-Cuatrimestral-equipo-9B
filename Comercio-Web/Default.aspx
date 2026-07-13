<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Comercio_Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .home-hero-title { font-weight: 700; }
        .home-kpi-card {
            border: 1px solid #dee2e6;
            border-radius: .75rem;
            transition: transform .15s ease, box-shadow .15s ease;
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
        .home-quick-card {
            border: 1px solid #dee2e6;
            border-radius: .75rem;
        }
        .home-quick-action {
            background: #eef0f5;
            border: 1px solid #e0e3ea;
            border-radius: .5rem;
            min-height: 110px;
            text-decoration: none;
            color: #1f2a37;
            transition: transform .15s ease, box-shadow .15s ease;
        }
        .home-quick-action:hover {
            transform: translateY(-2px);
            box-shadow: 0 .5rem 1rem rgba(0,0,0,.08);
            color: #1f2a37;
        }
        .home-quick-action-highlight {
            background: #e8eeff;
            border-color: #d5deff;
            box-shadow: 0 .25rem .6rem rgba(47,98,255,.12);
        }
        .home-quick-icon {
            font-size: 1.35rem;
            line-height: 1;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mb-4">
        <h1 class="h2 mb-1 home-hero-title">
            <asp:Label ID="lblSaludo" runat="server" />
        </h1>
        <p class="text-muted mb-0">Aquí tienes un resumen de tu actividad comercial.</p>
    </div>

    <div class="row g-3 mb-4">
        <div class="col-12 col-md-6 col-xl-3">
            <button type="button" class="btn w-100 text-start p-0 border-0 bg-transparent">
                <div class="card home-kpi-card h-100">
                    <div class="card-body p-3">
                        <div class="d-flex align-items-center justify-content-between mb-3">
                            <span class="home-kpi-icon" style="background:#e8eeff;color:#2f62ff;">💵</span>
                        </div>
                        <div class="text-uppercase text-muted small fw-semibold">Tu dinero facturado hoy</div>
                        <div class="fs-3 fw-semibold">$ <asp:Label ID="lblVentasDia" runat="server" Text="0,00" /></div>
                    </div>
                </div>
            </button>
        </div>

        <div class="col-12 col-md-6 col-xl-3">
            <button type="button" class="btn w-100 text-start p-0 border-0 bg-transparent">
                <div class="card home-kpi-card h-100">
                    <div class="card-body p-3">
                        <div class="d-flex align-items-center justify-content-between mb-3">
                            <span class="home-kpi-icon" style="background:#eef1f5;color:#6c757d;">📅</span>
                        </div>
                        <div class="text-uppercase text-muted small fw-semibold">Tu facturación desde inicio de mes</div>
                        <div class="fs-3 fw-semibold">$ <asp:Label ID="lblVentasMes" runat="server" Text="0,00" /></div>
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
                        <div class="fs-3 fw-semibold"><asp:Label ID="lblAlertasStock" runat="server" Text="0 productos bajos" /></div>
                    </div>
                </div>
            </button>
        </div>

        <div class="col-12 col-md-6 col-xl-3">
            <button type="button" class="btn w-100 text-start p-0 border-0 bg-transparent">
                <div class="card home-kpi-card h-100">
                    <div class="card-body p-3">
                        <div class="mb-3">
                            <span class="home-kpi-icon" style="background:#ede9ff;color:#5b4abf;">📈</span>
                        </div>
                        <div class="text-uppercase text-muted small fw-semibold">Tus ventas cerradas desde inicio de mes</div>
                        <div class="fs-3 fw-semibold"><asp:Label ID="lblCantidadVentasMes" runat="server" Text="0" /> ventas</div>
                    </div>
                </div>
            </button>
        </div>
    </div>

    <div class="row g-3">
        <div class="col-12">
            <div class="card home-quick-card h-100">
                <div class="card-body p-3 p-md-4">
                    <h5 class="mb-3">Acciones Rápidas</h5>
                    <div class="row g-3">
                        <div class="col-12 col-md-6">
                            <a href="VentasFormulario.aspx" class="home-quick-action home-quick-action-highlight d-flex flex-column justify-content-center align-items-center p-3">
                                <span class="home-quick-icon mb-2">🛒</span>
                                <span class="fw-semibold text-uppercase small text-center">Nueva Venta</span>
                            </a>
                        </div>
                        <div class="col-12 col-md-6">
                            <a href="ClientesFormulario.aspx" class="home-quick-action d-flex flex-column justify-content-center align-items-center p-3">
                                <span class="home-quick-icon mb-2">👤</span>
                                <span class="fw-semibold text-uppercase small text-center">Crear Cliente</span>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
