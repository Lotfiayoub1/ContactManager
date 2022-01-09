using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Security.Cryptography;
using Data;
using System.Text;

namespace DotNetProject
{
    public static class utilitis
    {

        // Fonction qui permet d'ajouter un dossier 
        // Exemple d'utilisation : 
        // ajouterdossier dossier1
        public static void AddFolder( List<GestionContact> gestionDeContacts, string nomdossier)
        {
            //Vérification du nom est ce que c'est vide
            if (nomdossier!= "")
            {
                
                GestionContact nouveaudossier = new GestionContact{ dossier = new Dossier{Nom = nomdossier, DateDeCréation = DateTime.Now, DateDeModification= DateTime.Now}, contacts = new List<Contact>()};
                gestionDeContacts.Add(nouveaudossier);
                int position = gestionDeContacts.Count;

                Console.Write("Dossier '" + nomdossier + "' ajouté sous '" + gestionDeContacts.ElementAt(position-2).dossier.Nom + "' en position " + (position-1));
            }else
            {
                Console.WriteLine("Merci d'ajouter le nom dossier");
            }

        }


        //la fonction qui permet d'afficher l'arborescence 
        public static void Afficher (List<GestionContact> gestionDeContacts)
        {
            int k = 0;
            foreach (var item in gestionDeContacts)
            {   
                k++;
                Console.WriteLine(((item.dossier != null) ? item.dossier.ToString() : "Aucun dossier Créer"));
                
                if (item.contacts !=null)
                {
                    foreach (var contact in item.contacts)
                    {
                        for (int i = 0; i < k-1; i++)
                        {
                            Console.Write(" ");
                        }
                        Console.WriteLine(contact.ToString());
                    }
                }

                for (int i = 0; i < k; i++)
                {
                    Console.Write(" ");
                }
               
            }
        }

        // la fonction qui permet de vérifier est ce que l'email est valide ou non
        public static bool IsEmail(string email)
        {
            const string motif = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            if (email != null) return Regex.IsMatch(email, motif);
            else return false;
        }


        //la fonction qui permet d'ajouter un contact 
        // Exemple d'utilisation :
        // ajoutercontact ayoub lotfi michelin ayoublotfi885@gmail.com ami
        // cette fonction gère les erreurs d'email ainsi les liens des contacts
        public static void AddContact(List<GestionContact> gestionDeContacts, string[] tableau)
        {
            int position = gestionDeContacts.Count;
            string [] links = new string[4] {"ami", "collegue", "relation","raison" };

            //Vérification de la syntaxe email et le lien 
            if (links.Contains(tableau[5]) && IsEmail(tableau[4]))
            {   
                //Convertir le string vers un type enum
                ContactLink _link = (ContactLink)Enum.Parse(typeof(ContactLink), tableau[5]);

                // Créer l'objet contact avec les informations saisie par l'utilisateur
                Contact contact = new Contact {
                    Nom = tableau[1].Substring(0, 1).ToUpper() + tableau[1].Substring(1),
                    Prénom = tableau[2].Substring(0,1).ToUpper() + tableau[2].Substring(1),
                    Société = tableau[3].Substring(0,1).ToUpper() + tableau[3].Substring(1),
                    Email = tableau[4],
                    DateDeCréation = DateTime.Now,
                    DateDeModification = DateTime.Now,
                    link = _link
                };

                gestionDeContacts[position - 1].contacts.Add(contact);
                Console.Write("Contact '" + contact.Nom + "' ajouté sous '" + gestionDeContacts.ElementAt(position-1).dossier.Nom + "' en position " + (position));
            }
            else{
                if (IsEmail(tableau[4]))
                {
                    Console.WriteLine("Link est incorrect");
                    Console.WriteLine("Les liens qui existent : { ami, collegue, relation, raison }");
                }else
                {
                    Console.WriteLine("Email est incorrect");
                }
            }
        }

