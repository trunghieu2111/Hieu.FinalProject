using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Hieu.FinalProject.Accout_Role
{
    public class Account_Role : Entity<long>
    {
        [ForeignKey("Account")]
        public long AccountID { set; get; }
        [ForeignKey("MyRole")]
        public long RoleID { set; get; }
        public Accounts.Account Account { set; get; }
        public Role.MyRole MyRole { set; get; }
    }
}
