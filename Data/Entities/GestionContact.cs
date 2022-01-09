namespace Data
{
    [Serializable]
    public class GestionContact
    {
        public Dossier? dossier { get; set; }

        public List<Contact>? contacts {get; set;}

        
    }
}