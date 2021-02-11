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
    public class GlumciController : Controller
    {
        private KazalisteManagerDbContext _dbContext;

        public GlumciController(KazalisteManagerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var ansambl = this._dbContext.Glumci.Include(p => p.Diplomirao).ToList();

            return View(ansambl);
        }

        [AllowAnonymous]
        [Route("detalji-o-glumcu/{selected:int:min(1):max(99)?}")]
        public IActionResult Details(int? id = null)
        {
            var glumac = this._dbContext.Glumci
                .Include(p => p.Diplomirao)
                .Where(p => p.Id == id)
                .FirstOrDefault();

            return View(glumac);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            this.FillDropdownValues();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Glumac model)
        {
            if (ModelState.IsValid)
            {
                this._dbContext.Glumci.Add(model);
                this._dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            this.FillDropdownValues();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [ActionName(nameof(Edit))]
        public IActionResult Edit(int id)
        {
            var model = this._dbContext.Glumci.FirstOrDefault(c => c.Id == id);
            this.FillDropdownValues();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> EditPost(int id)
        {
            var client = this._dbContext.Glumci.FirstOrDefault(c => c.Id == id);
            var ok = await this.TryUpdateModelAsync(client);

            if (ok && this.ModelState.IsValid)
            {
                this._dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            this.FillDropdownValues();
            return View();
        }

        private void FillDropdownValues()
        {
            var selectItems = new List<SelectListItem>();

            //Polje je opcionalno
            var listItem = new SelectListItem();
            listItem.Text = "- odaberite -";
            listItem.Value = "";
            selectItems.Add(listItem);

            foreach (var category in this._dbContext.Akademije)
            {
                listItem = new SelectListItem(category.Naziv, category.Id.ToString());
                selectItems.Add(listItem);
            }

            ViewBag.PossibleAkadem = selectItems;
        }
    }
}