﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.Entities.Repo_Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        public ICategoryRepo CategoryRepo { get; }
        public int Save();
    }
}
