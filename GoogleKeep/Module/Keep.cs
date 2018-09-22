using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
namespace EFCoreTutorials
{
public class Student
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int StudentId { get; set; }
    [Required]
    public string Title { get; set; }
     public string PlainText{ get; set; }
     public bool pinned{get;set;}
     public List<Label> labels{get;set;}
     public List<CheckList> checkLists{get;set;}
}
}

