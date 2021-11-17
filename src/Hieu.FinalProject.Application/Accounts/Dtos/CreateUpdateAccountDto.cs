using Hieu.FinalProject.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hieu.FinalProject.Accounts.Dtos
{
    public class CreateUpdateAccountDto
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Acc { set; get; }
        public string Pass { set; get; }
        public Guid PermissionId { set; get; }
    }
}
