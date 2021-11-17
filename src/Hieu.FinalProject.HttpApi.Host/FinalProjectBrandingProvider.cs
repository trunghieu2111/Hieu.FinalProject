using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Hieu.FinalProject
{
    [Dependency(ReplaceServices = true)]
    public class FinalProjectBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "FinalProject";
    }
}
