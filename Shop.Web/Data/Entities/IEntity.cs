using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data.Entities
{
    /// <summary>
    /// Interface with common properties for all classes that implement it
    /// </summary>
    public interface IEntity
    {
        public int Id { get; set; }
    }
}
