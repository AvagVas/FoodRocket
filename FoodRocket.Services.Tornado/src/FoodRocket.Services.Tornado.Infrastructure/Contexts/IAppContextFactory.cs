using FoodRocket.Services.Tornado.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRocket.Services.Tornado.Infrastructure.Contexts
{
    public interface IAppContextFactory
    {
        IAppContext Create();
    }
}
