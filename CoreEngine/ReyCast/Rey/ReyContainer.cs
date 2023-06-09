using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreEngine.ReyCast{
    public interface ReyContainer{

        IEnumerable<Hit> GetFloreHit();
        IEnumerable<Hit> GetWallHit();

        Hit GetLastHit(Hit type);
        Hit GetFirstHit(Hit type);

    }
}
