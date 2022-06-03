using GeekShopping.CouponAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CouponAPI.Model
{

    //Definir o nome da Tabela no banco
    [Table("coupon")]
    public class Coupon : BaseEntity{

        //Definir as colunas
        [Column("coupon_code")]
        [Required]
        [StringLength(30)] //Definir o tamanho máximo
        public string CouponCode { get; set; }

        [Column("discount_amount")]
        [Required]
        public decimal DiscountAmount { get; set; }

    }
}
