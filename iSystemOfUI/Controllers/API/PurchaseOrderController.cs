using iSystemOfUI.Models;
using iSystemOfUI.Models.PurchaseOrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iSystemOfUI.Controllers.API
{
    public class PurchaseOrderController : ApiController
    {
        DBContext db = new DBContext();

        // GET: api/PurchaseOrder/GetPurchaseOrderStatus
        public List<tblPurchaseOrderStatus> GetPurchaseOrderStatus()
        {
            return db.tblPurchaseOrderStatus.ToList();
        }

        // GET: api/PurchaseOrder/GetManufacture
        public List<tblManufacture> GetManufacture()
        {
            return db.tblManufactures.ToList();
        }

        // GET: api/PurchaseOrder/GetPurchaseOrder
        public List<vwPurchaseOrder> GetPurchaseOrder(int PurchaseOrderStatusCode = 1)
        {
            return db.vwPurchaseOrders.ToList();
        }

        // GET: api/PurchaseOrder/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PurchaseOrder
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/PurchaseOrder/PutPurchaseOrderProductModal1
        // sửa thông tin chi tiết sản phẩm trong đơn hàng từ modal 1
        public IHttpActionResult PutPurchaseOrderProductModal1(tblPurchaseOrderProduct model)
        {
            var ob = db.tblPurchaseOrderProducts.Where(x => x.PurchaseOrderCode == model.PurchaseOrderCode && x.ProductCode == model.ProductCode).FirstOrDefault();
            if (ob == null)
                return BadRequest("Không tìm thấy đơn hàng này");
            ob.ManufactureCode = model.ManufactureCode;
            ob.PurchaseOrderStatusCode = 2;
            db.SaveChanges();
            return Ok("Đã lưu thay đổi");
        }

        // PUT: api/PurchaseOrder/PutPurchaseOrderProductModal2
        // sửa thông tin chi tiết sản phẩm trong đơn hàng từ modal 2
        public IHttpActionResult PutPurchaseOrderProductModal2(tblPurchaseOrderProduct model)
        {
            var ob = db.tblPurchaseOrderProducts.Where(x => x.PurchaseOrderCode == model.PurchaseOrderCode && x.ProductCode == model.ProductCode).FirstOrDefault();
            if (ob == null)
                return BadRequest("Không tìm thấy đơn hàng này");
            if (String.IsNullOrEmpty(model.NoteEdit))
            {
                ob.ManufactureCode = model.ManufactureCode;
                ob.PurchaseOrderStatusCode = 3;
                ob.OrderQuantity = model.OrderQuantity;
            }
            else
            {
                ob.NoteEdit = model.NoteEdit;
                ob.PurchaseOrderStatusCode = 1;
                ob.ManufactureCode = null;
            }
            db.SaveChanges();
            return Ok("Đã lưu thay đổi");
        }

        // PUT: api/PurchaseOrder/PutPurchaseOrderProductModal3
        // sửa thông tin chi tiết sản phẩm trong đơn hàng từ modal 3
        public IHttpActionResult PutPurchaseOrderProductModal3(tblPurchaseOrderProduct model)
        {
            var ob = db.tblPurchaseOrderProducts.Where(x => x.PurchaseOrderCode == model.PurchaseOrderCode && x.ProductCode == model.ProductCode).FirstOrDefault();
            if (ob == null)
                return BadRequest("Không tìm thấy đơn hàng này");
            ob.DeliveryDate = model.DeliveryDate;
            ob.PurchaseOrderStatusCode = 4;
            ob.OrderQuantityDeliveryed = 0;
            db.SaveChanges();
            return Ok("Đã lưu thay đổi");
        }

        // PUT: api/PurchaseOrder/PutPurchaseOrderProductModal4
        // sửa thông tin chi tiết sản phẩm trong đơn hàng từ modal 4
        public IHttpActionResult PutPurchaseOrderProductModal4(tblPurchaseOrderProduct model)
        {
            var ob = db.tblPurchaseOrderProducts.Where(x => x.PurchaseOrderCode == model.PurchaseOrderCode && x.ProductCode == model.ProductCode).FirstOrDefault();
            if (ob == null)
                return BadRequest("Không tìm thấy đơn hàng này");
            ob.OrderQuantityDeliveryed = model.OrderQuantityDeliveryed;
            ob.OrderQuantityReturned = 0;

            if (ob.OrderQuantity <= ob.OrderQuantityDeliveryed)
                ob.PurchaseOrderStatusCode = 5;
            db.SaveChanges();
            return Ok("Đã lưu thay đổi");
        }

        // PUT: api/PurchaseOrder/PutPurchaseOrderProductModal5
        // sửa thông tin chi tiết sản phẩm trong đơn hàng từ modal 5
        public IHttpActionResult PutPurchaseOrderProductModal5(tblPurchaseOrderProduct model)
        {
            var ob = db.tblPurchaseOrderProducts.Where(x => x.PurchaseOrderCode == model.PurchaseOrderCode && x.ProductCode == model.ProductCode).FirstOrDefault();
            if (ob == null)
                return BadRequest("Không tìm thấy đơn hàng này");
            ob.OrderQuantityReturned = model.OrderQuantityReturned;
            db.SaveChanges();
            return Ok("Đã lưu thay đổi");
        }

        // DELETE: api/PurchaseOrder/5
        public void Delete(int id)
        {
        }
    }
}
