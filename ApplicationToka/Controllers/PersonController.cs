using ApplicationToka.ViewModel;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Core.DTOs;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationToka.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonsService personsService;
        private readonly IMapper mapper;
        public INotyfService notifyService { get; }

        public PersonController(IPersonsService personsService, IMapper mapper, INotyfService notifyService)
        {
            this.personsService = personsService;
            this.notifyService = notifyService;
            this.mapper = mapper;
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
            var persons = personsService.GetAllPersons();

            return View(persons);
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = personsService.GetPerson(id);
            var model = mapper.Map<PersonViewModel>(person);

            return View(model);
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        public IActionResult Create(PersonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var personDTO = new PersonDTO
                {
                    Nombre = model.Nombre,
                    ApellidoPaterno = model.ApellidoPaterno,
                    ApellidoMaterno = model.ApellidoMaterno,
                    RFC = model.RFC,
                    FechaNacimiento = model.FechaNacimiento,
                    Activo = model.Activo
                };
                personsService.AddPerson(personDTO);

                notifyService.Success("La persona se a creado correctamente");

                return RedirectToAction(nameof(Index));
            }

            notifyService.Warning("Ha ocurrido un error, favor de revisar sus datos");

            return View(model);
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                notifyService.Warning("No se encontro el usuario");
                return NotFound();
            }
            var person = personsService.GetPerson(id);
            var model = mapper.Map<PersonViewModel>(person);

            return View(model);
        }

        // POST: Person/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonViewModel model)
        {
            if (!model.IdPersonaFisica.HasValue)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var personDTO = new PersonDTO
                    {
                        IdPersonaFisica = model.IdPersonaFisica.Value,
                        Nombre = model.Nombre,
                        ApellidoPaterno = model.ApellidoPaterno,
                        ApellidoMaterno = model.ApellidoMaterno,
                        RFC = model.RFC,
                        FechaNacimiento = model.FechaNacimiento,
                        Activo = model.Activo
                    };
                    personsService.UpdatePerson(personDTO);

                    notifyService.Success("La actualización se ha realizado con éxito");

                }
                catch (Exception ex)
                {
                    notifyService.Error("Ha ocurrido un error, error: \n" + ex.Message);
                }

                return RedirectToAction(nameof(Index));
            }

            notifyService.Warning("Ha ocurrido un error, favor de revisar sus datos");

            return View(model);
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            personsService.ChangeStatusPerson(id, 0);

            notifyService.Success("Usuario eliminado correctamente");

            return RedirectToAction(nameof(Index));
        }
    }
}
