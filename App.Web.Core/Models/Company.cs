﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Web.Core
{
    /// <summary>
    /// -
    /// </summary>
    [Table("Company")]
    public partial class Company
    {

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public Guid CompanyID { get; set; }

		[StringLength(50)]
		public string CompanyName { get; set; }

		public Guid? PId { get; set; }

		public DateTime? CreateDate { get; set; }

		public bool? IsDelete { get; set; }

    }
}
