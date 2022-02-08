using Hieu.FinalProject.Accounts;
using Hieu.FinalProject.Accout_Role;
using Hieu.FinalProject.Branchs;
using Hieu.FinalProject.Customers;
using Hieu.FinalProject.Invoice.InvoiceDetail;
using Hieu.FinalProject.Invoice.InvoiceHeader;
using Hieu.FinalProject.Invoice.InvoiceTaxBreak;
using Hieu.FinalProject.Permission_Role;
using Hieu.FinalProject.Permissions;
using Hieu.FinalProject.Role;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Hieu.FinalProject.EntityFrameworkCore
{
    [ReplaceDbContext(typeof(IIdentityDbContext))]
    [ReplaceDbContext(typeof(ITenantManagementDbContext))]
    [ConnectionStringName("Default")]
    public class FinalProjectDbContext : 
        AbpDbContext<FinalProjectDbContext>,
        IIdentityDbContext,
        ITenantManagementDbContext
    {
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<MyPermission_Role> MyPermission_Roles { get; set; }
        public DbSet<MyRole> MyRoles { get; set; }
        public DbSet<Account_Role> Account_Roles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<InvoiceHeader> InvoiceHeaders { get; set; }
        public DbSet<InvoiceDetailEntity> InvoiceDetails { get; set; }
        public DbSet<InvoiceTaxBreakEntity> InvoiceTaxBreaks { get; set; }
        /* Add DbSet properties for your Aggregate Roots / Entities here. */
        #region Entities from the modules

        /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
         * and replaced them for this DbContext. This allows you to perform JOIN
         * queries for the entities of these modules over the repositories easily. You
         * typically don't need that for other modules. But, if you need, you can
         * implement the DbContext interface of the needed module and use ReplaceDbContext
         * attribute just like IIdentityDbContext and ITenantManagementDbContext.
         *
         * More info: Replacing a DbContext of a module ensures that the related module
         * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
         */

        //Identity
        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityClaimType> ClaimTypes { get; set; }
        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
        public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
        public DbSet<IdentityLinkUser> LinkUsers { get; set; }

        // Tenant Management
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }
        
        #endregion
        public FinalProjectDbContext(DbContextOptions<FinalProjectDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();
            builder.ConfigureIdentity();
            builder.ConfigureIdentityServer();
            builder.ConfigureFeatureManagement();
            builder.ConfigureTenantManagement();

            /* Configure your own tables/entities inside here */

            builder.Entity<Branch>(b =>
            {
                b.ToTable(FinalProjectConsts.DbTablePrefix + "Branchs", FinalProjectConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                //...
            });

            builder.Entity<MyPermission_Role>(b =>
            {
                b.ToTable(FinalProjectConsts.DbTablePrefix + "MyPermission_Roles", FinalProjectConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                //...
            });

            builder.Entity<MyRole>(b =>
            {
                b.ToTable(FinalProjectConsts.DbTablePrefix + "MyRoles", FinalProjectConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                //...
            });

            builder.Entity<Account_Role>(b =>
            {
                b.ToTable(FinalProjectConsts.DbTablePrefix + "Account_Roles", FinalProjectConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                //...
            });

            builder.Entity<Permission>(b =>
            {
                b.ToTable(FinalProjectConsts.DbTablePrefix + "Permissions", FinalProjectConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                //...
            });

            builder.Entity<Account>(b =>
            {
                b.ToTable(FinalProjectConsts.DbTablePrefix + "Accounts", FinalProjectConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                //...
            });

            builder.Entity<Customer>(b =>
            {
                b.ToTable(FinalProjectConsts.DbTablePrefix + "Customers", FinalProjectConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                //...
            });

            builder.Entity<InvoiceHeader>(b =>
            {
                b.ToTable(FinalProjectConsts.DbTablePrefix + "InvoiceHeaders", FinalProjectConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                //...
            });

            builder.Entity<InvoiceDetailEntity>(b =>
            {
                b.ToTable(FinalProjectConsts.DbTablePrefix + "InvoiceDetails", FinalProjectConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                //...
            });

            builder.Entity<InvoiceTaxBreakEntity>(b =>
            {
                b.ToTable(FinalProjectConsts.DbTablePrefix + "InvoiceTaxBreaks", FinalProjectConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                //...
            });
        }
    }
}
