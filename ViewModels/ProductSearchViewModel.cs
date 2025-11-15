namespace Harmic.ViewModels
{
    public class ProductSearchViewModel
    {
        public int CategoryId { get; set; } = 0;
        public int Page { get; set; } = 1;
        public string SearchText { get; set; } = "";
        public bool onSale { get; set; } = false;
        public bool isNew { get; set; } = false;
        public bool inStock { get; set; } = false;

    }
}
