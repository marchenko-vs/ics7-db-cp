using BlitzFlug.Repositories;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace BlitzFlug.Models
{
    public class Plane
    {
        private IPlaneRepository<Plane> _db;

        [Key]
        public Int64 Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public uint EconomyClassNum{ get; set; }
        public uint FirstClassNum { get; set; }
        public uint BusinessClassNum { get; set; }

        public Plane()
        {
            this.Manufacturer = string.Empty;
            this.Model = string.Empty;

            var factory = new MsSqlRepositoryFactory();
            this._db = factory.CreatePlaneRepository();
        }

        public IEnumerable<Plane> GetAllPlanes()
        {
            return this._db.GetAllPlanes();
        }
    }
}
