
create table Marcas(
idMarca int primary key identity(1,1),
descripcion varchar(30) not null
)
create table Categorias(
idCategoria int primary key identity(1,1),
descripcion varchar(30) not null
)

create table Productos(
idProducto int primary key identity(1,1),
nombre varchar(30)  not null,
ultimoPrecio decimal(10,2)  not null default 0,
porcentajeGanancia decimal(4,2)  not null,
stockActual int not null default 0,
stockMinimo int  not null default 0,
descripcion varchar(50)  null,
idMarca int  not null,
idCategoria int   not null

CONSTRAINT FK_Productos_Marcas FOREIGN KEY (idMarca) REFERENCES Marcas(idMarca),
CONSTRAINT FK_Productos_Categorias FOREIGN KEY (idCategoria) REFERENCES Categorias(idCategoria)
)

create table Clientes(
idCliente int primary key identity(1,1),
 dni int not null,
 nombre int not null,
 email varchar(50)
)

create table Usuarios(
idUsuario int primary key identity(1,1),
nombre varchar(30) not null,
usuarioLogin varchar(15) unique not null,
contraseña varchar(15) not null,
administrador bit not null default 0,
)

create table Proveedores(
idProveedor int primary key identity(1,1),
 dni int not null,
 nombre varchar(40) not null,
 Telefono varchar(25),
 email varchar(50)
)
CREATE TABLE Producto_Proveedor (
    idProducto INT NOT NULL,
    idProveedor INT NOT NULL,
    PRIMARY KEY (idProducto, idProveedor),
    CONSTRAINT FK_ProdProv_Productos FOREIGN KEY (idProducto) REFERENCES Productos(idProducto),
    CONSTRAINT FK_ProdProv_Proveedores FOREIGN KEY (idProveedor) REFERENCES Proveedores(idProveedor)
);
CREATE TABLE Compras (
    idCompra INT PRIMARY KEY IDENTITY(1,1),
    idProveedor INT NOT NULL,
    idUsuario INT NOT NULL, -- Quién registró la compra (Auditoría)
    fecha DATETIME NOT NULL DEFAULT GETDATE(),
    total DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_Compras_Proveedores FOREIGN KEY (idProveedor) REFERENCES Proveedores(idProveedor),
    CONSTRAINT FK_Compras_Usuarios FOREIGN KEY (idUsuario) REFERENCES Usuarios(idUsuario)
);
CREATE TABLE DetalleCompra (
    idDetalleCompra INT PRIMARY KEY IDENTITY(1,1),
    idCompra INT NOT NULL,
    idProducto INT NOT NULL, 
    cantidad INT NOT NULL,
    precioUnitario DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_DetalleCompra_Compras FOREIGN KEY (idCompra) REFERENCES Compras(idCompra),
    CONSTRAINT FK_DetalleCompra_Productos FOREIGN KEY (idProducto) REFERENCES Productos(idProducto)
);
CREATE TABLE Ventas (
    idVenta INT PRIMARY KEY IDENTITY(1,1),
    idCliente INT NOT NULL,
    idUsuario INT NOT NULL, -- Vendedor o Admin que atendió en el sistema
    fecha DATETIME NOT NULL DEFAULT GETDATE(),
    total DECIMAL(10,2) NOT NULL,
    numeroFactura VARCHAR(30) NOT NULL, -- Generada de forma única por  código
    CONSTRAINT FK_Ventas_Clientes FOREIGN KEY (idCliente) REFERENCES Clientes(idCliente),
    CONSTRAINT FK_Ventas_Usuarios FOREIGN KEY (idUsuario) REFERENCES Usuarios(idUsuario)
);
CREATE TABLE DetalleVenta (
    idDetalleVenta INT PRIMARY KEY IDENTITY(1,1),
    idVenta INT NOT NULL,
    idProducto INT NOT NULL, 
    cantidad INT NOT NULL,
    precioUnitario DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_DetalleVenta_Ventas FOREIGN KEY (idVenta) REFERENCES Ventas(idVenta),
    CONSTRAINT FK_DetalleVenta_Productos FOREIGN KEY (idProducto) REFERENCES Productos(idProducto)
);
