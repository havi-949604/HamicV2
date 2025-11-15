using System;
using System.Collections.Generic;

namespace Harmic.Models;

public partial class TbBlogcomment
{
    public int CommentId { get; set; }

    public string? Name { get; set; }

    public int AccountId { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Detail { get; set; }

    public int? BlogId { get; set; }

    public bool IsActive { get; set; }

    public virtual TbAccount Account { get; set; } = null!;

    public virtual TbBlog? Blog { get; set; }
}
