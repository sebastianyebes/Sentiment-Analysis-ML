namespace ProductReviewML
{
    public class SortHelper
    {
        public void Sort(float positive, float negative, float neutral, float notRelated)
        {
            List<float> init = new List<float> { positive, negative, neutral, notRelated};
            int i = 0;
            init.Sort();
            init.Reverse();

            while (i < 4)
            {
                if (positive == init[i])
                {
                    Console.WriteLine($"Positive: {Math.Round(positive * 100, 0)}%");
                }
                else if (negative == init[i])
                {
                    Console.WriteLine($"Negative: {Math.Round(negative * 100, 0)}%");
                }
                else if (neutral == init[i])
                {
                    Console.WriteLine($"Neutral: {Math.Round(neutral * 100, 0)}%");
                }
                else if(notRelated== init[i])
                {
                    Console.WriteLine($"Not Related: {Math.Round(notRelated * 100, 0)}%");
                }
                i++;
            }
        }
    }
}
