using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KipatSuchy.Data;
using KipatSuchy.Models;
using KipatSuchy.ViewModels;
using System.Threading;

namespace KipatSuchy.Controllers
{
    public class ResponsesController : Controller
    {
        private readonly KipatSuchyContext _context;

        public ResponsesController(KipatSuchyContext context)
        {
            _context = context;
        }

        // GET: Responses
        public async Task<IActionResult> Index()
        {
            var Managers = await _context.Manager.ToListAsync();
            var Threats = await _context.Threats.Where(t => t.IsActive).ToListAsync();
            var kipatSuchyContext = await _context.Responses.Include(r => r.Manager).Include(r => r.Threat).ToListAsync();
            AddResponseVM addResponseVM = new AddResponseVM()
            {
                Threats = Threats,
                Responses = kipatSuchyContext,
                Managers = Managers
            };
            
            return View(addResponseVM);
        }

        // GET: Responses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _context.Responses
                .Include(r => r.Manager)
                .Include(r => r.Threat)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        // GET: Responses/Create
        public IActionResult Create()
        {
            ViewData["ManagerId"] = new SelectList(_context.Manager, "Id", "Id");
            ViewData["ThreatId"] = new SelectList(_context.Threats, "Id", "Id");
            return View();
        }

        // POST: Responses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ThreatId,ManagerId")] Response response)
        {
            if (ModelState.IsValid)
            {
                _context.Add(response);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManagerId"] = new SelectList(_context.Manager, "Id", "Id", response.ManagerId);
            ViewData["ThreatId"] = new SelectList(_context.Threats, "Id", "Id", response.ThreatId);
            return View(response);
        }
        public async Task<IActionResult> Yirut(int id)
        {
            Response response = new Response()
            {
                ThreatId = id,
                ManagerId = 1
            };
            _context.Add(response);
            await _context.SaveChangesAsync();
            var Managers = await _context.Manager.ToListAsync();
            var Threats = await _context.Threats.Where(t => t.IsActive).ToListAsync();
            var kipatSuchyContext = await _context.Responses.Include(r => r.Manager).Include(r => r.Threat).ToListAsync();
            AddResponseVM addResponseVM = new AddResponseVM()
            {
                Threats = Threats,
                Responses = kipatSuchyContext,
                Managers = Managers
            };
            addResponseVM.Managers[0].LeftMissile -= 1;

            await _context.SaveChangesAsync();
            return View("Index", (addResponseVM));
        }



        // POST: Responses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ThreatId,ManagerId")] Response response)
        {
            if (id != response.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(response);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResponseExists(response.Id))
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
            ViewData["ManagerId"] = new SelectList(_context.Manager, "Id", "Id", response.ManagerId);
            ViewData["ThreatId"] = new SelectList(_context.Threats, "Id", "Id", response.ThreatId);
            return View(response);
        }

        // GET: Responses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _context.Responses
                .Include(r => r.Manager)
                .Include(r => r.Threat)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        // POST: Responses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _context.Responses.FindAsync(id);
            if (response != null)
            {
                _context.Responses.Remove(response);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResponseExists(int id)
        {
            return _context.Responses.Any(e => e.Id == id);
        }
    }
}
