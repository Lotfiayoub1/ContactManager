namespace Data
{
    [Serializable]
    public class Contact
    {
        public string Nom { get; set; } = string.Empty;
        public string Prénom { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Société { get; set; } = string.Empty;

        public ContactLink link { get; set; }

        public DateTime DateDeCréation { get; set; }

        public DateTime DateDeModification { get; set; }


        public override string ToString()
        {
            return "| [C] "+ Nom +", "+Prénom+" ("+ Société+"), Email:"+Email+", Link:"+link;
        }
    }
}