using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using EFCoreTutorials;

namespace EFCoreTutorials{
public class CheckList
{

    public int CheckListId{get;set;}
    [Required]
   public string CheckListName{get;set;}
  // [ForeignKey("id")]
   public int StudentId{get;set;}
}
}