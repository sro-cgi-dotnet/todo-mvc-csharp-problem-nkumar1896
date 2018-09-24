using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFCoreTutorials;
namespace EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        IKeepRepo context = null;
        public ValuesController(IKeepRepo _context){
            this.context = _context;
        }
    
    [HttpGet]
        public IActionResult Get()
        {
            
          
            var notes = context.GetAllNotes();
            if(notes==null)
            {
                return NotFound("null database");
            }
            else
            {

            return Ok(notes);
            }
            
           
            
        }
       

        // GET api/values/5
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            //return "value";
            var noteById = context.GetNote(id);
            if (noteById != null)
            {
                return Ok(noteById);
            }
            else
            {
                return NotFound($"Note with {id} not found.");
            }
        }
        [HttpGet("{text}")]
        public IActionResult Get(string text , [FromQuery] string type)
        {
            //return "value";
            List<Student> noteBytext=context.GetNote(text,type);
           // var noteById1 = context.GetNote(text,type);
            if (noteBytext == null)
            {
                return BadRequest($"Type : {type} or Text : {text}  is invalid. Please try again");
            }
            else if(noteBytext.Count==0)
            {
                return NotFound($"Note with {text} not found.");
            }
            else{
                return Ok(noteBytext);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Student value)
        {
           if(ModelState.IsValid){
                bool result = context.PostNote(value);
                if (result)
                {
                    return Created($"/values/{value.StudentId}",value);
                }
                else
                {
                    return BadRequest("Note already exists, please try again.");
                }
            }
            return BadRequest("Invalid Format");
    
    // or
    // context.Add<Student>(std);
    
    }


        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Student value)
        {
            if(ModelState.IsValid){
                bool result = context.PutNote(id, value);
                if(result){
                    return Created("/api/values", value);
                }
                else{
                    return NotFound($"Note with {id} not found.");
                }
            }
            return BadRequest("Invalid Format");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool result = context.DeleteNote(id);
            if(result){
                return Ok($"note with id : {id} deleted succesfully");
            }
            else{
                return NotFound($"Note with {id} not found.");
            }
        }
    }
}
