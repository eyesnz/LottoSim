// namespace LottoSim;

// public class Draw2
// {
//     [Flags]
//     public enum Balls 
//     {
//         B01 = 2^01,
//         B02 = 2^02,
//         B03 = 2^03,
//         B04 = 2^04,
//         B05 = 2^05,
//         B06 = 2^06,
//         B07 = 2^07,
//         B08 = 2^08,
//         B09 = 2^09,
//         B10 = 2^10,
//         B11 = 2^11,
//         B12 = 2^12,
//         B13 = 2^13,
//         B14 = 2^14,
//         B15 = 2^15,
//         B16 = 2^16,
//         B17 = 2^17,
//         B18 = 2^18,
//         B19 = 2^19,
//         B20 = 2^20,
//         B21 = 2^21,
//         B22 = 2^22,
//         B23 = 2^23,
//         B24 = 2^24,
//         B25 = 2^25,
//         B26 = 2^26,
//         B27 = 2^27,
//         B28 = 2^28,
//         B29 = 2^29,
//         B30 = 2^30,
//         B31 = 2^31,
//         B32 = 2^32,
//         B33 = 2^33,
//         B34 = 2^34,
//         B35 = 2^35,
//         B36 = 2^36,
//         B37 = 2^37,
//         B38 = 2^38,
//         B39 = 2^39,
//         B40 = 2^40
//     }
    
//     public static readonly int BallCount = 40;
//     public Balls LottoNumbers { get; set; }
//     //public IOrderedEnumerable<int> LottoNumbersSorted => LottoNumbers.OrderBy(c => c);
//     public int Bonus { get; set; }
//     public int PowerBall { get; set; }

//     public readonly int NoBallsToDraw = 7; // number of balls we need to draw (6 for normal + 1 for bonus)

//     public Random random { get; set; }
    
//     public Draw(Random rnd)
//     {
//         //random = new Random();
//         random = rnd;

//         NextDraw();
//     }

//     private void NextDraw()
//     {
//         // faster way of doing a draw
//         // var draw = Enumerable.Range(0, NoBallsToDraw) // number of balls we need to draw (6 for normal + 1 for bonus)
//         //             .Select(c => random.Next(0, 1 + BallCount - NoBallsToDraw)) // give each ball a random number between 0 and 34. There may be dups, but that will be fixed later
//         //             .OrderBy(c => c) // sort
//         //             .Select((c, index) => Balls[c + index]) // select via index into the balls array. c is the random number we assigned, and index removes the dup
//         //             .ToArray();
        
//         //var draw = Enum.GetValues(typeof(Balls))
//         //             .Cast<Balls>()
//         //             .Select(c => random.Next(0, 1 + BallCount - NoBallsToDraw)) // give each ball a random number between 0 and 34. There may be dups, but that will be fixed later
//         //             .OrderBy(c => c) // sort

//         var draw = (Balls)Enumerable.Range(0, NoBallsToDraw) // number of balls we need to draw (6 for normal + 1 for bonus)
//                     .Select(c => random.Next(0, 1 + BallCount - NoBallsToDraw)) // give each ball a random number between 0 and 34. There may be dups, but that will be fixed later
//                     .OrderBy(c => c) // sort
//                     .Select((c, index) => 2^(c + index)) // select via index into the balls array. c is the random number we assigned, and index removes the dup
//                     .Sum();                    
        
//         this.Bonus = draw[6]; // last number is bonus
        
//         this.PowerBall = random.Next(1, 11); // Powerball is 1 to 10
//     }
  
//     public Result Compare(LottoLine lottoLine)
//     {
//         var inDrawButNotInLine = this.LottoNumbers.Except(lottoLine.LottoNumbers).Count();
//         var hasBonus = lottoLine.LottoNumbers.Contains(this.Bonus);
//         var hasPowerBall = this.PowerBall == lottoLine.PowerBall;

//         var result = Result.None;
        
//         switch (inDrawButNotInLine)
//         {
//             case 0 when hasPowerBall:
//                 result = Result.Div1WithPowerball;
//                 break;
//             case 0:
//                 result = Result.Div1;
//                 break;
//             case 1 when hasPowerBall && hasBonus:
//                 result = Result.Div2WithPowerball;
//                 break;
//             case 1 when hasBonus:
//                 result = Result.Div2;
//                 break;                
//             case 1 when hasPowerBall:
//                 result = Result.Div3WithPowerball;
//                 break;
//             case 1:
//                 result = Result.Div3;
//                 break;
//             case 2 when hasPowerBall && hasBonus:
//                 result = Result.Div4WithPowerball;
//                 break;
//             case 2 when hasBonus:
//                 result = Result.Div4;
//                 break;                
//             case 2 when hasPowerBall:
//                 result = Result.Div5WithPowerball;
//                 break;
//             case 2:
//                 result = Result.Div5;
//                 break;                
//             case 3 when hasPowerBall && hasBonus:
//                 result = Result.Div6WithPowerball;
//                 break;
//             case 3 when hasBonus:
//                 result = Result.Div6;
//                 break;                
//             case 3 when hasPowerBall:
//                 result = Result.Div7WithPowerball;
//                 break;
//             case 3:
//                 result = Result.Div7;
//                 break;                
//         }
        
//         return result;
//     }
// }

// public enum Result
// {
//     None,
//     Div1WithPowerball,
//     Div2WithPowerball,
//     Div3WithPowerball,
//     Div4WithPowerball,
//     Div5WithPowerball,
//     Div6WithPowerball,
//     Div7WithPowerball,
//     Div1,
//     Div2,
//     Div3,
//     Div4,
//     Div5,
//     Div6,
//     Div7
// }