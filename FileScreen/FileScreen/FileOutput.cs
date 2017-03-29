using System;
using System.IO;

namespace FileScreen
{
	public class FileOutput:FileFeatures
	{
		/**File Screen Project C# - Short Project to provide useful information on files
		 * Project Design:
		 * Class File Features = take in file, identify key general features
		 * Class File Evaluation = analyse and return final file summary
		 * Write out to txt file is required
		 * Author: John Mulhall @johnmlhll (Twitter) jmulhall@yahoo.com
			**/

		//class objs
		StreamWriter writer;

		public void OutputSummaryToFile()
		{
			//local vars
			string input = "";

			//routine
			Console.WriteLine("Do you want to save this summary to file? y/n");
			input = Console.ReadLine();

			if (input == "y")
			{
				string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				string file = filePath + "FileSummary.txt";
				if (!File.Exists(file))
				{
					File.Create(filePath + "FileSummary");
				}

				using (writer = new StreamWriter(file))
				{
					writer.WriteLine("*****File Summary*****");
					writer.WriteLine("**********************");
					writer.WriteLine("Date Entered: "+DateTime.Now);
					writer.WriteLine("File Summary:");
					writer.WriteLine("File Name \tPathway \t\t\tLines \tLines (With Content)");
					foreach (string element in fileSummary)
					{
						writer.Write(element+"\t");
					}
					Console.WriteLine("\nFile Summary Record successfully written to DesktopSummaryFile.txt on your machine's desktop or home folder!");
					writer.WriteLine("\n**********************");
				}
				writer.Close();
			}
			else
			{
				Console.WriteLine("File Summary not saved to your desktop");
				Console.WriteLine("**********************\n");
			}
		}
	}
}
