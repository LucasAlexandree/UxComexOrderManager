
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class OrdersController : Controller
{
    private readonly IOrderRepository _orders;
    private readonly ICustomerRepository _customers;
    private readonly IProductRepository _products;

    public OrdersController(IOrderRepository orders, ICustomerRepository customers, IProductRepository products)
    {
        _orders = orders;
        _customers = customers;
        _products = products;
    }

    public async Task<IActionResult> Index(int? customerId, string? status)
    {
        var list = await _orders.GetAllAsync(customerId, status);
        ViewBag.CustomerId = customerId;
        ViewBag.Status = status;
        ViewBag.Customers = await _customers.GetAllAsync(null);
        return View(list);
    }

    public async Task<IActionResult> Details(int id)
    {
        var order = await _orders.GetByIdAsync(id);
        if (order == null) return NotFound();
        return View(order);
    }

    public async Task<IActionResult> Create()
    {
        var vm = new OrderCreateViewModel
        {
            Customers = (await _customers.GetAllAsync(null)).ToList(),
            Products = (await _products.GetAllAsync(null)).ToList()
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderCreateViewModel vm)
    {
        vm.Customers = (await _customers.GetAllAsync(null)).ToList();
        vm.Products = (await _products.GetAllAsync(null)).ToList();

        if (vm.Items == null || !vm.Items.Any())
        {
            ModelState.AddModelError("", "Please add at least one product.");
            return View(vm);
        }

        
        foreach (var it in vm.Items)
        {
            if (!await _products.HasStockAsync(it.ProductId, it.Quantity))
            {
                ModelState.AddModelError("", $"Insufficient stock for product #{it.ProductId}.");
                return View(vm);
            }
        }

        var order = new Order
        {
            CustomerId = vm.CustomerId,
            Status = "New",
            Items = vm.Items.Select(x => new OrderItem { ProductId = x.ProductId, Quantity = x.Quantity }).ToList()
        };

        var id = await _orders.CreateAsync(order, true);
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateStatus(int id, string status)
    {
        await _orders.UpdateStatusAsync(id, status);
        return RedirectToAction(nameof(Details), new { id });
    }
}
