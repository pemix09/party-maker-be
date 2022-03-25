using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class EntrancePass
{
    [Required]
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string PassType { get; set; }
    public float? Price { get; set; }

    public void SetPrice(float price) => this.Price = price;
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