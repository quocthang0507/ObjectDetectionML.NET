using App.Consoles.DataStructures;
using App.Consoles.YoloParser;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace App.Consoles
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

			MLContext mlContext = new MLContext();
			try
			{
				IEnumerable<ImageNetData> images = ImageNetData.ReadFromFile(imagesFolder);
				IDataView imageDataView = mlContext.Data.LoadFromEnumerable(images);
				var modelScorer = new OnnxModelScorer(imagesFolder, modelFilePath, mlContext);

				// Use model to score data
				IEnumerable<float[]> probabilities = modelScorer.Score(imageDataView);
				YoloOutputParser parser = new YoloOutputParser();

				var boundingBoxes =
					probabilities
					.Select(probability => parser.ParseOutputs(probability))
					.Select(boxes => parser.FilterBoundingBoxes(boxes, 5, .5F));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

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
