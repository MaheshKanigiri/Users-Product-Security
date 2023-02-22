using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users_Product_Security.Interfaces;
using Users_Product_Security.Models;
using Users_User_Security.Interfaces;

namespace Users_Product_Security.Controllers
{
    [ApiController]
    [Route("api/[Controller]",Name="Access-Controller")]
    public class AccessController:ControllerBase
    {
        private readonly IUser _repo;
        private readonly IProduct _prepo;
        private readonly ILogger<AccessController> _logger;

        public AccessController(IUser repo,IProduct prepo,ILogger<AccessController> logger) {
            _repo = repo;
            _prepo = prepo;
            _logger = logger;
        }
         
        [HttpGet,Authorize(Roles ="Admin")]
        [Route("Admin/GetallUsers")]
        public List<User> GetUsers()
        {
            _logger.LogInformation("WEATHERS CONTROLLER");
            var users= _repo.GetUsers();
            return users;
            
        }
        [HttpGet,Authorize(Roles ="User,Admin")]
        [Route("UserAdminGet/AllProds")]
        public List<Product> getProducts()
        {
            return _prepo.GetProducts();
        }
        [HttpPost,Authorize(Roles ="Admin,User")]
        [Route("AdminUserAdd/Products")]
        public List<Product> CreateProduct(Product product)
        {
            return _prepo.CreateProducts(product);
        }

        [HttpDelete("Admin/DeleteProd{id}"),Authorize(Roles ="Admin")]
        public String DeleteProduct(int id)
        {
            return _prepo.DeleteProduct(id);
        }
        [HttpDelete("Admin/DeleteUser/{id}"),Authorize(Roles ="Admin")]
        public String DeleteUser(int id)
        {
            return _repo.DeleteUser(id);
        }
    }
}
