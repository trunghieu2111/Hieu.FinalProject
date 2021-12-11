using System;
using System.ComponentModel.DataAnnotations;

namespace Hieu.FinalProject.Access
{
    public class CreateUpdatePermissionDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { set; get; }
        public bool UserPermission { set; get; }
        public bool BranchPermission { set; get; }
        public bool CustomerPermission { set; get; }
        public bool PerPermission { set; get; }
        public bool InvoicePermision { set; get; }
    }
}
