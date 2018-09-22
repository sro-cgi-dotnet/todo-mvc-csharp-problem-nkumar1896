using System;
using Xunit;
using Moq;
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
        public void GetAll_Positive()
        {
            var Drepo = new Mock<IKeepRepo>();
            List<Student> not = GetMockDatabase();
            Drepo.Setup(d => d.GetAllNotes()).Returns(not);
            ValuesController valuecontroller = new ValuesController(Drepo.Object);
            var result = valuecontroller.Get();
            Assert.NotNull(result);
            Assert.Equal(2 , not.Count);
        }
    }
}
