using Inspection.Data.Entities;
using InspectionCore.Common;
using InspectionCore.Reposiotories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Inspection.Data
{
    public class InspectionContext : InspectionEfContext
    {
        public InspectionContext()
          : base(null, "Data Source=DESKTOP-BS7R6AC;Initial Catalog=InspectionSuite;Integrated Security=True")
        {

        }
        public InspectionContext(IRequestInfo requestInfo)
             : base(requestInfo, requestInfo.Configuration.GetConnectionString("DefaultConnection"))
        {
        }

        
        protected override void InitializeEntities()
        {
            this.InitializeEntity<Tenant>();
            this.CreateRelation<Tenant, TenantApplication>(o => o.TenantApplications, o => o.Tenant, o => o.TenantId);

            this.InitializeEntity<TenantApplication>();

            this.InitializeEntity<Application>();
            this.CreateRelation<Application, TenantApplication>(o => o.TenantApplications, o => o.Application, o => o.ApplicationId);

            var roleEntityBuilder = this.InitializeEntity<Role>();
            roleEntityBuilder
                .Property(o => o.RoleType)
                .HasConversion(o => Convert.ToInt32(o), o => (RoleType)o);
            this.CreateRelation<Role, RoleRight>(o => o.RoleRights, o => o.Role, o => o.RoleId);
            this.CreateRelation<Role, User>(o => o.Users, o => o.Role, o => o.RoleId);

            this.InitializeEntity<RoleRight>();

            var userEntityBuilder = this.InitializeEntity<User>();

            byte[] Slt = new byte[128 / 8];

            using (var RandomNum = RandomNumberGenerator.Create())
            {
                RandomNum.GetBytes(Slt);
            }
            userEntityBuilder
                .Property(o => o.Password)
                .HasConversion(
                    o => o.ToMd5Hash(),
                    o => o);
        }

        protected override void SeedStaticData(ModelBuilder modelBuilder)
        {
            #region Applications
            this.SeedData(
                new Application { Id = 1, Name = "Admin" },
                new Application { Id = 2, Name = "Tenant" },
                new Application { Id = 3, Name = "Customer" });
            #endregion

            #region Roles
            this.SeedData(
                new Role { Id = 1, Name = "Admin", RoleType = RoleType.Admin });
            #endregion

            #region Users
            this.SeedData(
                new User { Id = "inspecionAdmin", Password = "inspecionAdmin", RoleId = 1 });

            #endregion
        }
        
        protected override void SeedTestingData(ModelBuilder modelBuilder)
        {
            
        }
    }
    public class GamaContextFactory : IDesignTimeDbContextFactory<InspectionContext>
    {
        InspectionContext IDesignTimeDbContextFactory<InspectionContext>.CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath($"{Directory.GetParent(Directory.GetCurrentDirectory())}/Inspection.Apis")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.Staging.json", optional: true)
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            return new InspectionContext(new RequestInfo(configuration, null));
        }
    }
}
