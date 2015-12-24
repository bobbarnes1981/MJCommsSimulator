namespace MJCommsSimulator
{
    class Version
    {
        public byte major;
        public byte minor;
        public byte bugfix;

        public byte[] ToBytes()
        {
            return new byte[]
            {
                major,
                minor,
                bugfix
            };
        }
    }
}
