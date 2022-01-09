namespace Data
{
    
    public class Exemple
    {
        
        public int id { get; set; }
        public Exemple(int _id)
        {
            id = _id;
        }

        public override string ToString()
        {
            return "id = " + id;
        }


    }

}
