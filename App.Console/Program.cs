using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var assetsRelativePath = @"../../../assets";
			string assetsPath = GetAbsolutePath(assetsRelativePath);
			var modelFilePath = Path.Combine(assetsPath, "Model", "TinyYolo2_model.onnx");
			var imagesFolder = Path.Combine(assetsPath, "images");
			var outputFolder = Path.Combine(assetsPath, "images", "output");
		}

		public static string GetAbsolutePath(string relativePath)
		{
			FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
			string assemblyFolderPath = _dataRoot.Directory.FullName;

			string fullPath = Path.Combine(assemblyFolderPath, relativePath);

			return fullPath;
		}
	}
}
