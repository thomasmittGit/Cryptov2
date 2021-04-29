using Cryptov2.Models;
using Cryptov2.DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cryptov2.Filters
{
    public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
    {
        private const String ApiKeyHeaderName = "ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ApiContext keysContext = context.HttpContext.RequestServices.GetRequiredService<ApiContext>();

            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            Keys key = keysContext.Keys.ToList<Keys>().Where(x => x.apiKey.Equals(potentialApiKey.ToString())).First();

            if (key == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
