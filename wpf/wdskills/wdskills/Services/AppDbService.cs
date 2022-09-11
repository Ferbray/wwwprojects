using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using wdskills.Data;
using wdskills.Model;

namespace wdskills.Services
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
            RefreshDbContext();
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
            RefreshDbContext();
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
            RefreshDbContext();
            if (string.IsNullOrEmpty(article))
            {
                throw new ArgumentNullException(nameof(article), "Article is empty or null");
            }
            Product? findProduct = await _context.Product.FirstOrDefaultAsync(x => x.ProductArticleNumber!.ToLower() == article.ToLower());
            return findProduct;
        }

        public async Task<bool> UpdateProductToAsync(Product newProduct)
        {
            RefreshDbContext();
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
            RefreshDbContext();
            Product? deleteProduct = await FindProductToArticleAsync(article);
            if (deleteProduct is not null)
            {
                _context.Product.Remove(deleteProduct);
                await _context.SaveChangesAsync();
            }
            return (deleteProduct is not null);
        }

        public async Task<bool> UpdateUserToAsync(User newUser)
        {
            RefreshDbContext();
            User? findOldDataUser = await FindUserToLoginAsync(newUser.UserLogin);
            bool isEditUser = findOldDataUser is not null;
            if (isEditUser)
            {
                newUser.UserId = findOldDataUser!.UserId;
                _context.Entry(findOldDataUser).CurrentValues.SetValues(newUser);
                await _context.SaveChangesAsync();
            }
            return isEditUser;
        }

        public async Task<bool> CanUserLogInAsync(User user)
        {
            User? findUser = await FindUserToLoginAsync(user.UserLogin);
            return (findUser != null && findUser.UserPassword == user.UserPassword);
        }

        public async Task<User?> FindUserToLoginAsync(string? login)
        {
            RefreshDbContext();
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentNullException(nameof(login), "User login is empty or null");
            }
            User? findUser = await _context.User.FirstOrDefaultAsync(x => x.UserLogin!.ToLower() == login.ToLower());
            return findUser;
        }

        public async Task<bool> AuthUserAsync(User user)
        {
            await AddUserAsync(user);

            bool isLogIn = await CanUserLogInAsync(user);

            if (!isLogIn) throw new ArgumentException("Password or login is incorrect");
            return isLogIn;
        }

        public ObservableCollection<Product>? GetProductsList()
        {
            RefreshDbContext();
            _context.Product.Load();
            ObservableCollection<Product>? products = _context.Product.Local.ToObservableCollection();
            foreach (Product product in products)
            {
                if (string.IsNullOrEmpty(product.ProductImage))
                {
                    product.ProductImage = "picture.png";
                }
            }
            return products;
        }

        public ObservableCollection<string> GetProviderList()
        {
            ObservableCollection<Product>? products = GetProductsList();
            ObservableCollection<string> providers = new ();
            providers.Add("Все поставщики");
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

        public ObservableCollection<string> GetCategoriesList()
        {
            ObservableCollection<Product>? products = GetProductsList();
            ObservableCollection<string> categories = new();
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

        public void RefreshDbContext()
        {
            _context = _contextFactory.CreateDbContext();
        }
    }
}
