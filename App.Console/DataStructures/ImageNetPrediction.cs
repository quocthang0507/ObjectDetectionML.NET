using Microsoft.ML.Data;

namespace App.Console.DataStructures
{
	public class ImageNetPrediction
	{
		[ColumnName("grid")]
		public float[] PredictedLabels;
	}
}
