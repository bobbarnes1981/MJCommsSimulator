namespace MJCommsSimulator
{
    class State
    {
        public byte advanceDegrees;
        public byte rpmHi;
        public byte rpmLo;
        public byte bin; // rpm (hi), load (lo)
        public byte load;
        public byte state; // out1, out2, out3, out4, revLimit, shitfLight, reserved, config
        public byte aux;
        public byte advanceCorrectionBin;
        public byte advanceCorrectionDeg;

        public byte[] ToBytes()
        {
            return new byte[]
            {
                advanceDegrees,
                rpmHi,
                rpmLo,
                bin,
                load,
                state,
                aux,
                advanceCorrectionBin,
                advanceCorrectionDeg
            };
        }
    }
}
