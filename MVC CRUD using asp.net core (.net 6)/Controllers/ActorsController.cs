using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_CRUD_using_asp.net_core__.net_6_.Models;
using MVC_CRUD_using_asp.net_core__.net_6_.Models.Domain;
using MVC_CRUD_using_asp.net_core__.net_6_.Repositary;

namespace MVC_CRUD_using_asp.net_core__.net_6_.Controllers
{
    public class ActorsController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public ActorsController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var actors = await mvcDemoDbContext.Actors.ToListAsync();
            return View(actors);
        }



        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddActorViewModel addActorRequest)
        {
            var actor = new ActorModel()
            {
                Id = Guid.NewGuid(),
                Name = addActorRequest.Name,
                CarrierBest = addActorRequest.CarrierBest,
                Salary = addActorRequest.Salary
            };
            await mvcDemoDbContext.Actors.AddAsync(actor);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var act = await mvcDemoDbContext.Actors.FirstOrDefaultAsync(x=>x.Id== id);
            if(act != null)
            {
                var viewModel = new UpdateActorViewModel()
                {
                    Id = act.Id,
                    Name = act.Name,
                    CarrierBest = act.CarrierBest,
                    Salary = act.Salary

                };
                return await Task.Run(()=> View("View",viewModel));
            }
           
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> View(UpdateActorViewModel model)
        {
            var act = await mvcDemoDbContext.Actors.FindAsync(model.Id);
            if(act != null )
            {
                act.Name = model.Name;  
                act.CarrierBest = model.CarrierBest;
                act.Salary = model.Salary;

                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateActorViewModel model)
        {
            var act = await mvcDemoDbContext.Actors.FindAsync(model.Id);
            if(act != null)
            {
                mvcDemoDbContext.Actors.Remove(act);
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


    }
}
