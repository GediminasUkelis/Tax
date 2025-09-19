using Microsoft.AspNetCore.Mvc.Filters;

namespace TaxRepo.Api.Infrastructure.Authorization
{
    public class RoleHeaderAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string role;

        public RoleHeaderAuthorizeAttribute(string role)
        {
            this.role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasRole = context.HttpContext.Request.Headers.TryGetValue("Role", out var roleHeader);

            if (!hasRole || roleHeader.ToString() != this.role)
            {
                throw new UnauthorizedAccessException("Forbidden: insufficient role.");
            }
        }
    }
}