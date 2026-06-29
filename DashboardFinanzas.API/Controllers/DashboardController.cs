using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DashboardFinanzas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    /// <summary>
    /// Endpoint protegido - accesible para cualquier usuario autenticado
    /// </summary>
    [HttpGet("summary")]
    public IActionResult GetSummary()
    {
        return Ok(new
        {
            TotalIngresos = 150000.00m,
            TotalEgresos = 85000.00m,
            Balance = 65000.00m,
            Mes = DateTime.UtcNow.ToString("MMMM yyyy")
        });
    }

    /// <summary>
    /// Endpoint solo para Administradores
    /// </summary>
    [HttpGet("admin/reports")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAdminReports()
    {
        return Ok(new
        {
            Message = "Reporte administrativo",
            TotalUsuarios = 25,
            TransaccionesDelMes = 142,
            CrecimientoMensual = 12.5
        });
    }

    /// <summary>
    /// Endpoint accesible para Admin y Contador
    /// </summary>
    [HttpGet("finance/details")]
    [Authorize(Roles = "Admin,Contador")]
    public IActionResult GetFinanceDetails()
    {
        return Ok(new
        {
            CuentasPorCobrar = 35000.00m,
            CuentasPorPagar = 22000.00m,
            FlujoCaja = 13000.00m,
            UltimaActualizacion = DateTime.UtcNow
        });
    }
}
