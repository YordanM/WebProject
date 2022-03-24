using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace WebProject.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set;}

        [ForeignKey("PlaylistId")]
        public int PlaylistId { get; set; }

        
        public int OwnerId { get; set; }

            
        public virtual User Owner { get; set; }


        
        
    }
}
