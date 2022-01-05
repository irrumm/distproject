using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using BLL.App.DTO;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels.GameInfos;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GameInfosController : Controller
    {
        private readonly IAppBLL _bll;

        public GameInfosController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
        }

        // GET: GameInfos
        public async Task<IActionResult> Index()
        {
            return View(await _bll.GameInfos.GetAllAsync());
        }

        // GET: GameInfos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameInfo = await _bll.GameInfos.FirstOrDefaultAsync(id.Value);
            
            if (gameInfo == null)
            {
                return NotFound();
            }

            return View(gameInfo);
        }

        // GET: GameInfos/Create
        public async Task<IActionResult> Create()
        {
            var vm = new GameInfoCreateEditViewModel
            {
                LanguageSelectList = new SelectList(await _bll.Languages.GetAllAsync(), "Id", "Name"),
                PublisherSelectList = new SelectList(await _bll.Publishers.GetAllAsync(), "Id", "Name")
            };
            return View(vm);
        }

        // POST: GameInfos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameInfoCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.GameInfos.Add(vm.GameInfo);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.LanguageSelectList = new SelectList(await _bll.Languages.GetAllAsync(), "Id", "Name", vm.GameInfo.LanguageId);
            vm.PublisherSelectList = new SelectList(await _bll.Publishers.GetAllAsync(), "Id", "Name", vm.GameInfo.PublisherId);
            return View(vm);
        }

        // GET: GameInfos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameInfo = await _bll.GameInfos.FirstOrDefaultAsync(id.Value);
            
            if (gameInfo == null)
            {
                return NotFound();
            }

            var vm = new GameInfoCreateEditViewModel();
            vm.GameInfo = gameInfo;
            
            vm.LanguageSelectList = new SelectList(await _bll.Languages.GetAllAsync(), "Id", "Name", vm.GameInfo.LanguageId);
            vm.PublisherSelectList = new SelectList(await _bll.Publishers.GetAllAsync(), "Id", "Name", vm.GameInfo.PublisherId);
            return View(vm);
        }

        // POST: GameInfos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GameInfoCreateEditViewModel vm)
        {
            if (id != vm.GameInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.GameInfos.Update(vm.GameInfo);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.LanguageSelectList = new SelectList(await _bll.Languages.GetAllAsync(), "Id", "Name", vm.GameInfo.LanguageId);
            vm.PublisherSelectList = new SelectList(await _bll.Publishers.GetAllAsync(), "Id", "Name", vm.GameInfo.PublisherId);
            
            return View(vm);
        }

        // GET: GameInfos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameInfo = await _bll.GameInfos.FirstOrDefaultAsync(id.Value);
            
            if (gameInfo == null)
            {
                return NotFound();
            }

            return View(gameInfo);
        }

        // POST: GameInfos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.GameInfos.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GameInfoExists(Guid id)
        {
            return await _bll.GameInfos.ExistsAsync(id);
        }
    }
}
