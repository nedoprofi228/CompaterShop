using System.Windows.Documents;


namespace CompaterShopWpf.Core.Entities;

public class Catalog
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<Category> Categories { get; set; }
}