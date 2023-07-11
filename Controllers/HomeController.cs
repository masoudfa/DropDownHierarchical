using Dapper;
using ShopEntity;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace DropDownHierarchical.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var ConnectionString = ConfigurationManager.ConnectionStrings["ShopConnectionString"].ConnectionString;
            IDbConnection sqlConnection = new SqlConnection(ConnectionString);
            var query = @"select BrandID,BrandName,BrandEnName,BrandLogo ,IsActive from dbo.Brand where IsActive=1";
            var connection = sqlConnection;
            var res = connection.Query<Brand>(query).ToList();
            ViewBag.Brands = res;

            return View(res);
        }

        public ActionResult GetModelByBrandID(short BrandId)
        {
            var ConnectionString = ConfigurationManager.ConnectionStrings["ShopConnectionString"].ConnectionString;
            IDbConnection sqlConnection = new SqlConnection(ConnectionString);
            var query = @"Select CarModelId,BrandID,BrandName,ModelName,CarModelBrandId,IsActive From CarModelView Where BrandID = @BrandID And IsActive = 1";
            var param = new DynamicParameters();
            param.Add("@BrandID", BrandId, DbType.Int16);
            var connection = sqlConnection;
            var res = connection.Query<CarModelView>(query, param).ToList();
            ViewBag.Models = res;

            return PartialView("_ModelsOptions", res);
        }

        public ActionResult GetCarModelYear(short CarModelId)
        {
            var ConnectionString = ConfigurationManager.ConnectionStrings["ShopConnectionString"].ConnectionString;
            IDbConnection sqlConnection = new SqlConnection(ConnectionString);
            var query = @"SELECT     CarModelYearId, CarModelId, azYear, TaYear, IsActive
                    FROM            CarModelYear
                Where CarModelId = @CarModelId And IsActive = 1";

            var param = new DynamicParameters();
            param.Add("@CarModelId", CarModelId, DbType.Int16);
            var connection = sqlConnection;
            var res = connection.Query<CarModelYear>(query, param).ToList();
            ViewBag.CarModelYear = res;

            return PartialView("_CarModelYearOption", res);
        }
    }
}