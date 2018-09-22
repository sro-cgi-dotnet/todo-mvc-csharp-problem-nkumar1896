using System.Collections.Generic;

namespace EFCoreTutorials{
    public interface IKeepRepo{
        Student GetNote(int Id);
       List<Student> GetNote(string text,string type );

        List<Student> GetAllNotes();

        bool PostNote(Student note);

        bool PutNote(int id, Student note);
        bool DeleteNote(int id);
        //bool DeleteAllNotes();
    }
}