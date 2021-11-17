using System;
using System.Collections.Generic;
using System.Text;
using Hieu.FinalProject.Localization;
using Volo.Abp.Application.Services;

namespace Hieu.FinalProject
{
    /* Inherit your application services from this class.
     */
    public abstract class FinalProjectAppService : ApplicationService
    {
        protected FinalProjectAppService()
        {
            LocalizationResource = typeof(FinalProjectResource);
        }
    }
}
