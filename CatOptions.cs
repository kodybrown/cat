﻿//
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
using System.IO;
using System.Reflection;
using Bricksoft.PowerCode;

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
		public bool showPlugins { get; set; }

		public bool showLineNumbers { get; set; }
		public bool wrapText { get; set; }
		public bool forcePlainText { get; set; }

		public bool pauseAfterEachPage { get; set; }
		public bool pauseAtEnd { get; set; }

		public string ignoreLines { get; set; }
		public bool ignoreBlankLines { get; set; }
		public bool ignoreWhitespaceLines { get; set; }

		public bool normalExpanded { get; set; }
		public bool extraExpanded { get; set; }

		public CatOptions()
		{
			showHelp = false;
			showPlugins = false;

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

			ignoreLines = "";
			ignoreBlankLines = false;
			ignoreWhitespaceLines = false;

			normalExpanded = true;
			extraExpanded = false;
		}

		public void displayHeader()
		{
			int width = Console.WindowWidth;

			//Console.WriteLine("cat.exe");
			//Console.WriteLine(Text.Wrap("cat.exe - Displays the contents of the file(s) specified.", width, 0));
			Console.WriteLine(Text.Wrap("Created by Kody Brown (kody@bricksoft.com)", width, 0));
			//Console.WriteLine(Text.Wrap("Released under the MIT License. https://github.com/kodybrown/cat", width, 0));
			Console.WriteLine(Text.Wrap("https://github.com/kodybrown/cat", width, 0));
			Console.WriteLine();
		}

		public void displayUsage()
		{
			int width = Console.WindowWidth,
				ind = 2,
				ind2 = 12,
				ind2b = 20;

			displayHeader();

			Console.WriteLine("SYNOPSIS:");
			if (extraExpanded) { Console.WriteLine(); }
			Console.WriteLine(Text.Wrap("Displays the contents of the file(s) specified.", width, ind));
			if (normalExpanded) { Console.WriteLine(); }

			Console.WriteLine("USAGE:");
			if (extraExpanded) { Console.WriteLine(); }
			Console.WriteLine(Text.Wrap("cat [options] file [file...n]", width, ind));
			if (normalExpanded) { Console.WriteLine(); }
			Console.WriteLine(Text.Wrap("file      The name of the file to display. Enclose file names within quotes if it includes a space.", width, ind, ind2));
			if (normalExpanded) { Console.WriteLine(); }
			Console.WriteLine(Text.Wrap("-p        Pauses after each screenful (applies -pp).", width, ind, ind2));
			Console.WriteLine(Text.Wrap("-pp       Pauses at the end.", width, ind, ind2));
			if (normalExpanded) { Console.WriteLine(); }
			Console.WriteLine(Text.Wrap("-l        Include line numbers in the output.", width, ind, ind2));
			Console.WriteLine(Text.Wrap("-w        Nicely wrap lines longer than window width.", width, ind, ind2));
			if (normalExpanded) { Console.WriteLine(); }
			Console.WriteLine(Text.Wrap("-ib       Ignore blank lines.", width, ind, ind2));
			Console.WriteLine(Text.Wrap("-ibw      Ignore blank and whitespace lines.", width, ind, ind2));
			Console.WriteLine(Text.Wrap("-il:xyz   Ignore lines starting with 'xyz'.", width, ind, ind2));
			if (normalExpanded) { Console.WriteLine(); }
			Console.WriteLine(Text.Wrap("-f        Forces plain text display (ignores plugins).", width, ind, ind2));
			if (normalExpanded) { Console.WriteLine(); }
			//Console.WriteLine(Text.Wrap("* Enclose file names within quotes if it includes a space.", width, ind, ind + 2));
			Console.WriteLine(Text.Wrap("* You can reverse the effect of a flag, by prefixing it with a bang (!). This is useful when you need to override an environment variable.", width, ind, ind + 2));
			if (normalExpanded) { Console.WriteLine(); }
			Console.WriteLine("PLUGINS:");
			if (extraExpanded) { Console.WriteLine(); }
			Console.WriteLine(Text.Wrap("--show-plugins    Displays all plugins that are available.", width, ind, ind2b));
			if (normalExpanded) { Console.WriteLine(); }
			Console.WriteLine(Text.Wrap("Plugins must be in the same directory as cat.exe and must match the file pattern: `cat.*.dll`. See https://github.com/kodybrown/cat for more details.", width, 2));
			if (normalExpanded) { Console.WriteLine(); }
			Console.WriteLine("ENVIRONMENT VARIABLES:");
			if (extraExpanded) { Console.WriteLine(); }
			Console.WriteLine(Text.Wrap("These values can be set in your environment so you don't have to type them into the command-line every time you run `cat.exe`.", width, ind));
			if (normalExpanded) { Console.WriteLine(); }
			Console.WriteLine(Text.Wrap("To set a value, prefix the (short) command-line argument name with `cat`. The values are the same as you would use for the command-line.", width, ind));
			if (normalExpanded) { Console.WriteLine(); }
			Console.WriteLine("  Examples:");
			if (extraExpanded) { Console.WriteLine(); }
			Console.WriteLine("    > SET cat_l=true");
			Console.WriteLine("    > SET cat_w=true");
			Console.WriteLine("    > SET cat_ib=true");
			Console.WriteLine("    > SET cat_ibw=true");
			Console.WriteLine("    > SET cat_il=xyz");
			Console.WriteLine("    > SET cat_f=true");
			if (normalExpanded) { Console.WriteLine(); }
			Console.WriteLine(Text.Wrap("* Only the examples displayed are considered valid environment variables.", width, ind, ind + ind));
			Console.WriteLine(Text.Wrap("* Arguments specified on the command-line will always override environment variables.", width, ind, ind + ind));
			Console.WriteLine(Text.Wrap("* Consult your operating system help for information on how to set environment variables for all sessions.", width, ind, ind + ind));
			if (normalExpanded) { Console.WriteLine(); }
		}

		public void displayPlugins()
		{
			int width = Console.WindowWidth,
				ind = 2;
			List<string> files = new List<string>();
			string exe = Assembly.GetEntryAssembly().Location;
			string dir = Path.GetDirectoryName(exe);
			Assembly driver_module;
			ICataloger driver;
			List<Type> types;

			displayUsage();

			Console.WriteLine(Text.Wrap("SHOWING AVAILABLE PLUGINS:", width, 0));
			if (extraExpanded) { Console.WriteLine(); }

			Console.WriteLine(Text.Wrap("Location: " + dir, width, ind + ind, ind + ind + 18));

			files.Add(exe);
			files.AddRange(Directory.GetFiles(dir, Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location) + ".*.dll", SearchOption.TopDirectoryOnly));

			foreach (string f in files) {
				if (normalExpanded) { Console.WriteLine(); }
				Console.WriteLine(Text.Wrap("" + Path.GetFileName(f), width, ind + ind));
				if (extraExpanded) { Console.WriteLine(); }

				// Instantiate each assembly and call Initialize on all enabled plugins.
				try {
					driver_module = Assembly.LoadFile(f);
				} catch (Exception) {
					continue;
				}
				if (driver_module == null) {
					continue;
				}

				try {
					types = new List<Type>(driver_module.GetTypes());
				} catch (Exception) {
					continue;
				}

				for (int j = 0; j < types.Count; j++) {
					if (types[j].GetInterface("ICataloger") != null) {
						try {
							driver = Activator.CreateInstance(types[j]) as ICataloger;
							if (driver != null) {
								Console.WriteLine(Text.Wrap("" + driver.GetType().Name + ": " + driver.Description, width, ind + ind + ind));
							}
						} catch (Exception ex) {
							Console.WriteLine(ex.Message);
							continue;
						}
					}
				}
			}

			if (normalExpanded) { Console.WriteLine(); }
		}

		public static CatOptions LoadOptions( string[] arguments )
		{
			CatOptions catOptions;

			catOptions = new CatOptions();

			catOptions.extraExpanded = false;
			catOptions.normalExpanded = true;

			// TODO Load the environment variables..


			// Load the command-line arguments..
			foreach (string a in arguments) {
				if (a.StartsWith("/") || a.StartsWith("-") || a.StartsWith("!")) {
					string arg = a;
					while (arg.StartsWith("/") || arg.StartsWith("-")) {
						arg = arg.Substring(1);
					}

					if (arg.Equals("?", StringComparison.CurrentCultureIgnoreCase) || arg.StartsWith("h", StringComparison.CurrentCultureIgnoreCase)) {
						// ?, h, help
						catOptions.showHelp = true;

					} else if (arg.Equals("pp", StringComparison.CurrentCultureIgnoreCase) || arg.StartsWith("pause-", StringComparison.CurrentCultureIgnoreCase)) {
						// pp, pause-at, pause-at-end
						catOptions.pauseAtEnd = true;
					} else if (arg.Equals("!pp", StringComparison.CurrentCultureIgnoreCase) || arg.StartsWith("!pause-", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.pauseAtEnd = false;

					} else if (arg.StartsWith("p", StringComparison.CurrentCultureIgnoreCase)) {
						// p, pause
						catOptions.pauseAfterEachPage = true;
					} else if (arg.StartsWith("!p", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.pauseAfterEachPage = false;

					} else if (arg.StartsWith("l", StringComparison.CurrentCultureIgnoreCase)) {
						// l, lines, line-numbers
						catOptions.showLineNumbers = true;
					} else if (arg.StartsWith("!l", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.showLineNumbers = false;

					} else if (arg.StartsWith("w", StringComparison.CurrentCultureIgnoreCase)) {
						// w, wrap, wrap-lines
						catOptions.wrapText = true;
					} else if (arg.StartsWith("!w", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.wrapText = false;

					} else if (arg.StartsWith("f", StringComparison.CurrentCultureIgnoreCase)) {
						// f, force, force-text, force-plain-text
						catOptions.forcePlainText = true;
					} else if (arg.StartsWith("!f", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.forcePlainText = false;

					} else if (arg.Equals("ib", StringComparison.CurrentCultureIgnoreCase) || arg.StartsWith("ignore-b", StringComparison.CurrentCultureIgnoreCase)) {
						// ib, ignore-b, ignore-blank, ignore-blank-lines
						catOptions.ignoreBlankLines = true;
					} else if (arg.Equals("!ib", StringComparison.CurrentCultureIgnoreCase) || arg.StartsWith("!ignore-b", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.ignoreBlankLines = false;

					} else if (arg.Equals("ibw", StringComparison.CurrentCultureIgnoreCase) || arg.StartsWith("ignore-w", StringComparison.CurrentCultureIgnoreCase)) {
						// ibw, ignore-w, ignore-white, ignore-whitespace, ignore-whitespace-lines
						catOptions.ignoreBlankLines = true;
						catOptions.ignoreWhitespaceLines = true;
					} else if (arg.Equals("!ibw", StringComparison.CurrentCultureIgnoreCase) || arg.StartsWith("!ignore-w", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.ignoreWhitespaceLines = false;

					} else if (arg.StartsWith("il:", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.ignoreLines = arg.Substring(3);
					} else if (arg.StartsWith("ignore:", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.ignoreLines = arg.Substring(7);
					} else if (arg.StartsWith("ignore-lines:", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.ignoreLines = arg.Substring(13);

					} else if (arg.Equals("expanded2", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.normalExpanded = true;
					} else if (arg.Equals("!expanded2", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.normalExpanded = false;

					} else if (arg.Equals("expanded", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.extraExpanded = true;
					} else if (arg.Equals("!expanded", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.extraExpanded = false;

					} else if (arg.Equals("no-wrap", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.wrapText = false;

					} else if (arg.Equals("show-plugins", StringComparison.CurrentCultureIgnoreCase)) {
						catOptions.showPlugins = true;

					}
				} else {
					catOptions.files.Add(a);
				}
			}

			return catOptions;
		}
	}
}
