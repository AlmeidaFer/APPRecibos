create database bdRecibos;
use bdRecibos;

create table users (
id int  not null PRIMARY KEY IDENTITY(1,1),
email VARCHAR(100)  not null,
pass VARCHAR(50)  not null,
name VARCHAR(max)  not null
)
GO;
create table proveedor(
id int not null PRIMARY KEY IDENTITY(1,1),
proveedor varchar(max)
)
GO;
create table moneda(
id int not null PRIMARY KEY IDENTITY(1,1),
moneda varchar(200),
clave varchar(10)
)
GO;
create table recibos(
id int not null PRIMARY KEY IDENTITY(1,1),
consecutivo int not null,
proveedorId int not null FOREIGN KEY  REFERENCES proveedor(id),
monto decimal(10,4) not NULL,
monedaId int not null FOREIGN KEY REFERENCES moneda(id),
fecha datetime not null,
commentario varchar(max)
)
GO;

create or alter procedure spUsers
@accion int  = 0,
@id int = 0
AS BEGIN
IF(@accion = 1)
    SELECT * FROM users WHERE id=@id
END
end
GO;

create or alter PROCEDURE spProveedor
@accion int = 0,
@id int  = 0
as BEGIN
if(@accion=1)
    SELECT 
END



insert into users values ('admin@hotmail.com','1234','admin')