using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hieu.FinalProject.Accounts.Dtos
{
    public class AccountNewDto
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Acc { set; get; }
        public string Pass { set; get; }
        public Guid TenantId { set; get; }
        public List<AccountRoleDto> AccountRoles { get; set; }
    }

    public class AccountRoleDto
    {
        public long Id { set; get; }
        public long AccountID { set; get; }
        public long RoleID { set; get; }
    }
}
