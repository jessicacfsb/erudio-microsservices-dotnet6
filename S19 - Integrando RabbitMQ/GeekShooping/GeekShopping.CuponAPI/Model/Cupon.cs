using GeekShopping.CuponAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CuponAPI.Model {

    //Definir o nome da Tabela no banco
    [Table("cupon")]
    public class Cupon : BaseEntity{

        [Column("cupon_code")]
        [Required]
        [StringLength(30)] //Definir o tamanho máximo de caracteres
        public string CuponCode { get; set; }

        [Column("discount-amount")]
        [Required]
        [Range(1,10000)]
        public decimal DiscountAmount { get; set; }

    }
}
