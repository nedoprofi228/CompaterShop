using CompaterShopWpf;
using CompaterShopWpf.Core.Entities;
using CompaterShopWpf.DataBase;
using Microsoft.EntityFrameworkCore;


public class AdminService
{
    private readonly ApplicationContext _dbContext = ApplicationContext.GetInstance();
    
    public long AddItem(Item item)
    {
        _dbContext.Items.Add(item);
        _dbContext.SaveChanges();
        return item.Id;
    }
    
    public void UpdateItem(Item updatedItem)
    {
        var existingItem = _dbContext.Items.Find(updatedItem.Id) ??
                           throw new Exception("Item not found");

        existingItem.Name = updatedItem.Name;
        existingItem.Description = updatedItem.Description;
        existingItem.Price = updatedItem.Price;
        existingItem.CategoryId = updatedItem.CategoryId;

        _dbContext.SaveChanges();
    }

    public bool DeleteItem(long id)
    {
        var itemToDelete = _dbContext.Items.Find(id) ?? throw new Exception("Item not found");
        
        _dbContext.Items.Remove(itemToDelete); 
        return _dbContext.SaveChanges() > 0;
    }

    public long AddCategory(Category category)
    {
        _dbContext.Categories.Add(category);
        _dbContext.SaveChanges();
        return category.Id;
    }

    public bool UpdateCategory(Category updatedCategory)
    {
        var existingCategory = _dbContext.Categories.Find(updatedCategory.Id) ?? 
                               throw new Exception("Category not found");

        existingCategory.CatalogId = updatedCategory.CatalogId;
        existingCategory.CategoryName = updatedCategory.CategoryName;
        existingCategory.Items = updatedCategory.Items;

        return _dbContext.SaveChanges() > 0;
    }

    public bool DeleteCategory(long id)
    {
        var categoryToDelete = _dbContext.Categories.Find(id) ?? throw new Exception("Category not found");
        
        _dbContext.Categories.Remove(categoryToDelete);
        foreach (var item in _dbContext.Items.Where(i => i.CategoryId == categoryToDelete.Id))
            _dbContext.Items.Remove(item);
        
        return _dbContext.SaveChanges() > 0;
    }

    public long AddCatalog(Catalog catalog)
    {
        var entity = _dbContext.Catalogs.Add(catalog);
        _dbContext.SaveChanges();
        return catalog.Id;
    }

    public bool UpdateCatalog(Catalog updatedCatalog)
    {
        var existingCatalog = _dbContext.Catalogs.Find(updatedCatalog.Id) ??
                              throw new Exception("Catalog not found");
        
        existingCatalog.Name = updatedCatalog.Name;
        return _dbContext.SaveChanges() > 0;
    }

    public bool DeleteCatalog(long id)
    {
        var catalogToDelete = _dbContext.Catalogs.Find(id) ?? throw new Exception("Catalog not found");
        _dbContext.Catalogs.Remove(catalogToDelete);
        foreach (var category in _dbContext.Categories.Where(c => c.CatalogId == catalogToDelete.Id))
        {
            _dbContext.Categories.Remove(category);
        }
        
        return _dbContext.SaveChanges() > 0;
    }
}