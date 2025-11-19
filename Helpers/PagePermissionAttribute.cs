using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HanuMediSoftCore.Helpers
{
    public class PagePermissionAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _permissionCode;

        public PagePermissionAttribute(string permissionCode)
        {
            _permissionCode = permissionCode;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;

            // get user id from session
            var userId = httpContext.Session.GetString("gUserId");
            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new RedirectToPageResult("/Login");
                return;
            }

            // read permissions from session (comma separated)
            var permissions = httpContext.Session.GetString("gPermissions");

            if (string.IsNullOrEmpty(permissions))
            {
                context.Result = new RedirectToPageResult("/Unauthorized");
                return;
            }

            // check permission
            var list = permissions.Split(',').Select(x => x.Trim()).ToList();
            if (!list.Contains(_permissionCode))
            {
                context.Result = new RedirectToPageResult("/Unauthorized");
            }
        }
    }
}
