namespace Tanks
{
    public class Model<P> where P : new()
    {
        private P property = new P();
        public virtual P Property
        {
            get
            {
                return property;
            }
            set
            {
                property = value;
            }
        }
    }
}
