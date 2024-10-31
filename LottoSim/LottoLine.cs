namespace LottoSim;

public class LottoLine
{
    public int[] LottoNumbers { get; set; } = new int[6];
    public IOrderedEnumerable<int> LottoNumbersSorted => LottoNumbers.OrderBy(c => c);
    public int PowerBall { get; set; }

    public static LottoLine NextRandom(Random random)
    {
        //Random random = new Random();
        var lottoLine = new LottoLine();

        for (int i = 0; i < 6; i++)
        {
            int newNumber;
            do
            {
                newNumber = random.Next(1, 41);
            } while (lottoLine.LottoNumbers.Contains(newNumber));

            lottoLine.LottoNumbers[i] = newNumber;
        }

        lottoLine.PowerBall = random.Next(1, 11);

        // if (lottoLine.LottoNumbers.Count != 6)
        // {
        //     throw new Exception("Bad draw!");
        // }

        return lottoLine;
    }
}