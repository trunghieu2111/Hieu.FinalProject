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
        public string ParentId { set; get; }
    }
}
