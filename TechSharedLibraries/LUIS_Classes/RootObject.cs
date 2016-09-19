namespace TechSharedLibraries.LUIS_Classes
{
    public class Rootobject
    {
        public Intentsresult[] IntentsResults { get; set; }
    }

    public class Intentsresult
    {
        public string Name { get; set; }
        public object label { get; set; }
        public float score { get; set; }
        
        public override string ToString()
        {
            return Name + " (" + score + ")";
        }
    }
}
