using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CDUCommunityMusic.Data;
using CDUCommunityMusic.Models;
using Razor.Templating.Core;

namespace CDUCommunityMusic.Controllers
{
    public class LettersController : Controller
    {
        private readonly CDUCommunityMusicContext _context;

        public LettersController(CDUCommunityMusicContext context)
        {
            _context = context;
        }

        // GET: Letters
        public async Task<IActionResult> Index()
        {
            var cDUCommunityMusicContext = _context.Letter.Include(l => l.Students);
            var lessonList = await cDUCommunityMusicContext.ToListAsync();
            return View(lessonList);
        }

        // GET: Letters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Letter == null)
            {
                return NotFound();
            }

            var letter = await _context.Letter
                .Include(l => l.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (letter == null)
            {
                return NotFound();
            }

            return View(letter);
        }

        // GET: Letters/Email/5
        public async Task<String> Email(int? id)
        {
            if (id == null || _context.Letter == null)
            {
                return "Not Found";
            }

            var letter = await _context.Letter
                .Include(l => l.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (letter == null)
            {
                return "Not Found";
            }

            var renderString = await RazorTemplateEngine.RenderAsync("~/Views/Letters/Email.cshtml",letter);
            return renderString;
        }

        // GET: Letters/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "LastName");
            return View();
        }

        // POST: Letters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,Payment,InitialComment,BankName,AccountName,AccountNumber,BSB,Term,CurrentSemestern,CurrentYear,TermStartDate")] Letter letter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(letter);
                await _context.SaveChangesAsync();

                // calculate cost
                List<Lessons> lessons = _context.Lesson.Where(l => l.StudentId == letter.StudentId).Where(l => l.PaymentStatus == false).Include(l => l.Durations).ToList();
                letter.Lessons = lessons;
                decimal TotalCost = 0;
                foreach (Lessons lesson in lessons)
                {
                    // decimal DurationCost = _context.Durations.Where(d => d.Id == lesson.DurationsId).FirstOrDefault().Cost;
                    // TotalCost += DurationCost;
                    TotalCost += lesson.Durations.Cost;
                }
                letter.TotalCost = TotalCost;
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "LastName", letter.StudentId);
            return View(letter);
        }

        // GET: Letters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Letter == null)
            {
                return NotFound();
            }

            var letter = await _context.Letter.FindAsync(id);
            if (letter == null)
            {
                return NotFound();
            }

            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "ContactNumber", letter.StudentId);
            return View(letter);
        }

        // POST: Letters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,Payment,InitialComment,BankName,AccountName,AccountNumber,BSB,Term,CurrentSemestern,CurrentYear,TermStartDate,TotalCost")] Letter letter)
        {
            if (id != letter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(letter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LetterExists(letter.Id))
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
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "LastName", letter.StudentId);
            return View(letter);
        }

        // GET: Letters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Letter == null)
            {
                return NotFound();
            }

            var letter = await _context.Letter
                .Include(l => l.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (letter == null)
            {
                return NotFound();
            }

            return View(letter);
        }

        // POST: Letters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Letter == null)
            {
                return Problem("Entity set 'CDUCommunityMusicContext.Letter'  is null.");
            }
            var letter = await _context.Letter.FindAsync(id);
            if (letter != null)
            {
                _context.Letter.Remove(letter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LetterExists(int id)
        {
          return _context.Letter.Any(e => e.Id == id);
        }

    }
}
