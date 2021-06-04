using MedicineTracking.Repo;
using MedicineTracking.Repo.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineTracking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicineController : ControllerBase
    {

        private readonly ILogger<MedicineController> _logger;
        public readonly IMedicineRepo _medicineRepo;

        public MedicineController(ILogger<MedicineController> logger, IMedicineRepo medicineRepo)
        {
            _logger = logger;
            _medicineRepo = medicineRepo;
        }

        /// <summary>
        /// Add/Edit Medicine (Pass Guid.Empty i/e 00000000-0000-0000-0000-000000000000 to add new record)
        /// </summary>
        /// <param name="medicinemaster"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("api/[controller]/manage")]
        public async Task<IActionResult> ManageMedicine([FromBody]MedicineMaster medicinemaster)
        {
            return Ok(await _medicineRepo.ManageMedicine(medicinemaster));
        }
        
        /// <summary>
        /// get list of medicines
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/[controller]/list")]
        public async Task<IActionResult> MedicineList()
        {
            return Ok(await _medicineRepo.MedicineList());
        }

        /// <summary>
        /// get medicine by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/[controller]/details")]
        public async Task<IActionResult> MedicineById(Guid id)
        {
            return Ok(await _medicineRepo.MedicineById(id));
        }
       
    }
}
