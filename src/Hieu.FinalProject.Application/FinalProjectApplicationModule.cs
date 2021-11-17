using Hieu.FinalProject.Access.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.Dapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Hieu.FinalProject
{
    [DependsOn(
        typeof(FinalProjectDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(FinalProjectApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpSettingManagementApplicationModule),
        typeof(AbpDapperModule)
        )]
    public class FinalProjectApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<FinalProjectApplicationModule>();
            });

            //context.Services.AddScoped<IPermissionDapperRepository, PermissionDapperRepository>();
        }
    }
}
