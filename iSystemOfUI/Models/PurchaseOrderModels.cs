using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
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
        public virtual DbSet<tblPurchaseOrder> tblPurchaseOrders { get; set; }
        public virtual DbSet<tblPurchaseOrderProduct> tblPurchaseOrderProducts { get; set; }
        public virtual DbSet<tblPurchaseOrderProductComponent> tblPurchaseOrderProductComponents { get; set; }
        public virtual DbSet<tblPurchaseOrderProductReturneImage> tblPurchaseOrderProductReturneImages { get; set; }
        public virtual DbSet<tblPurchaseOrderStatus> tblPurchaseOrderStatus { get; set; }
        public virtual DbSet<tblWarehouse> tblWarehouses { get; set; }
        public virtual DbSet<tblWarehouseProduct> tblWarehouseProducts { get; set; }
        public virtual DbSet<vwPurchaseOrder> vwPurchaseOrders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
    [Table("tblPurchaseOrder")]
    public partial class tblPurchaseOrder
    {
        [Key]
        public int Code { get; set; }

        public int? CustomerCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DeliveryDate { get; set; }

        public int? ProvineCode { get; set; }

        public int? DistricCode { get; set; }

        public int? WardCode { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(200)]
        public string Notes { get; set; }
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
        public int PurchaseOrderProductCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ComponentCode { get; set; }

        [StringLength(200)]
        public string Image { get; set; }
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
    [Table("tblWarehouse")]
    public partial class tblWarehouse
    {
        [Key]
        public int Code { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Notes { get; set; }
    }

    //----------------------------------------------------------------------------------------------
    [Table("tblWarehouseProduct")]
    public partial class tblWarehouseProduct
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WarehouseCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductCode { get; set; }

        public int Quantity { get; set; }
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
    }
}