using Hieu.FinalProject.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Hieu.FinalProject.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class FinalProjectController : AbpController
    {
        protected FinalProjectController()
        {
            LocalizationResource = typeof(FinalProjectResource);
        }
    }
}