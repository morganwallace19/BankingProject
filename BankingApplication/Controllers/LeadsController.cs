using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankingApplication.Data;
using BankingApplication.Models;
using Microsoft.AspNetCore.Authorization;

namespace BankingApplication.Controllers
{
    // Ensures that the user can't view the leads models unless registered and logged in
    [Authorize]
    public class LeadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Leads
        public async Task<IActionResult> Index()
        {
            return View(await _context.BankLead.ToListAsync());
        }

        // GET: Leads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankLeadEntity = await _context.BankLead
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankLeadEntity == null)
            {
                return NotFound();
            }

            return View(bankLeadEntity);
        }

        // GET: Leads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Leads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Mobile,Email,Source")] BankLeadEntity bankLeadEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bankLeadEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bankLeadEntity);
        }

        // GET: Leads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankLeadEntity = await _context.BankLead.FindAsync(id);
            if (bankLeadEntity == null)
            {
                return NotFound();
            }
            return View(bankLeadEntity);
        }

        // POST: Leads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Mobile,Email,Source")] BankLeadEntity bankLeadEntity)
        {
            if (id != bankLeadEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankLeadEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankLeadEntityExists(bankLeadEntity.Id))
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
            return View(bankLeadEntity);
        }

        // GET: Leads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankLeadEntity = await _context.BankLead
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankLeadEntity == null)
            {
                return NotFound();
            }

            return View(bankLeadEntity);
        }

        // POST: Leads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bankLeadEntity = await _context.BankLead.FindAsync(id);
            if (bankLeadEntity != null)
            {
                _context.BankLead.Remove(bankLeadEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankLeadEntityExists(int id)
        {
            return _context.BankLead.Any(e => e.Id == id);
        }
    }
}
