using System;

namespace FileScreen
{
	class MainClass
	{
		/**File Screen Project C# - Short Project to provide useful information on files
		 * Project Design:
		 * Class File Features = take in file, identify key general features
		 * Class File Evaluation = analyse and return final file summary
		 * Interface iValidate = to enure key methods are implemented in classes
		 * Inheritance is used
		 * Author: John Mulhall @johnmlhll (Twitter) jmulhall@yahoo.com
			**/

		public static void Main (string[] args)
		{
			//dclare vars/objs
			FileFeatures ff = new FileFeatures ();
			FileOutput fo = new FileOutput();

			//routine
			Console.WriteLine ("***FILE SCREEN***");
			Console.WriteLine ("");
			Console.WriteLine ("Welcome to File Screen.... Where we can tell you about your text file");
			ff.FileInput();
			ff.OpenFile();
			Console.WriteLine ("File Encoding Class is: "+ff.GetFileEncoding(""));
			Console.WriteLine("**********************");
			fo.OutputSummaryToFile();
			Console.WriteLine("**********************");
		}
	}
}
