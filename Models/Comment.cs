using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace wall2.Models {
 public class Comment: BaseEntity
    {
       
        [Key]
        public long id { get; set; }
        [Required]
        public string comment { get; set; }
        public Message message {get; set;}
         public int message_id {get; set;}
        
        public User user { get; set; }

       

        
    }
}       