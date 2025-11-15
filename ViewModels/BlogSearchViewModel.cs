namespace Harmic.ViewModels
{
    public class BlogSearchViewModel
    {
        public int CategoryId { set; get; } = 0;
        public int Page { set; get; } = 1;
        public string SearchText { set; get; } = "";
        public string SortBy { set; get; } = "NewFirst";

    }
}
