using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHero.Data;
using System;

namespace SuperHero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
        
        return Ok(await _context.SuperHeros.ToListAsync());
    }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHeroById(int id)
        {
            var hero = await _context.SuperHeros.FindAsync(id);
            if(hero == null)
             return BadRequest("Hero Not Found"); 
           
                return Ok(hero);
          
           
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero _superhero)
        {
            _context.Add(_superhero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeros.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero _superhero)
        {
            var hero = await _context.SuperHeros.FindAsync(_superhero.Id);
            if (hero == null)
                return BadRequest("Hero Not Found");
            else
            {
                hero.firstName = _superhero.firstName;
                hero.LastName = _superhero.LastName;
                hero.Name = _superhero.Name;
                hero.Place = _superhero.Place;
            }
                _context.Update(hero);
            await _context.SaveChangesAsync();
            
            return Ok(await _context.SuperHeros.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = await _context.SuperHeros.FindAsync(id);
            if (hero == null)
               
                return BadRequest("Hero Not Found");
            else
            {
                _context.Remove(hero);
              await  _context.SaveChangesAsync();
                return Ok(await _context.SuperHeros.ToListAsync());
            }
            
        }



        void test ()
        {
            Action<object> action = (object obj) =>
            {
                Console.WriteLine("Task={0}, obj={1}, Thread={2}",
                Task.CurrentId, obj,
                Thread.CurrentThread.ManagedThreadId);
            };
            Task t1 = new Task(action, "alpha");

            // Construct a started task
            Task t2 = Task.Factory.StartNew(action, "beta");
            // Block the main thread to demonstrate that t2 is executing
            t2.Wait();

            // Launch t1 
            t1.Start();
            Console.WriteLine("t1 has been launched. (Main Thread={0})",
                              Thread.CurrentThread.ManagedThreadId);
            // Wait for the task to finish.
            t1.Wait();

            // Construct a started task using Task.Run.
            String taskData = "delta";
            Task t3 = Task.Run(() => {
                Console.WriteLine("Task={0}, obj={1}, Thread={2}",
                                                         Task.CurrentId, taskData,
                                                          Thread.CurrentThread.ManagedThreadId);
            });
            // Wait for the task to finish.
            t3.Wait();

            // Construct an unstarted task
            Task t4 = new Task(action, "gamma");
            // Run it synchronously
            t4.RunSynchronously();
            // Although the task was run synchronously, it is a good practice
            // to wait for it in the event exceptions were thrown by the task.
            t4.Wait();
        }

    }
}
