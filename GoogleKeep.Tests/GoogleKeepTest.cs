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
                    Title = "Things to do",
                    checkLists = new List<CheckList>{
                        new CheckList{
                            CheckListId = 1,
                            CheckListName = "Submit assignment before 6:00PM",
                            StudentId = 1
                        }
                    },
                    labels = new List<Label>{
                        new Label{
                            LabelId = 1,
                            LabelName = "ASP.NETCore"
                        }
                    }
                },
                new Student{
                    StudentId = 2,
                    Title = "Trial",
                    checkLists = new List<CheckList>{
                        new CheckList{
                            CheckListId = 2,
                            CheckListName = "Try Xunit",
                            StudentId = 2
                        }
                    },
                    labels = new List<Label>{
                        new Label{
                            LabelId = 2,
                            LabelName = "Trial"
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
