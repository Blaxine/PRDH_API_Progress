using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using PRDH.constants;
using PRDH.Extensions;
using PRDH.models;
using PRDH.models.requests;
using PRDH.services;
using PRDH.validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;

namespace PRDH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrdhController : ControllerBase
    {
        private readonly WorkerService _userService;
        private readonly IMapper _covidToCaseMapper;
        public PrdhController(WorkerService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(_userService));
            _covidToCaseMapper = mapper;
        }

        [HttpPost("/getCases")]
        public async Task<IActionResult> GetUsers([FromBody] GetCovidFilters covidFilters)
        // DATE time example 2024-09-27T00%3A00%3A00.0000000Z
        {

            var validator = new GetCovidValidator();
            var validationResult = await validator.ValidateAsync(covidFilters);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult);
            }

            // this worked
            string apiUrl = PrdhContants.ENDPOINT_URL + $"?" + BuildQueryStrint(covidFilters);  // Replace with actual URL

            var users = await _userService.GetCovid(apiUrl);
            int positiveCaes = 0;
            int userCount = users.Count();

            if (userCount > 0)
            {
                var groupOrders = users.GroupBy(value => value?.patientId.ToString())
                                       .Select(group => new
                                       {
                                           PatientId = group.Key,
                                           Test = group.ToList(),
                                       });


                foreach (var userGroup in groupOrders)
                {
                    // Find the earliest start date for the current group
                    var earliestStartDate = userGroup.Test.FirstOrDefault(p => p.orderTestResult.Eq(PrdhContants.POSITIVE));
                    if (earliestStartDate == null) continue;
                        var positiveCase = _covidToCaseMapper.Map<CaseModel>(earliestStartDate);
                        await _userService.StoreCaseDate(positiveCase);
                        positiveCaes++;
                    
                }
                return Ok(new
                {
                    results = groupOrders,
                    totalResults = userCount,
                    positiveResults = positiveCaes
                });
            }

            return default;
           
        }

            private static string BuildQueryStrint(GetCovidFilters payload)
            {
                string query = "";

                foreach (var property in payload.GetType().GetProperties())
                {
                    var key = property.Name;
                    var value = property.GetValue(payload);
                    string filterProperty = string.Concat(key[0].ToString().ToUpper(), key.AsSpan(1));
                    if (string.IsNullOrEmpty(query))
                    {
                        query += $"{filterProperty}={value}";
                    }
                    else if(value != null )
                    {
                        query += $"&{filterProperty}={value}";
                    }
                }
                return query;
            }
        }
    }