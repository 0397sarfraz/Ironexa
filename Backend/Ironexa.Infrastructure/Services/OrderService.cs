using Azure.Core;
using Ironexa.Application.DTOs;
using Ironexa.Application.Interfaces;
using Ironexa.Domain.Entities;
using Ironexa.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Infrastructure.Services
{
    public class OrderService(AppDbContext _context) : IOrderService
    {
        public async Task<bool> SaveOrderAsyn(CreateOrderRequest request)
        {

            if (request.OrderId > 0)
            {
                return await UpdateOrderAsync(request);
            }
            else
            {
                return await AddOrderAsync(request);
            }

        }

        public async Task<List<OrderResponseDto>> GetAllOrder()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .Include(o => o.Payments)
                .Select(o => new OrderResponseDto
                {
                    Id = o.Id,
                    CustomerName = o.Customer.Name,
                    Phone = o.Customer.Phone,
                    Status = o.Status,
                    OrderDate = o.OrderDate.ToString("dd/MM/yyyy"),
                    TotalAmount = o.OrderItems.Sum(x => x.TotalAmount ?? 0),
                    Advance = o.Payments
                        .Where(p => p.PaymentType == "Advance")
                        .Sum(p => p.Amount),

                    Remaining = (o.OrderItems.Sum(x => x.TotalAmount ?? 0) > 0
                        ? o.OrderItems.Sum(x => x.TotalAmount ?? 0) - o.Payments.Sum(p => p.Amount)
                        : 0m)
                }).ToListAsync();

            return orders;
        }
        public async Task<OrderDetailDto> GetOrderById(int Id)
        {
            var orderbyId = new OrderDetailDto();
            orderbyId = await _context.Orders
                           .Include(o => o.Customer)
                           .Include(o => o.OrderItems)
                           .ThenInclude(i => i.Measurement)
                           .Include(o => o.Payments)
                           .Where(o => o.Id == Id)
                           .Select(o => new OrderDetailDto
                           {
                               Id = o.Id,
                               CustomerName = o.Customer.Name,
                               Phone = o.Customer.Phone,
                               Address = o.Customer.Address,
                               Status = o.Status,
                               TotalAmount = o.OrderItems.Sum(o => o.TotalAmount ?? 0m),
                               Advance = o.Payments.Where(x => x.PaymentType == "Advance").Sum(x => x.Amount),
                               Remaining = (o.OrderItems.Sum(x => x.TotalAmount ?? 0) > 0
                       ? o.OrderItems.Sum(x => x.TotalAmount ?? 0) - o.Payments.Sum(p => p.Amount)
                       : 0m),
                               Items = o.OrderItems.Select(i => new OrderItemDetailDto
                               {
                                   Id = i.Id,
                                   ProductName = i.ProductName,
                                   Image = i.DesignImage,
                                   RatePerKg = i.RatePerKg,
                                   EstimatedWeight = i.EstimatedWeight,
                                   FinalWeight = i.FinalWeight,
                                   TotalAmount = i.TotalAmount,

                                   Height = i.Measurement != null ? i.Measurement.Heigth : null,
                                   Width = i.Measurement != null ? i.Measurement.Weigth : null

                               }).ToList()
                           }).FirstOrDefaultAsync();

            return orderbyId;
        }

        private async Task<string> UploadImage(IFormFile image)
        {
            string fileName = string.Empty;
            try
            {
                if (image != null)
                {
                    fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return fileName;
        }

        private async Task<bool> AddOrderAsync(CreateOrderRequest request)
        {
            var customer = new Customer
            {
                Name = request.Customer.Name,
                Phone = request.Customer.Phone,
                Address = request.Customer.Address,
            };
            await _context.Customers.AddAsync(customer);

            var order = new Order
            {
                Customer = customer,
                Status = "Pending",
                OrderDate = DateTime.Now,
            };
            foreach (var item in request.OrderItems)
            {
                var orderItem = new OrderItem
                {
                    ProductName = item.ProductName,
                    EstimatedWeight = item.EstimatedWeight,
                    RatePerKg = item.RatePerKg,
                };

                orderItem.Measurement = new Measurement
                {
                    Heigth = item.Measurement.Height,
                    Weigth = item.Measurement.Width
                };

                if (item.Images != null)
                {
                    var filename = Guid.NewGuid() + Path.GetExtension(item.Images.FileName);
                    var path = Path.Combine("wwwroot/Images", filename);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await item.Images.CopyToAsync(stream);
                    }
                    orderItem.DesignImage = filename;
                }
                order.OrderItems ??= new List<OrderItem>();
                order.OrderItems.Add(orderItem);
            }

            if (request.AdvancePayment != null)
            {
                order.Payments = new List<Payment>
                {
                    new Payment
                    {
                         Amount = request.AdvancePayment.Amount,
                         PaymentMode = request.AdvancePayment.PaymentMode,
                         PaymentType="Advance"
                    }
                };
            }

            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<bool> UpdateOrderAsync(CreateOrderRequest request)
        {
            var getOrderById = await _context.Orders
                   .Include(o => o.Customer)
                   .Include(o => o.OrderItems)
                   .ThenInclude(i => i.Measurement)
                   .Include(o => o.Payments)
                   .FirstOrDefaultAsync(o => o.Id == request.OrderId);
            if (getOrderById == null) return false;

            //1. Update Customer
            getOrderById.Customer.Name = request.Customer.Name;
            getOrderById.Customer.Phone = request.Customer.Phone;
            getOrderById.Customer.Address = request.Customer.Address;

            //Existing item update + new item add

            foreach (var item in request.OrderItems)
            {
                if (item.Id > 0)
                {
                    var dbItem = getOrderById.OrderItems.FirstOrDefault(x => x.Id == item.Id);
                    dbItem.ProductName = item.ProductName;
                    dbItem.RatePerKg = item.RatePerKg;
                    dbItem.EstimatedWeight = item.EstimatedWeight;

                    //Measurement
                    if (dbItem.Measurement != null)
                    {

                        dbItem.Measurement.Heigth = item.Measurement.Height;
                        dbItem.Measurement.Weigth = item.Measurement.Width;
                    }

                    //udpate image

                    if (item.Images != null)
                    {
                        if (!string.IsNullOrEmpty(dbItem.DesignImage))
                        {
                            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", dbItem.DesignImage);
                            if (File.Exists(oldPath)) { File.Delete(oldPath); }
                            ;
                        }

                        dbItem.DesignImage = await UploadImage(item.Images);
                    }
                }
                else
                {
                    //Handle new order Items
                    var newItem = new OrderItem
                    {
                        ProductName = item.ProductName,
                        RatePerKg = item.RatePerKg,
                        EstimatedWeight = item.EstimatedWeight,

                        Measurement = new Measurement
                        {
                            Heigth = item.Measurement.Height,
                            Weigth = item.Measurement.Width
                        }
                    };

                    if (item.Images != null)
                    {
                        newItem.DesignImage = await UploadImage(item.Images);
                    }

                    getOrderById.OrderItems.Add(newItem);
                }
            }

            //Delete removed Item
            var requestIds = request.OrderItems
                .Where(x => x.Id > 0)
                .Select(x => x.Id)
                .ToList();

            var toDelete = getOrderById.OrderItems
                .Where(x => !requestIds.Contains(x.Id))
                .ToList();
            _context.RemoveRange(toDelete);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> CancelOrder(int id)
        {
            bool isCancelled = false;
            if (id > 0)
            {
                var getOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
                if (getOrder != null)
                {
                    getOrder.Status = "Cancelled";
                    await _context.SaveChangesAsync();
                    isCancelled = true;
                }
            }
            return isCancelled;
        }
    }
}
