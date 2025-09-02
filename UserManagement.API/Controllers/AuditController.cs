using Microsoft.AspNetCore.Mvc;
using UserManagement.Services.Interfaces;

namespace UserManagement.API.Controllers;

[Route("logs")]
[ApiController]
public class AuditController: ControllerBase
{
  

    private readonly IAuditLogService _auditLogService;
    public AuditController(IAuditLogService auditLogService) => _auditLogService = auditLogService;

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetUserAuditLogs()
    {
       var items =  await _auditLogService.GetAllAsync();
        var result = items.Select(ImplicitOperatorMapper.Map).ToList();
        return Ok(items);
    }
}
