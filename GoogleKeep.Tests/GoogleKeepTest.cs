using System;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using EFCoreTutorials;
using EFCore.Controllers;
using System.Collections.Generic;

namespace GoogleKeep.Tests
{
    public class GoogleKeepTest
    {
        private List<Student> GetMockDatabase(){
            return new List<Student>{
                new Student{
                    StudentId = 1,
                    Title = "do more what you love",
                    checkLists = new List<CheckList>{
                        new CheckList{
                            CheckListId = 1,
                            CheckListName = "how are you",
                            StudentId = 1
                        }
                    },
                    labels = new List<Label>{
                        new Label{
                            LabelId = 1,
                            LabelName = "awesome"
                        }
                    }
                },
                new Student{
                    StudentId = 2,
                    Title = "work to do",
                    checkLists = new List<CheckList>{
                        new CheckList{
                            CheckListId = 2,
                            CheckListName = "will do it",
                            StudentId = 2
                        }
                    },
                    labels = new List<Label>{
                        new Label{
                            LabelId = 2,
                            LabelName = "you can do it"
                        }
                    }
                }
            };
        }

        [Fact]
        public void GetAll_Positive_ListWithEntries()
        {
            var Drepo = new Mock<IKeepRepo>();
            List<Student> not = GetMockDatabase();
            Drepo.Setup(d => d.GetAllNotes()).Returns(not);
            ValuesController valuecontroller = new ValuesController(Drepo.Object);
            var result = valuecontroller.Get();
            var okObject = result as OkObjectResult;
            Assert.NotNull(okObject);
            var model = okObject.Value as List<Student>;
            Assert.NotNull (model);

            Assert.Equal (not.Count, model.Count);
            
        }
        [Fact]
        public void GetAll_Negative_EmptyList()
        {
            var Drepo = new Mock<IKeepRepo>();
            List<Student> not = new List<Student>();
            Drepo.Setup(d => d.GetAllNotes()).Returns(not);
            ValuesController valuecontroller = new ValuesController(Drepo.Object);
            var result = valuecontroller.Get();
            var okObject = result as OkObjectResult;
            Assert.NotNull(okObject);
            var model = okObject.Value as List<Student>;
            Assert.NotNull (model);

            Assert.Equal (not.Count, model.Count);
            
        }
        [Fact]
        public void GetAll_Negative_DatabaseError () {
            var Drepo = new Mock<IKeepRepo>();
            List<Student> not = null;
            Drepo.Setup(d => d.GetAllNotes()).Returns(not);
            ValuesController valuecontroller = new ValuesController(Drepo.Object);
            var result = valuecontroller.Get();
            Assert.IsType<NotFoundObjectResult> (result);
        }
        [Fact]
        public void GetById_Positive_ReturnsNoteWithId1()
        {
            var Drepo = new Mock<IKeepRepo>();
            List<Student> not = GetMockDatabase();
            int id=1;
            Drepo.Setup(d => d.GetNote(id)).Returns(not.Find(n => n.StudentId == id));
            ValuesController valuecontroller = new ValuesController(Drepo.Object);
            var result = valuecontroller.Get(id);
            var okObject = result as OkObjectResult;
            Assert.NotNull(okObject);
            var model = okObject.Value as Student;
            Assert.NotNull (model);

            Assert.Equal (id, model.StudentId);
        }
         [Fact]
        public void GetById_Negative_ReturnsNullNotFound()
        {
            var Drepo = new Mock<IKeepRepo>();
            List<Student> not = GetMockDatabase();
            int id=3;
            Drepo.Setup(d => d.GetNote(id)).Returns(not.Find(n => n.StudentId == id));
            ValuesController valuecontroller = new ValuesController(Drepo.Object);
            var result = valuecontroller.Get(id);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        
        [Fact]
        public void GetByTitle_Positive_ReturnsNoteWithTitle()
        {
            var Drepo = new Mock<IKeepRepo>();
            List<Student> not = GetMockDatabase();
            int expected=1;
            string type = "Title";
            string text = "work to do";
            Drepo.Setup(d => d.GetNote(text, type)).Returns(not.FindAll(n => n.Title == text));
            ValuesController valuecontroller = new ValuesController(Drepo.Object);
            var result = valuecontroller.Get(text, type);

            var okObject = result as OkObjectResult;
            Assert.NotNull(okObject);

            var model = okObject.Value as List<Student>;
            Assert.NotNull(model);

            Assert.Equal(expected, model.Count);
        }
        [Fact]
        public void GetByTitle_Negative_ReturnsNotFound()
        {
           var Drepo = new Mock<IKeepRepo>();
            List<Student> not = GetMockDatabase();
            string type = "Title";
            string text = "notitle";
            Drepo.Setup(d => d.GetNote(text, type)).Returns(not.FindAll(n => n.Title == text));
            ValuesController valuecontroller = new ValuesController(Drepo.Object);
            var result = valuecontroller.Get(text, type);

            var nfokObject = result as NotFoundObjectResult;
            Assert.NotNull(nfokObject);
        }
        [Fact]
         public void GetByTitle_Negative_ReturnsBadRequest()
        {
            var Drepo = new Mock<IKeepRepo>();
            List<Student> not = null;
            string type = "notTitle";
            string text = "notext";
            Drepo.Setup(d => d.GetNote(text, type)).Returns(not);
            ValuesController valuecontroller = new ValuesController(Drepo.Object);
            var result = valuecontroller.Get(text, type);

            var brokObject = result as BadRequestObjectResult;
            Assert.NotNull(brokObject);
        }
        [Fact]
        public void PostById_Positive_ReturnsCreated()
        {
           var Drepo = new Mock<IKeepRepo>();
            Student not = new Student
            {
                StudentId = 4,
                Title = "createdpost"
            };
            Drepo.Setup(d => d.PostNote(not)).Returns(true);
            ValuesController valuecontroller = new ValuesController(Drepo.Object);
            var result = valuecontroller.Post(not);

            var crObject = result as CreatedResult;
            Assert.NotNull(crObject);

            var model = crObject.Value as Student;
            Assert.Equal(not.StudentId, model.StudentId);
        }
        [Fact]
         public void PostById_Negative_ReturnsBadRequest()
        {
            var Drepo = new Mock<IKeepRepo>();
            Student not = new Student
            {
                StudentId = 4,
                Title = "createdpost"
            };
            Drepo.Setup(d => d.PostNote(not)).Returns(false);
            ValuesController valuecontroller = new ValuesController(Drepo.Object);
            var result = valuecontroller.Post(not);

            var brObject = result as BadRequestObjectResult;
            Assert.NotNull(brObject);
            
        }
        [Fact]
        public void PutById_Positive_ReturnsCreated()
        {
           var Drepo = new Mock<IKeepRepo>();
            Student not = new Student
            {
                StudentId = 4,
                Title = "createdpost"
            };
            int id = (int)not.StudentId;
            Drepo.Setup(d => d.PutNote(id, not)).Returns(true);
            ValuesController valuecontroller = new ValuesController(Drepo.Object);
            var result = valuecontroller.Put(id,not);

            var crObject = result as CreatedResult;
            Assert.NotNull(crObject);

            var model = crObject.Value as Student;
            Assert.Equal(id, model.StudentId);
        }
      [Fact]
    
        public void DeleteById_Positive_ReturnsCreated()
        {
            var Drepo = new Mock<IKeepRepo>();
            int id=1;
            Drepo.Setup(d => d.DeleteNote(id)).Returns(true);
            ValuesController valuecontroller = new ValuesController(Drepo.Object);
            var result = valuecontroller.Delete(id);

            var okObject = result as ObjectResult;
            Assert.NotNull(okObject);
        }


    }
}
 
