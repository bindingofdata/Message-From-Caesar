using System;
using System.IO;
using System.Linq;
using System.Text;

namespace MessageFromCaesar
{
    internal static class Caesar
    {
        private static readonly char[] replacementLetters = "0AaBb1CcDd2EeFf3GgHh4IiJj5KkLl6MmNn7OoPp8QqRr9SsTtUuVvWwXxYyZz".ToCharArray();
        private static readonly char[] replacementCharacters = (".&,-! $?_%\"()\'[]{}" + Environment.NewLine).ToCharArray();

        #region Encryption
        /// <summary>
        /// Encrypts a single line of text using a custom encryption algorithm.
        /// </summary>
        /// <param name="text">String to encrypt</param>
        /// <param name="key">Key used for encrypting the string</param>
        /// <param name="extraSecure">Include non-letter/number characters in encryption</param>
        /// <returns>An encrypted version of the provided string.</returns>
        public static string EncryptText(string text, int key, bool extraSecure = false )
        {
            char[] newCharacters = text.ToArray();
            StringBuilder newString = new StringBuilder( newCharacters.Length );

            for ( int i = 0; i < text.Length; i++ )
            {
                if ( Char.IsLetter( newCharacters[i] ) || Char.IsNumber( newCharacters[i] ) )
                    newCharacters[i] = GetNewLetter( newCharacters[i], key );
                else if ( extraSecure && IsSourceCharacter( text[i] ) )
                    newCharacters[i] = GetNewCharacter( newCharacters[i], key );
            }

            foreach ( char character in newCharacters )
                newString.Append( character );

            return newString.ToString();
        }

        /// <summary>
        /// Encrypts a file using a custom encryption algorithm.
        /// The encrypted file is placed in the source file's directory.
        /// </summary>
        /// <param name="filePath">The path to the file for encrypting</param>
        /// <param name="key">Key used for encrypting the file</param>
        /// <param name="extraSecure">Include non-letter/number characters in encryption</param>
        public static void EncryptFile(string filePath, int key, bool extraSecure = false )
        {
            if ( !File.Exists( filePath ) )
                throw new FileNotFoundException();

            StringBuilder resultString = new StringBuilder();

            using ( StreamReader reader = new StreamReader(File.OpenRead(filePath)) )
            {
                string currentLine = "";
                while ( (currentLine = reader.ReadLine()) != null )
                {
                    resultString.Append( EncryptText( currentLine, key, extraSecure ) );
                }
            }

            using ( StreamWriter writer = new StreamWriter(
                File.Create( filePath + "(ENCRYPTED).txt" ) ) )
            {
                writer.Write( resultString.ToString() );
            }
        }

        #region Private Encryption Methods
        private static char GetNewLetter ( char sourceLetter, int key )
        {
            int sourceIndex = 0;

            for ( int i = 0; i < replacementLetters.Length; i++ )
            {
                if ( replacementLetters[i] == sourceLetter )
                {
                    sourceIndex = i;
                    break;
                }
            }

            return replacementLetters[EncryptIndex( replacementLetters, sourceIndex, key )];
        }

        private static char GetNewCharacter ( char sourceCharacter, int key )
        {
            int sourceIndex = 0;

            for ( int i = 0; i < replacementCharacters.Length; i++ )
            {
                if ( replacementCharacters[i] == sourceCharacter )
                {
                    sourceIndex = i;
                    break;
                }
            }

            return replacementCharacters[EncryptIndex( replacementCharacters, sourceIndex, key )];
        }

        private static int EncryptIndex ( char[] replacementCharacterArray, int sourceIndex, int key )
        {
            int maxIndex = replacementCharacterArray.Length - 1;
            Random random = new Random( key );
            key = random.Next( 1, 65 );
            key *= 2;

            int finalIndex = sourceIndex + key;

            while ( finalIndex > maxIndex )
                finalIndex -= maxIndex;

            return finalIndex;
        }
        #endregion
        #endregion

        #region Decryption
        /// <summary>
        /// Decrypts the provided line of text using a custom decryption algorithm.
        /// </summary>
        /// <param name="text">String to decrypt</param>
        /// <param name="key">Key used to encrypt the string originally</param>
        /// <param name="extraSecure">Used if the source was encrypted with extraSecure = true.</param>
        /// <returns>A decrypted string.</returns>
        public static string DecryptText(string text, int key, bool extraSecure = false )
        {
            char[] newCharacters = text.ToArray();
            StringBuilder newString = new StringBuilder( newCharacters.Length );

            for ( int i = 0; i < text.Length; i++ )
            {
                if ( Char.IsLetter( newCharacters[i] ) || Char.IsNumber( newCharacters[i] ) )
                    newCharacters[i] = GetOriginalLetter( newCharacters[i], key );
                else if ( extraSecure && IsSourceCharacter( text[i] ) )
                    newCharacters[i] = GetOriginalCharacter( newCharacters[i], key );
            }

            foreach ( char character in newCharacters )
                newString.Append( character );

            return newString.ToString();
        }

        /// <summary>
        /// Decrypts a file using a custom decryption algorithm.
        /// The decrypted file is places in the source file's directory.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="key"></param>
        /// <param name="extraSecure"></param>
        public static void DecryptFile(string filePath, int key, bool extraSecure = false )
        {
            if ( !File.Exists( filePath ) )
                throw new FileNotFoundException();

            StringBuilder resultString = new StringBuilder();

            using ( StreamReader reader = new StreamReader( File.OpenRead( filePath ) ) )
            {
                string currentLine = "";
                while ( ( currentLine = reader.ReadLine() ) != null )
                {
                    resultString.Append( DecryptText( currentLine, key, extraSecure ) );
                }
            }

            using ( StreamWriter writer = new StreamWriter(
                File.Create( filePath + "(DECRYPTED).txt" ) ) )
            {
                writer.Write( resultString.ToString() );
            }
        }

        #region Private Decryption Methods
        private static char GetOriginalLetter ( char sourceLetter, int key )
        {
            int sourceIndex = 0;

            for ( int i = 0; i < replacementLetters.Length; i++ )
            {
                if ( replacementLetters[i] == sourceLetter )
                {
                    sourceIndex = i;
                    break;
                }
            }

            return replacementLetters[DecryptIndex( replacementLetters, sourceIndex, key )];
        }

        private static char GetOriginalCharacter ( char sourceCharacter, int key )
        {
            int sourceIndex = 0;

            for ( int i = 0; i < replacementCharacters.Length; i++ )
            {
                if ( replacementCharacters[i] == sourceCharacter )
                {
                    sourceIndex = i;
                    break;
                }
            }

            return replacementCharacters[DecryptIndex( replacementCharacters, sourceIndex, key )];
        }

        private static int DecryptIndex ( char[] replacementCharacterArray, int sourceIndex, int key )
        {
            int maxIndex = replacementCharacterArray.Length - 1;
            Random random = new Random( key );
            key = random.Next( 1, 65 );
            key *= 2;

            int finalIndex = sourceIndex - key;

            while ( finalIndex < 0 )
            {
                finalIndex = maxIndex - Math.Abs( finalIndex );
            }

            return finalIndex;
        }
        #endregion
        #endregion

        private static bool IsSourceCharacter ( char testCharacter )
        {
            bool isSourceCharacter = false;

            foreach ( char character in replacementCharacters )
            {
                if (character == testCharacter)
                {
                    isSourceCharacter = true;
                    break;
                }
            }

            return isSourceCharacter;
        }
    }
}
