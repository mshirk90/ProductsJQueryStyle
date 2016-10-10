using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BusinessObjects;
using System.ComponentModel;
using System.Web.Script.Services;
namespace ProductsJQueryStyle
{
    /// <summary>
    /// Summary description for ProductWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 

    [ScriptService]
    public class ProductWS : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat=ResponseFormat.Json)]
        public BindingList<Product> GetProductsByCategoryId(string id)
        {
            ProductList products = new ProductList();
            products = products.GetByCategoryId(new Guid(id));
            return products.List;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public BindingList<Category> CategoryGetAll()
        {
            CategoryList categories = new CategoryList();
            categories = categories.GetAll();
            return categories.List;
        }
    }
}
