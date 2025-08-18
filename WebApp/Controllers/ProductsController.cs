
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repositories;

namespace WebApp.Controllers;

public class ProductsController : Controller
{
    private readonly IProductRepository _repo;
    public ProductsController(IProductRepository repo) => _repo = repo;

    public async Task<IActionResult> Index(string? q)
    {
        var list = await _repo.GetAllAsync(q);
        ViewBag.Query = q;
        return View(list);
    }

    public IActionResult Create() => View(new Product());

    [HttpPost]
    public async Task<IActionResult> Create(Product p)
    {
        if (!ModelState.IsValid) return View(p);
        await _repo.CreateAsync(p);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var p = await _repo.GetByIdAsync(id);
        if (p == null) return NotFound();
        return View(p);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Product p)
    {
        if (!ModelState.IsValid) return View(p);
        await _repo.UpdateAsync(p);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var p = await _repo.GetByIdAsync(id);
        if (p == null) return NotFound();
        return View(p);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _repo.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
