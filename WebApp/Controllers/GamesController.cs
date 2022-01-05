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
using WebApp.ViewModels.Games;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GamesController : Controller
    {
        private readonly IAppBLL _bll;

        public GamesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Games.GetAllAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _bll.Games.FirstOrDefaultAsync(id.Value);
            
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public async Task<IActionResult> Create()
        {
            var vm = new GameCreateEditViewModel
            {
                GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title")
            };
            return View(vm);
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.Games.Add(vm.Game);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title", vm.Game.GameInfoId);
            return View(vm);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _bll.Games.FirstOrDefaultAsync(id.Value);
            
            if (game == null)
            {
                return NotFound();
            }

            var vm = new GameCreateEditViewModel {Game = game};

            vm.GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title", vm.Game.GameInfoId);
            return View(vm);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GameCreateEditViewModel vm)
        {
            if (id != vm.Game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Games.Update(vm.Game);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GameExists(vm.Game.Id))
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
            vm.GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title", vm.Game.GameInfoId);
            return View(vm);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _bll.Games.FirstOrDefaultAsync(id.Value);
            
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Games.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GameExists(Guid id)
        {
            return await _bll.Games.ExistsAsync(id);
        }
    }
}