        //Binary serialisation
        public static void BinarySerialize(object data, string filePath)
        {
            Console.WriteLine("Enregistrement du fichier '" + filePath +"'...");
            FileStream fileStream;
            BinaryFormatter bf = new BinaryFormatter();
            if(File.Exists(filePath)) File.Delete(filePath);

            //Cryptage
            DESCryptoServiceProvider crp = new DESCryptoServiceProvider();
            crp.Key = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
            crp.IV = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
            fileStream = new FileStream(filePath,FileMode.CreateNew);
            CryptoStream crStream = new CryptoStream(fileStream, crp.CreateEncryptor(), CryptoStreamMode.Write);
            //End Cryptage

            bf.Serialize(crStream, data);
            crStream.Close();
        } 



        //Xml serialisation
        public static void XmlSerialize(Type dataType, object data, string filePath)
        {
            Console.WriteLine("Enregistrement du fichier '" + filePath +"'...");
            XmlSerializer xmlSerializer = new XmlSerializer(dataType);
            if(File.Exists(filePath)) File.Delete(filePath);

            //Cryptage
            DESCryptoServiceProvider crp = new DESCryptoServiceProvider();
            crp.Key = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
            crp.IV = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
            FileStream stream = new FileStream(filePath,FileMode.CreateNew);
            CryptoStream crStream = new CryptoStream(stream, crp.CreateEncryptor(), CryptoStreamMode.Write);
            //End Cryptage

            xmlSerializer.Serialize(crStream,data);
            crStream.Close();
        }


        //Binary Deserialisation
        public static object? BinaryDeserialize(string filePath)
        {
            List<GestionContact>? obj = null;
            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();
            cryptic.Key = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
            cryptic.IV = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
            FileStream fileStream;
            BinaryFormatter bf  = new BinaryFormatter();
            string? motDePasse;
            int conteur = 0;
            if(File.Exists(filePath))
            {
                Console.WriteLine("Entrer un mot de passe :");
                do
                {
                    motDePasse = Console.ReadLine();
                    conteur++;
                } while (motDePasse != "TD_Cs" && conteur <3);
                if (conteur < 3)
                {
                    // fileStream = File.OpenRead(filePath);
                    // obj = (List<GestionContact>) bf.Deserialize(fileStream);
                    // fileStream.Close();   
                    CryptoStream crStream = new CryptoStream(File.Open(filePath,FileMode.Open), cryptic.CreateDecryptor(), CryptoStreamMode.Read);
                    obj = (List<GestionContact>?)bf.Deserialize(crStream);
                    crStream.Close(); 
                }else
                {
                    File.Delete(filePath);
                    Console.WriteLine("Supression du fichier...");
                }
                
                return obj;
            }else
            {
                Console.WriteLine("Le fichier que vous voulez charger n'existe pas veuillez enregistrer d'abord");
                return obj;
            } 
            
        }

        // Xml Deserialisation
        public static object? XmlDeserialize(Type dataType,string filePath)
        {
            object? obj = null;
            XmlSerializer xmlSerializer = new XmlSerializer(dataType);
            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();
            cryptic.Key = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
            cryptic.IV = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
            string? motDePasse;
            int conteur = 0;
            if (File.Exists(filePath))
            {
                Console.WriteLine("Entrer un mot de passe :");
                do
                {
                    motDePasse = Console.ReadLine();
                    conteur++;
                } while (motDePasse != "TD_Cs" && conteur <3);
                if (conteur < 3)
                {
                    CryptoStream crStream = new CryptoStream(File.Open(filePath,FileMode.Open), cryptic.CreateDecryptor(), CryptoStreamMode.Read);
                    obj = xmlSerializer.Deserialize(crStream);
                    crStream.Close();  
                }else
                {
                    File.Delete(filePath);
                    Console.WriteLine("Supression du fichier...");
                }
                return obj;
            }else
            {
                Console.WriteLine("Le fichier que vous voulez charger n'existe pas veuillez enregistrer d'abord");
                return obj;
            } 
        }
    }


}