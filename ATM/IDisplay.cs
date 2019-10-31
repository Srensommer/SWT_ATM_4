using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;

namespace ATM
{
    public interface IDisplay
    {
        void Render(List<TrackData> trackData);
        void Clear();
    }
}
