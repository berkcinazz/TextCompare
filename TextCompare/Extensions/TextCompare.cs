
namespace TextCompare.Extensions
{
    public static class TextCompare
    {
        private static int CheckDistance(string source, string target)
        {
            int[,] distance = new int[source.Length + 1, target.Length + 1];

            for (int i = 0; i <= source.Length; i++)
                distance[i, 0] = i;
            for (int j = 0; j <= target.Length; j++)
                distance[0, j] = j;

            for (int i = 1; i <= source.Length; i++)
            {
                for (int j = 1; j <= target.Length; j++)
                {
                    int cost = (source[i - 1] == target[j - 1]) ? 0 : 1;

                    distance[i, j] = Math.Min(
                        Math.Min(
                            distance[i - 1, j] + 1,
                            distance[i, j - 1] + 1
                        ),
                        distance[i - 1, j - 1] + cost
                    );
                }
            }

            return distance[source.Length, target.Length];
        }

        public static bool IsSimilarText(this string source, string target, double thresholdPercentage = 0.5)
        {
            // Calculate the Levenshtein distance between the source and target
            int distance = CheckDistance(source, target);

            // Calculate the maximum possible length to normalize the distance
            int maxLength = Math.Max(source.Length, target.Length);

            // Calculate the threshold based on the percentage of allowed differences
            int threshold = (int)(maxLength * thresholdPercentage);

            // If the distance is less than or equal to the threshold, treat as "similar"
            return distance <= threshold;
        }
    }
}
