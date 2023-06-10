using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using MNSGamesWebAPI.Models;

namespace MNSGamesWebAPI.Services
{
    public class CascadeDeleteService
    {
        private readonly MNS_Games_DBContext _context;

        public CascadeDeleteService(MNS_Games_DBContext context)
        {
            _context = context;
        }

        public async Task DeleteEntityAndRelatedEntities<TEntity>(DbSet<TEntity> dbSet, int id) where TEntity : class
        {
            var entity = await dbSet.FindAsync(id);
            if (entity == null)
            {
                return;
            }

            await DeleteRelatedEntities(entity);

            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRelatedEntities(object entity)
        {
            var navigationProperties = _context.Entry(entity).Navigations;
            foreach (var navigationProperty in navigationProperties)
            {
                if (navigationProperty is CollectionEntry collectionEntry)
                {
                    await collectionEntry.LoadAsync();
                    var relatedEntities = collectionEntry.CurrentValue as IEnumerable<object>;
                    if (relatedEntities != null)
                    {
                        foreach (var relatedEntity in relatedEntities)
                        {
                            await DeleteRelatedEntities(relatedEntity);
                            _context.Remove(relatedEntity);
                        }
                    }
                }
                else if (navigationProperty is ReferenceEntry referenceEntry)
                {
                    await referenceEntry.LoadAsync();
                    var relatedEntity = referenceEntry.CurrentValue;
                    if (relatedEntity != null)
                    {
                        await DeleteRelatedEntities(relatedEntity);
                        _context.Remove(relatedEntity);
                    }
                }
            }
        }
    }
}
