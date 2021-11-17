using Hieu.FinalProject.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Hieu.FinalProject
{
    [DependsOn(
        typeof(FinalProjectEntityFrameworkCoreTestModule)
        )]
    public class FinalProjectDomainTestModule : AbpModule
    {

    }
}