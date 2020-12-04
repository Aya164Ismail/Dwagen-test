
using Dwagen.Model;
using Dwagen.Repository.implementation;
using Dwagen.Repository.Interface;
using Dwagen.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dwagen.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DwagenContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        IUserRepository userRepository;
        IProductRepository productRepository;
        IOrderRepository orderRepository;
        public UnitOfWork(DwagenContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        //Content Service
        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(_context);
                }
                return userRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(_context);
                }
                return productRepository;
            }
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                if (orderRepository == null)
                {
                    orderRepository = new OrderRepository(_context);
                }
                return orderRepository;
            }
        }

        public int Save()
        {
           return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
