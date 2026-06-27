
-- 2. Luego: Crear con EsAdmin  
CREATE TABLE Marcas (
    idMarca INT PRIMARY KEY IDENTITY(1,1),
    descripcion VARCHAR(30) NOT NULL
);

CREATE TABLE Categorias (
    idCategoria INT PRIMARY KEY IDENTITY(1,1),
    descripcion VARCHAR(30) NOT NULL
);

CREATE TABLE Productos (
    idProducto INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(30) NOT NULL,
    UltimoPrecioCompra DECIMAL(10,2) NOT NULL DEFAULT 0,  -- renombrado de ultimoPrecio
    porcentajeGanancia DECIMAL(4,2) NOT NULL,
    stockActual INT NOT NULL DEFAULT 0 CHECK (stockActual >= 0),
    stockMinimo INT NOT NULL DEFAULT 0 CHECK (stockMinimo >= 0),
    descripcion VARCHAR(50) NULL,
    idMarca INT NOT NULL,
    idCategoria INT NOT NULL,

    CONSTRAINT FK_Productos_Marcas FOREIGN KEY (idMarca) REFERENCES Marcas(idMarca),
    CONSTRAINT FK_Productos_Categorias FOREIGN KEY (idCategoria) REFERENCES Categorias(idCategoria)
);

CREATE TABLE Clientes (
    idCliente INT PRIMARY KEY IDENTITY(1,1),
    dni VARCHAR(8) NOT NULL UNIQUE,
    nombre VARCHAR(30) NOT NULL,
    email VARCHAR(50)
);

CREATE TABLE Usuarios (
    idUsuario INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(30) NOT NULL UNIQUE,
    usuarioLogin VARCHAR(15) UNIQUE NOT NULL,
    contraseña VARCHAR(15) NOT NULL,
    EsAdmin BIT NOT NULL DEFAULT 0  -- ✅ Nombre correcto
);

CREATE TABLE Proveedores (
    idProveedor INT PRIMARY KEY IDENTITY(1,1),
    cuit VARCHAR(11) NOT NULL UNIQUE,
    nombre VARCHAR(40) NOT NULL,
    Telefono VARCHAR(25),
    email VARCHAR(50)
);

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
    idUsuario INT NOT NULL,
    fechaCompra DATETIME NOT NULL DEFAULT GETDATE(),
    total DECIMAL(10,2) NOT NULL DEFAULT 0,  -- DEFAULT 0: el negocio no inserta este campo
    CONSTRAINT FK_Compras_Proveedores FOREIGN KEY (idProveedor) REFERENCES Proveedores(idProveedor),
    CONSTRAINT FK_Compras_Usuarios FOREIGN KEY (idUsuario) REFERENCES Usuarios(idUsuario)
);

CREATE TABLE DetalleCompras (  -- renombrado de DetalleCompra
    idDetalleCompra INT PRIMARY KEY IDENTITY(1,1),
    idCompra INT NOT NULL,
    idProducto INT NOT NULL,
    cantidad INT NOT NULL,
    precioUnitario DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_DetalleCompras_Compras FOREIGN KEY (idCompra) REFERENCES Compras(idCompra),
    CONSTRAINT FK_DetalleCompras_Productos FOREIGN KEY (idProducto) REFERENCES Productos(idProducto)
);

CREATE TABLE Ventas (
    idVenta INT PRIMARY KEY IDENTITY(1,1),
    idCliente INT NOT NULL,
    idUsuario INT NOT NULL,
    fechaVenta DATETIME NOT NULL DEFAULT GETDATE(),
    total DECIMAL(10,2) NOT NULL,
    numeroFactura INT NOT NULL UNIQUE,  -- cambiado de VARCHAR(30) a INT
    CONSTRAINT FK_Ventas_Clientes FOREIGN KEY (idCliente) REFERENCES Clientes(idCliente),
    CONSTRAINT FK_Ventas_Usuarios FOREIGN KEY (idUsuario) REFERENCES Usuarios(idUsuario)
);

