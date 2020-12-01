using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace TestHelloWorld
{
    public class Helper
    {
        public static void Serialize(string xmlData) {
            FileStream fs = new FileStream("DataFile.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try {
                formatter.Serialize(fs, xmlData);
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                fs.Close();
            }
        }

        static void Deserialize() {
            // Declare the hashtable reference.
            Hashtable addresses = null;

            // Open the file containing the data that you want to deserialize.
            FileStream fs = new FileStream("DataFile.dat", FileMode.Open);
            try {
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the hashtable from the file and
                // assign the reference to the local variable.
                addresses = (Hashtable)formatter.Deserialize(fs);
            }
            catch (SerializationException e) {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally {
                fs.Close();
            }

            // To prove that the table deserialized correctly,
            // display the key/value pairs.
            foreach (DictionaryEntry de in addresses) {
                Console.WriteLine("{0} lives at {1}.", de.Key, de.Value);
            }
        }
    }
}
