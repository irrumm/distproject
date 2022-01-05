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
using WebApp.ViewModels.Feedbacks;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FeedbacksController : Controller
    {
        private readonly IAppBLL _bll;

        public FeedbacksController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: Feedbacks
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole("Admin"))
            { 
                return View(await _bll.Feedbacks.GetAllAsync(User.GetUserId()!.Value));
            }
            return View(await _bll.Feedbacks.GetAllAsync());
        }

        // TODO: Clients can only see their feedbacks' details
        // GET: Feedbacks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _bll.Feedbacks.FirstOrDefaultAsync(id.Value);
            
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // GET: Feedbacks/Create
        public async Task<IActionResult> Create()
        {
            var vm = new FeedbackCreateEditViewModel
            {
                GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title")
            };

            return View(vm);
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeedbackCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Feedback.AppUserId = User.GetUserId()!.Value;
                _bll.Feedbacks.Add(vm.Feedback);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title", vm.Feedback.GameInfoId);
            return View(vm);
        }

        // GET: Feedbacks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _bll.Feedbacks.FirstOrDefaultAsync(id.Value);
            
            if (feedback == null)
            {
                return NotFound();
            }

            var vm = new FeedbackCreateEditViewModel {Feedback = feedback};

            vm.GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title", vm.Feedback.GameInfoId);
            return View(vm);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FeedbackCreateEditViewModel vm)
        {
            if (id != vm.Feedback.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.Feedbacks.Update(vm.Feedback);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.GameInfoSelectList = new SelectList(await _bll.GameInfos.GetAllAsync(), "Id", "Title", vm.Feedback.GameInfoId);
            return View(vm);
        }

        // TODO: Client can delete their own feedbacks
        // GET: Feedbacks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _bll.Feedbacks.FirstOrDefaultAsync(id.Value);
            
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Feedbacks.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> FeedbackExists(Guid id)
        {
            return await _bll.Addresses.ExistsAsync(id);
        }
    }
}
