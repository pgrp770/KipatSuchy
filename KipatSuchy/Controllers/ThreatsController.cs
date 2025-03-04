﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KipatSuchy.Data;
using KipatSuchy.Models;
using KipatSuchy.Services;

namespace KipatSuchy.Controllers
{
    public class ThreatsController : Controller
    {
        private readonly KipatSuchyContext _context;
        private readonly AttackHandlerService _attackHandlerService; 

        public ThreatsController(KipatSuchyContext context)
        {
            _context = context;
            _attackHandlerService = new AttackHandlerService();

             }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Threats.ToListAsync());
        }

        [HttpGet]
        [Route("Threats/StartAttack/{threatId}")]
        public async Task<IActionResult> StartAttack(int threatId)
        {
            var a = _context.Threats.Where(x => x.Id == threatId).FirstOrDefault();
            a.IsActive = true;
            await _attackHandlerService.RegisterAndRunAttackTask(a);
            a.IsActive = false;
            a.puzaz = true;
            a.Amount -= 1;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index), a);
        }

        // GET: Threats

        // GET: Threats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var threat = await _context.Threats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (threat == null)
            {
                return NotFound();
            }

            return View(threat);
        }

        // GET: Threats/Create
        public IActionResult Create()
        {
            
            ViewData["Origins"] = new SelectList(HelperData.Origins.Keys.ToList());
            ViewData["Weapon"] = new SelectList(HelperData.Veapons.Keys.ToList());
            return View();
        }

        // POST: Threats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,Weapon,Origin,IsActive,LaunchTime")] Threat threat)
        {
                threat.IsActive = false;
            if (ModelState.IsValid)
            {
                _context.Add(threat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(threat);
        }

        // GET: Threats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var threat = await _context.Threats.FindAsync(id);
            if (threat == null)
            {
                return NotFound();
            }
            return View(threat);
        }

        // POST: Threats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,Weapon,Origin,IsActive,LaunchTime")] Threat threat)
        {
            if (id != threat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(threat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThreatExists(threat.Id))
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
            return View(threat);
        }

        // GET: Threats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var threat = await _context.Threats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (threat == null)
            {
                return NotFound();
            }

            return View(threat);
        }

        // POST: Threats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var threat = await _context.Threats.FindAsync(id);
            if (threat != null)
            {
                _context.Threats.Remove(threat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThreatExists(int id)
        {
            return _context.Threats.Any(e => e.Id == id);
        }
    }
}
