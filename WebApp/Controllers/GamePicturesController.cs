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
using WebApp.ViewModels.GamePictures;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GamePicturesController : Controller
    {
        private readonly IAppBLL _bll;

        public GamePicturesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: GamePictures
        public async Task<IActionResult> Index()
        {
            return View(await _bll.GamePictures.GetAllAsync());
        }

        // GET: GamePictures/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _bll.GamePictures.FirstOrDefaultAsync(id.Value);
            
            if (picture == null)
            {
                return NotFound();
            }

            return View(picture);
        }

        // GET: GamePictures/Create
        public async Task<IActionResult> Create()
        {
            var vm = new GamePictureCreateEditViewModel
            {
                GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title")
            };
            return View(vm);
        }

        // POST: GamePictures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GamePictureCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.GamePictures.Add(vm.GamePicture);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title", vm.GamePicture.GameInfoId);
            return View(vm);
        }

        // GET: GamePictures/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePicture = await _bll.GamePictures.FirstOrDefaultAsync(id.Value);
            
            if (gamePicture == null)
            {
                return NotFound();
            }

            var vm = new GamePictureCreateEditViewModel {GamePicture = gamePicture};

            vm.GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title", vm.GamePicture.GameInfoId);
            return View(vm);
        }

        // POST: GamePictures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GamePictureCreateEditViewModel vm)
        {
            if (id != vm.GamePicture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.GamePictures.Update(vm.GamePicture);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title", vm.GamePicture.GameInfoId);
            return View(vm);
        }

        // GET: GamePictures/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePicture = await _bll.GamePictures.FirstOrDefaultAsync(id.Value);
            
            if (gamePicture == null)
            {
                return NotFound();
            }

            return View(gamePicture);
        }

        // POST: GamePictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.GamePictures.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GamePictureExists(Guid id)
        {
            return await _bll.GamePictures.ExistsAsync(id);
        }
    }
}
