using Microsoft.EntityFrameworkCore;
using Vaccinatedapi.data;
using Vaccinatedapi.models;
using Vaccinatedapi.Repository.Abstract;

namespace Vaccinatedapi.Repository.implemantion
{
    public class ProductRepostory: IProductRepository
	{

		private readonly dbdatacontexts _context;
		public ProductRepostory(dbdatacontexts context)
		{
			this._context = context;
		}
		public bool Add(parents model)
		{
			try
			{
				_context.parents.Add(model);
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{

				return false;
			}
		}	public Task<bool> Edit(parents model)
		{
			try
			{
               _context.Entry(model).State = EntityState.Modified;
			   _context.SaveChangesAsync();
				return Task.FromResult(true);
			}
			catch (Exception ex)
			{

				return Task.FromResult(false);
			}
		}

   
    }
}
