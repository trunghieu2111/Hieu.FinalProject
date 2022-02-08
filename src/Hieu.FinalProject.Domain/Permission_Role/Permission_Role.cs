using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Hieu.FinalProject.Permission_Role
{
    public class MyPermission_Role :  Entity<long>
    {
        public Guid PermissionID { set; get; }
        public long RoleID { set; get; }
    }
}
