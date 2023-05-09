//Load sample data
using ProductReviewML;

var sort = new SortHelper();

Console.Write("Input: ");
var review = Console.ReadLine();


if (review != null)
{
    var sampleData = new SentimentModel.ModelInput()
    {
        Review = review
    };

    //Load model and predict output
    var result = SentimentModel.Predict(sampleData);

    Console.WriteLine("\nResult: " + result.PredictedLabel + "\n");
    sort.Sort(result.Score[0], result.Score[1], result.Score[2], result.Score[3]);
}
else
{
    Console.WriteLine("Error: Input is null");
}
