using Hieu.FinalProject.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Hieu.FinalProject.Accounts.Dtos
{
    public class AccountDto : AuditedEntityDto<long>
    {
        public string Name { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Acc { set; get; }
        public string Pass { set; get; }
        public Guid PermissionId { set; get; }
    }
}
