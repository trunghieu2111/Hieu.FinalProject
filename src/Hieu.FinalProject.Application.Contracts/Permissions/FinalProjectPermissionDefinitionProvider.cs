using Hieu.FinalProject.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Hieu.FinalProject.Permissions
{
    public class FinalProjectPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(FinalProjectPermissions.GroupName);
            //Define your own permissions here. Example:
            //myGroup.AddPermission(FinalProjectPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<FinalProjectResource>(name);
        }
    }
}
