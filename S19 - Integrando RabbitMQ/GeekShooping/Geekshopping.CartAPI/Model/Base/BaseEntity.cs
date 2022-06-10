﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geekshopping.CartAPI.Model.Base {
    public class BaseEntity {
        [Key]
        [Column("id")] //nome da coluna
        public long Id { get; set; }
    }
}
