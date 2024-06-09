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
    public class ProductController : Controller
    {
        private readonly ShopProjectContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductController(ShopProjectContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Product
        public async Task<IActionResult> Index(int? id, string? searchCategory)
        {
            IQueryable<Product> dataset = _context.Product.Include(p => p.Category);
            if (id is not null)
            {
                dataset = dataset.Where(p => p.Id == id);
            }
            else
            {
                if (!string.IsNullOrEmpty(searchCategory))
                {
                    dataset = dataset.Where(p => p.Category.Name.Contains(searchCategory));
                }
            }
        
            FilterViewModel filterVm = new FilterViewModel()
            {
                Products = await dataset.ToListAsync()
            }; 
            ViewData["Categories"] = new SelectList(_context.Set<Category>(), "Name", "Name");
            return View(filterVm);
        }
        public async Task<string?> UploadImageAsync(IFormFile file)
        {
            if (file.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Images", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return uniqueFileName;
            }

            return null;
        }


        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(c => c.Category)
                .Include(p => p.FieldValues)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            // var product = _context.Product;

            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                if (photo.Length > 0)
                {
                    product.ImageUrl = await UploadImageAsync(photo);
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return View("FieldValues", new FieldValuesViewModel()
                {
                    ProductId = product.Id,
                    CategoryId = product.CategoryId,
                    Fields = _context.Set<Field>().Where(field => field.CategoryId == product.CategoryId).ToList(),
                    FieldValues = [],
                    // idStatus = 0
                });
            }

            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET : FieldValue
        // [HttpGet]
        // public IActionResult FieldValues(int categoryId)
        // {
        //     var fields = _context.Field
        //         .Where(f => f.CategoryId == categoryId)
        //         .ToList();
        //
        //     FieldValuesViewModel fieldValueVm = new FieldValuesViewModel
        //     {
        //         CategoryId = categoryId,
        //         Fields = fields,
        //         FieldValues = new List<FieldValue>(),
        //     };
        //
        //     return View(fieldValueVm);
        // }


        // GET: Product/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", product.CategoryId);
            return View(new EditProductViewModel
            {
                CategoryId = product.CategoryId,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Price = product.Price
            });
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditProductViewModel productVm, IFormFile? photo)
        {
            if (id != productVm.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", productVm.CategoryId);
                return View(productVm);
            }

            try
            {
                Product? product = await _context.Product.FindAsync(id);
                product.Name = productVm.Name;
                product.Price = productVm.Price;
                product.CategoryId = productVm.CategoryId;
                // product.FieldValues = productVm.FieldValues;
                if (photo is not null)
                {
                    product.ImageUrl = await UploadImageAsync(photo);
                }

                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(productVm.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return View("EditFieldValues", new FieldValuesViewModel()
            {
                ProductId = productVm.Id,
                CategoryId = productVm.CategoryId,
                ImageUrl = productVm.ImageUrl,
                Fields = _context.Set<Field>().Where(field => field.CategoryId == productVm.CategoryId).ToList(),
                // FieldValues = productVm.FieldValues
                FieldValues = _context.Set<FieldValue>().Where(fValue => fValue.ProductId == productVm.Id).ToList(),
                // idStatus = 1
            });
        }

        // POST : FieldValue
        // [HttpPost]
        // public IActionResult FieldValues(FieldValuesViewModel fieldValueVm)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         fieldValueVm.Fields = _context.Set<Field>()
        //             .Where(f => f.CategoryId == fieldValueVm.CategoryId).ToList();
        //         return View(fieldValueVm);
        //     }
        //     // fieldValueVm.idStatus = FieldValue
        //     foreach (var fValue in fieldValueVm.FieldValues)
        //     {
        //         if (fieldValueVm.idStatus == 0)
        //         {
        //             FieldValue fieldValue = new FieldValue()
        //             {
        //                 ProductId = fValue.ProductId,
        //                 FieldId = fValue.FieldId,
        //                 Value = fValue.Value,
        //                 Product = fValue.Product,
        //                 Field = fValue.Field,
        //             };
        //                 _context.Set<FieldValue>().Add(fieldValue);
        //         }
        //
        //         FieldValue DbFieldValue = _context.Set<FieldValue>()
        //             .FirstOrDefault(fv => fv.ProductId == fValue.ProductId 
        //                                   && fv.FieldId == fValue.FieldId);
        //         if (DbFieldValue != null)
        //         {
        //             DbFieldValue.Value = fValue.Value;
        //             _context.Set<FieldValue>().Update(DbFieldValue);
        //         }
        //
        //     }
        //
        //     _context.SaveChanges();
        //     return RedirectToAction(nameof(Index));
        // }

        // Create FieldValues
        [HttpPost]
        public IActionResult CreateFieldValues(FieldValuesViewModel fieldValueVm)
        {
            if (!ModelState.IsValid)
            {
                fieldValueVm.Fields = _context.Set<Field>()
                    .Where(f => f.CategoryId == fieldValueVm.CategoryId).ToList();
                return View(fieldValueVm);
            }

            if (fieldValueVm.FieldValues != null)
            {
                foreach (var fValue in fieldValueVm.FieldValues)
                {
                    FieldValue fieldValue = new FieldValue()
                    {
                        ProductId = fValue.ProductId,
                        FieldId = fValue.FieldId,
                        Value = fValue.Value,
                        Product = fValue.Product,
                        Field = fValue.Field,
                    };
                    _context.Set<FieldValue>().Add(fieldValue);
                }
            }


            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // Edit Fieldvalues
        [HttpPost]
        public IActionResult EditFieldValues(FieldValuesViewModel fieldValueVm)
        {
            if (!ModelState.IsValid)
            {
                fieldValueVm.Fields = _context.Set<Field>()
                    .Where(f => f.CategoryId == fieldValueVm.CategoryId).ToList();
                return View(fieldValueVm);
            }

            if (fieldValueVm.FieldValues != null)
            {
                foreach (var fValue in fieldValueVm.FieldValues)
                {
                    FieldValue fieldValue = _context.Set<FieldValue>()
                        .FirstOrDefault(fv => fv.ProductId == fValue.ProductId && fv.FieldId == fValue.FieldId);
                    if (fieldValue != null)
                    {
                        fieldValue.Value = fValue.Value;
                        _context.Set<FieldValue>().Update(fieldValue);
                    }
                }
            }


            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool isParentCategory = await _context.Category.AnyAsync(c => c.ParentId == id);
            if (isParentCategory)
            {
                // Return an error message or handle it accordingly
                ModelState.AddModelError(string.Empty, "Cannot delete this category as it is a parent of another category.");
                return RedirectToAction(nameof(Index)); // or return the view with an error message
            }
            var product = await _context.Product
                .Include(p => p.FieldValues).FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}