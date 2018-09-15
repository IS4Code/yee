/* Date: 16.6.2018, Time: 14:50 */
using System;
using System.Collections.Generic;
using System.IO;

namespace yee
{
	class Program
	{
		public static void Main(string[] args)
		{
			const int bufferSize = 4096;
			
			var outputs = new List<Stream>();
			foreach(string file in args)
			{
				try{
					var stream = new FileStream(file, FileMode.Open, FileAccess.Write, FileShare.Delete, bufferSize);
					outputs.Add(stream);
				}catch(IOException)
				{
					
				}
			}
			outputs.Add(Console.OpenStandardOutput(bufferSize));
			
			byte[] buffer = new byte[bufferSize];
			using(var input = Console.OpenStandardInput(bufferSize))
			{
				var delete = new List<Stream>();
				int count;
				while(outputs.Count > 0 && (count = input.Read(buffer, 0, bufferSize)) != 0)
				{
					foreach(var output in outputs)
					{
						try{
							output.Write(buffer, 0, count);
							output.Flush();
						}catch(IOException)
						{
							delete.Add(output);
						}
					}
					foreach(var output in delete)
					{
						outputs.Remove(output);
						try{
							output.Dispose();
						}catch{
							
						}
					}
					delete.Clear();
				}
			}
		}
	}
}