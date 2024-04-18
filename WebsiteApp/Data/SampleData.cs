using WebsiteApp.Models;

namespace WebsiteApp.Data
{
    public class SampleData
    {
        public static void Create(AppDbContext database)
        {
            // If there are no fake accounts, add some.
            string fakeIssuer = "https://example.com";
            if (!database.Accounts.Any(a => a.OpenIDIssuer == fakeIssuer))
            {
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "1111111111",
                    Name = "Brad"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "2222222222",
                    Name = "Angelina"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "3333333333",
                    Name = "Will"
                });
              
            }

            if (!database.Products.Any())
            {
                database.Products.Add(new Product
                {
                    Name = "Chad Volvo 240",
                    Description = "Finaste bilen i Sverige",
                    Price = 300,
                    Category = "240 blazera däck",
                    ImagePath = "volvo240.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Virgin SAAB 900",
                    Description = "Vänd effektivt ditt mående på ända när du ser volvon susa förbi i din underlägsna låda med hjul",
                    Price = 10,
                    Category = "900 bränn humör",
                    ImagePath = "saab900.jpg"
                });

                database.Products.Add(new Product
                {
                    Name = "Betesfisk Gäddluraren 400",
                    Description = "När du behöver en produkt som triggar jaktinstinkten hos fisken du är ute efter rekommenderas Gäddluraren. Mångsidig och med så många krokar att fisken enbart måste vara i närheten av draget för att fastna. Vikt 20 gram",
                    Price = 159.46,
                    Category = "Bete",
                    ImagePath = "baitfish.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Metmask",
                    Description = "Klassisk metmask. Kräver viss sadism för montering av bete, men är mycket effektiv som bete",
                    Price = 19.90,
                    Category = "Bete",
                    ImagePath = "baitworm.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "LurryMcFluffy spinnare",
                    Description = "Enkel spinnare för lite större fisk. Funktionen av fluffet bak är okänt men det tycks locka till sig fisk. Vikt 10 gram",
                    Price = 41.49,
                    Category = "Bete",
                    ImagePath = "baitspin.jpg"

                });
                database.Products.Add(new Product
                {
                    Name = "Blått skeddrag",
                    Description = "Ett skeddrag som fungerar till fiske av medelstora fiskar, lämpar sig väl för gäddfiske men kan också locka annan fisk. Vikt 15 gram",
                    Price = 60,
                    Category = "Bete",
                    ImagePath = "baitspoon.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Piñata",
                    Description = "När du tröttnat på småfångsten du dragit upp och vill satsa på lite större fisk, lägg över tidigare fångst i denna och sikta mot stjärnorna. Vikt varierar beroende på vad du fyller den med men den är hållbar upp till 250 kilo",
                    Price = 400,
                    Category = "Bete",
                    ImagePath = "baitpinata.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Rustikt metspö",
                    Description = "Ett modest prissatt men utmärkt sammansatt metspö av ypperlig kvalité. Maxvikt för fångst okänt men håller för drag rakt nedåt upp till 5 kilo. Undvik hastiga ryck",
                    Price = 15,
                        Category = "Fiskespö",
                    ImagePath = "cheaprod.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Pimpelspö 9000",
                    Description = "Ett enkelt pimpelfiskespö. Tänkt för småfisk",
                    Price = 199,
                    Category = "Fiskespö",
                    ImagePath = "pimpelfiske.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Metspö Extreme",
                    Description = "Metspö, för metande av fisk. Referens, Christoffer Robin",
                    Price = 311,
                    Category = "Fiskespö",
                    ImagePath = "meta.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Haspelspö Deluxe",
                    Description = "Haspelspö för fiske av det mesta inom Svensk inomskärsfiske. Tål upp till 35 kilo fiskrackare",
                    Price = 1999,
                    Category = "Fiskespö",
                    ImagePath = "truerod.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Jawscatcher",
                    Description = "För fiske av extremt tunga fiskar. Kan dra upp till 800 kilo",
                    Price = 9999,
                    Category = "Fiskespö",
                    ImagePath = "jawscatcher.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Bambuflotte",
                    Description = "Handgjord flotte efter konstens alla regler. Kan kräva eftermontage av flera knopar samt mer tamp beroende på vikt av användare. Tål krokar",
                    Price = 99,
                    Category = "Fartyg",
                    ImagePath = "raft.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Explorer 300",
                    Description = "Smidig fiskefarkost. Tål inte krokar jättebra, men är lätt att blåsa upp under färd",
                    Price = 399,
                    Category = "Fartyg",
                    ImagePath = "explorer.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Floatmaster X",
                    Description = "Öka din värdighet i fisket med denna lyxiga fiskefåtölj. Simfötter ingår ej",
                    Price = 999,
                    Category = "Fartyg",
                    ImagePath = "floatmasterx.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Berkley Boatface Tarpless",
                    Description = "Fiskefåtölj med många funktioner. Lådor överallt för drag, mat och fiskespön. Världsklass",
                    Price = 1999,
                    Category = "Fartyg",
                    ImagePath = "berkleyred.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Berkley Boatface With Tarp",
                    Description = "Fiskefåtölj med många funktioner. Lådor överallt för drag, mat och fiskespön. Världsklass. Även med slittålig nyloninklädning",
                    Price = 2800,
                    Category = "Fartyg",
                    ImagePath = "berkleyredwithtarp.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Kinetic camo",
                    Description = "Om du har skam över din oerhörda fåtölj, välj denna med camouflage så du kanske undviker blickar. Lämpar sig väl för älgjakt på vatten",
                    Price = 3000,
                    Category = "Fartyg",
                    ImagePath = "kinetic.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Savage Floatboat",
                    Description = "För den som vill ha en fiskefåtölj med extra allt. Flyt runt med hela vardagsrummet om du vill. Förvaringsutrymme överallt och möjlighet att fästa utombordare",
                    Price = 4999,
                    Category = "Fartyg",
                    ImagePath = "savage.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Showroom Floatboat",
                    Description = "För den som har gett upp att faktiskt fiska men vill visa att det är ett intresse säljer vi denna fåtölj med montagemöjlighet. Låt din hobby vara i centrum, alltid. Även löstagbar för faktiskt fiske",
                    Price = 6899,
                    Category = "Fartyg",
                    ImagePath = "spaceage.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Orca fiskebåt",
                    Description = "För när du behöver en större båt. Visst slitage finns efter förra ägaren",
                    Price = 900000,
                    Category = "Fartyg",
                    ImagePath = "orca.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Trålarbåt von Båtsson",
                    Description = "Trött på att inte få fisk? Se till att ingen annan får det heller med denna monstrositet. Förstör effektivt både habitat och fiskebestånd i en tillfredställande hastighet",
                    Price = 14000000,
                    Category = "Fartyg",
                    ImagePath = "trawler.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Ubåt",
                    Description = "Ta striden ner till fisken och ta hämnd på den som kom undan, samt upp till 400m radie runt sagda fisk. För den som inte bryr sig så mycket om fångsten som jakten",
                    Price = 100000000,
                    Category = "Fartyg",
                    ImagePath = "skipfishingboat.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Vadarbyxa",
                    Description = "En vadarbyxa som håller dig torr även i höga vatten",
                    Price = 400,
                    Category = "Utrustning",
                    ImagePath = "vadarbyxor.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Baltic seglarflytväst",
                    Description = "Flytväst främst för segling i lugna vatten",
                    Price = 600,
                    Category = "Flytväst",
                    ImagePath = "floatbaltic.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Baltic självuppblåsande flytväst",
                    Description = "Självuppblåsande flytväst som aktiveras i kontakt med vatten. Ampull för uppblåsning utbytes efter uppblåsning",
                    Price = 899,
                    Category = "Flytväst",
                    ImagePath = "floatbalticinflate.jpg"
                });
                database.Products.Add(new Product
                {
                    Name = "Bauer flytväst",
                    Description = "Klassisk flytväst för all typ av färd över hav. Nackkragen håller uppe huvudet ifall man faller i samt BÖR vända även avsvimmade med ansiktet över ytan",
                    Price = 359.40,
                    Category = "Flytväst",
                    ImagePath = "floatbauer.jpg"
                });
            }

                database.SaveChanges();
        }
    }
}

