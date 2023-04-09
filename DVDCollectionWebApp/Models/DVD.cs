using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DVDCollectionWebApp.Models
{
    [PrimaryKey(nameof(Id), nameof(title))]
    public class DVD
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string title { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string category { get; set; }
        [Required]
        [Display(Name = "Run time")]
        [RegularExpression(@"[0-9]+ mins\.", ErrorMessage = "Please enter in the following format \"123 mins.\"")]
        public string runtime { get; set; }
        [Required]
        [Display(Name = "Year of Release")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid year")]
        public int yearOfRelease { get; set; }
        [Required]
        [Display(Name = "Price in $")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid price")]
        public double price { get; set; }
    }
}

