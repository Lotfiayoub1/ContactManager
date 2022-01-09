using Data;

namespace DotNetProject 
{

    class Program
    {
        static void Main(string[] args)
        {


            Dossier root = new Dossier();
            root.Nom = "Root";
            root.DateDeCréation = DateTime.Now;
            root.DateDeModification= DateTime.Now;
            List<GestionContact> gestionDeContacts = new List<GestionContact>();
            List<GestionContact> gestionDeContactsUpload = new List<GestionContact>();
            GestionContact gestionContact = new GestionContact{dossier = root, contacts = new  List<Contact>() };
            gestionDeContacts.Add(gestionContact);



            bool MenuExit = false;
            string? MenuChoice;
            Console.WriteLine(@"/*-------------Menu---------\*");
            Console.WriteLine(@"/*1- Afficher               \*");
            Console.WriteLine(@"/*2- Charger                \*");
            Console.WriteLine(@"/*3- Enregistrer            \*");
            Console.WriteLine(@"/*4- Ajouterdossier         \*");
            Console.WriteLine(@"/*5- Ajoutercontact         \*");
            Console.WriteLine(@"/*6- Sortir                 \*");
            Console.WriteLine(@"/*--------------------------\*");

            while (MenuExit != true)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("> ");
                
                MenuChoice = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;

                MenuChoice = (!string.IsNullOrEmpty(MenuChoice) ? MenuChoice.ToLower() : string.Empty);

                string[] tableau = MenuChoice.Split(' ', StringSplitOptions.None);




                switch (tableau[0])
                {

                    case "afficher":
                        utilitis.Afficher(gestionDeContacts);
                        break;


                    case "ajouterdossier":

                        if (tableau.Length > 1)
                        {
                            utilitis.AddFolder(gestionDeContacts, tableau[1]);
                        }
                        else
                        {
                            Console.WriteLine("Merci de mentionner le nom du dossier");
                        }
                        break;

                    case "ajoutercontact" :
                        if (tableau.Length > 5)
                        {
                            utilitis.AddContact(gestionDeContacts, tableau);
                        }else
                        {
                            Console.WriteLine("Merci d'ajouter les informations du contact");
                        }
                        break;

                    case "charger" :
                        string NameUserUpload = Environment.UserName;
                        Console.WriteLine("Quelle type de chargement vous voullez : ");
                        Console.WriteLine(" Xml     : tapez 1 ");
                        Console.WriteLine(" Binaire : tapez 2 ");
                        int? typeCharger = int.Parse(Console.ReadLine());

                        if (typeCharger == 1)
                        {
                            gestionDeContactsUpload =(List<GestionContact>) (utilitis.XmlDeserialize(typeof(List<GestionContact>),@$"C:\Users\{NameUserUpload}\Documents\ContactManager2.db"));
                             if (gestionDeContactsUpload != null)
                            {
                                gestionDeContacts =gestionDeContactsUpload;
                                Console.WriteLine("L'objet est bien chargé.");
                            }

                        }else if(typeCharger == 2)
                        {
                            gestionDeContactsUpload = (List<GestionContact>) utilitis.BinaryDeserialize(@$"C:\Users\{NameUserUpload}\Documents\ContactManager1.db");
                             if (gestionDeContactsUpload != null)
                            {
                                gestionDeContacts =gestionDeContactsUpload;
                                Console.WriteLine("L'objet est bien chargé.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ce Choix n'exite pas");
                        }
                       
                        
                        break;
                    
                    case "enregistrer" :
                        string NameUser = Environment.UserName;
                        Console.WriteLine("Quelle type d'enregistrement vous voullez : ");
                        Console.WriteLine(" Xml     : tapez 1 ");
                        Console.WriteLine(" Binaire : tapez 2 ");
                        

                        int? type = int.Parse(Console.ReadLine());

                        if (type == 1)
                        {
                            utilitis.XmlSerialize(typeof(List<GestionContact>),gestionDeContacts, @$"C:\Users\{NameUser}\Documents\ContactManager2.db");
                        }else
                        {
                            utilitis.BinarySerialize(gestionDeContacts, @$"C:\Users\{NameUser}\Documents\ContactManager.db");
                        }
                        Console.WriteLine(@"Fichier 'C:\Users\lenovo\Documents\ContactManager.db' enregistré.");
                        break;
                    case "sortir" :
                        Console.WriteLine("See you next time bro !");
                        MenuExit= true;
                        break;
                    
                    default:
                        Console.WriteLine("Instruction inconnue.");
                        break;
                }
                

              
            }
        }

       
    }
}
  
