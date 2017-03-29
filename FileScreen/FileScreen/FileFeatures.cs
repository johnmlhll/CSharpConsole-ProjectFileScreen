using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FileScreen
{
	public class FileFeatures : iValidate
	{
		/**File Screen Project C# - Short Project to provide useful information on files
		 * Project Design:
		 * Class File Features = take in file, identify key general features
		 * Class File Evaluation = analyse and return final file summary
		 * Interface iValidate = to enure key methods are implemented in classes
		 * Inheritance is used in a 'has a' relationship but the program is arguably more functional in nature!
		 * Author: John Mulhall @johnmlhll (Twitter) jmulhall@yahoo.com
			**/

		//variables/field properties
		public string FileName {get; set;}
		protected string FilePackage { get; set;}
		protected string Input { get; set;}
		protected string EncodingStr { get; set;}
		protected bool InvalidFile { get; set;}
		protected bool CorrectFileExt { get; set;}
		protected bool IsBinaryFile{ get; set;}

		//static list to make the save to file possible
		public static List<string> fileSummary = new List<string>();


		//declaration of objects

		//Method 01ff Take in file name and provide tentative information about it
		public string FileInput()
		{
			Console.WriteLine ("What kind of file do you have? Please enter its full name and extension now...");
			Input = Console.ReadLine();
			//validate for nulls
			if(Input == "" || Input == null) 
			{
				Console.WriteLine("\n**********************\n***Error Report***\n**********************\n");
			}
			else
			{
				Console.WriteLine("\n**********************\n***File Summary Report***\n**************************\n");
			}
			while (Input.Contains(".") & !(Input.Equals(null)))
			{
				Console.WriteLine("File name " + Input + " received");
				if (Input.EndsWith(".tsv"))
				{
					Console.WriteLine("Your file is tab seperated, text file normally containing datasets in tabular form");
					Console.WriteLine("**********************\n");
					CorrectFileExt = true;
				}
				else if (Input.EndsWith(".csv"))
				{
					Console.WriteLine("Your file is comma seperated, text file normally containing datasets in tabular form");
					Console.WriteLine("**********************\n");
					CorrectFileExt = true;
				}
				else if (Input.EndsWith(".txt"))
				{
					Console.WriteLine("Your file is colon, semi colon, bar or comma seperated. It's a text file, which may be in tabular form");
					Console.WriteLine("**********************\n");
					CorrectFileExt = true;
				}
				else if (Input.EndsWith(".xml"))
				{
					Console.WriteLine("Your file is a data file of type XML (eXtensive Markup Language). Its is designed for table tables and semi structured data");
					Console.WriteLine("**********************\n");
					CorrectFileExt = true;
				}
				else
				{
					Console.WriteLine("Your file extension " + Input + " is not a recognised (text) file name for screening");
					Console.WriteLine("Please retry your file name again...");
					Console.WriteLine("**********************\n");
					InvalidFile = true;
					CorrectFileExt = false;
				}
				if (CorrectFileExt == true)
				{
					Console.WriteLine("Please note your file will be read from your home folder (user/name or MyDocuments) so please make sure your file is saved there...");
					Console.WriteLine("**********************\n");
					FileName = Input;
					Input = "";
				}
				else
				{
					Console.WriteLine("File was not found in your home folder, please revise...");
					Console.WriteLine("**********************\n");
				}
				//Validation on finding FileName
				if (FileName == null | CorrectFileExt == false)
				{
					Console.WriteLine("**********************\n");
					Console.WriteLine("Opps... File not found in your home folder...");
					Console.WriteLine("Exiting...");
					break;
				}
				else
				{
					Console.WriteLine("File Name found and confirmed as : " + FileName);
					Console.WriteLine("**********************\n");
				}
				fileSummary.Add(FileName);
			}
			return FileName;
		}

		//Method 02ff Open text file and read it
		public void OpenFile()
		{
			//local variables
			string fileLine = "";
			int fileLineCounter = 0, fileContentCount = 0;
			string homeFolder = "";

			//get home folder path 
			try {
				if (InvalidFile == false && CorrectFileExt == true) {
					homeFolder = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);	
					FilePackage = homeFolder + "/" + FileName;
					Console.WriteLine ("File Path Package is confirmed as " + FilePackage); 
					Console.WriteLine("**********************\n");
					fileSummary.Add((FilePackage));
				}
			} 
			catch (FileNotFoundException p) 
			{
				Console.WriteLine ("File Name or Path Not Found... Please try again...");
				Console.WriteLine("**********************\n");
			}
			//with filepath found, now file gets opened and read
			try 
			{
				//method object to open file
				StreamReader br = new StreamReader (FilePackage);

				//Declaring data processing/storage objects
				List <string> fr = new List<string> ();

				//read file
				while ((fileLine = br.ReadLine ()) != null) {
					fileLine = br.ReadLine ();
					if (fileLine != null) 
					{
						fr.Add (fileLine);
						fileContentCount++;
					} 	
					fileLine = "";
					fileLineCounter++;
				}
				br.Close();
			} 
			catch (FileNotFoundException fd) 
			{
				Console.WriteLine ("File "+FileName+" Error...");
				Console.WriteLine ("File Validation Failed... Cannot read file...");
				Console.WriteLine ("Issue is: "+fd.Message);
				Console.WriteLine("**********************\n");
			} 
			catch (Exception aa) 
			{
				Console.WriteLine ("File "+FileName+" Error...");
				Console.WriteLine ("File Validation Failed... Cannot read file...");
				Console.WriteLine ("Issue is: "+aa.Message);
				Console.WriteLine("**********************\n");
			}
			finally
			{
				Console.WriteLine("**********************\n");
				Console.WriteLine ("File Lines Read - Total Count = " + fileLineCounter); 
				Console.WriteLine ("File Lines with Content  = " + fileContentCount); 
				Console.WriteLine("**********************\n");
				fileSummary.Add(fileLineCounter.ToString());
				fileSummary.Add(fileContentCount.ToString());
			}
		}

		//Method 03ff - confirmBinaryFile() method to open file and confirm if its a text file or binary file 
		public Boolean ConfirmBinaryFile()
		{
			int ascii = 0, otherFiletypes = 0;
			bool asciiConfirm = false;
			byte b = 0;
			byte[] dataRead = null;

			try
			{
				if(FileName.EndsWith(".txt") | FileName.EndsWith(".csv") | FileName.EndsWith(".tsv") | FileName.EndsWith(".xml"))
				{
					FileStream sr = new FileStream(FilePackage, FileMode.Open, FileAccess.Read);
					int size = (int)sr.Length;


					if (size > 1024)
					{
						size = 1024;
					}
					//file byte read
					dataRead = new byte[size];
					b = (byte) sr.ReadByte();
					sr.Close();
				}
				//confirm text file control characters
				for (int i = 0; i < dataRead.Length; i++)
				{
					dataRead[i] = b;
					if(b > 0x89)
					{
						asciiConfirm = true;
					}
					else if(b == 0x89 | b == 0x0a | b == 0x0c | b == 0x0D)
					{
						ascii++;
					}
					else if (b > 0x20 & b < 0x7E)
					{
						ascii++;
					}
					else
					{
						otherFiletypes++;
					}
				}
				if (100 * otherFiletypes / (ascii + otherFiletypes) < 95)
				{
					asciiConfirm = true;
					if (IsBinaryFile == true)
					{
						Console.WriteLine("File " + FileName + " at " + FilePackage + " is not an ASCII Text File");
						Console.WriteLine("**********************\n");
					}
					else
					{
						Console.WriteLine("File " + FileName + " at " + FilePackage + " is confirmed (by internal file binary check) as an ASCII Text File");
						Console.WriteLine("**********************\n");
					}
				}
			}
			catch(Exception f)
			{
				Console.WriteLine ("Opps.. something went wrong with the non-binary validation of the file");
				Console.WriteLine ("The Error Message Return is: " + f.Message);
				Console.WriteLine("Unable to confirm file is Non-Binary in nature, please try again...");
				Console.WriteLine("**********************\n");
			}
			return asciiConfirm;
		}

		//method04ff - getFileEncoding(string filePathName) is confirming the UTF encoding class for the file
		public Encoding GetFileEncoding(string filePathName)
		{
			//Use Default of Encoding.Default (Ansi CodePage) 
			Encoding enc = Encoding.Default;
			filePathName = FilePackage;//assigning file package (filepath) to the variabe filePathName

			try
			{
				
				//Detect byte order mark if any - otherwise assume default
				byte[] buffer = new byte[5];

				bool isValid = ConfirmBinaryFile();
				if(isValid == true)
				{
				FileStream file = new FileStream(filePathName, FileMode.Open);
				file.Read(buffer, 0, 5);
				file.Close();

				if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
					enc = Encoding.UTF8;
				else if (buffer[0] == 0xfe && buffer[1] == 0xff)
					enc = Encoding.Unicode;
				else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
					enc = Encoding.UTF32;
				else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
					enc = Encoding.UTF7;
					EncodingStr = enc.ToString();
				}
				else
				{
					Console.WriteLine("UTF Class cannot be read as the file read is unauthorised...");
					Console.WriteLine("UTF-8 encoding is being returned 'unchecked' as a default encoding type.");
					Console.WriteLine("**********************\n");
					EncodingStr = "Unchecked - Unavailable at this time";
				}
			}
			catch(FileNotFoundException e) 
			{
				Console.WriteLine ("File Not Found... Details are: " + e.Message);
				Console.WriteLine("**********************\n");
			}
			catch(Exception f)
			{
				Console.WriteLine("Something went wrong reading the files encoding...");
				Console.WriteLine ("Error Message Return: " + f.Message);
				Console.WriteLine("**********************\n");
			}
			return enc;
		}
	}
}

