namespace LottoSim;

public class Draw
{
    public int[] Balls = Enumerable.Range(1, 40).ToArray();
    public int[] LottoNumbers { get; set; }
    public IOrderedEnumerable<int> LottoNumbersSorted => LottoNumbers.OrderBy(c => c);
    public int Bonus { get; set; }
    public int PowerBall { get; set; }

    public readonly int NoBallsToDraw = 7; // number of balls we need to draw (6 for normal + 1 for bonus)

    public Random random { get; set; }
    
    public Draw(Random rnd)
    {
        //random = new Random();
        random = rnd;

        NextDraw();
    }

    private void NextDraw()
    {
        this.LottoNumbers = new int[6];

        // for (int i = 0; i < 6; i++)
        // {
        //     this.LottoNumbers[i] = DrawNumber();
        // }

        // faster way of doing a draw
        var draw = Enumerable.Range(0, NoBallsToDraw) // number of balls we need to draw (6 for normal + 1 for bonus)
                    .Select(c => random.Next(0, 1 + Balls.Length - NoBallsToDraw)) // give each ball a random number between 0 and 34. There may be dups, but that will be fixed later
                    .OrderBy(c => c) // sort
                    .Select((c, index) => Balls[c + index]) // select via index into the balls array. c is the random number we assigned, and index removes the dup
                    .ToArray();

        //this.LottoNumbers = draw.Take(6).ToArray(); // first 6 numbers are normal draw
        Array.Copy(draw, this.LottoNumbers, 6); // faster?
        
        this.Bonus = draw[6]; // last number is bonus
        
        //this.Bonus = DrawNumber();
        
        this.PowerBall = random.Next(1, 11); // Powerball is 1 to 10
    }

    private int DrawNumber()
    {
        int newNumber;
        do
        {
            newNumber = random.Next(1, 41);
        } while (this.LottoNumbers.Contains(newNumber));

        return newNumber;
    }
    
    public Result Compare(LottoLine lottoLine)
    {
        var inDrawButNotInLine = this.LottoNumbers.Except(lottoLine.LottoNumbers).Count();
        var hasBonus = lottoLine.LottoNumbers.Contains(this.Bonus);
        var hasPowerBall = this.PowerBall == lottoLine.PowerBall;

        var result = Result.None;
        
        switch (inDrawButNotInLine)
        {
            case 0 when hasPowerBall:
                result = Result.Div1WithPowerball;
                break;
            case 0:
                result = Result.Div1;
                break;
            case 1 when hasPowerBall && hasBonus:
                result = Result.Div2WithPowerball;
                break;
            case 1 when hasBonus:
                result = Result.Div2;
                break;                
            case 1 when hasPowerBall:
                result = Result.Div3WithPowerball;
                break;
            case 1:
                result = Result.Div3;
                break;
            case 2 when hasPowerBall && hasBonus:
                result = Result.Div4WithPowerball;
                break;
            case 2 when hasBonus:
                result = Result.Div4;
                break;                
            case 2 when hasPowerBall:
                result = Result.Div5WithPowerball;
                break;
            case 2:
                result = Result.Div5;
                break;                
            case 3 when hasPowerBall && hasBonus:
                result = Result.Div6WithPowerball;
                break;
            case 3 when hasBonus:
                result = Result.Div6;
                break;                
            case 3 when hasPowerBall:
                result = Result.Div7WithPowerball;
                break;
            case 3:
                result = Result.Div7;
                break;                
        }
        
        return result;
    }
}

public enum Result
{
    None,
    Div1WithPowerball,
    Div2WithPowerball,
    Div3WithPowerball,
    Div4WithPowerball,
    Div5WithPowerball,
    Div6WithPowerball,
    Div7WithPowerball,
    Div1,
    Div2,
    Div3,
    Div4,
    Div5,
    Div6,
    Div7
}