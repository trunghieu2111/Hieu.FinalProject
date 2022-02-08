using System;
using System.ComponentModel.DataAnnotations;

namespace Hieu.FinalProject.Access
{
    public class CreateUpdatePermissionDto
    {
        public Guid Id { get; set; }

        [Required]
        public string NamePermission { set; get; }
    }
}
