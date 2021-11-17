using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Hieu.FinalProject.Data
{
    /* This is used if database provider does't define
     * IFinalProjectDbSchemaMigrator implementation.
     */
    public class NullFinalProjectDbSchemaMigrator : IFinalProjectDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}