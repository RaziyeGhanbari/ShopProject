using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopProject.Data;
using ShopProject.Models;

namespace ShopProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ShopProjectContext _context;

        public CategoryController(ShopProjectContext context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var categories = _context.Category;
            return View(categories);
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var 
                category = await _context.Category
                .Include(c => c.Parent)
                .Include((c => c.Fields ))
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            var categoryFieldVm = new CategoryFieldViweModel()
            {
                Categori = new Category(),
                Fields = new List<Field>()
            };
            ViewData["ParentId"] = new SelectList(_context.Category, "Id", "Name");
            return View(categoryFieldVm);
        }
        
        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryFieldViweModel categoryFieldVm)
        {
            
            if (ModelState.IsValid)
            {
                Category category = new Category()
                {
                    ParentId = categoryFieldVm.Categori.ParentId,
                    Name = categoryFieldVm.Categori.Name
                    
                };
                
                var cat = _context.Add(category);
                // _context.AddRange(categoryFieldVm.Fields);
                if(categoryFieldVm.Fields != null)
                {
                    foreach (var f in categoryFieldVm.Fields)
                    {
                        Field field = new Field()
                        {
                            Name   = f.Name,
                            Category = category
                        };
                    
                        _context.Set<Field>().Add(field);
                    }
                }
                
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(_context.Category, "Id", "Name", categoryFieldVm.Categori.ParentId);
            return View(categoryFieldVm);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var category = await _context.Category
                .Include(c => c.Fields)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryFieldVm = new CategoryFieldViweModel()
            {
                Categori = category,
                Fields = category.Fields.ToList()
            };
            ViewData["ParentId"] = new SelectList(_context.Category, "Id", "Name", category.ParentId);
            return View(categoryFieldVm);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, CategoryFieldViweModel categoryFieldVm)
        // {
        //     if (id != categoryFieldVm.Categori.Id)
        //     {
        //         return NotFound();
        //     }
        //
        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             Category? category = await _context.Category
        //                 .Include(c => c.Fields)
        //                 .FirstOrDefaultAsync(c => c.Id == id);
        //             if (category == null)
        //             {
        //                 return NotFound();
        //             }
        //             category.Fields = categoryFieldVm.Fields;
        //             category.ParentId = categoryFieldVm.Categori.ParentId;
        //             category.Name = categoryFieldVm.Categori.Name;
        //             _context.Update(category);
        //             if (categoryFieldVm.Fields != null)
        //             {
        //                 foreach (var field in categoryFieldVm.Fields)
        //                 {
        //                     var fields = category.Fields.FirstOrDefault(f => f.Id == field.Id);
        //                     fields.Name = field.Name;
        //                     fields.CategoryId = field.CategoryId;
        //                     _context.Field.Update(fields);
        //                 }
        //             }
        //             // _context.AddRangeAsync(categoryFieldVm.Fields);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!CategoryExists(categoryFieldVm.Categori.Id))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     ViewData["ParentId"] = new SelectList(_context.Category, "Id", "Name", categoryFieldVm.Categori.ParentId);
        //     return View(categoryFieldVm);
        // }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryFieldViweModel categoryFieldVm)
        {
            if (id != categoryFieldVm.Categori.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Category? category = await _context.Category
                        .Include(c => c.Fields)
                        .FirstOrDefaultAsync(c => c.Id == id);
                    if (category == null)
                    {
                        return NotFound();
                    }

                    // Update category properties
                    category.Name = categoryFieldVm.Categori.Name;
                    category.ParentId = categoryFieldVm.Categori.ParentId;
                    if (categoryFieldVm.Fields != null)
                    {
                        foreach (var field in categoryFieldVm.Fields)
                        {
                            if (field.Id == 0)
                            {
                                // Add new field
                                var newField = new Field
                                {
                                    Name = field.Name,
                                    Category = category,
                                };
                                _context.Field.Add(newField);
                            }
                            else
                            {
                                // Update existing field
                                var existingField = category.Fields.FirstOrDefault(f => f.Id == field.Id);
                                existingField.Name = field.Name;
                                existingField.CategoryId = field.CategoryId;
                                existingField.IsDeleted = field.IsDeleted;
                                _context.Field.Update(existingField);
                            }
                        }
                    }
                    // Update existing fields and add new fields

                    // _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(categoryFieldVm.Categori.Id))
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
            ViewData["ParentId"] = new SelectList(_context.Category, "Id", "Name", categoryFieldVm.Categori.ParentId);
            return View(categoryFieldVm);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .Include(c => c.Parent)
                .Include(c => c.Products)
                .Include(c => c.Fields)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["IsParentCategory"] = await _context.Category.AnyAsync(c => c.ParentId == id);
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Category
                .Include(c => c.Fields )
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                foreach (var field in category.Fields)
                {
                    _context.Field.Remove(field);
                }
                _context.Category.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}
