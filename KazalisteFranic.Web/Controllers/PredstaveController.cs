using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KazalisteFranic.DAL;
using KazalisteFranic.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KazalisteFranic.Web.Controllers
{
    public class PredstaveController : Controller
    {
        private KazalisteManagerDbContext _dbContext;

        public PredstaveController(KazalisteManagerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var predstave = this._dbContext.Predstave
                .Include(p => p.GlumacPredstave)
                .Include(p => p.Redatelj)
                .ToList();

            return View(predstave);
        }

        [AllowAnonymous]
        public IActionResult Details(int? id = null)
        {
            var predstava = this._dbContext.Predstave
                .Include(p => p.Redatelj)
                .Include(p => p.GlumacPredstave)
                .Where(p => p.Id == id)
                .FirstOrDefault();

            return View(predstava);
        }

        [AllowAnonymous]
        public IActionResult DetailsRedatelj(int? id = null)
        {
            var redatelj = this._dbContext.Redatelji
                .Include(p => p.Diplomirao)
                .Where(p => p.Id == id)
                .FirstOrDefault();

            SvePredstaveRedatelja(id);

            return View(redatelj);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            this.FillDropdownRedatelji();
            this.FillDropdownGlumci();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Predstava model)
        {
            if (ModelState.IsValid)
            {
                this._dbContext.Predstave.Add(model);
                this._dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            this.FillDropdownRedatelji();
            this.FillDropdownGlumci();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [ActionName(nameof(Edit))]
        public IActionResult Edit(int id)
        {
            var model = this._dbContext.Predstave.FirstOrDefault(c => c.Id == id);
            this.FillDropdownRedatelji();
            this.FillDropdownGlumci();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> EditPost(int id)
        {
            var client = this._dbContext.Predstave.FirstOrDefault(c => c.Id == id);
            var ok = await this.TryUpdateModelAsync(client);

            if (ok && this.ModelState.IsValid)
            {
                this._dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            this.FillDropdownRedatelji();
            this.FillDropdownGlumci();
            return View();
        }

        private void FillDropdownRedatelji()
        {
            var selectItems = new List<SelectListItem>();

            //Polje je opcionalno
            var listItem = new SelectListItem();
            listItem.Text = "- odaberite -";
            listItem.Value = "";
            selectItems.Add(listItem);

            foreach (var category in this._dbContext.Redatelji)
            {
                listItem = new SelectListItem(category.Prezime, category.Id.ToString());
                selectItems.Add(listItem);
            }

            ViewBag.PossibleRedatelj = selectItems;
        }

        private void FillDropdownGlumci()
        {
            var selectItems = new List<SelectListItem>();

            //Polje je opcionalno
            var listItem = new SelectListItem();
            listItem.Text = "- odaberite -";
            listItem.Value = "";
            selectItems.Add(listItem);

            foreach (var category in this._dbContext.Glumci)
            {
                listItem = new SelectListItem(category.Prezime, category.Id.ToString());
                selectItems.Add(listItem);
            }

            ViewBag.PossibleGlumac = selectItems;
        }

        private void SvePredstaveRedatelja(int? id)
        {
            var selectItems = new List<String>();

            foreach (var category in this._dbContext.Predstave)
            {
                if (category.RedateljId == id)
                {
                    string naziv = category.Naslov;
                    selectItems.Add(naziv);
                }
                                                                     
            }
            ViewBag.SvePredstaveRedatelja = selectItems;
        }

        private void SviGlumciPredstave(int? id)
        {
            var selectItems = new List<String>();

            foreach (var predstava in this._dbContext.GlumacPredstave)
            {
                if (predstava.PredstavaId == id)
                {
                    foreach (var glumac in this._dbContext.Glumci)
                    {
                        if (glumac.Id == predstava.GlumacId)
                        {
                            string ime = glumac.FullIme;
                            selectItems.Add(ime);
                        }
                    }                    
                }

            }
            ViewBag.SviGlumciPredstave = selectItems;
        }
    }
}