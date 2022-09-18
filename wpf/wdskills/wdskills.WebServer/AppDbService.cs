using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wdskills.WebServer.Data;
using wdskills.WebServer.Model;

namespace wdskills.WebServer
{
    public sealed class AppDbService
    {
        private AppDbContext _context;
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public AppDbService(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
            _context = _contextFactory.CreateDbContext();
        }
        public async Task<bool> AddUserAsync(User user)
        {
            bool isAddUser = await FindUserToLoginAsync(user.UserLogin) is null;
            if (isAddUser)
            {
                await _context.User.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            return isAddUser;
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            bool isAddProduct = await FindUserToLoginAsync(product.ProductArticleNumber) is null;
            if (isAddProduct)
            {
                await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            return isAddProduct;
        }

        public async Task<Product?> FindProductToArticleAsync(string? article)
        {
            await RefreshDbContext();
            if (string.IsNullOrEmpty(article))
            {
                throw new ArgumentNullException(nameof(article), "Article is empty or null");
            }
            Product? findProduct = await _context.Product.FirstOrDefaultAsync(x => x.ProductArticleNumber!.ToLower() == article.ToLower());
            return findProduct;
        }

        public async Task<bool> UpdateProductToAsync(Product newProduct)
        {
            Product? findOldDataUser = await FindProductToArticleAsync(newProduct.ProductArticleNumber);
            bool isEditUser = findOldDataUser is not null;
            if (isEditUser)
            {
                _context.Entry(findOldDataUser!).CurrentValues.SetValues(newProduct);
                await _context.SaveChangesAsync();
            }
            return isEditUser;
        }

        public async Task<bool> DeleteProductAsync(string article)
        {
            Product? deleteProduct = await FindProductToArticleAsync(article);
            if (deleteProduct is not null)
            {
                _context.Product.Remove(deleteProduct);
                await _context.SaveChangesAsync();
            }
            return (deleteProduct is not null);
        }

        public async Task<bool> CanUserLogInAsync(User user)
        {
            User? findUser = await FindUserToLoginAsync(user.UserLogin);
            return (findUser != null && findUser.UserPassword == user.UserPassword);
        }

        public async Task<User?> FindUserToLoginAsync(string? login)
        {
            await RefreshDbContext();
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentNullException(nameof(login), "User login is empty or null");
            }
            User? findUser = await _context.User.FirstOrDefaultAsync(x => x.UserLogin!.ToLower() == login.ToLower());
            return findUser;
        }

        public async Task<List<Product>> GetProductsListAsync()
        {
            await RefreshDbContext();
            List<Product> products = await _context.Product.ToListAsync();
            foreach (Product product in products)
            {
                if (string.IsNullOrEmpty(product.ProductImage))
                {
                    product.ProductImage = "picture.png";
                }
            }
            return products;
        }

        public async Task<List<string>> GetProviderListAsync()
        {
            List<Product> products = await GetProductsListAsync();
            List<string> providers = new () {"Все поставщики"};
            if(products is not null)
            {
                foreach(Product product in products)
                {
                    if(providers.FirstOrDefault(p => p == product.ProductProvider!) == null)
                    {
                        providers.Add(product.ProductProvider!);
                    }
                }
            }
            return providers;
        }

        public async Task<List<string>> GetCategoriesListAsync()
        {
            List<Product> products = await GetProductsListAsync();
            List<string> categories = new();
            if (products is not null)
            {
                foreach (Product product in products)
                {
                    if (categories.FirstOrDefault(p => p == product.ProductCategory!) == null)
                    {
                        categories.Add(product.ProductCategory!);
                    }
                }
            }
            return categories;
        }

        public async Task RefreshDbContext()
        {
            _context = await _contextFactory.CreateDbContextAsync();
        }
    }
}
