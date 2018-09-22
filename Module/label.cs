using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using EFCoreTutorials;

namespace EFCoreTutorials{
public class Label
{

    public int LabelId{get;set;}
    [Required]
   public string LabelName{get;set;}
  // [ForeignKey("id")]
   public int StudentId{get;set;}
}
}