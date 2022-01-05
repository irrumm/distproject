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
using WebApp.ViewModels.GameAwards;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GameAwardsController : Controller
    {
        private readonly IAppBLL _bll;

        public GameAwardsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: GameAwards
        public async Task<IActionResult> Index()
        {
            return View(await _bll.GameAwards.GetAllAsync());
        }

        // GET: GameAwards/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameAward = await _bll.GameAwards.FirstOrDefaultAsync(id.Value);
            
            if (gameAward == null)
            {
                return NotFound();
            }

            return View(gameAward);
        }

        // GET: GameAwards/Create
        public async Task<IActionResult> Create()
        {
            var vm = new GameAwardCreateEditViewModel
            {
                AwardSelectList = new SelectList(await _bll.Awards.GetAllAsync(), "Id", "Name"),
                GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title")
            };

            return View(vm);
        }

        // POST: GameAwards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameAwardCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.GameAwards.Add(vm.GameAward);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.AwardSelectList = new SelectList(await _bll.Awards.GetAllAsync(), "Id", "Name", vm.GameAward.AwardId);
            vm.GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title", vm.GameAward.GameInfoId);
            return View(vm);
        }

        // GET: GameAwards/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameAward = await _bll.GameAwards.FirstOrDefaultAsync(id.Value);
            
            if (gameAward == null)
            {
                return NotFound();
            }

            var vm = new GameAwardCreateEditViewModel {GameAward = gameAward};

            vm.AwardSelectList = new SelectList(await _bll.Awards.GetAllAsync(), "Id", "Name", vm.GameAward.AwardId);
            vm.GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title", vm.GameAward.GameInfoId);
            return View(vm);
        }

        // POST: GameAwards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GameAwardCreateEditViewModel vm)
        {
            if (id != vm.GameAward.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.GameAwards.Update(vm.GameAward);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GameAwardExists(vm.GameAward.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            vm.AwardSelectList = new SelectList(await _bll.Awards.GetAllAsync(), "Id", "Name", vm.GameAward.AwardId);
            vm.GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title", vm.GameAward.GameInfoId);
            return View(vm);
        }

        // GET: GameAwards/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameAward = await _bll.GameAwards.FirstOrDefaultAsync(id.Value);
            
            if (gameAward == null)
            {
                return NotFound();
            }

            return View(gameAward);
        }

        // POST: GameAwards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.GameAwards.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GameAwardExists(Guid id)
        {
            return await _bll.GameAwards.ExistsAsync(id);
        }
    }
}
