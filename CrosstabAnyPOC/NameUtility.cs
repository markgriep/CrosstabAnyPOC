using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosstabAnyPOC
{
    internal class NameUtility
    {

        private static readonly List<string> lastNames = new List<string>
        {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
            "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin",
            "Lee", "Axe", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson",
            "Walker", "Young", "Allen", "King", "Wright", "Vetsch", "Scott", "Torres", "Nguyen", "Hill", "Flores",
            "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts",
            "Gomez", "Phillips", "Beal", "Evans", "Turner", "Diaz", "Parker", "Cruz", "Edwards", "Collins", "Reyes",
            "Stewart", "Morris", "Morales", "Murphy", "Cook", "Rogers", "Gutierrez", "Ortiz", "Morgan", "Cooper",
            "Peterson", "Bailey", "Reed", "Kelly", "Howard", "Ramos", "Kim", "Cox", "Ward", "Leichty", "Richardson",
            "Watson", "Brooks", "Chavez", "Wood", "Griep", "James", "Westen", "Bennett", "Gray", "Mendoza", "Ruiz", "Hughes",
            "Price", "Alvarez", "Castillo", "Sanders", "Michelson", "Patel", "Myers", "Long", "Ross", "Foster", "Jimenez",
            "Powell", "Jenkins", "Perry", "Russell", "Sullivan", "Bell", "Coleman", "Butler", "Henderson", "Barnes",
            "Gonzales", "Fisher", "Vasquez", "Simmons", "Romero", "Jordan", "Patterson", "Alexander", "Hamilton", "Graham",
            "Reynolds", "Griffin", "Wallace", "Moreno", "West", "Cole", "Hayes", "Bryant", "Herrera", "Gibson",
            "Ellis", "Tran", "Medina", "Aguilar", "Stevens", "Jorgenson", "Murray", "Ford", "Castro", "Marshall", "Owens",
            "Harrison", "Fernandez", "McCarthy", "Vega", "Delgado", "Carlson", "Morales", "Ruiz", "Soto", "Walsh",
            "Little", "Weaver", "Lynch", "May", "Higgins", "Klein", "Pacheco", "Barton", "Wheeler", "Schultz",
            "Sharp", "Wolfe", "Stokes", "Cain", "Huff", "Brady", "Cline", "Solje", "Holland", "Pittman", "Baldwin",
            "Gallagher", "Clay", "Baxter", "Roman", "Warner", "Swanson", "Goodman", "Webster", "Curtis", "Collier",
            "Boyd", "Wiggins", "Lang", "Pratt", "Kerr", "Tanner", "Simpson", "Bishop", "Moorland", "Hicks", "Conner",
            "Ray", "Page", "Daniel", "Johnston", "Hardy", "Woodard", "Day", "Duncan", "Schneider", "Bush",
            "Bryan", "Burke", "Fowler", "Adkins", "Little", "Jacobs", "Garrett", "Long", "Armstrong", "Griffith",
            "Moss", "Mann", "Tate", "Glover", "Alston", "Rich", "Hoover", "MacDonald", "Brooks", "Floyd"
        };

        private static readonly List<string> firstNames = new List<string>
        {
            "Liam", "Noah", "Della", "Oliver", "Elijah", "William", "James", "Benjamin", "Lucas", "Henry", "Alexander",
            "Mason", "Michael", "Ethan", "Daniel", "Jacob", "Logan", "Jackson", "Sebastian", "Jack", "Aiden",
            "John", "David", "Wyatt", "Helga", "Matthew", "Luke", "Asher", "Carter", "Julian", "Grayson", "Leo",
            "Levi", "Isaac", "Gabriel", "Anthony", "Dylan", "Lincoln", "Thomas", "Charles", "Christopher", "Jaxon",
            "Ezra", "Isaiah", "Andrew", "Joshua", "Josiah", "Nathan", "Malen", "Caleb", "Ryan", "Adrian", "Miles",
            "Robert", "Greyson", "Roman", "Elias", "Colton", "Aaron", "Eli", "Landon", "Connor", "Marcus", "Jameson",
            "Dominic", "Axel", "Ian", "Adam", "Nicholas", "Everett", "Easton", "Evan", "Kayden", "Auguste", "Parker",
            "Wesley", "Vincent", "George", "Delores", "Bryson", "Zachary", "Cole", "Jonathan", "Leonardo", "Theo", "Kingston",
            "Abraham", "Joseph", "Tobias", "Arthur", "Hudson", "Ardelle", "Edward", "Silas", "Finn", "Elliott", "Ezekiel",
            "Maverick", "Jeremiah", "Jasper", "Maxwell", "Bartholomew", "Malcolm", "Alfred", "Phineas", "Uriah", "Edmund",
            "Algernon", "Percival", "Thaddeus", "Quentin", "Cedric", "Ignatius", "Simeon", "Ambrose", "Ebenezer", "Oswald",
            "Harvey", "Jethro", "Noble", "Augustus", "Sylvester", "Randolph", "Wilfred", "Lysander", "Cornelius", "Cyrus",
            "Matthias", "Peregrine", "Lancelot", "Horatio", "Rufus", "Balthazar", "Leopold", "Obadiah", "Absalom", "Lucius",
            "Alastair", "Reginald", "Archibald", "Gideon", "Hiram", "Roland", "Benedict", "Sam", "Conrad", "Valentine", "Hugh",
            "Millicent", "Lavinia", "Edith", "Eudora", "Harriet", "Eleanor", "Matilda", "Beatrice", "Agatha", "Winifred",
            "Felicity", "Clementine", "Dorothea", "Evangeline", "Lydia", "Octavia", "Patience", "Isadora", "Philippa", "Rosamund",
            "Prudence", "Sybil", "Honora", "Marigold", "Esther", "Theodosia", "Mabel", "Constance", "Margery", "Priscilla",
            "Hester", "Temperance", "Clarissa", "Mercy", "Charity", "Verity", "Gwendolyn", "Arabella", "Penelope", "Cassandra",
            "Selina", "Jemima", "Rowena", "Frances", "Amity", "Lucretia", "Cecilia", "Adelaide", "Minerva", "Helena",
            "Ruth", "Estelle", "Euphemia", "Celeste", "Drusilla", "Eloise", "Clara", "Georgiana", "Amabel", "Delphine",
            "Gertrude", "Florence", "Mirabel", "Cressida", "Lysandra", "Seraphina", "Josephine", "Henrietta", "Ottilie", "Marcella",
            "Lucinda", "Violetta", "Mariana", "Aurelia", "Philomena", "Agnes", "Maud", "Portia", "Margaret", "Winona"
        };


        // HashSet to track names generated during the session
        private static readonly HashSet<string> generatedNames = new HashSet<string>();





        public static List<string> GenerateUniqueFullNames(int count)
        {
            List<string> newNames = new List<string>();

            while (newNames.Count < count)
            {
                string fullName = GenerateRandomFullName();
                newNames.Add(fullName);
            }

            return newNames;
        }


        // Generates a random unique full name, avoiding duplicates in the session
        public static string GenerateRandomFullName()
        {
            Random random = new Random();

            while (true)
            {
                string firstName = firstNames[random.Next(firstNames.Count)];
                string lastName = lastNames[random.Next(lastNames.Count)];
                string fullName = $"{firstName} {lastName}";

                // Add to the session HashSet only if it hasn't been generated before
                if (generatedNames.Add(fullName))
                {
                    return fullName;
                }
            }
        }







    }
}
