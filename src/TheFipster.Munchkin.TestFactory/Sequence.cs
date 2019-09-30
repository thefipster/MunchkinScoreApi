namespace TheFipster.Munchkin.TestFactory
{
    public class Sequence
    {
        int number = 0;

        public Sequence() { }

        public Sequence(int sequence)
        {
            number = sequence;
        }

        public int Next
        {
            get
            {
                lock (this)
                {
                    return ++number;
                }
            }
        }
    }
}