CREATE TABLE DetalleVentas (  -- renombrado de DetalleVenta
    idDetalleVenta INT PRIMARY KEY IDENTITY(1,1),
    idVenta INT NOT NULL,
    idProducto INT NOT NULL,
    cantidad INT NOT NULL,
    precioUnitario DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_DetalleVentas_Ventas FOREIGN KEY (idVenta) REFERENCES Ventas(idVenta),
    CONSTRAINT FK_DetalleVentas_Productos FOREIGN KEY (idProducto) REFERENCES Productos(idProducto)
);


-- 1. Marcas
INSERT INTO Marcas (descripcion) VALUES 
    ('Samsung'), ('LG'), ('Sony'), ('Philips');

-- 2. Categorias
INSERT INTO Categorias (descripcion) VALUES 
    ('Electrónica'), ('Hogar'), ('Computación'), ('Telefonía');

-- 3. Clientes
INSERT INTO Clientes (dni, nombre, email) VALUES 
    ('12345678', 'Juan Pérez',    'juan.perez@email.com'),
    ('87654321', 'María González','maria.gonzalez@email.com'),
    ('11223344', 'Carlos López',  'carlos.lopez@email.com');

-- 4. Usuarios  (columna EsAdmin)
INSERT INTO Usuarios (nombre, usuarioLogin, contraseña, EsAdmin) VALUES 
    ('Administrador', 'admin',     'admin123', 1),
    ('Vendedor 1',    'vendedor1', 'vend123',  0),
    ('Vendedor 2',    'vendedor2', 'vend456',  0);

-- 5. Proveedores
INSERT INTO Proveedores (cuit, nombre, Telefono, email) VALUES 
    ('20123456789', 'Distribuidora Tech SRL', '011-4567-8900', 'ventas@techsrl.com'),
    ('20987654321', 'ElectroMundo SA',         '011-1234-5678', 'info@electromundo.com');

-- 6. Productos  (columna UltimoPrecioCompra)
INSERT INTO Productos (nombre, UltimoPrecioCompra, porcentajeGanancia, stockActual, stockMinimo, descripcion, idMarca, idCategoria) VALUES 
    ('TV 55 pulgadas',  85000.00, 25.00, 10, 3, 'Smart TV 4K',         1, 1),
    ('Heladera 300L',  120000.00, 30.00,  5, 2, 'Heladera no frost',   2, 2),
    ('Notebook Core i5',95000.00, 20.00,  8, 3, '8GB RAM, 256GB SSD', 1, 3),
    ('Celular Android', 45000.00, 35.00, 15, 5, 'Dual SIM, 128GB',    1, 4);

-- 7. Producto_Proveedor
INSERT INTO Producto_Proveedor (idProducto, idProveedor) VALUES 
    (1, 1), (2, 2), (3, 1), (4, 1), (1, 2);

-- 8. Compras  (sin total, usa DEFAULT 0)
INSERT INTO Compras (idProveedor, idUsuario, fechaCompra) VALUES 
    (1, 1, '2024-01-15'),
    (2, 1, '2024-01-20');

-- 9. DetalleCompras  (tabla renombrada)
INSERT INTO DetalleCompras (idCompra, idProducto, cantidad, precioUnitario) VALUES 
    (1, 1, 3, 68000.00),
    (1, 3, 1, 76000.00),
    (2, 2, 2, 92000.00);

-- 10. Ventas  (numeroFactura ahora es INT)
INSERT INTO Ventas (idCliente, idUsuario, fechaVenta, total, numeroFactura) VALUES 
    (1, 2, '2024-02-01',  85000.00, 1),
    (2, 3, '2024-02-02', 140000.00, 2),
    (3, 2, '2024-02-03',  90000.00, 3);

-- 11. DetalleVentas  (tabla renombrada)
INSERT INTO DetalleVentas (idVenta, idProducto, cantidad, precioUnitario) VALUES 
    (1, 1, 1,  85000.00),
    (2, 2, 1, 120000.00),
    (2, 4, 2,  10000.00),
    (3, 3, 1,  90000.00);