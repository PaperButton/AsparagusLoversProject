using System;
using System.Collections.Generic;
using System.Linq;
using AsparagusLoversProject.Repositories;
using AsparagusLoversProject.ViewModels;

namespace AsparagusLoversProject.Domain
{
    public class AsparagusLoverRepository<TLover> : ILoverRepository<TLover>
    where TLover: ILover//ILoverRepository<AsparagusLover> 
    {
        private readonly AppDbContext context;
        public AsparagusLoverRepository(AppDbContext context)
        {
            this.context = context;
        }
        /*public IQueryable<AsparagusLover> GetAsparagusLovers()
        {
            
        }
        public AsparagusLover GetAsparagusLoverById(Guid id)
        {
            
        }
        public Guid SaveAsparagusLover(AsparagusLover entity)
        {
            
        }
        public void DeleteAsparagusLover(AsparagusLover entity)
        {
            
        }*/



        /*--------

        public IQueryable<AsparagusLover> GetLovers()
        {
            return context.Lovers.OrderByDescending(x => x.Fname);
        }

        public AsparagusLover GetLoverById(Guid id)
        {
            return context.Lovers.Single(x => x.LoverID == id);
        }

        public Guid SaveLover(GetLoverDataForEatingViewModel entity)
        {
            ILover lover = new AsparagusLover { Fname = entity.LoverFname, EMail = entity.LoverEMail};
            if (context.Lovers.Where(x => x.EMail == entity.LoverEMail).ToList().Count == 0)
            {
                // if (entity.LoverID == Guid.Empty)
                // entity.LoverID = Guid.NewGuid();
                lover.LoverID = Guid.NewGuid();
                context.Entry(lover).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                context.SaveChanges();
            }
            else
            {
                lover.LoverID = context.Lovers.Single(x => x.EMail == entity.LoverEMail).LoverID;
            }
                
            return lover.LoverID;
        }

        public void DeleteLover(AsparagusLover entity)
        {
            context.Lovers.Remove(entity);
            context.SaveChanges();
        }
        */



      
        public IQueryable<TLover> GetLovers()
        {
            return (IQueryable<TLover>)context.Lovers.OrderByDescending(x => x.Fname);
        }

        public TLover GetLoverById(Guid id)
        {
            return (TLover)System.Convert.ChangeType(context.Lovers.Single(x => x.LoverID == id), typeof(TLover));
        }
        public Guid SaveLover(GetLoverDataForEatingViewModel inputLoverData)
        {
            ILover lover = new AsparagusLover { Fname = inputLoverData.LoverFname, EMail = inputLoverData.LoverEMail, 
                                                AuthenticationProviderrr = new AuthenticationProviderrr() 
                                                    {AuthenticationProviderrrID = inputLoverData.ProviderId },
                                                ExternalId= inputLoverData.ExternalId };

            if (context.Lovers.Where(x => (x.EMail == inputLoverData.LoverEMail && string.IsNullOrEmpty(x.EMail) == false) || 
                                    (x.AuthenticationProviderrr.AuthenticationProviderrrID == inputLoverData.ProviderId && x.ExternalId == inputLoverData.ExternalId))
                                    .ToList().Count == 0)
            {
                lover.LoverID = Guid.NewGuid();
                context.Entry(lover).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                context.SaveChanges();
            }
            else
            {
                lover.LoverID = context.Lovers.Single(x => (x.EMail == inputLoverData.LoverEMail && 
                                                        string.IsNullOrEmpty(x.EMail) == false) ||
                                                        (x.AuthenticationProviderrr.AuthenticationProviderrrID == inputLoverData.ProviderId 
                                                        && x.ExternalId == inputLoverData.ExternalId)).LoverID;
            }
           
            return lover.LoverID;
        }

        public void DeleteLover(TLover entity)
        {
            context.Lovers.Remove(context.Lovers.Single(x => x.LoverID == entity.LoverID)); 
            context.SaveChanges();
        }

        
    }
}
