# Message-From-Caesar
My solution to the Message From Caesar project from The C# Player's Guide 3rd Ed.
http://starboundsoftware.com/books/c-sharp/try-it-out/message-from-julius-caesar

The program does one to one character replacement using an intiger key provided by the user to determine the new character.
An additional boolean parameter is used to determine whether or not to include non-letter/number characters in the encryption (including new lines).

Character conversion is obfuscated in two primary ways: 
1. The source character/letter array contains both lower and capital characters, and numbers. This means that shifting X positions doesn't 
   automatically give you the Xth letter from the starting one in the alphabet.
2. The key is used as the seed for a random. The newly generated number is the actual shift amount. This means that even if you have the 
   source array and the original key, you won't have the correct value to shift by.

The program allows users to do one of two primary things: 
1. Encrypt/Decrypt a line of text provided via the console.
2. EncryptDecypt one or more text files.

When encrypting/decrypting a single line, the original and updated lines are displayed.
When encrypting/decrypting files, a new file is created with the base file's name and "(ENCRYPTED)" or "(DECRYPTED)" appended.
A file or directory path can be provided. The application will determine if the path is a file or directory and either encrypt the file or 
   all text files within the directory.

The secure option will replace New Lines, making the option more useful for mangling files that individual lines. 
Currently, New Lines are NOT restored when decrypting, though the text is otherwise decrypting correctly.
