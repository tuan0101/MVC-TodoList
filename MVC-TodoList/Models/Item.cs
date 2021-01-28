using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_TodoList.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime DueDate { get; set; }
        public string Note { get; set; }
        public string Priority { get; set; }
        public bool Status { get; set; }
    }
}
