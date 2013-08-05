using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class GettingStartedExample
    {
        public static string apiToken = "Y2QyOTg1YzUtY2E4MC00M2YyLThhNTMtYjQxZmY5MTQzNmVhOkJzYnAyeXNJQURnSA==";
        public static string baseUrl = "http://localhost:8080";

        public static void Main (string[] args)
        {
            createPackage();
        }

        public static void createPackage()
        {
            // Create new esl client with api token and base url
            EslClient client = new EslClient (apiToken, baseUrl);
            FileInfo file = new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf");

            DocumentPackage package = PackageBuilder.NewPackageNamed ("C# Package " + DateTime.Now)
                .DescribedAs ("This is a new package")
                    .WithSigner(SignerBuilder.NewSignerWithEmail("john.smith@email.com")
                                .WithFirstName("John")
                                .WithLastName("Smith")
                                .WithId("SIGNER1"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
                                  .FromFile(file.FullName)
                                  .WithSignature(SignatureBuilder.SignatureFor("john.smith@email.com")
                                   .OnPage(0)
                                   .AtPosition(500, 100))
                                  .WithSignature(SignatureBuilder.InitialsFor("john.smith@email.com")
                                   .OnPage (0)
                                   .AtPosition (500, 200))
                                  .WithSignature(SignatureBuilder.CaptureFor("john.smith@email.com")
                                   .OnPage (0)
                                   .AtPosition (500, 300)))
                    .Build ();

            PackageId id = client.CreatePackage (package);
            client.CreateAndSendPackage(package);

            Console.WriteLine("Package was created and sent", id.Id);

            // Optionally, get the session token for integrated signing.
            SessionToken sessionToken = client.SessionService.CreateSessionToken(id, "SIGNER1");
        }
    }
}