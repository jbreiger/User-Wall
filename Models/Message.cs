using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace wall2.Models {
 public class Message: BaseEntity
    {
        public Message() {
            comments = new List<Comment>();
        }

        [Key]
        public long id { get; set; }
        [Required]
        public string message { get; set; }
        public string email { get; set; }
        public int user_id { get; set; }
        public User user { get; set; }
        public ICollection<Comment> comments {get; set;}
    }
}       