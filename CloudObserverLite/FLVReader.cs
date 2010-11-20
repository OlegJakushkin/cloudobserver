﻿using System.IO;

namespace CloudObserverLite
{
    public class FLVReader
    {
        public Stream stream;
        public uint timestampDelta;

        public FLVReader(Stream stream, uint timestampDelta)
        {
            this.stream = stream;
            this.timestampDelta = timestampDelta;
        }
    }
}
