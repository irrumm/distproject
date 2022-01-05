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
using WebApp.ViewModels.Orders;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly IAppBLL _bll;

        public OrdersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole("Admin"))
            { 
                return View(await _bll.Orders.GetAllAsync(User.GetUserId()!.Value));
            }
            return View(await _bll.Orders.GetAllAsync());
        }

        // TODO: Clients can only see their orders' details
        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _bll.Orders.FirstOrDefaultAsync(id.Value);
            
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            var vm = new OrderCreateEditViewModel
            {
                AddressSelectList = 
                    new SelectList(await _bll.Addresses.GetAllAsync(), "Id", "City"),
                PaymentMethodSelectList =
                    new SelectList(await _bll.PaymentMethods.GetAllAsync(), "Id", "Description")
            };
            return View(vm);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Orders.AppUserId = User.GetUserId()!.Value;
                _bll.Orders.Add(vm.Orders);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.AddressSelectList = new SelectList(await _bll.Addresses.GetAllAsync(), "Id", "City", vm.Orders.AddressId);
            vm.PaymentMethodSelectList = new SelectList(await _bll.PaymentMethods.GetAllAsync(), "Id", "Description", vm.Orders.PaymentMethodId);
            return View(vm);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, OrderCreateEditViewModel vm)
        {
            if (id != vm.Orders.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.Orders.Update(vm.Orders);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.AddressSelectList = 
                new SelectList(await _bll.Addresses.GetAllAsync(), "Id", "City", vm.Orders.AddressId);
            vm.PaymentMethodSelectList = 
                new SelectList(await _bll.PaymentMethods.GetAllAsync(), "Id", "Description", vm.Orders.PaymentMethodId);
            return View(vm);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _bll.Orders.FirstOrDefaultAsync(id.Value);
            
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Orders.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> OrdersExists(Guid id)
        {
            return await _bll.Orders.ExistsAsync(id);
        }
    }
}
