using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageFromCaesar
{
    internal static class Program
    {
        /*
         * Encrypt a string by shifting characters n values to the right.
         * Accept a string or file path and an int value.
         * Encrypt/decrypt using the provided int as the key.
         * 
         * BONUS: 
         * Add optional support for special characters.
         * Add optional support for user supplied characters.
         * Add support for brute force decrypting, including English word lookups
         */

        private static void Main ( string[] args )
        {
            PlayIntro();

            while (MainMenu())
            {
                Console.ReadLine();
                Console.Clear();
            }
        }

        #region General
        private static bool MainMenu ()
        {
            Console.WriteLine( "String and File En/Decryptor" );
            Console.WriteLine( "Please make a selection: " );
            Console.WriteLine( "1. Encrypt Text" );
            Console.WriteLine( "2. Encrypt File(s)" );
            Console.WriteLine( "3. Decypt Text" );
            Console.WriteLine( "4. Decrypt File(s)" );
            Console.WriteLine( "5. Help" );
            Console.WriteLine( "6. Exit" );
            Console.Write( "-> " );

            switch ( Console.ReadLine().ToLower() )
            {
                case "1":
                    EncryptLine();
                    return true;
                case "2":
                    EncryptFile();
                    return true;
                case "3":
                    DecryptLine();
                    return true;
                case "4":
                    DecryptFile();
                    return true;
                case "5":
                case "h":
                case "?":
                    ShowHelp();
                    return true;
                case "6":
                case "e":
                case "q":
                    return false;
                default:
                    Console.WriteLine( "Invalid Selection. Please try again." );
                    Console.WriteLine( "Press any key to continue..." );
                    Console.ReadKey();
                    Console.Clear();
                    return true;
            }
        }

        private static void PlayIntro ()
        {
            Console.WriteLine( "String and File En/Decryptor" );

            Console.Clear();
        }

        #region Help
        private static void ShowHelp ()
        {
            Console.Clear();
            GeneralInfo();
            EncryptTextHelp();
            EncryptFilesHelp();
            DecryptTextHelp();
            DecryptFilesHelp();
        }

        private static void GeneralInfo ()
        {
            Console.Clear();
            StringBuilder message = new StringBuilder();
            message.Append( "This program is designed to allow users to encrypt and decrypt messages.\n" );
            message.Append( "Encryption/decryption is done using a custom built algorithm.\n" );
            message.Append( Environment.NewLine );
            message.Append( "This algorithm supports a \"secure\" option, which makes the encrypted message\n" );
            message.Append( "more difficult for humans to read by encrypting symbols in addition to letters and numbers.\n\n" );
            message.Append( "press any key to continue..." );
            Console.Write( message.ToString() );
            Console.ReadKey();
        }

        private static void EncryptTextHelp ()
        {
            Console.Clear();
            StringBuilder message = new StringBuilder();
            message.Append( "=== ENCRYPT TEXT ===\n\n" );
            message.Append( "This option allows you to encrypt a single line of text entered via the console.\n" );
            message.Append( "If you just need to send a quick message, this is the ideal option.\n\n" );
            message.Append( "press any key to continue..." );
            Console.Write( message );
            Console.ReadKey();

            message.Clear();
            message.Append( "=== ENCRYPT TEXT ===\n\n" );
            message.Append( "First, you will need to provide the initial text you wish to encrypt.\n\n" );
            message.Append( "Second, you will need to provide a number to act as a key for encrypting the text.\n" );
            message.Append( "(This key will be required by other users to decrypt the message later)\n\n" );
            message.Append( "Finally, you must specify if you wish to use additional security.\n" );
            message.Append( "(The same choice is required to decrypt the text)\n\n" );
            message.Append( "The original text, and your encrypted version, will be displayed in the console.\n\n" );
            message.Append( "press any key to continue..." );
            Console.Clear();
            Console.Write( message );
            Console.ReadKey();
        }

        private static void EncryptFilesHelp ()
        {
            Console.Clear();
            StringBuilder message = new StringBuilder();
            message.Append( "=== ENCRYPT FILES ===\n\n" );
            message.Append( "This option allows you to encrypt one or more files.\n" );
            message.Append( "If you need to encrypt more than one line of text at a time, this is the ideal option.\n\n" );
            message.Append( "press any key to continue..." );
            Console.Write( message );
            Console.ReadKey();

            message.Clear();
            message.Append( "=== ENCRYPT FILES ===\n\n" );
            message.Append( "First, you will need to provide a path to a file or directory.\n" );
            message.Append( "(the application will detect whether it's a file or directory automatically)\n\n" );
            message.Append( "Second, you will need to provide a number to act as a key for encrypting the text.\n" );
            message.Append( "(This key will be required by other users to decrypt the message later)\n\n" );
            message.Append( "Finally, you must specify if you wish to use additional security.\n" );
            message.Append( "(The same choice is required to decrypt the text)\n\n" );
            message.Append( "press any key to continue..." );
            Console.Clear();
            Console.Write( message );
            Console.ReadKey();

            message.Clear();
            message.Append( "=== ENCRYPT FILES ===\n\n" );
            message.Append( "Encrypted files are placed in the same directory as the source file.\n" );
            message.Append( "Files will have the same name as the original, with \"(ENCRYPTED).txt\" appended to the name.\n\n" );
            message.Append( "press any key to continue..." );
            Console.Clear();
            Console.Write( message );
            Console.ReadKey();
        }

        private static void DecryptTextHelp ()
        {
            Console.Clear();
            StringBuilder message = new StringBuilder();
            message.Append( "=== DECRYPT TEXT ===\n\n" );
            message.Append( "This option allows you to decrypt a single line of text entered via the console.\n" );
            message.Append( "If you just need to read a quick message, this is the ideal option.\n\n" );
            message.Append( "press any key to continue..." );
            Console.Write( message );
            Console.ReadKey();

            message.Clear();
            message.Append( "=== DECRYPT TEXT ===\n\n" );
            message.Append( "First, you will need to provide the initial text you wish to decrypt.\n\n" );
            message.Append( "Second, you will need to provide the number used as a key to encrypt the text.\n\n" );
            message.Append( "Finally, you must specify if the text was encrypted with additional security.\n\n" );
            message.Append( "The original text, and your decrypted version, will be displayed in the console.\n\n" );
            message.Append( "press any key to continue..." );
            Console.Clear();
            Console.Write( message );
            Console.ReadKey();
        }

        private static void DecryptFilesHelp ()
        {
            Console.Clear();
            StringBuilder message = new StringBuilder();
            message.Append( "=== DECRYPT FILES ===\n\n" );
            message.Append( "This option allows you to decrypt one or more files.\n" );
            message.Append( "If you need to decrypt more than one line of text at a time, this is the ideal option.\n\n" );
            message.Append( "press any key to continue..." );
            Console.Write( message );
            Console.ReadKey();

            message.Clear();
            message.Append( "=== DECRYPT FILES ===\n\n" );
            message.Append( "First, you will need to provide a path to a file or directory.\n" );
            message.Append( "(the application will detect whether it's a file or directory automatically)\n\n" );
            message.Append( "Second, you will need to provide the number used as the key for encrypting the file.\n\n" );
            message.Append( "Finally, you must specify if the file was encrypted with additional security.\n\n" );
            message.Append( "press any key to continue..." );
            Console.Clear();
            Console.Write( message );
            Console.ReadKey();

            message.Clear();
            message.Append( "=== DECRYPT FILES ===\n\n" );
            message.Append( "Decrypted files are placed in the same directory as the source file.\n" );
            message.Append( "Files will have the same name as the original, with \"(DECRYPTED).txt\" appended to the name.\n\n" );
            message.Append( "press any key to continue..." );
            Console.Clear();
            Console.Write( message );
            Console.ReadKey();
        }
        #endregion

        private static void ValidatePath ( string userProvidedPath, ref FileAttributes pathTargetType, ref bool validPath )
        {
            try
            {
                pathTargetType = File.GetAttributes( userProvidedPath );

                validPath = true;
            }
            catch ( ArgumentException e )
            {
                Console.Clear();
                Console.WriteLine( $"The provided path ({userProvidedPath}) is not valid." );
                Console.WriteLine( "Please provide a valid path to a file or directory." );
                Console.Write( "press any key to continue..." );
                Console.ReadKey();
                Console.Clear();
            }
            catch ( PathTooLongException e )
            {
                Console.Clear();
                Console.WriteLine( $"The provided path ({userProvidedPath}) is too long." );
                Console.WriteLine( "Please provide a valid path to a file or directory." );
                Console.Write( "press any key to continue..." );
                Console.ReadKey();
                Console.Clear();
            }
            catch ( FileNotFoundException e )
            {
                Console.Clear();
                Console.WriteLine( $"No file was found at: {userProvidedPath}" );
                Console.WriteLine( "Please provide a valid path to a file." );
                Console.Write( "press any key to continue..." );
                Console.ReadKey();
                Console.Clear();
            }
            catch ( DirectoryNotFoundException e )
            {
                Console.Clear();
                Console.WriteLine( $"No directory was found at: {userProvidedPath}" );
                Console.WriteLine( "Please provide a valid path to a directory." );
                Console.Write( "press any key to continue..." );
                Console.ReadKey();
                Console.Clear();
            }
            catch ( UnauthorizedAccessException e )
            {
                Console.Clear();
                Console.WriteLine( $"You do not have access to: {userProvidedPath}" );
                Console.Write( "press any key to continue..." );
                Console.ReadKey();
                Console.Clear();
            }
        }
        #endregion

        #region Encryption
        private static void EncryptFile ()
        {
            Console.Clear();
            string userProvidedPath = "";
            FileAttributes pathTargetType = 0;
            int key = 0;
            bool secure = false;
            bool validPath = false;

            // Use this method to test if a path is valid or not, and what type it is:
            // https://bytes.com/topic/c-sharp/answers/248663-how-tell-if-path-file-directory
            // https://docs.microsoft.com/en-us/dotnet/api/system.io.file.getattributes?view=netframework-4.7.2
            while ( !validPath )
            {
                Console.WriteLine( "=== ENCRYPT FILE ===" );
                Console.WriteLine( "Enter the path to a file (including extension) or directory: " );
                Console.Write( "-> " );
                userProvidedPath = Console.ReadLine();

                ValidatePath( userProvidedPath, ref pathTargetType, ref validPath );
            }//end while

            string userResponse = "";
            while ( !int.TryParse( userResponse, out key ) )
            {
                Console.Clear();
                Console.WriteLine( "=== ENCRYPT FILE ===" );
                Console.WriteLine( "Enter a whole number to use as an encyption key." );
                Console.WriteLine( "(you will need this number to decrypt the file later)" );
                Console.Write( "-> " );
                userResponse = Console.ReadLine();
            }

            while ( userResponse != "y" && userResponse != "n" )
            {
                Console.Clear();
                Console.WriteLine( "=== ENCRYPT FILE ===" );
                Console.WriteLine( "Would you like to use extra security? (y/n)" );
                Console.Write( "-> " );
                userResponse = Console.ReadLine().ToLower();

                if ( userResponse == "y" )
                    secure = true;
            }

            if ( pathTargetType == FileAttributes.Directory )
                EncryptMultipleFiles( userProvidedPath, key, secure );
            else
                EncryptSingleFile( userProvidedPath, key, secure );
        }

        private static void EncryptSingleFile ( string userProvidedPath, int key, bool secure )
        {
            Caesar.EncryptFile( userProvidedPath, key, secure );
        }

        private static void EncryptMultipleFiles ( string userProvidedPath, int key, bool secure )
        {
            foreach ( string file in Directory.EnumerateFiles( userProvidedPath ) )
            {
                Caesar.EncryptFile( file, key, secure );
            }
        }

        private static void EncryptLine ()
        {
            Console.Clear();
            string userResponse = "";
            string lineToEncrypt = "";
            string encryptedLine = "";
            int key = 0;
            bool secure = false;

            Console.WriteLine( "=== ENCRYPT TEXT ===" );
            Console.WriteLine( "Enter a line of text you would like to encrypt." );
            Console.Write( "-> " );
            lineToEncrypt = Console.ReadLine();

            while ( !int.TryParse( userResponse, out key ) )
            {
                Console.Clear();
                Console.WriteLine( "=== ENCRYPT TEXT ===" );
                Console.WriteLine( "Enter a whole number to use as an encyption key." );
                Console.WriteLine( "(you will need this number to decrypt the text later)" );
                Console.Write( "-> " );
                userResponse = Console.ReadLine();
            }

            while ( userResponse != "y" && userResponse != "n" )
            {
                Console.Clear();
                Console.WriteLine( "=== ENCRYPT TEXT ===" );
                Console.WriteLine( "Would you like to use extra security? (y/n)" );
                Console.Write( "-> " );
                userResponse = Console.ReadLine().ToLower();

                if ( userResponse == "y" )
                    secure = true;
            }

            encryptedLine = Caesar.EncryptText( lineToEncrypt, key, secure );

            Console.Clear();
            Console.WriteLine( "=== ENCRYPT TEXT ===" );
            Console.WriteLine( $"Original Text: {lineToEncrypt}" );
            Console.WriteLine( $"Encrypted Text: {encryptedLine}" );
        }
        #endregion

        #region Decryption
        private static void DecryptFile ()
        {
            Console.Clear();
            string userProvidedPath = "";
            FileAttributes pathTargetType = 0;
            int key = 0;
            bool secure = false;
            bool validPath = false;

            // Use this method to test if a path is valid or not, and what type it is:
            // https://bytes.com/topic/c-sharp/answers/248663-how-tell-if-path-file-directory
            // https://docs.microsoft.com/en-us/dotnet/api/system.io.file.getattributes?view=netframework-4.7.2
            while ( !validPath )
            {
                Console.WriteLine( "=== DECRYPT FILE ===" );
                Console.WriteLine( "Enter the path to a file (including extension) or directory: " );
                Console.Write( "-> " );
                userProvidedPath = Console.ReadLine();

                ValidatePath( userProvidedPath, ref pathTargetType, ref validPath );
            }//end while

            string userResponse = "";
            while ( !int.TryParse( userResponse, out key ) )
            {
                Console.Clear();
                Console.WriteLine( "=== DECRYPT FILE ===" );
                Console.WriteLine( "Enter the whole number that was used as the encyption key." );
                Console.Write( "-> " );
                userResponse = Console.ReadLine();
            }

            while ( userResponse != "y" && userResponse != "n" )
            {
                Console.Clear();
                Console.WriteLine( "=== DECRYPT FILE ===" );
                Console.WriteLine( "Was the file encrypted with extra security? (y/n)" );
                Console.Write( "-> " );
                userResponse = Console.ReadLine().ToLower();

                if ( userResponse == "y" )
                    secure = true;
            }

            if ( pathTargetType == FileAttributes.Directory )
                DecryptMultipleFiles( userProvidedPath, key, secure );
            else
                DecryptSingleFile( userProvidedPath, key, secure );
        }

        private static void DecryptSingleFile ( string userProvidedPath, int key, bool secure )
        {
            Caesar.DecryptFile( userProvidedPath, key, secure );
        }

        private static void DecryptMultipleFiles ( string userProvidedPath, int key, bool secure )
        {
            foreach ( string file in Directory.EnumerateFiles( userProvidedPath ) )
            {
                Caesar.DecryptFile( file, key, secure );
            }
        }

        private static void DecryptLine ()
        {
            Console.Clear();
            string userResponse = "";
            string lineToDecrypt = "";
            string decryptedLine = "";
            int key = 0;
            bool secure = false;

            Console.WriteLine( "=== DECRYPT TEXT ===" );
            Console.WriteLine( "Enter a line of text you would like to decrypt." );
            Console.Write( "-> " );
            lineToDecrypt = Console.ReadLine();

            while ( !int.TryParse(userResponse, out key) )
            {
                Console.Clear();
                Console.WriteLine( "=== DECRYPT TEXT ===" );
                Console.WriteLine( "Enter the decryption key for the text." );
                Console.WriteLine( "(this is a whole number used when the text was encrypted)" );
                Console.Write( "-> " );
                userResponse = Console.ReadLine();
            }

            while ( userResponse != "y" && userResponse != "n" )
            {
                Console.Clear();
                Console.WriteLine( "=== DECRYPT TEXT ===" );
                Console.WriteLine( "Was your line encrypted with extra securit? (y/n)" );
                Console.Write( "-> " );
                userResponse = Console.ReadLine().ToLower();

                if ( userResponse == "y" )
                    secure = true;
            }

            decryptedLine = Caesar.DecryptText( lineToDecrypt, key, secure );

            Console.Clear();
            Console.WriteLine( "=== DECRYPT TEXT ===" );
            Console.WriteLine( $"Original Text: {lineToDecrypt}" );
            Console.WriteLine( $"Decrypted Text: {decryptedLine}" );
        }
        #endregion
    }
}
