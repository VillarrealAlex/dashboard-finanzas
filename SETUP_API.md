# Dashboard Finanzas - Backend API (.NET 8)

## Comandos ejecutados para crear el proyecto

```bash
# 1. Crear solución
dotnet new sln -n DashboardFinanzas

# 2. Crear proyecto Web API
dotnet new webapi -n DashboardFinanzas.API -o DashboardFinanzas.API

# 3. Agregar proyecto a la solución
dotnet sln DashboardFinanzas.sln add DashboardFinanzas.API/DashboardFinanzas.API.csproj

# 4. Instalar paquetes NuGet
dotnet add DashboardFinanzas.API/DashboardFinanzas.API.csproj package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.0
dotnet add DashboardFinanzas.API/DashboardFinanzas.API.csproj package Microsoft.EntityFrameworkCore.InMemory --version 8.0.0
dotnet add DashboardFinanzas.API/DashboardFinanzas.API.csproj package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 8.0.0

# 5. Compilar
dotnet build DashboardFinanzas.sln

# 6. Ejecutar
dotnet run --project DashboardFinanzas.API/DashboardFinanzas.API.csproj
```

---

## Estructura de carpetas

```
DashboardFinanzas/
├── DashboardFinanzas.sln
├── SETUP_API.md
├── README.md
├── DashboardFinanzas.API/          # Backend
│   ├── Controllers/
│   │   ├── AuthController.cs        # Registro y login
│   │   └── DashboardController.cs   # Endpoints protegidos por roles
│   ├── Data/
│   │   └── ApplicationDbContext.cs   # DbContext con Identity
│   ├── Models/
│   │   ├── ApplicationUser.cs        # Usuario extendido de Identity
│   │   └── DTOs/
│   │       ├── AuthResponseDto.cs    # Respuesta de login (token + info)
│   │       ├── LoginDto.cs           # Datos para login
│   │       └── RegisterDto.cs        # Datos para registro
│   ├── Services/
│   │   ├── ITokenService.cs          # Interfaz del servicio JWT
│   │   └── TokenService.cs           # Implementación generación JWT
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── appsettings.json              # Configuración JWT
│   ├── appsettings.Development.json
│   ├── Program.cs                    # Configuración principal
│   └── DashboardFinanzas.API.csproj
└── DashboardFinanzasFront/          # Frontend (futuro)
```

---

## Lo que se implementó

### 1. Autenticación JWT
- Generación de tokens JWT con claims personalizados (id, email, nombre, roles)
- Validación de tokens con issuer, audience y signing key
- Expiración configurable (por defecto 24 horas)
- Configuración centralizada en `appsettings.json`

### 2. Sistema de roles con ASP.NET Identity
- Tres roles predefinidos que se crean al iniciar la app: `Admin`, `Contador`, `User`
- Registro de usuarios con asignación de rol
- Los roles se validan en los endpoints con `[Authorize(Roles = "...")]`

### 3. Endpoints disponibles

| Método | Ruta | Acceso | Descripción |
|--------|------|--------|-------------|
| POST | `/api/auth/register` | Público | Registrar nuevo usuario |
| POST | `/api/auth/login` | Público | Login y obtener token |
| GET | `/api/dashboard/summary` | Autenticado | Resumen financiero general |
| GET | `/api/dashboard/admin/reports` | Solo Admin | Reportes administrativos |
| GET | `/api/dashboard/finance/details` | Admin o Contador | Detalles financieros |

### 4. Swagger con soporte JWT
- Swagger UI disponible en desarrollo (`/swagger`)
- Configurado con botón "Authorize" para probar con token Bearer

### 5. CORS
- Permite peticiones desde `http://localhost:3000` y `http://localhost:5173` (frontends comunes)

### 6. Base de datos InMemory
- Usa EF Core InMemory para desarrollo rápido (los datos se pierden al reiniciar)
- Para producción, cambia a SQL Server o PostgreSQL

---

## Cómo probar

### 1. Ejecutar la API
```bash
dotnet run --project DashboardFinanzas.API/DashboardFinanzas.API.csproj
```

### 2. Abrir Swagger
Navegar a: `https://localhost:{puerto}/swagger`

### 3. Registrar un usuario Admin
```json
POST /api/auth/register
{
  "firstName": "Admin",
  "lastName": "Sistema",
  "email": "admin@finanzas.com",
  "password": "Admin123",
  "role": "Admin"
}
```

### 4. Hacer login
```json
POST /api/auth/login
{
  "email": "admin@finanzas.com",
  "password": "Admin123"
}
```

### 5. Usar el token
Copiar el token de la respuesta y usarlo en Swagger (botón Authorize) o en el header:
```
Authorization: Bearer {tu-token-aquí}
```

---

## Paquetes NuGet instalados

| Paquete | Versión | Propósito |
|---------|---------|-----------|
| Microsoft.AspNetCore.Authentication.JwtBearer | 8.0.0 | Autenticación por JWT |
| Microsoft.EntityFrameworkCore.InMemory | 8.0.0 | BD en memoria para dev |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 8.0.0 | Sistema de usuarios y roles |

---

## Notas de seguridad

> ⚠️ La clave JWT en `appsettings.json` es solo para desarrollo.  
> En producción usa variables de entorno o Azure Key Vault.
