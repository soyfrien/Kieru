using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kieru.Data;
using Kieru.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Identity;

namespace Kieru.Controllers
{
    public class SecretsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public SecretsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Secrets
        public async Task<IActionResult> Index(Guid? id)
        {
            if (id != null)
            {
                var secret = await _context.Secret
                    .SingleOrDefaultAsync(m => m.Id == id);
                if (secret == null)
                {
                    return NotFound();
                }

                return View("Details", secret);
            }

            return View();
        }

        // POST: Secrets
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Guid id)
        {
            var secret = _context.Secret
                .SingleOrDefaultAsync(m => m.Id == id);
            if(secret==null)
            {
                return NotFound();
            }

            return View("Details", secret);
        }

        // GET: Secrets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secret = await _context.Secret
                .SingleOrDefaultAsync(m => m.Id == id);
            if (secret == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                if (!new Guid(_userManager.GetUserId(User)).Equals(secret.OwnerId))
                {
                    _context.Secret.Remove(secret);
                    await _context.SaveChangesAsync();
                }
            }
            else if (secret.ViewedBy == ViewedBy.Recipient || secret.ViewedBy == ViewedBy.Owner)
            {
                secret.ViewedBy = ViewedBy.Recipient;
                _context.Secret.Remove(secret);
                await _context.SaveChangesAsync();
            }

            if (SecretExists(new Guid(id.ToString())) && secret.ViewedBy == ViewedBy.Owner)
            {
                if (!new Guid(_userManager.GetUserId(User)).Equals(secret.OwnerId))
                {
                    secret.ViewedBy = ViewedBy.Recipient;
                    await _context.SaveChangesAsync();
                }
            }

            if(secret.ViewedBy == ViewedBy.NotViewed)
            {
                secret.ViewedBy = ViewedBy.Owner;
                await _context.SaveChangesAsync();
            }

            return View(secret);
        }

        // GET: Secrets/Create
        [HttpGet]
        public async Task<IActionResult> Create(Guid? id)
        {
            if (id != null)
            {
                var secret = await _context.Secret
                    .SingleOrDefaultAsync(m => m.Id == id);
                if (secret != null)
                {
                    return View("Details", secret);
                }

            }
            return View();
        }

        // POST: Secrets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Phrase")] Secret secret)
        {
            if (ModelState.IsValid)
            {
                secret.Id = Guid.NewGuid();
                _context.Add(secret);

                if(User.Identity.IsAuthenticated)
                {
                    secret.OwnerId = new Guid(_userManager.GetUserId(User));
                }
                secret.ViewedBy = ViewedBy.NotViewed;

                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Secrets", new RouteValueDictionary(new
                {
                    id = secret.Id
                }));
            }

            return View();
        }

        // GET: Secrets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secret = await _context.Secret.SingleOrDefaultAsync(m => m.Id == id);
            if (secret == null)
            {
                return NotFound();
            }
            return View(secret);
        }

        // POST: Secrets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Phrase")] Secret secret)
        {
            if (id != secret.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(secret);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecretExists(secret.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(secret);
        }

        // GET: Secrets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secret = await _context.Secret
                .SingleOrDefaultAsync(m => m.Id == id);
            if (secret == null)
            {
                return NotFound();
            }

            return View(secret);
        }

        // POST: Secrets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var secret = await _context.Secret.SingleOrDefaultAsync(m => m.Id == id);
            _context.Secret.Remove(secret);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SecretExists(Guid id)
        {
            return _context.Secret.Any(e => e.Id == id);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Aboue Kieru";

            return View();
        }
    }
}
