using Volo.Abp.Modularity;

namespace Hieu.FinalProject
{
    [DependsOn(
        typeof(FinalProjectApplicationModule),
        typeof(FinalProjectDomainTestModule)
        )]
    public class FinalProjectApplicationTestModule : AbpModule
    {

    }
}