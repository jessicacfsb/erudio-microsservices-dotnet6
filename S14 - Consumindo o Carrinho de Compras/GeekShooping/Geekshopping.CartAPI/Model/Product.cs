using GeekShopping.CartAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartAPI.Model {

    //Definir o nome da Tabela no banco
    [Table("product")]

    public class Product : BaseEntity {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public long Id { get; set; }

        //Definir as colunas
        [Column("name")]
        [Required]
        [StringLength(150)] //Definir o tamanho máximo
        public string Name { get; set; }

        [Column("price")]
        [Required]
        [Range(1, 10000)]
        public decimal Price { get; set; }

        [Column("description")]
        [StringLength(500)]
        public string Description { get; set; }

        [Column("category_name")]
        [StringLength(50)]
        public string CategoryName { get; set; }

        [Column("image_url")]
        [StringLength(300)]
        public string ImageURL { get; set; }



    }
}
