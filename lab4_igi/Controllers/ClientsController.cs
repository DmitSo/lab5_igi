using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab1_ef;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using lab4_igi.Models;
using Lab4.Models.Filters;
using Microsoft.AspNetCore.Authorization;

namespace lab4_igi.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private readonly HotelContext _context;

        public ClientsController(HotelContext context)
        {
            _context = context;
        }

        // GET: Clients
        [Authorize]
        public async Task<IActionResult> Index()
        {
            Initializer.Initialize(_context);
            var journal = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("Journal"));
            ViewBag.UserActions = journal;

            var hotelContext = _context.Clients.Include(c => c.Room);
            return View(await hotelContext.ToListAsync());
        }

        public ActionResult SortedList(bool first, int? pageStart = 0)
        {
            var sortedList = _context.Clients.ToList();
            if (first)
            {
                sortedList.Sort(new ClientComparer());
            }
            if (pageStart != null)
                ViewBag.StartIndex = pageStart * 5;

            return PartialView("SortedList", sortedList);
        }

        public void SaveFiltration(string find, bool first, bool second)
        {
            var findingTextJSON = JsonConvert.SerializeObject(find);
            HttpContext.Session.SetString("Clients.Finding", findingTextJSON);
            var filterFirstJSON = JsonConvert.SerializeObject(first.ToString());
            HttpContext.Session.SetString("Clients.Filter.First", filterFirstJSON);
            var filterSecondJSON = JsonConvert.SerializeObject(second.ToString());
            HttpContext.Session.SetString("Clients.Filter.Second", filterSecondJSON);
        }


        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Room)
                .SingleOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            var instanceStr = HttpContext.Session.GetString("Client");
            var instance = instanceStr == null ?
                new Client() :
                JsonConvert.DeserializeObject<Client>(instanceStr);

            ViewBag.Name = instance.Name;
            ViewBag.RoomId = instance.RoomId;
            ViewBag.Passport = instance.Passport;
            ViewBag.DepartureDate = instance.DepartureDate;

            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClientsSaveForm]
        public async Task<IActionResult> Create([Bind("ClientId,RoomId,Name,Passport,OccupancyDate,DepartureDate")] Client client)
        {
            if (ModelState.IsValid)
            {
                client.DepartureDate = DateTime.Now;
                client.OccupancyDate = DateTime.Now + new TimeSpan(7, 0, 0,0);
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", client.RoomId);
            return View(client);
        }

        // GET: Clients/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.SingleOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", client.RoomId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,RoomId,Name,Passport,OccupancyDate,DepartureDate")] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
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
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", client.RoomId);
            return View(client);
        }

        // GET: Clients/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Room)
                .SingleOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.SingleOrDefaultAsync(m => m.ClientId == id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }
    }
}
