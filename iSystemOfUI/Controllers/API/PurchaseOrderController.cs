using iSystemOfUI.Models;
using iSystemOfUI.Models.PurchaseOrderModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace iSystemOfUI.Controllers.API
{
    public class PurchaseOrderController : ApiController
    {
        DBContext db = new DBContext();

        // GET
        //------------------------------------------------------------------------------------------------

        // GET: api/PurchaseOrder/GetPurchaseOrderStatus
        // lấy danh sách trạng thái đơn hàng đặt hàng đến nhà cung cấp
        public List<tblPurchaseOrderStatus> GetPurchaseOrderStatus()
        {
            return db.tblPurchaseOrderStatus.ToList();
        }

        // GET: api/PurchaseOrder/GetManufacture
        // lấy danh sách nhà cung cấp
        public List<tblManufacture> GetManufacture()
        {
            return db.tblManufactures.ToList();
        }

        // GET: api/PurchaseOrder/GetPurchaseOrder
        // lấy danh sách các đơn hàng kèm chi tiết đơn hàng đặt đến nhà cung cấp
        public List<vwPurchaseOrder> GetPurchaseOrder(int PurchaseOrderStatusCode = 1)
        {
            return db.vwPurchaseOrders.Where(x=>x.PurchaseOrderStatusCode == PurchaseOrderStatusCode).ToList();
        }


        // GET: api/PurchaseOrder/GetPurchaseOrderProductComponent
        // lấy danh sách các component của chi tiết đơn hàng đặt đến nhà cung cấp
        public List<vwPurchaseOrderProductComponent> GetPurchaseOrderProductComponent(int PurchaseOrderCode, int ProductCode)
        {
            return db.vwPurchaseOrderProductComponents.Where(x => x.PurchaseOrderCode == PurchaseOrderCode && x.ProductCode == ProductCode).ToList();
        }



        // PUT
        //-------------------------------------------------------------------------------------------

        // PUT: api/PurchaseOrder/PutPurchaseOrderProductModalToNextStepMulti
        // sửa thông tin chi tiết sản phẩm trong đơn hàng từ modal 2
        public IHttpActionResult PutPurchaseOrderProductModalToNextStepMulti() {
            var POPs = db.tblPurchaseOrderProducts.Where(x => x.PurchaseOrderStatusCode == 2).ToList();
            foreach (var item in POPs)
            {
                item.PurchaseOrderStatusCode = 3;
                var ob = db.vwPurchaseOrders.Where(x => x.ProductCode == item.ProductCode && x.PurchaseOrderCode == item.PurchaseOrderCode).First();
                item.OrderQuantity = item.SaleQuantity - ob.ProductInventory ;
            }
            db.SaveChanges();
            return Ok("Đã lưu thay đổi");
        }

        // PUT: api/PurchaseOrder/PutPurchaseOrderProductModalToNextStep
        // sửa thông tin chi tiết sản phẩm trong đơn hàng từ modal 
        public IHttpActionResult PutPurchaseOrderProductModalToNextStep()
        {
            // lấy thông tin POProduct
            var FormBody = HttpContext.Current.Request.Form;
            // get step
            int step = int.Parse(FormBody.Get("Step"));

            var model = new tblPurchaseOrderProduct
            {
                PurchaseOrderCode = int.Parse(FormBody.Get("PurchaseOrderCode")),
                ProductCode = int.Parse(FormBody.Get("ProductCode")),
            };

            var ob = db.tblPurchaseOrderProducts.Where(x => x.PurchaseOrderCode == model.PurchaseOrderCode && x.ProductCode == model.ProductCode).FirstOrDefault();
            if (ob == null)
                return BadRequest("Không tìm thấy đơn hàng này");

            // check step 
            switch (step)
            {
                case 1:
                    {
                        ob.ManufactureCode = int.Parse(FormBody.Get("ManufactureCode"));
                        ob.PurchaseOrderStatusCode = 2;
                        ResolveImgsUploadModal1_2(HttpContext.Current.Request.Files, ob);
                        break;
                    }
                case 2:
                    {
                        ob.ManufactureCode = int.Parse(FormBody.Get("ManufactureCode"));
                        ob.NoteEdit = FormBody.Get("NoteEdit");
                        if (String.IsNullOrEmpty(ob.NoteEdit))
                        {
                            ob.PurchaseOrderStatusCode = 3;
                            ob.OrderQuantity = int.Parse(FormBody.Get("OrderQuantity"));
                        }
                        else
                        {
                            ob.PurchaseOrderStatusCode = 1;
                        }
                        ResolveImgsUploadModal1_2(HttpContext.Current.Request.Files, ob);
                        break;
                    }
                case 3:
                    {
                        ob.DeliveryDate = Convert.ToDateTime(FormBody.Get("DeliveryDate"));
                        ob.PurchaseOrderStatusCode = 4;
                        ob.OrderQuantityDeliveryed = 0;
                        break;
                    }
                case 4:
                    {
                        ob.OrderQuantityDeliveryed = int.Parse(FormBody.Get("OrderQuantityDeliveryed"));
                        ob.OrderQuantityReturned = 0;
                        if (ob.OrderQuantity <= ob.OrderQuantityDeliveryed)
                            ob.PurchaseOrderStatusCode = 5;
                        break;
                    }
                case 5:
                    {
                        ob.OrderQuantityReturned = int.Parse(FormBody.Get("OrderQuantityReturned"));
                        break;
                    }
            }
            db.SaveChanges();
            return Ok("Đã lưu thay đổi");
        }

        // xử lý và lưu hình ảnh step 1 và 2
        private void ResolveImgsUploadModal1_2(HttpFileCollection files, tblPurchaseOrderProduct ob)
        {
            var PurchaseOrderProductImage = files.Get("PurchaseOrderProductImage");
            if (PurchaseOrderProductImage!=null && PurchaseOrderProductImage.ContentLength > 0)
            {
                var fl = FileUploadViewModel.ResolveFileUpload(PurchaseOrderProductImage);
                if (!String.IsNullOrEmpty(ob.Image))
                {
                    FileUploadViewModel.DeleteImg(ob.Image);
                }
                ob.Image = fl.FileName;
                PurchaseOrderProductImage.SaveAs(fl.FilePath);
            }
            foreach (var item in db.vwPurchaseOrderProductComponents.Where(x=>x.PurchaseOrderCode == ob.PurchaseOrderCode && x.ProductCode == ob.ProductCode).ToList())
            {
                // lấy hình ảnh của từng component theo key được set từ trước với công thức "PurchaseOrderProductComponentImage_" + item.ComponentCode
                var PurchaseOrderProductComponentImage = files.Get("PurchaseOrderProductComponentImage_" + item.ComponentCode);
                if (PurchaseOrderProductComponentImage != null && PurchaseOrderProductComponentImage.ContentLength > 0)
                {
                    var fl = FileUploadViewModel.ResolveFileUpload(PurchaseOrderProductComponentImage);
                    if (!String.IsNullOrEmpty(item.Image))
                    {
                        FileUploadViewModel.DeleteImg(item.Image);
                    }
                    item.Image = fl.FileName;
                    PurchaseOrderProductComponentImage.SaveAs(fl.FilePath);
                }
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
