namespace Assets.Scripts
{
    public class Review
    {
        private string description;
        private int rate;
        public Review(string description, int rate)
        {
            this.description = description;
            this.rate = rate;
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public int Rate
        {
            get { return rate; }
            set { rate = value; }
        }
    }
}
