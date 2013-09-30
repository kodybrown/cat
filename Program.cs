//
// Copyright (C) 2003-2013 Kody Brown (kody@bricksoft.com).
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
using System.Text;

namespace cat
{
	public class Program
	{
		public static List<ICataloger> handlers;

		public static int Main( string[] arguments )
		{
			if (arguments.Length == 0) {
				CatOptions.showUsage();
				return 0;
			}

			CatOptions catOptions;
			PlainText textHandler;
			bool foundIt;

			textHandler = new PlainText();

			LoadPlugins();

			catOptions = CatOptions.LoadOptions(arguments);
			if (catOptions.showHelp) {
				CatOptions.showUsage();
				return 0;
			}

			if (catOptions.files.Count == 0) {
				Console.WriteLine("No file was specified.");
				CatOptions.showUsage();
				return 1;
			}

			foreach (string file in catOptions.files) {
				if (!File.Exists(file)) {
					Console.WriteLine("File was not found: " + Path.GetFileName(file));
					continue;
				}

				foundIt = false;

				if (!catOptions.forcePlainText) {
					foreach (ICataloger ic in handlers) {
						if (ic.CanCat(catOptions, file)) {
							ic.Cat(catOptions, file);
							foundIt = true;
							break;
						}
					}
				}

				if (!foundIt) {
					textHandler.Cat(catOptions, file);
				}
			}

			return 0;
		}

		private static void LoadPlugins()
		{
			Assembly driver_module;
			List<string> files;
			ICataloger driver;
			List<Type> types;

			handlers = new List<ICataloger>();

			files = new List<string>();
			files.AddRange(Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
					, Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location) + ".*.dll", SearchOption.TopDirectoryOnly));

			// Instantiate each assembly and call Initialize on all enabled plugins.
			foreach (string f in files) {
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
								handlers.Add(driver);
							}
						} catch (Exception ex) {
							Console.WriteLine(ex.Message);
							continue;
						}
					}
				}
			}
		}

		#region new stuff
		//private static void WriteLineNumber( bool ShowNumber )
		//{
		//	Console.ForegroundColor = ConsoleColor.DarkCyan;
		//	Console.Write("{0,-" + numLen + "}|", ShowNumber ? lineNumber.ToString() : "");
		//	Console.ForegroundColor = foreColor;
		//	//Console.Write(" ");
		//}

		//private static void ClearLine()
		//{
		//	Console.CursorLeft = 0;
		//	Console.BufferWidth = Console.WindowWidth + 1;
		//	Console.Write(new string(' ', Console.WindowWidth));
		//	Console.CursorLeft = 0;
		//	Console.BufferWidth = Console.WindowWidth;
		//}

		//private static bool Pause()
		//{
		//	//ConsoleKey k;
		//	ConsoleKeyInfo ki;
		//	Console.Write("[Space to Continue][Q to Quit]: ");

		//	Console.TreatControlCAsInput = true;

		//	//while ((k = GetPause(Console.ReadKey(true))) != ConsoleKey.Q && k != ConsoleKey.Spacebar)
		//	while ((ki = Console.ReadKey(true)).Key != ConsoleKey.Q && ki.Key != ConsoleKey.Spacebar)
		//		;

		//	Console.TreatControlCAsInput = false;
		//	ClearLine();

		//	return ki.Key == ConsoleKey.Spacebar;
		//}

		//private static void PauseAtEnd()
		//{
		//	Console.Write("[Press Any Key to Quit]: ");

		//	Console.TreatControlCAsInput = true;
		//	Console.ReadKey(true);
		//	Console.TreatControlCAsInput = false;
		//	ClearLine();
		//}

		//private static ConsoleKey GetPause( ConsoleKeyInfo key )
		//{
		//   if (key.Key == ConsoleKey.Spacebar) {
		//      return ConsoleKey.Spacebar;
		//   } else if (key.Key == ConsoleKey.C && key.Modifiers == ConsoleModifiers.Control) {
		//      return ConsoleKey.Q;
		//   } else if (key.Key == ConsoleKey.Q) {
		//      return ConsoleKey.Q;
		//   }
		//   return ConsoleKey.A;
		//}
		#endregion

		private static void PressAnyKey()
		{
			StringBuilder sb;
			string s;
			int w;
			string pad;

			s = " Press any key to exit ";
			w = (Console.WindowWidth - s.Length) / 2 - 1;

			sb = new StringBuilder();
			for (int i = 0; i < w; i++) {
				sb.Append("-");
			}
			pad = sb.ToString();

			Console.WriteLine();
			Console.WriteLine();
			Console.SetCursorPosition(0, Console.CursorTop - 2);
			Console.Write(pad + s + pad);

			Console.CursorVisible = false;
			Console.ReadKey(true);
			Console.SetCursorPosition(0, Console.CursorTop);

			sb.Clear();
			for (int i = 0; i < Console.WindowWidth - 1; i++) {
				sb.Append(" ");
			}
			Console.WriteLine(sb.ToString());
			Console.CursorVisible = true;
		}
	}
}
