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
using WebApp.ViewModels.OrderLines;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderLinesController : Controller
    {
        private readonly IAppBLL _bll;
        // TODO: Clients can create orderlines and edit/delete them? or does the system do it?

        public OrderLinesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: OrderLines
        public async Task<IActionResult> Index()
        {
            return View(await _bll.OrderLines.GetAllAsync());
        }

        // GET: OrderLines/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderLine = await _bll.OrderLines.FirstOrDefaultAsync(id.Value);
            
            if (orderLine == null)
            {
                return NotFound();
            }

            return View(orderLine);
        }

        // GET: OrderLines/Create
        public async Task<IActionResult> Create()
        {
            var vm = new OrderLineCreateEditViewModel
            {
                GameSelectList = new SelectList(await _bll.Games.GetAllAsync(), "Id", "Id")
            };
            return View(vm);
        }

        // POST: OrderLines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderLineCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.OrderLines.Add(vm.OrderLine);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.GameSelectList = new SelectList(await _bll.Games.GetAllAsync(), "Id", "Id", vm.OrderLine.GameId);
            return View(vm);
        }

        // GET: OrderLines/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderLine = await _bll.OrderLines.FirstOrDefaultAsync(id.Value);
            
            if (orderLine == null)
            {
                return NotFound();
            }

            var vm = new OrderLineCreateEditViewModel {OrderLine = orderLine};

            vm.GameSelectList = new SelectList(await _bll.Games.GetAllAsync(), "Id", "Id", vm.OrderLine.GameId);
            return View(vm);
        }

        // POST: OrderLines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, OrderLineCreateEditViewModel vm)
        {
            if (id != vm.OrderLine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.OrderLines.Update(vm.OrderLine);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await OrderLineExists(vm.OrderLine.Id))
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
            vm.GameSelectList = new SelectList(await _bll.Games.GetAllAsync(), "Id", "Id", vm.OrderLine.GameId);
            return View(vm);
        }

        // GET: OrderLines/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderLine = await _bll.OrderLines.FirstOrDefaultAsync(id.Value);
            
            if (orderLine == null)
            {
                return NotFound();
            }

            return View(orderLine);
        }

        // POST: OrderLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.OrderLines.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> OrderLineExists(Guid id)
        {
            return await _bll.OrderLines.ExistsAsync(id);
        }
    }
}
