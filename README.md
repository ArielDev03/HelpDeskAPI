# HelpDesk API

API REST desarrollada con ASP.NET Core Web API para la gestión de tickets de soporte técnico. El proyecto implementa una arquitectura por capas utilizando DTOs, servicios, validaciones, manejo global de excepciones y Entity Framework Core para el acceso a datos.

---

# Características

## Gestión de Usuarios

* Crear usuarios.
* Obtener listado de usuarios.
* Obtener usuario por ID.
* Actualizar información de usuarios.
* Eliminar usuarios.
* Validación de correos duplicados.

## Gestión de Tickets

* Crear tickets de soporte.
* Obtener listado de tickets.
* Obtener ticket por ID.
* Actualizar tickets.
* Asociación de tickets con usuarios.
* Asignación de tickets a usuarios.
* Gestión de estados y prioridades.

## Gestión de Comentarios

* Agregar comentarios a tickets.
* Obtener comentarios asociados a un ticket.
* Asociación de comentarios con usuarios.
* Registro automático de fecha de creación.

## Catálogos

### Estados de Ticket

* Abierto
* En Proceso
* Resuelto
* Cerrado

### Prioridades de Ticket

* Baja
* Media
* Alta
* Crítica

---

# Tecnologías Utilizadas

* ASP.NET Core Web API
* Entity Framework Core
* Entity Framework Core SQL Server
* Entity Framework Core Tools
* SQL Server
* Swagger / Swashbuckle
* FluentValidation
* C#

---

# Arquitectura Implementada

```text
Controllers
    ↓
Interfaces
    ↓
Services
    ↓
Entity Framework Core
    ↓
SQL Server
```

### Componentes utilizados

* Controllers
* DTOs
* Services
* Interfaces
* Mappers
* Middleware Global de Excepciones
* Logging
* Dependency Injection
* Entity Framework Core

---

# Validaciones

El proyecto utiliza FluentValidation para la validación de datos de entrada.

Validaciones implementadas:

* Campos obligatorios.
* Longitudes máximas.
* Formato válido de correo electrónico.
* Rangos numéricos.
* Validación de entidades relacionadas.
* Mensajes personalizados de validación.

FluentValidation se utiliza en lugar de Data Annotations para mantener las reglas de negocio separadas de los DTOs.

---

# Logging

Se implementa logging mediante ILogger para registrar:

* Operaciones realizadas.
* Creación de registros.
* Actualizaciones.
* Advertencias.
* Errores.
* Eventos importantes de la aplicación.

Los registros son visibles en la consola durante la ejecución de la API.

---

# Manejo Global de Excepciones

La API utiliza un Middleware Global de Excepciones para centralizar el manejo de errores.

Excepciones personalizadas implementadas:

* NotFoundException
* BusinessException

Respuestas HTTP manejadas:

* 400 Bad Request
* 404 Not Found
* 500 Internal Server Error

---

# Base de Datos

* SQL Server
* Entity Framework Core
* Migraciones
* Seed Data

Entidades principales:

* Users
* Tickets
* TicketComments
* TicketStatuses
* TicketPriorities

---

# Requisitos Previos

Antes de ejecutar el proyecto asegúrate de tener instalado:

* .NET 8 SDK
* SQL Server
* Entity Framework Core Tools

Verificar instalación:

```bash
dotnet --version
```

Instalar EF Tools:

```bash
dotnet tool install --global dotnet-ef
```

Verificar instalación:

```bash
dotnet ef
```

---

# Clonar el Proyecto

```bash
git clone https://github.com/ArielDev03/HelpDeskAPI.git
```

Entrar al proyecto:

```bash
cd HelpDeskAPI
```

---

# Restaurar Dependencias

```bash
dotnet restore
```

---

# Configuración de Base de Datos

Modificar la cadena de conexión en:

```json
appsettings.json
```

Ejemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=HelpDeskDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

---

# Aplicar Migraciones

```bash
dotnet ef database update
```

Este comando:

* Crea la base de datos si no existe.
* Crea las tablas necesarias.
* Inserta los datos iniciales mediante Seed Data.

---

# Ejecutar el Proyecto

```bash
dotnet run
```

O desde Visual Studio:

```text
F5
```

---

# Documentación de la API

Swagger se encuentra habilitado para la documentación y pruebas de los endpoints.

Acceder a:

```text
https://localhost:<PUERTO>/swagger
```

---

# Estructura General del Proyecto

```text
HelpDeskAPI
│
├── Controllers
├── DTOs
├── Data
├── Interfaces
├── Services
├── Validators
├── Mappers
├── Middleware
├── Models
│   ├── Users
│   ├── Tickets
│   ├── TicketComments
│   ├── TicketStatuses
│   └── TicketPriorities
│
├── Migrations
├── Program.cs
└── appsettings.json
```

---

# Estado Actual del Proyecto

Actualmente el proyecto cuenta con:

* CRUD completo de usuarios.
* Gestión de tickets.
* Gestión de comentarios de tickets.
* Catálogo de estados.
* Catálogo de prioridades.
* DTOs y Mappers.
* FluentValidation.
* Middleware Global de Excepciones.
* Logging.
* Dependency Injection.
* Entity Framework Core.
* Migraciones y Seed Data.

---

# Próximas Mejoras

* Repository Pattern.
* AutoMapper.
* Autenticación JWT.
* Roles y permisos.
* Refresh Tokens.
* Historial de cambios.
* Adjuntos de archivos.
* Auditoría de acciones.
* Notificaciones.
* Paginación.
* Filtros avanzados.
* Docker.
* Pruebas unitarias e integración.

---

# Autor

Proyecto desarrollado con fines de aprendizaje y práctica de desarrollo backend utilizando ASP.NET Core, Entity Framework Core y SQL Server.
