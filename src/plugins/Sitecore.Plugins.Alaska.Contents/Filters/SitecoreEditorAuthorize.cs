using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Sitecore.Plugins.Alaska.Contents.Filters
{
    public class SitecoreEditorAuthorize : AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (Sitecore.Context.User == null || !Sitecore.Context.User.IsAuthenticated)
                return false;

            if (RoleValues.Any())
                return RoleValues.Any(x => IsInRole(x));

            if (EditorRoles.Any())
                return EditorRoles.Any(x => IsInRole(x));

            return true;
        }

        private bool IsInRole(string role)
        {
            return Sitecore.Context.User.Roles.Any(userRole => role.Equals($"{userRole.Domain}\\{userRole.Name}", StringComparison.InvariantCultureIgnoreCase));
        }

        protected IEnumerable<string> RoleValues => Roles?
            .Split(',')
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();

        protected IEnumerable<string> EditorRoles => Sitecore.Configuration.Settings.GetSetting("Alaska.Editing.AllowedRoles")
            .Split(',')
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();
    }
}
