using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EFContext.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var thingy = Windsor.Container.Resolve<Thingy>();
            thingy.WaitWhenFinished = true;
            thingy.DoWork();
        }
    }
}
