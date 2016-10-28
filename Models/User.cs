using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace wall2.Models {
 public class User: BaseEntity
    {
        public User() {
            messages = new List<Message>();
        }

        [Key]
        public long id { get; set; }
        [Required]
        public string first_name { get; set; }
        public string last_name { get; set; }
        // [DataType(DataType.Password)]
        public string password { get; set; }
        public string email { get; set; }

        public ICollection<Message> messages {get; set;}
    }
}       