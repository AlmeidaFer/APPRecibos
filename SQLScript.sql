create database bdRecibos;
use bdRecibos;

create table users (
id int  not null PRIMARY KEY IDENTITY(1,1),
email VARCHAR(100)  not null,
pass VARCHAR(50)  not null,
name VARCHAR(max)  not null
)

create table proveedores(
id int not null PRIMARY KEY IDENTITY(1,1),
proveedor varchar(max)
)

create table monedas(
id int not null PRIMARY KEY IDENTITY(1,1),
moneda varchar(200),
clave varchar(10)
)

create table recibos(
id int not null PRIMARY KEY IDENTITY(1,1),
consecutivo int not null,
proveedorId int not null FOREIGN KEY  REFERENCES proveedores(id),
monto decimal(10,4) not NULL,
monedaId int not null FOREIGN KEY REFERENCES monedas(id),
fecha datetime not null,
comentario varchar(max)
)


insert into users values ('admin@hotmail.com','1234','admin')
insert into proveedores values ('proveedor 1')
insert into proveedores values ('proveedor 2')
insert into proveedores values ('proveedor 3')
insert into monedas values ('Peso Mexicano','MXN')
insert into monedas values ('Dolar','USA')
insert into monedas values ('Pero Argentino','ARS')

select * from users
select * from proveedores
select * from monedas
select * from recibos

-- create or alter procedure spListRecibos
-- as BEGIN
--     SELECT r.*,p.proveedor,m.clave as moneda 
--     FROM recibos r,proveedores p , monedas m
--     where r.proveedorId=p.id AND
--     r.monedaId=m.id
-- end
