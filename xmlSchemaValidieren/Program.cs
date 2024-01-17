using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace xmlSchemaValidieren
{

    class Program
    {
        static void Main()
        {
            bool error = false;
            string sError = String.Empty;

            try
            {
                string xsdPath = @"C:\dataformat\mySchema.xsd";
                string xmlPath = @"C:\dataformat\myXml.xml";

                //string xsdPath = "mySchema.xsd";
                //string xmlPath = "myXml.xml";

                ////XML-Schema erstellen und hinzufügen
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add(null, xsdPath);

                //XML-Dokument validieren 
                settings.ValidationType = ValidationType.Schema;

                XmlReader reader = XmlReader.Create(xmlPath, settings);
                ////XML-Datei laden
                XmlDocument document = new XmlDocument();
                document.Load(reader);
                //
                ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);
                

                document.Validate(eventHandler);
                error = false;
                sError = String.Empty;
            } 
            catch (Exception ex)
            {
                error = true;
                sError = ex.Message.ToString();
            }
            

            if (error == true )
            {
                Console.WriteLine("Fehler: " + sError);
            }
            else
            {
                Console.WriteLine("XML-Dokument ist gültig.");
            }
        }
        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Console.WriteLine("Error: {0}", e.Message);
                    break;
                case XmlSeverityType.Warning:
                    Console.WriteLine("Warning: {0}", e.Message);
                    break;
            }
        }
    }
}

/*using System;
using System.Xml;
using System.Xml.Schema;

namespace XmlSchemaExceptionMatching
{
    class Programm
    {
        static void Main()
        {

            try 
            {
                //XML-Datei laden
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("XmlDatei1.xml");

                //XML-Schema erstellen und hinzufügen
                XmlSchemaSet schemaSet = new XmlSchemaSet();
                schemaSet.Add(null, "XmlSchema1.xsd");


                //XML-Dokument validieren 
                xmlDoc.Schemas.Add(schemaSet);

                xmlDoc.Validate(ValidationEventHandler);
            }
            catch(Exception ex)
            {
                   Console.WriteLine($"Fehler beim Validieren: {ex.Message}"); 
            }

            static void ValidationEventHandler(object sender, ValidationEventArgs e)
            {
                Console.WriteLine($"Fehler beim Validieren: {e.Message}");
            }
        }

    }
}*/