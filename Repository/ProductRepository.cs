using Users_Product_Security.DTO;
using Users_Product_Security.Interfaces;
using Users_Product_Security.Models;
using Product = Users_Product_Security.Models.Product;

namespace Users_Product_Security.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly Practice1Context _context;

        public ProductRepository(Practice1Context context)
        {
            _context = context;
        }
        //GET-IMP
        public List<Product> CreateProducts(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return _context.Products.ToList();
        }

        public string DeleteProduct(int id)
        {
            ///NOte that product table used as FK  in Users Table so before
            ///deleting the product record we must make sure that user delete first
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    throw new Exception("Product with ID:" + id + " Not Found");
                }
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return "Deleted successfully";
        }

        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }
        public Product ProductGetProductById(int id)
        {
            var prod = _context.Products.Find(id);
            if (prod == null)
            {
                throw new Exception("Product with ID:" + id + " Not Found");
            }
            else { return (prod); };
        }
        public List<Product> updateEmployee(Product product)
        {
            var oldProduct = _context.Products.Find(product.Pid);
            if (oldProduct == null)
            {
                throw new Exception("Product with ID:" + product.Pid + " Not Found");
            }

            oldProduct.Pname = product.Pname;
            _context.SaveChanges();

            return _context.Products.ToList();

        }
    }
}
