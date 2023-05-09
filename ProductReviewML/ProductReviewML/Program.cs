//Load sample data
using Microsoft.VisualBasic.FileIO;
using ProductReviewML;
using System.Reflection.Emit;

var sort = new SortHelper();


//var sampleData = new SentimentModel.ModelInput()
//{
//    Review = review
//};
var path = "C:\\Users\\Kid Omar Costelo\\Downloads\\sample3.csv";

// Create a TextFieldParser object
int column = 8; // the zero-based index of the column to extract values from (i.e., the 6th column)
string name = "";
IEnumerable<string> data = new List<string>();
try
{
    // Read all lines from the CSV file
    //string[] lines = File.ReadAllLines(path);
    //name = (from line in File.ReadLines(path).Skip(1)
    //       let columns = line.Split(',')
    //       select columns[2]).First();


    // Extract the values from the specified column
        data = from line in File.ReadLines(path).Skip(1)
               let columns = line.Split(',')
               where !string.IsNullOrEmpty(columns[0])
               select columns[0];
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}

float positive = 0, negative = 0, neutral = 0, notRelated = 0;

var total = data.Count();

foreach (var review in data)
{
    Console.WriteLine("tick");
    var sampleData = new SentimentModel.ModelInput()
    {
        Review = review
    };

    var result = SentimentModel.Predict(sampleData);

    switch (result.PredictedLabel)
    {
        case ("Positive"):
            positive++;
            break;
        case ("Negative"):
            negative++;
            break;
        case ("Neutral"):
            neutral++;
            break;
        case ("Not Related"):
            notRelated++;
            break;
    }
}

float[] averages = { positive, negative, neutral, notRelated };


Console.WriteLine(String.Join(",", averages));

averages = averages.Select(num => num / total).ToArray();

sort.Sort(averages[0], averages[1], averages[2], averages[3]);
