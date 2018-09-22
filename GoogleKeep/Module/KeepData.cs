using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
namespace EFCoreTutorials {
    public class KeepData : IKeepRepo{
        
        KeepContext not=null;
        public KeepData(KeepContext _not)
        {
            this.not=_not;
        }
        public Student GetNote(int id){
            using(not)
            {
            return not.Stu.Include(n=>n.labels).Include(n=>n.checkLists).ToList().FirstOrDefault(note => note.StudentId == id);
            }
        }
        public List<Student> GetNote(string text,string type){
            List<Student> Final = new List<Student>();
            using(not)
            {

                if(type=="title")    
                {
                    List<Student> S_all = not.Stu.Where(s=> s.Title==text).Include(n=>n.labels).Include(n=>n.checkLists).ToList();
                    return S_all;
            //return not.Stu.Include(n=>n.labels).Include(n=>n.checkLists).ToList().FirstOrDefault(note => note.Title == text);
                }
                else if(type=="label")
                {
                    List<Student> S = not.Stu.Include(n=>n.labels).Include(n=>n.checkLists).ToList();
                    foreach(Student S1 in S)
                    {
                        foreach (Label l in S1.labels)
                        {
                            if(l.LabelName==text)
                            {
                                Final.Add(S1);
                            }
                        }
                    }
                     return Final;
                }
                else if(type=="pinned")
                {
                if(text=="true")
                {
                 List<Student> S_all1 = not.Stu.Where(s=> s.pinned==true).Include(n=>n.labels).Include(n=>n.checkLists).ToList();
                 
                 return S_all1;
                }
               

                }
            }
            return Final;
        }

        public List<Student> GetAllNotes(){
            using(not)
            {
            return not.Stu.Include(n=>n.labels).Include(n=>n.checkLists).ToList();
            }
        }

        public bool PostNote(Student note){
            if(not.Stu.FirstOrDefault(n => n.StudentId == note.StudentId) == null){
                not.Stu.Add(note);
                PostChecklist(note);
                not.SaveChanges();
                return true;
            }
            else{
                return false;
            }
        }
        void PostChecklist(Student note){
            foreach(Label cl in note.labels){
                not.Lab.Add(cl);
            }
            foreach(CheckList pl in note.checkLists){
                not.Check.Add(pl);
            }
            not.SaveChanges();
        }

        public bool PutNote(int id, Student note){
            // Student retrievedNote = not.Stu.FirstOrDefault(n => n.StudentId == id);
            // if( retrievedNote != null){
                not.Update(note);
                // not.Stu.Add(note);
                not.SaveChanges();
                return true;
            // }
            // else{
            //     return false;
            // }
        }

        public bool DeleteNote(int id){
            using(not)
            {
            Student retrievedNote = not.Stu.FirstOrDefault(n => n.StudentId == id);
            if (retrievedNote != null){
                not.Stu.Remove(retrievedNote);
                not.SaveChanges();
                return true;
            }
            else{
                return false;
            }
        }
        }
       /* public List<Student> DeleteAllNotes(){
            using(not)
            {
             return not.Stu.RemoveAll();
            }
        }*/
    }
}