using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace iSystemOfUI.Models.PurchaseOrderModels
{
    public class PurchaseOrderModels
    {

    }

    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<tblManufacture> tblManufactures { get; set; }
        public virtual DbSet<tblPurchaseOrderProduct> tblPurchaseOrderProducts { get; set; }
        public virtual DbSet<tblPurchaseOrderProductComponent> tblPurchaseOrderProductComponents { get; set; }
        public virtual DbSet<tblPurchaseOrderProductReturneImage> tblPurchaseOrderProductReturneImages { get; set; }
        public virtual DbSet<tblPurchaseOrderStatus> tblPurchaseOrderStatus { get; set; }
        public virtual DbSet<vwPurchaseOrder> vwPurchaseOrders { get; set; }
        public virtual DbSet<vwPurchaseOrderProductComponent> vwPurchaseOrderProductComponents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }

    //----------------------------------------------------------------------------------------------
    public class FileUploadViewModel
    {
        private const string UPLOAD_FOLDER_NAME = @"/assets/uploads";
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public static FileUploadViewModel ResolveFileUpload(HttpPostedFile file)
        {
            var fl = new FileUploadViewModel();
            fl.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetFileName(file.FileName);
            fl.FilePath = Path.Combine(HttpContext.Current.Server.MapPath(UPLOAD_FOLDER_NAME), fl.FileName);
            return fl;
        }

        public static void DeleteImg(string fileName)
        {
            try
            {
                string path = Path.Combine(
                    HttpContext.Current.Server.MapPath(UPLOAD_FOLDER_NAME),
                    fileName
                );
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch (Exception)
            {
            }
        }
    }




    //----------------------------------------------------------------------------------------------
    [Table("tblManufacture")]
    public partial class tblManufacture
    {
        [Key]
        public int Code { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }
    }


    //----------------------------------------------------------------------------------------------
    [Table("tblPurchaseOrderProduct")]
    public partial class tblPurchaseOrderProduct
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PurchaseOrderCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductCode { get; set; }

        public int? PurchaseOrderStatusCode { get; set; }

        public int? ManufactureCode { get; set; }

        public int? SaleQuantity { get; set; }

        public int? OrderQuantity { get; set; }

        public int? OrderQuantityDeliveryed { get; set; }

        public int? OrderQuantityReturned { get; set; }

        [StringLength(200)]
        public string OrderQuantityReturnedReason { get; set; }

        [StringLength(200)]
        public string NoteEdit { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DeliveryDate { get; set; }

        [StringLength(200)]
        public string Image { get; set; }

        [StringLength(200)]
        public string Notes { get; set; }
    }

    //----------------------------------------------------------------------------------------------
    [Table("tblPurchaseOrderProductComponent")]
    public partial class tblPurchaseOrderProductComponent
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PurchaseOrderCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ComponentCode { get; set; }

        [StringLength(200)]
        public string Image { get; set; }

        public int? ColorCode { get; set; }

        public int? MaterialCode { get; set; }

        [StringLength(50)]
        public string Width { get; set; }

        [StringLength(50)]
        public string Depth { get; set; }

        [StringLength(50)]
        public string Height { get; set; }

    }

    //----------------------------------------------------------------------------------------------
    [Table("tblPurchaseOrderProductReturneImage")]
    public partial class tblPurchaseOrderProductReturneImage
    {
        [Key]
        public int Code { get; set; }

        public int? PurchaseOrderCode { get; set; }

        public int? ProductCode { get; set; }

        [StringLength(200)]
        public string Image { get; set; }
    }

    //----------------------------------------------------------------------------------------------
    public partial class tblPurchaseOrderStatus
    {
        [Key]
        public int Code { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }
    }



    //----------------------------------------------------------------------------------------------
    [Table("vwPurchaseOrder")]
    public partial class vwPurchaseOrder
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PurchaseOrderCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PurchaseOrderDeliveryDate { get; set; }

        public int? CustomerCode { get; set; }

        [StringLength(200)]
        public string CustomerName { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductCode { get; set; }

        public int? SaleQuantity { get; set; }

        public int? OrderQuantity { get; set; }

        public int? OrderQuantityDeliveryed { get; set; }

        public int? OrderQuantityReturned { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PurchaseOrderProductDeliveryDate { get; set; }

        [StringLength(200)]
        public string ProductGroupName { get; set; }

        [StringLength(200)]
        public string ProductTypeName { get; set; }

        [StringLength(50)]
        public string ProductWidth { get; set; }

        [StringLength(50)]
        public string ProductHeight { get; set; }

        [StringLength(50)]
        public string ProductDepth { get; set; }

        public int? ManufactureCode { get; set; }

        public int? PurchaseOrderStatusCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string PurchaseOrderStatusName { get; set; }

        [StringLength(200)]
        public string ProductName { get; set; }

        public int? ProductInventory { get; set; }

        [StringLength(200)]
        public string PurchaseOrderProductNotes { get; set; }

        [StringLength(200)]
        public string PurchaseOrderProductNoteEdit { get; set; }

        [StringLength(200)]
        public string UnitName { get; set; }

        public int? ColorCode { get; set; }

        public int? MaterialCode { get; set; }

        [StringLength(200)]
        public string ProductColorName { get; set; }

        [StringLength(200)]
        public string ProductMaterialName { get; set; }

        [StringLength(200)]
        public string PurchaseOrderNotes { get; set; }

        [StringLength(200)]
        public string PurchaseOrderProductImage { get; set; }
    }

    //----------------------------------------------------------------------------------------------
    [Table("vwPurchaseOrderProductComponent")]
    public partial class vwPurchaseOrderProductComponent
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PurchaseOrderCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ComponentCode { get; set; }

        [StringLength(200)]
        public string Image { get; set; }

        public int? ColorCode { get; set; }

        public int? MaterialCode { get; set; }

        [StringLength(50)]
        public string Width { get; set; }

        [StringLength(50)]
        public string Depth { get; set; }

        [StringLength(50)]
        public string Height { get; set; }

        [StringLength(200)]
        public string MaterialName { get; set; }

        [StringLength(200)]
        public string ComponentName { get; set; }

        [StringLength(200)]
        public string ColorName { get; set; }
    }
}