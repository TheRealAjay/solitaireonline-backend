﻿using Solitaire.DataAccess.Context;
using Solitaire.DataAccess.Repositories.IRepositories;
using Solitaire.Models;

namespace Solitaire.DataAccess.Repositories
{
    public class ScoreRepository : Repository<Score>, IScoreRepository
    {
        public ScoreRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
