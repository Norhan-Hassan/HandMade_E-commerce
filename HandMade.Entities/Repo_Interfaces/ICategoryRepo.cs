using HandMade.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.Entities.Repo_Interfaces
{
    public interface ICategoryRepo:IGenericRepo<Category>
    {
        void Update(Category category);
    }
}
