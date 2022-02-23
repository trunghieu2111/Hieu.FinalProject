using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hieu.FinalProject.Access.Dtos
{
    public class MyRoleDto
    {
        public long Id { set; get; }
        public string RoleName { set; get; }
        public List<MyPermissionRoleDto> MyPermissionRoles { get; set; }
    }
    
    public class MyPermissionRoleDto
    {
        public long Id { set; get; }
        public Guid PermissionID { set; get; }
        public long RoleID { set; get; }
    }
}
