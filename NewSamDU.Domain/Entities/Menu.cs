namespace NewSamDU.Domain.Entities;

public class Menu : BaseEntity
{
    public string NameUz { get; set; } = string.Empty;
    public string NameRu { get; set; } = string.Empty;

    public string NameEn { get; set; } = string.Empty;
    public int Priority { get; set; } = 1;

    public Menu? Parent { get; set; }
    public int? ParentId { get; set; }

    public Page? RelatedPage { get; set; }

    public int RelatedPageId { get; set; }
}
