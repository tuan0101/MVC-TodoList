﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_TodoList.Models;
using Microsoft.EntityFrameworkCore;

namespace MVC_TodoList.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Item Item { get; set; }
        public ItemsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int? id)
        {
            Item = new Item();
            if (id == null)
            {
                //create
                return View(Item);
            }
            // else update
            Item = _db.Items.FirstOrDefault(Item => Item.Id == id);
            if (Item == null)
            {
                return NotFound();
            }
            return View(Item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            if (ModelState.IsValid)
            {
                if (Item.Id == 0)
                {
                    //create
                    _db.Items.Add(Item);
                }
                else
                {
                    _db.Items.Update(Item);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Item);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Items.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _db.Items.FirstOrDefaultAsync(item => item.Id == id);
            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Items.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}
