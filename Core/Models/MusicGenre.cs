using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Core.Models;

public class MusicGenre
{
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }

    public void SetName(string name) => this.Name = name;

    public MusicGenre(){}
    private MusicGenre(string name) => this.Name = name;
    public static MusicGenre Create(string name) => new MusicGenre(name);
}