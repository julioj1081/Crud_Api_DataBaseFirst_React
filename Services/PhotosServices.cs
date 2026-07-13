using ApiCoreID.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ApiCoreID.Services
{
    public class PhotosServices
    {
        private readonly FotosCrudContext _context;
        public PhotosServices(FotosCrudContext context)
        {
            this._context = context;
        }

        public async Task<bool> InsertFoto(Photo model)
        {
            var entity = new Photo()
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Url = model.Url,
            };

            await _context.Photos.AddAsync(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Photo>> GetPhotos()
        {
            return await _context.Photos.Select(p => new Photo
            {
                IdPhoto = p.IdPhoto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Url = p.Url
            }).ToListAsync();
        }

        public async Task<bool> DeletePhoto(int id)
        {
            var entity = await _context.Photos.Where(x => x.IdPhoto == id).FirstOrDefaultAsync();
            if (entity != null)
            {
                _context.Photos.Remove(entity);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }

        public async Task<bool> UpdatePhotos(Photo model)
        {
            var entity = await _context.Photos.Where(x => x.IdPhoto == model.IdPhoto).FirstOrDefaultAsync();
            if(entity != null)
            {
                entity.IdPhoto = model.IdPhoto;
                entity.Nombre = model.Nombre;
                entity.Descripcion = model.Descripcion;
                entity.Url = model.Url;

                _context.Photos.Update(entity);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }
    }
}
