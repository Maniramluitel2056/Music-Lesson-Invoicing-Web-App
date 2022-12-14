using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CDUCommunityMusic.Data;
using CDUCommunityMusic.Models;

namespace CDUCommunityMusic.Controllers
{
    public class LessonsController : Controller
    {
        private readonly CDUCommunityMusicContext _context;

        public LessonsController(CDUCommunityMusicContext context)
        {
            _context = context;
        }

        // GET: Lessons
        public async Task<IActionResult> Index()
        {
            var cDUCommunityMusicContext = _context.Lesson.Include(l => l.Durations).Include(l => l.Instruments).Include(l => l.Students).Include(l => l.Tutors);
            return View(await cDUCommunityMusicContext.ToListAsync());
        }

        // GET: Lessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lesson == null)
            {
                return NotFound();
            }

            var lessons = await _context.Lesson
                .Include(l => l.Durations)
                .Include(l => l.Instruments)
                .Include(l => l.Students)
                .Include(l => l.Tutors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lessons == null)
            {
                return NotFound();
            }

            return View(lessons);
        }

        // GET: Lessons/Create
        public IActionResult Create()
        {
            ViewData["DurationsId"] = new SelectList(_context.Durations, "Id", "Minutes");
            ViewData["InstrumentId"] = new SelectList(_context.Instrument, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName");
            ViewData["TutorId"] = new SelectList(_context.Tutors, "Id", "Name");
            return View();
        }

        //Paid/unpaid
        public async Task<IActionResult> UpdatePayment(int? id)
        {
            if (id == null || _context.Lesson == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lesson
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }
            lesson.PaymentStatus = !lesson.PaymentStatus;
            await _context.SaveChangesAsync();


            var cDUCommunityMusicContext = _context.Lesson.Include(l => l.Durations).Include(l => l.Instruments).Include(l => l.Students).Include(l => l.Tutors);
            return View("Index",await cDUCommunityMusicContext.ToListAsync());
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,InstrumentId,TutorId,DurationsId,DateNtime,LetterId")] Lessons lessons)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lessons);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DurationsId"] = new SelectList(_context.Durations, "Id", "Minutes", lessons.DurationsId);
            ViewData["InstrumentId"] = new SelectList(_context.Instrument, "Id", "Name", lessons.InstrumentId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName", lessons.StudentId);
            ViewData["TutorId"] = new SelectList(_context.Tutors, "Id", "Name", lessons.TutorId);
            return View(lessons);
        }

        // GET: Lessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lesson == null)
            {
                return NotFound();
            }

            var lessons = await _context.Lesson.FindAsync(id);
            if (lessons == null)
            {
                return NotFound();
            }
            ViewData["DurationsId"] = new SelectList(_context.Durations, "Id", "Minutes", lessons.DurationsId);
            ViewData["InstrumentId"] = new SelectList(_context.Instrument, "Id", "Name", lessons.InstrumentId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName", lessons.StudentId);
            ViewData["TutorId"] = new SelectList(_context.Tutors, "Id", "Name", lessons.TutorId);
            return View(lessons);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,InstrumentId,TutorId,DurationsId,DateNtime")] Lessons lessons)
        {
            if (id != lessons.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lessons);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonsExists(lessons.Id))
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
            ViewData["DurationsId"] = new SelectList(_context.Durations, "Id", "Minutes", lessons.DurationsId);
            ViewData["InstrumentId"] = new SelectList(_context.Instrument, "Id", "Name", lessons.InstrumentId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Full Name", lessons.StudentId);
            ViewData["TutorId"] = new SelectList(_context.Tutors, "Id", "Name", lessons.TutorId);
            return View(lessons);
        }

        // GET: Lessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lesson == null)
            {
                return NotFound();
            }

            var lessons = await _context.Lesson
                .Include(l => l.Durations)
                .Include(l => l.Instruments)
                .Include(l => l.Students)
                .Include(l => l.Tutors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lessons == null)
            {
                return NotFound();
            }

            return View(lessons);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lesson == null)
            {
                return Problem("Entity set 'CDUCommunityMusicContext.Lesson'  is null.");
            }
            var lessons = await _context.Lesson.FindAsync(id);
            if (lessons != null)
            {
                _context.Lesson.Remove(lessons);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonsExists(int id)
        {
          return _context.Lesson.Any(e => e.Id == id);
        }
    }
}
