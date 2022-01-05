using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using BLL.App.DTO;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels.GameCategories;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GameCategoriesController : Controller
    {
        private readonly IAppBLL _bll;

        public GameCategoriesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: GameCategories
        public async Task<IActionResult> Index()
        {
            return View(await _bll.GameCategories.GetAllAsync());
        }

        // GET: GameCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameCategory = await _bll.GameCategories.FirstOrDefaultAsync(id.Value);
            
            if (gameCategory == null)
            {
                return NotFound();
            }

            return View(gameCategory);
        }

        // GET: GameCategories/Create
        public async Task<IActionResult> Create()
        {
            var vm = new GameCategoryCreateEditViewModel
            {
                CategorySelectList = new SelectList(await _bll.Categories.GetAllAsync(), "Id", "Name"),
                GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title")
            };
            return View(vm);
        }

        // POST: GameCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameCategoryCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.GameCategories.Add(vm.GameCategory);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.CategorySelectList = new SelectList(await _bll.Categories.GetAllAsync(), "Id", "Name", vm.GameCategory.CategoryId);
            vm.GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title", vm.GameCategory.GameInfoId);
            return View(vm);
        }

        // GET: GameCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameCategory = await _bll.GameCategories.FirstOrDefaultAsync(id.Value);
            
            if (gameCategory == null)
            {
                return NotFound();
            }

            var vm = new GameCategoryCreateEditViewModel {GameCategory = gameCategory};
            
            vm.CategorySelectList = new SelectList(await _bll.Categories.GetAllAsync(), "Id", "Name", vm.GameCategory.CategoryId);
            vm.GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title", vm.GameCategory.GameInfoId);
            return View(vm);
        }

        // POST: GameCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GameCategoryCreateEditViewModel vm)
        {
            if (id != vm.GameCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.GameCategories.Update(vm.GameCategory);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.CategorySelectList = new SelectList(await _bll.Categories.GetAllAsync(), "Id", "Name", vm.GameCategory.CategoryId);
            vm.GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title", vm.GameCategory.GameInfoId);
            return View(vm);
        }

        // GET: GameCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameCategory = await _bll.GameCategories.FirstOrDefaultAsync(id.Value);
            
            if (gameCategory == null)
            {
                return NotFound();
            }

            return View(gameCategory);
        }

        // POST: GameCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.GameCategories.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GameCategoryExists(Guid id)
        {
            return await _bll.GameCategories.ExistsAsync(id);
        }
    }
}
