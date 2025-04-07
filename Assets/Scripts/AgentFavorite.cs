namespace Assets.Scripts
{
    public class AgentFavorite
    {
        private string name;
        private float weight;
        public AgentFavorite(string name, float weight)
        {
            this.name = name;
            this.weight = weight;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public float Weight
        {
            get { return weight; }
            set { weight = value; }
        }
    }
}
