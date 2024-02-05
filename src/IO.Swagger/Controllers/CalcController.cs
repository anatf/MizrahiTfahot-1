/*
 * Calculation API
 *
 * Calculation API
 *
 * OpenAPI spec version: 1.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using IO.Swagger.Attributes;

using Microsoft.AspNetCore.Authorization;
using IO.Swagger.Models;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace IO.Swagger.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class CalcController : ControllerBase
    {
        ILogger _logger;

        public CalcController(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Perform arithmetic operation
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200">Successful operation</response>
        [HttpPost]
        [Route("/ANATAFR/CalcAPI/1.0/calculate")]
        [ValidateModelState]
        [SwaggerOperation("CalculatePost")]
        [SwaggerResponse(statusCode: 200, type: typeof(InlineResponse200), description: "Successful operation")]
        [Authorize]
        public virtual IActionResult CalculatePost([FromBody]CalculateBody body)
        {
            decimal calcResult = 0;

            try
            {
                calcResult = body.Calculate();
            }
            catch (CalcException cex)
            {
                _logger.LogError(cex.Message);
                return BadRequest(cex.Message);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }

            string resultJson = null;
            resultJson = $"{{\n  \"result\" : {calcResult}\n}}";
            
                        var result = resultJson != null
                        ? JsonConvert.DeserializeObject<InlineResponse200>(resultJson)
                        : default(InlineResponse200);            
            return new ObjectResult(result);
        }
    }
}