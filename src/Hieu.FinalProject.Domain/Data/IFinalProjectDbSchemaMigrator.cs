using System.Threading.Tasks;

namespace Hieu.FinalProject.Data
{
    public interface IFinalProjectDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
