using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Hieu.FinalProject.Permission_Role
{
    public class MyPermission_Role :  Entity<long>
    {
        [ForeignKey("Permission")]
        public Guid PermissionID { set; get; }

        [ForeignKey("MyRole")]
        public long RoleID { set; get; }
        public Permissions.Permission Permission { set; get; }
        public Role.MyRole MyRole { set; get; }

    }
}
