﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab1_ef;
using Newtonsoft.Json;
using lab4_igi.Models;
using Microsoft.AspNetCore.Http;
using Lab4.Models.Filters;
using Microsoft.AspNetCore.Authorization;

namespace lab4_igi.Controllers
{
    [Authorize]
    public class RoomsController : Controller
    {
        private readonly HotelContext _context;

        public RoomsController(HotelContext context)
        {
            _context = context;
        }

        // GET: Rooms
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var journal = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("Journal"));
            ViewBag.UserActions = journal;
            var hotelContext = _context.Rooms.Include(r => r.RoomType);
            return View(await hotelContext.ToListAsync());
        }
        
        public ActionResult SortedList(bool first, int? pageStart = 0)
        {
            var sortedList = _context.Rooms.ToList();
            if (first)
            {
                sortedList.Sort(new RoomsComparer());
            }
            if (pageStart != null)
                ViewBag.StartIndex = pageStart * 5;

            return PartialView("SortedList", sortedList);
        }

        public void SaveFiltration(string find, bool first)
        {
            var findingTextJSON = JsonConvert.SerializeObject(find);
            HttpContext.Session.SetString("Room.Finding", findingTextJSON);
            var filterFirstJSON = JsonConvert.SerializeObject(first.ToString());
            HttpContext.Session.SetString("Room.Filter.First", filterFirstJSON);
        }


        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.RoomType)
                .SingleOrDefaultAsync(m => m.RoomId == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            var roomString = HttpContext.Session.GetString("Room");
            var room = roomString == null ? new Room() : JsonConvert.DeserializeObject<Room>(roomString);
            ViewBag.RoomNo = room.RoomNo;
            ViewBag.Capacity = room.Capacity;
            ViewBag.RoomType = room.RoomType?.Name;
            ViewData["RoomTypeId"] = new SelectList(_context.RoomTypes, "Id", "Id");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoomsSaveForm]
        public async Task<IActionResult> Create([Bind("RoomId,RoomTypeId,RoomNo,Capacity,Description,Cost,CostDate")] Room room)
        {
            if (ModelState.IsValid)
            {
                room.CostDate = DateTime.Now;
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomTypeId"] = new SelectList(_context.RoomTypes, "Id", "Id", room.RoomTypeId);
            return View(room);
        }

        // GET: Rooms/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.SingleOrDefaultAsync(m => m.RoomId == id);
            if (room == null)
            {
                return NotFound();
            }
            ViewData["RoomTypeId"] = new SelectList(_context.RoomTypes, "Id", "Id", room.RoomTypeId);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,RoomTypeId,RoomNo,Capacity,Description,Cost,CostDate")] Room room)
        {
            if (id != room.RoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.RoomId))
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
            ViewData["RoomTypeId"] = new SelectList(_context.RoomTypes, "Id", "Id", room.RoomTypeId);
            return View(room);
        }

        // GET: Rooms/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.RoomType)
                .SingleOrDefaultAsync(m => m.RoomId == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.SingleOrDefaultAsync(m => m.RoomId == id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomId == id);
        }
    }
}
