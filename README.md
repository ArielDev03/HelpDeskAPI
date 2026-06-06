# HelpDesk API

API REST desarrollada con ASP.NET Core Web API para la gestión de tickets de soporte técnico. El proyecto está diseñado siguiendo buenas prácticas de arquitectura backend, utilizando Entity Framework Core para el acceso a datos y SQL Server como motor de base de datos.

## Características

### Gestión de Usuarios

* Crear usuarios.
* Obtener listado de usuarios.
* Obtener usuario por ID.
* Actualizar información de usuarios.
* Eliminar usuarios.
* Validación de correos duplicados.

### Gestión de Tickets

* Creación de tickets de soporte.
* Asociación de tickets con usuarios.
* Catálogo de estados de ticket.
* Catálogo de prioridades de ticket.
* Soporte para asignación de tickets a usuarios.

### Gestión de Comentarios

* Agregar comentarios a tickets.
* Relación entre tickets y comentarios.
* Registro de fecha de creación.

### Base de Datos

* Entity Framework Core.
* SQL Server.
* Migraciones para control de versiones de la base de datos.
* Seed Data para catálogos iniciales.

---

# Tecnologías Utilizadas

* ASP.NET Core Web API
* Entity Framework Core
* Entity Framework Core SQL Server
* Entity Framework Core Tools
* SQL Server
* Swagger / Swashbuckle
* C#

---

# Requisitos Previos

Antes de ejecutar el proyecto asegúrate de tener instalado:

* .NET 8 SDK
* SQL Server
* Entity Framework Core Tools

Verificar instalación de .NET:

```bash
dotnet --version
```

Instalar EF Tools (si no está instalado):

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

# Configuración de Base de Datos

Configurar la cadena de conexión en:

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

# Restaurar Dependencias

```bash
dotnet restore
```

---

# Aplicar Migraciones

Crear la base de datos y aplicar las migraciones existentes:

```bash
dotnet ef database update
```

Este comando:

* Crea la base de datos si no existe.
* Crea las tablas necesarias.
* Inserta los datos iniciales configurados mediante Seed Data.

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

Swagger se encuentra habilitado para facilitar la exploración y prueba de los endpoints.

Una vez iniciada la aplicación, acceder a:

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
├── Mappers
├── Models
│   ├── Users
│   ├── Tickets
│   ├── TicketComments
│   ├── TicketsStatus
│   └── TicketsPriority
│
├── Services
├── Migrations
├── Program.cs
└── appsettings.json
```

---

# Catálogos Iniciales

## Estados de Ticket

* Abierto
* En Proceso
* Resuelto
* Cerrado

## Prioridades de Ticket

* Baja
* Media
* Alta
* Crítica

Estos registros se generan automáticamente mediante Seed Data al ejecutar las migraciones.

---

# Estado Actual del Proyecto

Actualmente el proyecto cuenta con:

* CRUD de usuarios.
* Modelado de tickets.
* Modelado de comentarios.
* Catálogos de estados y prioridades.
* Relaciones configuradas mediante Entity Framework Core.
* Migraciones y Seed Data.

## Próximas Mejoras

* Autenticación JWT.
* Roles y permisos.
* Asignación de tickets.
* Historial de cambios.
* Adjuntos de archivos.
* Auditoría de acciones.
* Notificaciones.
* Validaciones avanzadas.
* Pruebas unitarias e integración.

---

# Autor

Proyecto desarrollado con fines de aprendizaje y práctica de desarrollo backend utilizando ASP.NET Core y Entity Framework Core.
