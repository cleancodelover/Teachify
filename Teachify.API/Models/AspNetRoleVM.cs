// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teachify.API.VM
{
    public partial class AspNetRoleVM
    {

        [Key]
        public string Id { get; set; }
        [StringLength(256)]
        public string Name { get; set; }
        public int? ProviderId { get; set; }
        [StringLength(256)]
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}