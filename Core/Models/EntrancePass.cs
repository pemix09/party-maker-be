using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class EntrancePass
{
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string PassType { get; set; }
    public decimal? Price { get; set; }

    public void SetPrice(decimal price) => this.Price = price;
    public void SetType(string newType) => this.PassType = newType;
    public EntrancePass(){}
    private EntrancePass(string passType)
    {
        this.PassType = passType;
    }

    public static EntrancePass Create(string passType)
    {
        return new EntrancePass(passType);
    }
}