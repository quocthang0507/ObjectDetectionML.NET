using Microsoft.ML.Data;

namespace App.Consoles.DataStructures
{
	public class ImageNetPrediction
	{
		[ColumnName("grid")]
		public float[] PredictedLabels;
	}
}
