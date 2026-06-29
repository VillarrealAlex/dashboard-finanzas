# Dashboard Finanzas

Sistema de gestión financiera con panel de control (dashboard) para visualizar ingresos, egresos, reportes y flujo de caja.

---

## Tecnologías

| Capa | Tecnología | Versión |
|------|-----------|---------|
| Backend | .NET (ASP.NET Core Web API) | 8.0 |
| Autenticación | JWT (JSON Web Tokens) | — |
| Gestión de usuarios | ASP.NET Core Identity | 8.0 |
| Base de datos (dev) | Entity Framework Core InMemory | 8.0 |
| Documentación API | Swagger / OpenAPI | — |
| Frontend | Por definir | — |

---

## Qué hace

- **Autenticación segura** con tokens JWT y manejo de sesiones sin estado (stateless)
- **Sistema de roles** (Admin, Contador, User) para controlar acceso a endpoints
- **Registro y login** de usuarios con validación de contraseña
- **Endpoints protegidos** que devuelven datos financieros según el rol del usuario
- **Swagger UI** para explorar y probar la API directamente desde el navegador

---

## Estructura del proyecto

```
DashboardFinanzas/
├── DashboardFinanzas.sln
├── DashboardFinanzas.API/           # Backend API
│   ├── Controllers/                 # Endpoints (Auth, Dashboard)
│   ├── Data/                        # DbContext
│   ├── Models/                      # Entidades y DTOs
│   ├── Services/                    # Lógica de negocio (JWT)
│   ├── Program.cs                   # Configuración y arranque
│   ├── appsettings.json             # Configuración (JWT, etc.)
│   └── DashboardFinanzas.API.csproj
└── DashboardFinanzasFront/          # Frontend (futuro)
```

---

## Requisitos previos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) o superior
- Un editor de código (Visual Studio, VS Code, Rider)

---

## Cómo iniciar el proyecto

### 1. Clonar el repositorio

```bash
git clone <url-del-repositorio>
cd DashboardFinanzas
```

### 2. Restaurar dependencias

```bash
dotnet restore
```

### 3. Compilar

```bash
dotnet build
```

### 4. Ejecutar la API

```bash
dotnet run --project DashboardFinanzas.API/DashboardFinanzas.API.csproj
```

La API se levanta en:
- `https://localhost:7xxx` (HTTPS)
- `http://localhost:5xxx` (HTTP)

> Los puertos exactos están en `DashboardFinanzas.API/Properties/launchSettings.json`

### 5. Abrir Swagger

Una vez ejecutando, navega a:

```
https://localhost:{puerto}/swagger
```

---

## Cómo probar la API

### Registrar un usuario

```http
POST /api/auth/register
Content-Type: application/json

{
  "firstName": "Juan",
  "lastName": "Pérez",
  "email": "juan@finanzas.com",
  "password": "MiClave123",
  "role": "Admin"
}
```

### Iniciar sesión

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "juan@finanzas.com",
  "password": "MiClave123"
}
```

La respuesta incluye el token JWT. Úsalo en las peticiones protegidas:

```
Authorization: Bearer <tu-token>
```

### Endpoints disponibles

| Método | Ruta | Acceso | Descripción |
|--------|------|--------|-------------|
| POST | `/api/auth/register` | Público | Registrar usuario |
| POST | `/api/auth/login` | Público | Obtener token JWT |
| GET | `/api/dashboard/summary` | Autenticado | Resumen financiero |
| GET | `/api/dashboard/admin/reports` | Admin | Reportes administrativos |
| GET | `/api/dashboard/finance/details` | Admin, Contador | Detalle financiero |

---

## Roles del sistema

| Rol | Permisos |
|-----|----------|
| Admin | Acceso total a todos los endpoints |
| Contador | Acceso a endpoints financieros |
| User | Acceso solo al resumen general |

---

## Notas

- La base de datos es **InMemory** (en memoria) para desarrollo. Los datos se pierden al reiniciar la aplicación.
- Para producción, configurar SQL Server o PostgreSQL en `Program.cs`.
- La clave JWT en `appsettings.json` es solo para desarrollo. En producción usar variables de entorno o un vault de secretos.
