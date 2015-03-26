using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloModel;
using TrelloModel.Interfaces;

namespace TrelloModelTests
{
    public class NinjectBindings : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
           /* Bind<IRepository<Board>>().To<BoardRepositoryTest>();
            Bind<IRepository<Card>>().To<BoardRepositoryTest>();
            Bind<IRepository<List>>().To<BoardRepositoryTest>();*/
        }
    }
}
