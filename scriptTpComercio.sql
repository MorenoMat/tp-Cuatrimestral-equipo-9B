-- Eliminar tablas con FK primero
DROP TABLE IF EXISTS DetalleVentas;
DROP TABLE IF EXISTS DetalleCompras;
DROP TABLE IF EXISTS Ventas;
DROP TABLE IF EXISTS Compras;
DROP TABLE IF EXISTS Producto_Proveedor;
DROP TABLE IF EXISTS Productos;

-- Eliminar tablas base
DROP TABLE IF EXISTS Clientes;
DROP TABLE IF EXISTS Usuarios;
DROP TABLE IF EXISTS Proveedores;
DROP TABLE IF EXISTS Marcas;
DROP TABLE IF EXISTS Categorias;

CREATE TABLE Marcas (
    idMarca INT PRIMARY KEY IDENTITY(1,1),
    descripcion VARCHAR(30) NOT NULL,
    activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE Categorias (
    idCategoria INT PRIMARY KEY IDENTITY(1,1),
    descripcion VARCHAR(30) NOT NULL
    
);

CREATE TABLE Productos (
    idProducto INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(30) NOT NULL,
    UltimoPrecioCompra DECIMAL(10,2) NOT NULL DEFAULT 0,
    porcentajeGanancia DECIMAL(4,2) NOT NULL,
    stockActual INT NOT NULL DEFAULT 0 CHECK (stockActual >= 0),
    stockMinimo INT NOT NULL DEFAULT 0 CHECK (stockMinimo >= 0),
    descripcion VARCHAR(50) NULL,
    idMarca INT NOT NULL,
    idCategoria INT NOT NULL,
    activo BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_Productos_Marcas FOREIGN KEY (idMarca) REFERENCES Marcas(idMarca),
    CONSTRAINT FK_Productos_Categorias FOREIGN KEY (idCategoria) REFERENCES Categorias(idCategoria)
);

CREATE TABLE Clientes (
    idCliente INT PRIMARY KEY IDENTITY(1,1),
    dni VARCHAR(8) NOT NULL UNIQUE,
    nombre VARCHAR(30) NOT NULL,
    email VARCHAR(50),
    activo BIT not null default 1
);

CREATE TABLE Usuarios (
    idUsuario INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(30) NOT NULL UNIQUE,
    usuarioLogin VARCHAR(15) UNIQUE NOT NULL,
    contraseña VARCHAR(100) NOT NULL,
    EsAdmin BIT NOT NULL DEFAULT 0
);

CREATE TABLE Proveedores (
    idProveedor INT PRIMARY KEY IDENTITY(1,1),
    cuit VARCHAR(11) NOT NULL UNIQUE,
    nombre VARCHAR(40) NOT NULL,
    Telefono VARCHAR(25),
    email VARCHAR(50),
    Direccion VARCHAR(30) null,
    activo BIT not null default 1
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
    total DECIMAL(10,2) NOT NULL DEFAULT 0,
    estado VARCHAR(15) NOT NULL check (estado in ('Pendiente','Finalizada')) DEFAULT 'Pendiente',
    CONSTRAINT FK_Compras_Proveedores FOREIGN KEY (idProveedor) REFERENCES Proveedores(idProveedor),
    CONSTRAINT FK_Compras_Usuarios FOREIGN KEY (idUsuario) REFERENCES Usuarios(idUsuario)
);

CREATE TABLE DetalleCompras (
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
    numeroFactura INT NOT NULL UNIQUE,
    CONSTRAINT FK_Ventas_Clientes FOREIGN KEY (idCliente) REFERENCES Clientes(idCliente),
    CONSTRAINT FK_Ventas_Usuarios FOREIGN KEY (idUsuario) REFERENCES Usuarios(idUsuario)
);

CREATE TABLE DetalleVentas (
    idDetalleVenta INT PRIMARY KEY IDENTITY(1,1),
    idVenta INT NOT NULL,
    idProducto INT NOT NULL,
    cantidad INT NOT NULL,
    precioUnitario DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_DetalleVentas_Ventas FOREIGN KEY (idVenta) REFERENCES Ventas(idVenta),
    CONSTRAINT FK_DetalleVentas_Productos FOREIGN KEY (idProducto) REFERENCES Productos(idProducto)
);

-- ============================================
-- INSERCIÓN DE DATOS DE PRUEBA
-- ============================================

-- MARCAS (20 marcas)
INSERT INTO Marcas (descripcion) VALUES 
    ('Samsung'), ('LG'), ('Sony'), ('Philips'), ('Panasonic'),
    ('Motorola'), ('Apple'), ('Xiaomi'), ('Huawei'), ('Nokia'),
    ('HP'), ('Dell'), ('Lenovo'), ('Asus'), ('Acer'),
    ('Whirlpool'), ('BGH'), ('Noblex'), ('TCL'), ('Hyundai');

-- CATEGORÍAS (10 categorías)
INSERT INTO Categorias (descripcion) VALUES 
    ('Electrónica'), ('Hogar'), ('Computación'), ('Telefonía'),
    ('Audio'), ('Climatización'), ('Cocina'), ('Gaming'),
    ('Accesorios'), ('Iluminación');

-- USUARIOS (5 usuarios)
INSERT INTO Usuarios (Nombre, UsuarioLogin, Contraseña, EsAdmin) VALUES
    ('Administrador', 'admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 1),
    ('Laura Martínez', 'lmartinez', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 0),
    ('Roberto Silva', 'rsilva', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 0),
    ('Ana Torres', 'atorres', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 0),
    ('Diego Ruiz', 'druiz', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 1);

-- CLIENTES (30 clientes)
INSERT INTO Clientes (dni, nombre, email) VALUES 
    ('12345678', 'Juan Pérez', 'juan.perez@email.com'),
    ('87654321', 'María González', 'maria.gonzalez@email.com'),
    ('11223344', 'Carlos López', 'carlos.lopez@email.com'),
    ('22334455', 'Ana Rodríguez', 'ana.rodriguez@email.com'),
    ('33445566', 'Luis Fernández', 'luis.fernandez@email.com'),
    ('44556677', 'Sofía Martínez', 'sofia.martinez@email.com'),
    ('55667788', 'Miguel Sánchez', 'miguel.sanchez@email.com'),
    ('66778899', 'Laura Díaz', 'laura.diaz@email.com'),
    ('77889900', 'Pedro Ramírez', 'pedro.ramirez@email.com'),
    ('88990011', 'Carmen Torres', 'carmen.torres@email.com'),
    ('99001122', 'Jorge Flores', 'jorge.flores@email.com'),
    ('10111213', 'Lucía Morales', 'lucia.morales@email.com'),
    ('20212223', 'Roberto Castro', 'roberto.castro@email.com'),
    ('30313233', 'Daniela Ortiz', 'daniela.ortiz@email.com'),
    ('40414243', 'Fernando Ruiz', 'fernando.ruiz@email.com'),
    ('50515253', 'Patricia Jiménez', 'patricia.jimenez@email.com'),
    ('60616263', 'Andrés Vargas', 'andres.vargas@email.com'),
    ('70717273', 'Gabriela Herrera', 'gabriela.herrera@email.com'),
    ('80818283', 'Ricardo Mendoza', 'ricardo.mendoza@email.com'),
    ('90919293', 'Valeria Silva', 'valeria.silva@email.com'),
    ('11121314', 'Martín Rojas', 'martin.rojas@email.com'),
    ('21222324', 'Carolina Medina', 'carolina.medina@email.com'),
    ('31323334', 'Javier Navarro', 'javier.navarro@email.com'),
    ('41424344', 'Natalia Campos', 'natalia.campos@email.com'),
    ('51525354', 'Gustavo Reyes', 'gustavo.reyes@email.com'),
    ('61626364', 'Mónica Gutiérrez', 'monica.gutierrez@email.com'),
    ('71727374', 'Sebastián Vega', 'sebastian.vega@email.com'),
    ('81828384', 'Claudia Romero', 'claudia.romero@email.com'),
    ('91929394', 'Eduardo Benítez', 'eduardo.benitez@email.com'),
    ('12131415', 'Silvia Acosta', 'silvia.acosta@email.com');

-- PROVEEDORES (8 proveedores)
INSERT INTO Proveedores (cuit, nombre, Telefono, email, Direccion) VALUES 
    ('20123456789', 'Distribuidora Tech SRL', '011-4567-8900', 'ventas@techsrl.com', 'Av. Corrientes 1234'),
    ('20987654321', 'ElectroMundo SA', '011-1234-5678', 'info@electromundo.com', 'Av. Rivadavia 5678'),
    ('20111222333', 'Importadora Global', '011-2345-6789', 'compras@global.com', 'Av. Santa Fe 2345'),
    ('20444555666', 'TecnoPartes SRL', '011-3456-7890', 'ventas@tecnopartes.com', 'Av. Callao 3456'),
    ('20777888999', 'Mayorista Digital', '011-4567-8901', 'contacto@mayorista.com', 'Av. Cabildo 4567'),
    ('20101010101', 'Electro Sur SA', '011-5678-9012', 'info@electrosur.com', 'Av. Belgrano 5678'),
    ('20202020202', 'Proveedor Cinco', '011-6789-0123', 'ventas@cinco.com', 'Av. Córdoba 6789'),
    ('20303030303', 'Tech Solutions', '011-7890-1234', 'info@techsol.com', 'Av. Pueyrredón 7890');

-- PRODUCTOS (60 productos variados)
INSERT INTO Productos (nombre, UltimoPrecioCompra, porcentajeGanancia, stockActual, stockMinimo, descripcion, idMarca, idCategoria) VALUES 
    ('TV 55 pulgadas', 85000.00, 25.00, 10, 3, 'Smart TV 4K', 1, 1),
    ('TV 65 pulgadas', 125000.00, 25.00, 5, 2, 'Smart TV 4K QLED', 1, 1),
    ('TV 43 pulgadas', 65000.00, 28.00, 15, 5, 'Full HD Smart', 2, 1),
    ('Heladera 300L', 120000.00, 30.00, 5, 2, 'Heladera no frost', 2, 2),
    ('Heladera 400L', 180000.00, 28.00, 3, 2, 'No frost inverter', 16, 2),
    ('Freezer vertical', 95000.00, 30.00, 8, 3, '200L no frost', 2, 2),
    ('Lavarropas 8kg', 110000.00, 32.00, 6, 2, 'Carga frontal', 1, 2),
    ('Lavarropas 6kg', 85000.00, 32.00, 10, 3, 'Carga superior', 16, 2),
    ('Microondas 25L', 35000.00, 35.00, 20, 5, 'Digital', 4, 7),
    ('Microondas 30L', 45000.00, 35.00, 15, 4, 'Grill digital', 5, 7),
    ('Notebook Core i5', 95000.00, 20.00, 8, 3, '8GB RAM, 256GB SSD', 11, 3),
    ('Notebook Core i7', 145000.00, 22.00, 5, 2, '16GB RAM, 512GB SSD', 12, 3),
    ('Notebook Ryzen 5', 115000.00, 20.00, 7, 3, '16GB RAM, 512GB SSD', 13, 3),
    ('PC Gamer Ryzen 7', 185000.00, 25.00, 4, 2, '16GB, RTX 3060', 14, 8),
    ('PC Oficina i3', 75000.00, 28.00, 12, 4, '8GB, 256GB SSD', 11, 3),
    ('Monitor 24" Full HD', 42000.00, 30.00, 18, 6, 'IPS 75Hz', 1, 3),
    ('Monitor 27" QHD', 68000.00, 28.00, 10, 3, 'IPS 144Hz', 2, 3),
    ('Teclado mecánico', 18000.00, 40.00, 25, 8, 'RGB switches blue', 14, 9),
    ('Mouse gaming', 12000.00, 45.00, 30, 10, 'RGB 12000 DPI', 14, 9),
    ('Auriculares gamer', 15000.00, 42.00, 22, 7, 'RGB 7.1 surround', 3, 5),
    ('Celular Android', 45000.00, 35.00, 15, 5, 'Dual SIM, 128GB', 8, 4),
    ('Celular gama alta', 125000.00, 28.00, 8, 3, '256GB, 5G', 1, 4),
    ('Celular gama media', 75000.00, 32.00, 12, 4, '128GB, 4G', 6, 4),
    ('Tablet 10"', 55000.00, 30.00, 10, 3, 'Android 64GB', 8, 4),
    ('Smartwatch', 35000.00, 38.00, 18, 5, 'GPS, monitor cardíaco', 1, 9),
    ('Aire acondicionado 3000', 95000.00, 30.00, 6, 2, 'Frío/calor inverter', 17, 6),
    ('Aire acondicionado 2200', 75000.00, 32.00, 8, 3, 'Frío/calor', 18, 6),
    ('Ventilador de pie', 18000.00, 40.00, 25, 8, '20" 3 velocidades', 4, 6),
    ('Caloventor', 12000.00, 42.00, 20, 6, '2000W termostato', 5, 6),
    ('Parlante Bluetooth', 8500.00, 45.00, 35, 12, 'Portátil 20W', 3, 5),
    ('Barra de sonido', 35000.00, 35.00, 12, 4, '2.1 Bluetooth', 2, 5),
    ('Home theatre', 65000.00, 32.00, 8, 3, '5.1 1000W', 3, 5),
    ('Cafetera express', 45000.00, 35.00, 10, 3, '15 bar presión', 4, 7),
    ('Licuadora', 15000.00, 38.00, 22, 7, '1000W 5 velocidades', 5, 7),
    ('Procesadora', 28000.00, 36.00, 15, 5, '800W multifunción', 4, 7),
    ('Aspiradora robot', 85000.00, 30.00, 7, 2, 'Mapeo inteligente', 8, 2),
    ('Aspiradora vertical', 35000.00, 35.00, 14, 4, 'Ciclónica sin bolsa', 17, 2),
    ('Plancha a vapor', 12000.00, 40.00, 28, 9, 'Antiadherente 2200W', 5, 2),
    ('Batidora', 18000.00, 38.00, 18, 6, '500W 5 velocidades', 4, 7),
    ('Tostadora', 9500.00, 42.00, 25, 8, '2 ranuras 7 niveles', 5, 7),
    ('Pava eléctrica', 7500.00, 45.00, 30, 10, '1.7L 2200W', 4, 7),
    ('Multiprocesadora', 38000.00, 34.00, 12, 4, '1200W accesorios', 5, 7),
    ('Router Wi-Fi 6', 22000.00, 38.00, 20, 6, 'Dual band gigabit', 19, 3),
    ('Webcam Full HD', 15000.00, 40.00, 25, 8, '1080p 60fps', 2, 9),
    ('Impresora multifunción', 48000.00, 32.00, 10, 3, 'WiFi sistema continuo', 11, 3),
    ('Disco SSD 480GB', 18000.00, 35.00, 30, 10, 'SATA III 550MB/s', 1, 3),
    ('Disco HDD 1TB', 15000.00, 38.00, 25, 8, '7200rpm 64MB cache', 20, 3),
    ('Memoria RAM 8GB', 12000.00, 40.00, 35, 12, 'DDR4 3200MHz', 1, 3),
    ('Memoria RAM 16GB', 22000.00, 38.00, 28, 9, 'DDR4 3200MHz', 1, 3),
    ('Fuente PC 600W', 18000.00, 38.00, 20, 6, '80+ Bronze modular', 19, 3),
    ('Gabinete PC RGB', 25000.00, 36.00, 15, 5, 'ATX vidrio templado', 19, 3),
    ('Silla gamer', 45000.00, 35.00, 12, 4, 'Ergonómica reclinable', 19, 8),
    ('Webcam 4K', 28000.00, 36.00, 15, 5, 'HDR autoenfoque', 2, 9),
    ('Micrófono USB', 22000.00, 38.00, 18, 6, 'Condensador streaming', 3, 5),
    ('Lampara LED escritorio', 8500.00, 42.00, 30, 10, 'Regulable USB', 4, 10),
    ('Tira LED RGB', 6500.00, 45.00, 40, 12, '5m control remoto', 5, 10),
    ('Mouse pad XL', 4500.00, 50.00, 45, 15, '90x40cm gaming', 19, 9),
    ('Cable HDMI 2.1', 3500.00, 55.00, 50, 18, '2m 4K 120Hz', 4, 9),
    ('Cargador inalámbrico', 8500.00, 45.00, 35, 12, '15W fast charge', 1, 9),
    ('Soporte monitor dual', 15000.00, 40.00, 20, 6, 'Regulable 13-32"', 19, 9);

-- PRODUCTO_PROVEEDOR (asignación múltiple)
INSERT INTO Producto_Proveedor (idProducto, idProveedor) VALUES 
    (1, 1), (1, 2), (2, 1), (3, 2), (4, 2), (4, 3), (5, 3), (6, 2),
    (7, 2), (7, 3), (8, 3), (9, 4), (10, 4), (11, 1), (11, 5), (12, 1),
    (13, 5), (14, 5), (15, 1), (16, 1), (16, 2), (17, 2), (18, 5), (19, 5),
    (20, 4), (21, 1), (21, 6), (22, 1), (23, 6), (24, 6), (25, 1), (26, 3),
    (27, 3), (28, 4), (29, 4), (30, 4), (31, 4), (32, 4), (33, 4), (34, 4),
    (35, 4), (36, 6), (37, 3), (38, 4), (39, 4), (40, 4), (41, 4), (42, 4),
    (43, 7), (44, 7), (45, 7), (46, 5), (47, 5), (48, 5), (49, 5), (50, 5),
    (51, 5), (52, 8), (53, 7), (54, 7), (55, 7), (56, 8), (57, 8), (58, 7),
    (59, 8), (60, 8);

-- COMPRAS (40 compras distribuidas en los últimos 6 meses)
INSERT INTO Compras (idProveedor, idUsuario, fechaCompra, total, estado) VALUES 
    (1, 1, '2025-08-05', 540000.00, 'Finalizada'),
    (2, 1, '2025-08-12', 420000.00, 'Finalizada'),
    (3, 2, '2025-08-20', 385000.00, 'Finalizada'),
    (1, 2, '2025-09-03', 625000.00, 'Finalizada'),
    (4, 3, '2025-09-10', 295000.00, 'Finalizada'),
    (5, 1, '2025-09-18', 480000.00, 'Finalizada'),
    (2, 3, '2025-09-25', 350000.00, 'Finalizada'),
    (6, 2, '2025-10-02', 410000.00, 'Finalizada'),
    (3, 1, '2025-10-09', 525000.00, 'Finalizada'),
    (1, 3, '2025-10-16', 680000.00, 'Finalizada'),
    (7, 2, '2025-10-23', 245000.00, 'Finalizada'),
    (4, 1, '2025-10-30', 315000.00, 'Finalizada'),
    (5, 3, '2025-11-06', 580000.00, 'Finalizada'),
    (2, 2, '2025-11-13', 395000.00, 'Finalizada'),
    (8, 1, '2025-11-20', 465000.00, 'Finalizada'),
    (1, 3, '2025-11-27', 720000.00, 'Finalizada'),
    (3, 2, '2025-12-04', 385000.00, 'Finalizada'),
    (6, 1, '2025-12-11', 510000.00, 'Finalizada'),
    (4, 3, '2025-12-18', 275000.00, 'Finalizada'),
    (2, 2, '2025-12-26', 445000.00, 'Finalizada'),
    (5, 1, '2026-01-08', 625000.00, 'Finalizada'),
    (1, 3, '2026-01-15', 580000.00, 'Finalizada'),
    (7, 2, '2026-01-22', 320000.00, 'Finalizada'),
    (3, 1, '2026-01-29', 495000.00, 'Finalizada'),
    (8, 3, '2026-02-05', 410000.00, 'Finalizada'),
    (2, 2, '2026-02-12', 535000.00, 'Finalizada'),
    (6, 1, '2026-02-19', 385000.00, 'Finalizada'),
    (4, 3, '2026-02-26', 295000.00, 'Finalizada'),
    (1, 2, '2026-03-04', 670000.00, 'Finalizada'),
    (5, 1, '2026-03-11', 525000.00, 'Finalizada'),
    (3, 3, '2026-03-18', 445000.00, 'Finalizada'),
    (2, 2, '2026-03-25', 380000.00, 'Finalizada'),
    (7, 1, '2026-04-01', 315000.00, 'Finalizada'),
    (8, 3, '2026-04-08', 485000.00, 'Finalizada'),
    (1, 2, '2026-04-15', 595000.00, 'Finalizada'),
    (4, 1, '2026-04-22', 340000.00, 'Finalizada'),
    (6, 3, '2026-04-29', 425000.00, 'Finalizada'),
    (2, 2, '2026-05-06', 510000.00, 'Finalizada'),
    (5, 1, '2026-05-13', 465000.00, 'Pendiente'),
    (3, 3, '2026-05-20', 385000.00, 'Pendiente');

-- DETALLE COMPRAS (múltiples productos por compra)
INSERT INTO DetalleCompras (idCompra, idProducto, cantidad, precioUnitario) VALUES 
    (1, 1, 5, 85000.00), (1, 11, 3, 95000.00), (1, 16, 4, 42000.00),
    (2, 4, 3, 120000.00), (2, 7, 2, 110000.00),
    (3, 21, 4, 45000.00), (3, 22, 2, 125000.00), (3, 24, 3, 55000.00),
    (4, 2, 3, 125000.00), (4, 12, 2, 145000.00), (4, 14, 1, 185000.00),
    (5, 9, 8, 35000.00), (5, 10, 5, 45000.00),
    (6, 11, 4, 95000.00), (6, 13, 3, 115000.00), (6, 46, 8, 18000.00),
    (7, 4, 2, 120000.00), (7, 6, 3, 95000.00),
    (8, 21, 5, 45000.00), (8, 23, 4, 75000.00), (8, 24, 3, 55000.00),
    (9, 5, 2, 180000.00), (9, 26, 3, 95000.00),
    (10, 1, 6, 85000.00), (10, 2, 2, 125000.00), (10, 3, 4, 65000.00),
    (11, 43, 6, 22000.00), (11, 44, 8, 15000.00), (11, 45, 4, 48000.00),
    (12, 9, 7, 35000.00), (12, 33, 5, 45000.00), (12, 34, 4, 15000.00),
    (13, 11, 5, 95000.00), (13, 12, 2, 145000.00), (13, 46, 10, 18000.00),
    (14, 7, 3, 110000.00), (14, 8, 4, 85000.00),
    (15, 22, 2, 125000.00), (15, 23, 3, 75000.00), (15, 25, 4, 35000.00),
    (16, 1, 7, 85000.00), (16, 16, 6, 42000.00), (16, 17, 3, 68000.00),
    (17, 4, 2, 120000.00), (17, 26, 3, 95000.00),
    (18, 21, 6, 45000.00), (18, 36, 2, 85000.00), (18, 37, 4, 35000.00),
    (19, 28, 10, 18000.00), (19, 29, 8, 12000.00), (19, 40, 12, 7500.00),
    (20, 11, 3, 95000.00), (20, 13, 2, 115000.00), (20, 45, 4, 48000.00),
    (21, 2, 3, 125000.00), (21, 14, 2, 185000.00), (21, 52, 4, 45000.00),
    (22, 1, 5, 85000.00), (22, 11, 4, 95000.00), (22, 46, 8, 18000.00),
    (23, 43, 5, 22000.00), (23, 50, 6, 18000.00), (23, 56, 15, 4500.00),
    (24, 4, 3, 120000.00), (24, 5, 2, 180000.00), (24, 36, 2, 85000.00),
    (25, 21, 5, 45000.00), (25, 22, 2, 125000.00), (25, 59, 10, 8500.00),
    (26, 7, 4, 110000.00), (26, 26, 3, 95000.00), (26, 27, 4, 75000.00),
    (27, 23, 3, 75000.00), (27, 24, 4, 55000.00), (27, 25, 5, 35000.00),
    (28, 9, 8, 35000.00), (28, 38, 10, 12000.00), (28, 39, 6, 18000.00),
    (29, 1, 6, 85000.00), (29, 2, 2, 125000.00), (29, 16, 8, 42000.00),
    (30, 11, 4, 95000.00), (30, 12, 2, 145000.00), (30, 13, 3, 115000.00),
    (31, 4, 3, 120000.00), (31, 6, 2, 95000.00), (31, 37, 5, 35000.00),
    (32, 21, 4, 45000.00), (32, 23, 3, 75000.00), (32, 45, 3, 48000.00),
    (33, 43, 6, 22000.00), (33, 54, 8, 8500.00), (33, 55, 12, 6500.00),
    (34, 14, 2, 185000.00), (34, 52, 3, 45000.00), (34, 53, 4, 28000.00),
    (35, 1, 5, 85000.00), (35, 11, 4, 95000.00), (35, 46, 10, 18000.00),
    (36, 28, 8, 18000.00), (36, 29, 10, 12000.00), (36, 30, 12, 8500.00),
    (37, 21, 5, 45000.00), (37, 24, 4, 55000.00), (37, 36, 2, 85000.00),
    (38, 4, 3, 120000.00), (38, 7, 2, 110000.00), (38, 26, 3, 95000.00),
    (39, 11, 3, 95000.00), (39, 13, 2, 115000.00), (39, 46, 8, 18000.00),
    (40, 5, 2, 180000.00), (40, 27, 3, 75000.00);

-- VENTAS (80 ventas distribuidas en los últimos 5 meses)
INSERT INTO Ventas (idCliente, idUsuario, fechaVenta, total, numeroFactura) VALUES 
    (1, 2, '2025-09-01', 106250.00, 1001), (2, 3, '2025-09-02', 156000.00, 1002),
    (3, 2, '2025-09-03', 114000.00, 1003), (4, 4, '2025-09-05', 85200.00, 1004),
    (5, 2, '2025-09-07', 234000.00, 1005), (6, 3, '2025-09-09', 52500.00, 1006),
    (7, 4, '2025-09-11', 156000.00, 1007), (8, 2, '2025-09-13', 168750.00, 1008),
    (9, 3, '2025-09-15', 91200.00, 1009), (10, 4, '2025-09-17', 175500.00, 1010),
    (11, 2, '2025-09-19', 138000.00, 1011), (12, 3, '2025-09-21', 60750.00, 1012),
    (13, 4, '2025-09-23', 114000.00, 1013), (14, 2, '2025-09-25', 202800.00, 1014),
    (15, 3, '2025-09-27', 98400.00, 1015), (16, 4, '2025-09-29', 127500.00, 1016),
    (17, 2, '2025-10-01', 175500.00, 1017), (18, 3, '2025-10-03', 83200.00, 1018),
    (19, 4, '2025-10-05', 156000.00, 1019), (20, 2, '2025-10-07', 201600.00, 1020),
    (21, 3, '2025-10-09', 91200.00, 1021), (22, 4, '2025-10-11', 138000.00, 1022),
    (23, 2, '2025-10-13', 168750.00, 1023), (24, 3, '2025-10-15', 54600.00, 1024),
    (25, 4, '2025-10-17', 106250.00, 1025), (26, 2, '2025-10-19', 234000.00, 1026),
    (27, 3, '2025-10-21', 127500.00, 1027), (28, 4, '2025-10-23', 175500.00, 1028),
    (29, 2, '2025-10-25', 98400.00, 1029), (30, 3, '2025-10-27', 156000.00, 1030),
    (1, 4, '2025-10-29', 83200.00, 1031), (2, 2, '2025-10-31', 114000.00, 1032),
    (3, 3, '2025-11-02', 202800.00, 1033), (4, 4, '2025-11-04', 91200.00, 1034),
    (5, 2, '2025-11-06', 138000.00, 1035), (6, 3, '2025-11-08', 168750.00, 1036),
    (7, 4, '2025-11-10', 60750.00, 1037), (8, 2, '2025-11-12', 156000.00, 1038),
    (9, 3, '2025-11-14', 175500.00, 1039), (10, 4, '2025-11-16', 106250.00, 1040),
    (11, 2, '2025-11-18', 234000.00, 1041), (12, 3, '2025-11-20', 127500.00, 1042),
    (13, 4, '2025-11-22', 98400.00, 1043), (14, 2, '2025-11-24', 156000.00, 1044),
    (15, 3, '2025-11-26', 83200.00, 1045), (16, 4, '2025-11-28', 114000.00, 1046),
    (17, 2, '2025-11-30', 202800.00, 1047), (18, 3, '2025-12-02', 91200.00, 1048),
    (19, 4, '2025-12-04', 138000.00, 1049), (20, 2, '2025-12-06', 168750.00, 1050),
    (21, 3, '2025-12-08', 175500.00, 1051), (22, 4, '2025-12-10', 106250.00, 1052),
    (23, 2, '2025-12-12', 234000.00, 1053), (24, 3, '2025-12-14', 127500.00, 1054),
    (25, 4, '2025-12-16', 98400.00, 1055), (26, 2, '2025-12-18', 156000.00, 1056),
    (27, 3, '2025-12-20', 83200.00, 1057), (28, 4, '2025-12-22', 114000.00, 1058),
    (29, 2, '2025-12-24', 202800.00, 1059), (30, 3, '2025-12-26', 91200.00, 1060),
    (1, 4, '2026-01-02', 138000.00, 1061), (2, 2, '2026-01-04', 168750.00, 1062),
    (3, 3, '2026-01-06', 175500.00, 1063), (4, 4, '2026-01-08', 106250.00, 1064),
    (5, 2, '2026-01-10', 234000.00, 1065), (6, 3, '2026-01-12', 127500.00, 1066),
    (7, 4, '2026-01-14', 98400.00, 1067), (8, 2, '2026-01-16', 156000.00, 1068),
    (9, 3, '2026-01-18', 83200.00, 1069), (10, 4, '2026-01-20', 114000.00, 1070),
    (11, 2, '2026-01-22', 202800.00, 1071), (12, 3, '2026-01-24', 91200.00, 1072),
    (13, 4, '2026-01-26', 138000.00, 1073), (14, 2, '2026-01-28', 168750.00, 1074),
    (15, 3, '2026-01-30', 175500.00, 1075), (16, 4, '2026-02-01', 106250.00, 1076),
    (17, 2, '2026-02-03', 234000.00, 1077), (18, 3, '2026-02-05', 127500.00, 1078),
    (19, 4, '2026-02-07', 98400.00, 1079), (20, 2, '2026-02-09', 156000.00, 1080);

-- DETALLE VENTAS (2-4 productos por venta)
INSERT INTO DetalleVentas (idVenta, idProducto, cantidad, precioUnitario) VALUES
    (1, 1, 1, 106250.00), (2, 4, 1, 156000.00), (3, 11, 1, 114000.00),
    (4, 21, 1, 60750.00), (4, 18, 1, 24400.00), (5, 2, 1, 156250.00),
    (5, 12, 1, 77750.00), (6, 9, 1, 47250.00), (6, 40, 1, 5250.00),
    (7, 4, 1, 156000.00), (8, 7, 1, 146200.00), (8, 19, 1, 22550.00),
    (9, 11, 1, 114000.00), (10, 22, 1, 175500.00), (11, 13, 1, 138000.00),
    (12, 21, 1, 60750.00), (13, 11, 1, 114000.00), (14, 5, 1, 234000.00),
    (15, 3, 1, 82000.00), (15, 28, 1, 16400.00), (16, 26, 1, 123500.00),
    (16, 40, 1, 4000.00), (17, 22, 1, 175500.00), (18, 23, 1, 96000.00),
    (18, 30, 1, -12800.00), (19, 4, 1, 156000.00), (20, 1, 1, 106250.00),
    (20, 16, 2, 47750.00), (21, 11, 1, 114000.00), (21, 46, 2, -22800.00),
    (22, 7, 1, 146200.00), (22, 29, 1, -8200.00), (23, 2, 1, 156250.00),
    (23, 19, 1, 12550.00), (24, 9, 1, 47250.00), (24, 33, 1, 7350.00),
    (25, 1, 1, 106250.00), (26, 5, 1, 234000.00), (27, 26, 1, 123500.00),
    (27, 40, 1, 4000.00), (28, 22, 1, 175500.00), (29, 3, 1, 82000.00),
    (29, 28, 1, 16400.00), (30, 4, 1, 156000.00), (31, 21, 1, 60750.00),
    (31, 18, 1, 22450.00), (32, 11, 1, 114000.00), (33, 2, 1, 156250.00),
    (33, 46, 2, 46550.00), (34, 9, 1, 47250.00), (34, 34, 2, 43950.00),
    (35, 7, 1, 146200.00), (35, 38, 1, -8200.00), (36, 22, 1, 175500.00),
    (36, 25, 1, -6750.00), (37, 21, 1, 60750.00), (38, 4, 1, 156000.00),
    (39, 1, 1, 106250.00), (39, 16, 1, 69250.00), (40, 11, 1, 114000.00),
    (40, 46, 2, -7750.00), (41, 2, 1, 156250.00), (41, 12, 1, 77750.00),
    (42, 26, 1, 123500.00), (42, 40, 1, 4000.00), (43, 3, 1, 82000.00),
    (43, 28, 1, 16400.00), (44, 4, 1, 156000.00), (45, 23, 1, 96000.00),
    (45, 30, 1, -12800.00), (46, 11, 1, 114000.00), (47, 5, 1, 234000.00),
    (47, 37, 1, -31200.00), (48, 9, 1, 47250.00), (48, 33, 1, 43950.00),
    (49, 7, 1, 146200.00), (49, 38, 1, -8200.00), (50, 22, 1, 175500.00),
    (50, 25, 1, -6750.00), (51, 1, 1, 106250.00), (51, 16, 1, 69250.00),
    (52, 21, 1, 60750.00), (52, 59, 3, 45500.00), (53, 2, 1, 156250.00),
    (53, 12, 1, 77750.00), (54, 26, 1, 123500.00), (54, 40, 1, 4000.00),
    (55, 3, 1, 82000.00), (55, 28, 1, 16400.00), (56, 4, 1, 156000.00),
    (57, 23, 1, 96000.00), (57, 30, 1, -12800.00), (58, 11, 1, 114000.00),
    (59, 5, 1, 234000.00), (59, 37, 1, -31200.00), (60, 9, 1, 47250.00),
    (60, 34, 2, 43950.00), (61, 7, 1, 146200.00), (61, 38, 1, -8200.00),
    (62, 22, 1, 175500.00), (62, 25, 1, -6750.00), (63, 1, 1, 106250.00),
    (63, 16, 1, 69250.00), (64, 21, 1, 60750.00), (64, 18, 1, 45500.00),
    (65, 2, 1, 156250.00), (65, 12, 1, 77750.00), (66, 26, 1, 123500.00),
    (66, 40, 1, 4000.00), (67, 3, 1, 82000.00), (67, 28, 1, 16400.00),
    (68, 4, 1, 156000.00), (69, 23, 1, 96000.00), (69, 30, 1, -12800.00),
    (70, 11, 1, 114000.00), (71, 5, 1, 234000.00), (71, 37, 1, -31200.00),
    (72, 9, 1, 47250.00), (72, 33, 1, 43950.00), (73, 7, 1, 146200.00),
    (73, 38, 1, -8200.00), (74, 22, 1, 175500.00), (74, 25, 1, -6750.00),
    (75, 1, 1, 106250.00), (75, 16, 1, 69250.00), (76, 21, 1, 60750.00),
    (76, 59, 3, 45500.00), (77, 2, 1, 156250.00), (77, 12, 1, 77750.00),
    (78, 26, 1, 123500.00), (78, 40, 1, 4000.00), (79, 3, 1, 82000.00),
    (79, 28, 1, 16400.00), (80, 4, 1, 156000.00);
