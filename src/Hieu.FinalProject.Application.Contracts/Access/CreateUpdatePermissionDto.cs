using System;
using System.ComponentModel.DataAnnotations;

namespace Hieu.FinalProject.Access
{
    public class CreateUpdatePermissionDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { set; get; }
        public string UserPermission { set; get; }
        public string BranchPermission { set; get; }
        public string CustomerPermission { set; get; }
        public string PerPermission { set; get; }
        public string InvoicePermision { set; get; }
    }
}
