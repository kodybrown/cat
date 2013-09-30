//
// Copyright (C) 2008-2013 Kody Brown (kody@bricksoft.com).
// 
// MIT License:
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections.Generic;

namespace cat
{
	public class CatOptions
	{
		public ConsoleColor defaultBackColor { get; set; }
		public ConsoleColor defaultForeColor { get; set; }
		public ConsoleColor lineNumBackColor { get; set; }
		public ConsoleColor lineNumForeColor { get; set; }

		public List<string> files { get; set; }

		public bool showHelp { get; set; }

		public bool showLineNumbers { get; set; }
		public bool wrapText { get; set; }
		public bool forcePlainText { get; set; }

		public bool pauseAfterEachPage { get; set; }
		public bool pauseAtEnd { get; set; }

		public string ignoreLines { get; set; }
		public bool ignoreBlankLines { get; set; }
		public bool ignoreWhitespaceLines { get; set; }

		public CatOptions()
		{
			showHelp = false;

			defaultBackColor = Console.BackgroundColor;
			defaultForeColor = Console.ForegroundColor;
			lineNumBackColor = ConsoleColor.Blue;
			lineNumForeColor = ConsoleColor.Gray;

			files = new List<string>();

			showLineNumbers = false;
			wrapText = false;
			forcePlainText = false;

			pauseAfterEachPage = false;
			pauseAtEnd = false;

			ignoreBlankLines = false;
			ignoreWhitespaceLines = false;
			ignoreLines = "";
		}

		public static void showUsage()
		{
			Console.WriteLine("cat.exe");
			Console.WriteLine("displays the contents of the file specified.");
			Console.WriteLine();
			Console.WriteLine("USAGE: cat [options] file [file2] [file..n]");
			Console.WriteLine();
			//Console.WriteLine("   -p   pauses at the end");
			//Console.WriteLine("   -pp  pauses after each screenful (applies -p)");
			Console.WriteLine("   -l   includes line numbers in the output");
			Console.WriteLine("   -w   nicely wrap lines longer than window width");
			Console.WriteLine("   -f   forces plain text display");
			Console.WriteLine();
			Console.WriteLine("   -ib       ignore blank lines");
			Console.WriteLine("   -ibw      ignore blank and whitespace lines");
			Console.WriteLine("   -il:xyz   ignore lines starting with 'xyz'");
			Console.WriteLine();
			Console.WriteLine("   note: enclose the filename within quotes if it includes a space.");
			Console.WriteLine();
		}

		public static CatOptions LoadOptions( string[] arguments )
		{
			CatOptions catOptions;

			catOptions = new CatOptions();

			foreach (string a in arguments) {
				if (a.StartsWith("/") || a.StartsWith("-")) {
					string arg = a.Substring(1);
					if (arg.Equals("l", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.showLineNumbers = true;
					} else if (arg.Equals("pp", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.pauseAfterEachPage = true;
					} else if (arg.Equals("p", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.pauseAtEnd = true;
					} else if (arg.Equals("w", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.wrapText = true;
					} else if (arg.Equals("f", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.forcePlainText = true;
					} else if (arg.Equals("ib")) {
						catOptions.ignoreBlankLines = true;
					} else if (arg.Equals("ibw")) {
						catOptions.ignoreBlankLines = true;
						catOptions.ignoreWhitespaceLines = true;
					} else if (arg.StartsWith("il:", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.ignoreLines = arg.Substring(3);
					} else if (arg.Equals("?", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.showHelp = true;
					}
				} else {
					catOptions.files.Add(a);
				}
			}
			
			return catOptions;
		}
	}
}
