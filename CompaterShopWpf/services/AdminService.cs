using CompaterShopWpf;
using CompaterShopWpf.Core.Entities;
using CompaterShopWpf.DataBase;
using Microsoft.EntityFrameworkCore;


public class AdminService
{
    private readonly ApplicationContext _context = ApplicationContext.GetInstance();
    
    public long AddItem(Item item)
    {
        _context.Items.Add(item);
        _context.SaveChanges();
        return item.Id;
    }
    
    public void UpdateItem(Item updatedItem)
    {
        var existingItem = _context.Items.Find(updatedItem.Id) ??
                           throw new Exception("Item not found");

        existingItem.Name = updatedItem.Name;
        existingItem.Description = updatedItem.Description;
        existingItem.Price = updatedItem.Price;
        existingItem.CategoryId = updatedItem.CategoryId;

        _context.SaveChanges();
    }

    public bool DeleteItem(long id)
    {
        var itemToDelete = _context.Items.Find(id) ?? throw new Exception("Item not found");
        
        _context.Items.Remove(itemToDelete); 
        return _context.SaveChanges() > 0;
    }

    public long AddCategory(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
        return category.Id;
    }

    public bool UpdateCategory(Category updatedCategory)
    {
        var existingCategory = _context.Categories.Find(updatedCategory.Id) ?? 
                               throw new Exception("Category not found");

        existingCategory.CatalogId = updatedCategory.CatalogId;
        existingCategory.CategoryName = updatedCategory.CategoryName;
        existingCategory.Items = updatedCategory.Items;

        return _context.SaveChanges() > 0;
    }

    public bool DeleteCategory(long id)
    {
        var categoryToDelete = _context.Categories.Find(id) ?? throw new Exception("Category not found");
        
        _context.Categories.Remove(categoryToDelete);
        foreach (var item in _context.Items.Where(i => i.CategoryId == categoryToDelete.Id))
            _context.Items.Remove(item);
        
        return _context.SaveChanges() > 0;
    }

    public long AddCatalog(Catalog catalog)
    {
        var entity = _context.Catalogs.Add(catalog);
        _context.SaveChanges();
        return catalog.Id;
    }

    public bool UpdateCatalog(Catalog updatedCatalog)
    {
        var existingCatalog = _context.Catalogs.Find(updatedCatalog.Id) ??
                              throw new Exception("Catalog not found");
        
        existingCatalog.Name = updatedCatalog.Name;
        return _context.SaveChanges() > 0;
    }

    public bool DeleteCatalog(long id)
    {
        var catalogToDelete = _context.Catalogs.Find(id) ?? throw new Exception("Catalog not found");
        _context.Catalogs.Remove(catalogToDelete);
        foreach (var category in _context.Categories.Where(c => c.CatalogId == catalogToDelete.Id))
        {
            _context.Categories.Remove(category);
        }
        
        return _context.SaveChanges() > 0;
    }
}