
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repositories;

namespace WebApp.Controllers;

public class CustomersController : Controller
{
    private readonly ICustomerRepository _repo;
    public CustomersController(ICustomerRepository repo) => _repo = repo;

    public async Task<IActionResult> Index(string? q)
    {
        var list = await _repo.GetAllAsync(q);
        ViewBag.Query = q;
        return View(list);
    }

    public IActionResult Create() => View(new Customer());

    [HttpPost]
    public async Task<IActionResult> Create(Customer c)
    {
        if (!ModelState.IsValid) return View(c);
        await _repo.CreateAsync(c);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var c = await _repo.GetByIdAsync(id);
        if (c == null) return NotFound();
        return View(c);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Customer c)
    {
        if (!ModelState.IsValid) return View(c);
        await _repo.UpdateAsync(c);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var c = await _repo.GetByIdAsync(id);
        if (c == null) return NotFound();
        return View(c);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _repo.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
