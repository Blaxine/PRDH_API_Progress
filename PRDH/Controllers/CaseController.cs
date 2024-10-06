using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRDH.DataBase;
using PRDH.models;
using PRDH.services;
using PRDH.validators;

namespace PRDH.Controllers
{
    [ApiController]
    [Route("cases/[Controller]")]
    public class CaseController : ControllerBase

    {
        private readonly CaseDataBaseContext _caseDatabaseContext;
        private readonly WorkerService _workerService ; 
        public CaseController(CaseDataBaseContext caseDatabaseContext, WorkerService workerService)
        {
            _caseDatabaseContext = caseDatabaseContext?? throw new ArgumentNullException(nameof(_caseDatabaseContext));
            _workerService = workerService ?? throw new ArgumentNullException(nameof(workerService));
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<CaseModel>>> ListCases()
        {
            var results = await _caseDatabaseContext.Cases.ToListAsync();
            return Ok(
                new
                {
                    cases = results,
                    totalCases = results.Count(),
                });
        }

        [HttpGet("read")]
        public async Task<ActionResult<CaseModel>> GetCaseById([FromQuery] string caseId)
        {

            if(string.IsNullOrWhiteSpace(caseId)) return BadRequest(string.Empty);
            var @case = await _caseDatabaseContext.Cases.FindAsync(caseId);
            if(@case == null) return NotFound(new { message = "Case not found with the provided ID.", caseId = caseId});

            return Ok(@case); 

        }

        [HttpPost("insert")]
        public async Task<ActionResult<CaseModel>> CreateCase([FromBody]CaseModel @case)
        {
            var validator = new InsertCaseValidator();

            var caseClosed = _workerService.StoreCaseDate(@case,0);
            return Ok(new
            {
                success = true,
                message = "Item created successfully!",
                data = caseClosed,
            });
        }

        [HttpPost("update")]
        public async Task <IActionResult> UpdateCase([FromBody] CaseModel @case)
        {
            var validator = new CaseModelValidator();

            var validatorResponse = await validator.ValidateAsync(@case);

            if (!validatorResponse.IsValid)  return BadRequest(validatorResponse.Errors);

            _caseDatabaseContext.Entry(@case).State = EntityState.Modified;

            await _caseDatabaseContext.SaveChangesAsync(); 

            return Ok(@case);
        }


    }
}
