namespace Data
{

    [Serializable]
    public class Dossier
    {
        public string Nom { get; set; } = string.Empty;

        public DateTime DateDeCréation { get; set; }

        public DateTime DateDeModification { get; set; }

        public override string ToString()
        {
            return "[D] " + Nom + " ( création " + DateDeCréation + " ) ";
        }
    }
}

    
