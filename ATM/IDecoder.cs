﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ATM
{
    public interface IDecoder
    {
        List<TrackData> Decode(RawTransponderDataEventArgs e);
    }
}
