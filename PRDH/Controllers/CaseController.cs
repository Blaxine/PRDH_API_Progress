using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRDH.DataBase;
using PRDH.models;
using PRDH.validators;

namespace PRDH.Controllers
{
    [ApiController]
    [Route("cases/[Controller]")]
    public class CaseController : ControllerBase

    {
        private readonly CaseDataBaseContext _caseDatabaseContext; 

        public CaseController(CaseDataBaseContext caseDatabaseContext)
        {
            _caseDatabaseContext = caseDatabaseContext?? throw new ArgumentNullException(nameof(_caseDatabaseContext));
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<CaseModel>>> ListCases()
        {
           return await _caseDatabaseContext.Cases.ToListAsync();
        }

        [HttpGet("read/{caseId}")]
        public async Task<ActionResult<CaseModel>> GetCaseById(string caseId)
        {

            var @case = await _caseDatabaseContext.Cases.FindAsync(caseId);
            if(@case == null) return NotFound(new { message = "Case not found with the provided ID.", caseId = caseId});

            return Ok(@case); 

        }

        [HttpPost]
        public async Task<ActionResult<CaseModel>> CreateCase(CaseModel @case)
        {
            @case.caseId = Guid.NewGuid().ToString();
            var validator = new CaseModelValidator(); 
            var validationResult = await validator.ValidateAsync(@case);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            _caseDatabaseContext.Add(@case); 
            await _caseDatabaseContext.SaveChangesAsync();

            return CreatedAtAction("CaseCreateSuccess", new { id = @case.caseId }, @case);
        }

        [HttpPost("update/{caseId}")]
        public async Task <IActionResult> UpdateCase( string caseId, CaseModel @case)
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
