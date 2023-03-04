﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters
{
    public class ApiLogginFilter : IActionFilter
    {

        private readonly ILogger<ApiLogginFilter> _logger;
        public ApiLogginFilter(ILogger<ApiLogginFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("### Executando -> OnActionExecuted");
            _logger.LogInformation("#################################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString}");
            _logger.LogInformation("#################################################");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("### Executando -> OnActionExecuting");
            _logger.LogInformation("#################################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString}");
            _logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
            _logger.LogInformation("#################################################");
        }
    }
}
