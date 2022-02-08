using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Hieu.FinalProject.Accout_Role
{
    public class Account_Role : Entity<long>
    {
        public long AccountID { set; get; }
        public long RoleID { set; get; }
    }
}
