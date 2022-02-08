using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hieu.FinalProject.Branchs
{
    public class CreateUpdateBranchDto
    {
        public Guid Id { get; set; }

        [Required]
        public string MST { set; get; }

        [Required]
        public string URL { set; get; }

        [Required]
        public string NameBranch { set; get; }

        [Required]
        public string Address { set; get; }

        public string ParentId { set; get; }
    }
}
