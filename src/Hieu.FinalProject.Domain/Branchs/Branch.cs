using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Hieu.FinalProject.Branchs
{
    public class Branch : Entity<Guid>
    {
        public string MST { set; get; }
        public string URL { set; get; }
        public string NameBranch { set; get; }
        public string Address { set; get; }
        public string LegalName { set; get; }
        public string BankAcount { set; get; }
        public string BankName { set; get; }
        public string Phone { set; get; }
        public string Fax { set; get; }
        public string Email { set; get; }
        public Guid ParentId { set; get; }
    }
}
