# BackendAPI - Sistema de Gestión de Reservas

## Descripción

Este proyecto corresponde al backend de un sistema multiplataforma de gestión de reservas.  
Fue desarrollado con ASP.NET Core Web API y utiliza arquitectura cliente-servidor.

La API permite gestionar usuarios, clientes, servicios, reservas y pagos.  
Además, implementa autenticación mediante JWT para proteger los endpoints principales.

## Tecnologías utilizadas

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger
- Git
- GitHub

## Funcionalidades principales

- Registro de usuarios
- Inicio de sesión
- Generación de token JWT
- Protección de endpoints con autorización
- CRUD de servicios
- CRUD de clientes
- CRUD de reservas
- CRUD de pagos
- Base de datos relacional usando Entity Framework Core

## Modelo de datos

El sistema cuenta con las siguientes entidades:

- Roles
- Usuarios
- Clientes
- Servicios
- Reservas
- Pagos

## Relaciones principales

- Un rol puede pertenecer a muchos usuarios.
- Un cliente puede tener muchas reservas.
- Un servicio puede estar asociado a muchas reservas.
- Una reserva puede tener un pago.

## Endpoints principales

### Autenticación

| Método | Endpoint | Descripción |
|---|---|---|
| POST | /api/Auth/register | Registrar usuario |
| POST | /api/Auth/login | Iniciar sesión y obtener token JWT |

### Servicios

| Método | Endpoint | Descripción |
|---|---|---|
| GET | /api/Servicios | Listar servicios |
| GET | /api/Servicios/{id} | Obtener servicio por ID |
| POST | /api/Servicios | Crear servicio |
| PUT | /api/Servicios/{id} | Actualizar servicio |
| DELETE | /api/Servicios/{id} | Eliminar servicio |

### Clientes

| Método | Endpoint | Descripción |
|---|---|---|
| GET | /api/Clientes | Listar clientes |
| GET | /api/Clientes/{id} | Obtener cliente por ID |
| POST | /api/Clientes | Crear cliente |
| PUT | /api/Clientes/{id} | Actualizar cliente |
| DELETE | /api/Clientes/{id} | Eliminar cliente |

### Reservas

| Método | Endpoint | Descripción |
|---|---|---|
| GET | /api/Reservas | Listar reservas |
| GET | /api/Reservas/{id} | Obtener reserva por ID |
| POST | /api/Reservas | Crear reserva |
| PUT | /api/Reservas/{id} | Actualizar reserva |
| DELETE | /api/Reservas/{id} | Eliminar reserva |

### Pagos

| Método | Endpoint | Descripción |
|---|---|---|
| GET | /api/Pagos | Listar pagos |
| GET | /api/Pagos/{id} | Obtener pago por ID |
| POST | /api/Pagos | Crear pago |
| PUT | /api/Pagos/{id} | Actualizar pago |
| DELETE | /api/Pagos/{id} | Eliminar pago |

## Seguridad

La API utiliza autenticación JWT.

Flujo de autenticación:

1. El usuario se registra en `/api/Auth/register`.
2. El usuario inicia sesión en `/api/Auth/login`.
3. La API devuelve un token JWT.
4. El token se usa en Swagger mediante el botón `Authorize`.
5. Los endpoints protegidos solo funcionan con token válido.

## Cómo ejecutar el proyecto

1. Clonar el repositorio.
2. Abrir la solución en Visual Studio.
3. Verificar la cadena de conexión en `appsettings.json`.
4. Abrir la Consola del Administrador de paquetes.
5. Ejecutar:

```powershell
Update-Database