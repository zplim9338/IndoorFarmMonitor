using IndoorFarmMonitor.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IndoorFarmMonitor.Controllers
{
    [ApiController]
    [Route("plant-sensor-data")]
    public class PlantSensorDataController : ControllerBase
    {
        private readonly IPlantSensorService _service;

        public PlantSensorDataController(IPlantSensorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var data = await _service.GetCombinedSensorDataAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
