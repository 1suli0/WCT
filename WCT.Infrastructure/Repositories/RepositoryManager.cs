using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WCT.Core;
using WCT.Infrastructure.DBContexts;
using WCT.Infrastructure.Interfaces;

namespace WCT.Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly DBContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private IRoleRepository _roleRepository;
        private IUserRepository _userRepository;
        private IProductRepository _productRepository;
        private IShoppingListRepository _shoppingListRepository;

        public IRoleRepository RoleRepository
        {
            get
            {
                if (this._roleRepository == null)
                    this._roleRepository = new RoleRepository(this._roleManager);

                return this._roleRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (this._userRepository == null)
                    this._userRepository = new UserRepository(this._userManager,
                        this._signInManager, this._context);

                return this._userRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (this._productRepository == null)
                    this._productRepository = new ProductRepository(this._context);

                return this._productRepository;
            }
        }

        public IShoppingListRepository ShoppingListRepository
        {
            get
            {
                if (this._shoppingListRepository == null)
                    this._shoppingListRepository = new ShoppingListRepository(this._context);

                return this._shoppingListRepository;
            }
        }

        public RepositoryManager(DBContext context, UserManager<User> userManager,
            SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }

        public async Task SaveAsync()
        {
            await this._context.SaveChangesAsync();
        }
    }
}