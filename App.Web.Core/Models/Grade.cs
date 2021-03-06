﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Web.Core
{
    /// <summary>
    /// -
    /// </summary>
    [Table("Grade")]
    public partial class Grade
    {

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public Guid GradeID { get; set; }

		[StringLength(50)]
		public string GradeName { get; set; }

		public DateTime? GradeCreateDate { get; set; }

		public bool? GradeIsDelete { get; set; }

		public string GradeReamrk { get; set; }

		public Guid? ID { get; set; }

    }
}
