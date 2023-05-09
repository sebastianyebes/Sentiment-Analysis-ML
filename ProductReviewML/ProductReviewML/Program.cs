//Load sample data
using ProductReviewML;
using System.Text;

var sort = new SortHelper();

Console.Write("Choices:\n0 - Ananlyze Comment\n1 - Analyze Product Review\nAnswer: ");
string? choice = Console.ReadLine();

if(choice == "0")
{
    Console.Write("\nInput: ");
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
}
else if(choice == "1")
{
    var path = "stringpath";

    List<string> data = new List<string>();
    try
    {
        using (var reader = new StreamReader(path))
        {
            // Skip the header row
            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                // Read the next line
                var line = reader.ReadLine();

                // Split the line into columns using a comma as the delimiter
                var columns = line?.Trim().Split(',');

                // Add the value from the first column to the list
                if (!string.IsNullOrEmpty(columns[0]))
                {
                    data.Add(columns[0]);
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }

    float positive = 0, negative = 0, neutral = 0, notRelated = 0;
    var total = data.Count();
    var count = 0;

    StringBuilder reviewBuilder = new StringBuilder();
    foreach (var review in data)
    {
        reviewBuilder.Clear();
        reviewBuilder.Append(review);

        Console.WriteLine($"{count++} Reviews Checked");
        var sampleData = new SentimentModel.ModelInput()
        {
            Review = reviewBuilder.ToString()
        };

        var result = SentimentModel.Predict(sampleData);

        switch (result.PredictedLabel)
        {
            case ("Positive"):
                positive++;
                //Console.WriteLine($"Positive - {review}");
                break;
            case ("Negative"):
                negative++;
                //Console.WriteLine($"Negative - {review}");
                break;
            case ("Neutral"):
                neutral++;
                //Console.WriteLine($"Neutral - {review}");
                break;
            case ("Not Related"):
                notRelated++;
                //Console.WriteLine($"Not Related - {review}");
                break;
        }

        if (count == 500)
            break;
    }

    float[] averages = { positive, negative, neutral, notRelated };

    Console.WriteLine($"\n\nTotal:\nPositive: {positive}\nNegative: {negative}\nNeutral: {neutral}\nNot Related: {notRelated}\n\n");

    averages = averages.Select(num => num / count).ToArray();

    Console.WriteLine("Average:");
    sort.Sort(averages[0], averages[1], averages[2], averages[3]);
}
