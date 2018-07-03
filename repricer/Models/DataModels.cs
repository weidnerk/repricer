using System.ComponentModel.DataAnnotations.Schema;

[Table("PostedListings")]
public class PostedListing
{
    public int ID { get; set; }
    public int SourceID { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string EbayUrl { get; set; }

    public string EbayItemId { get; set; }
    public string SupplierItemId { get; set; }
    public string SourceUrl { get; set; }
    public decimal SupplierPrice { get; set; }
    public string PrimaryCategoryID { get; set; }
    public string PrimaryCategoryName { get; set; }
    public string Pictures { get; set; }
    public string Description { get; set; }
    public int CategoryID { get; set; }
    public string ListedItemID { get; set; }
}
