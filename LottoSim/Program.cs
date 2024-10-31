// See https://aka.ms/new-console-template for more information

using System.Collections.Concurrent;
using System.Diagnostics;
using LottoSim;

var random = new Random();
var stopwatch = Stopwatch.StartNew();

var ticketLineCost = 1.5D;
var ticketLines = 10;
var ticket = Ticket.Dip(random, ticketLines);
var ticketCost = ticketLines * ticketLineCost;
var spendAmount = 0D;
var wonAmount = 0D;

Console.WriteLine("Your ticket:");
Console.BackgroundColor = ConsoleColor.Black;
Console.ForegroundColor = ConsoleColor.Yellow;
foreach (var line in ticket.Lines)
{
    foreach (var number in line.LottoNumbersSorted)
    {
        Console.Write($"{number:00} ");
    }
    Console.WriteLine($"|{line.PowerBall:00} ");
}
Console.ResetColor();

int drawCount = 0;
var bigWin = false;

var drawStopWatch = Stopwatch.StartNew();

while (!bigWin)
{
    drawStopWatch.Start();

    var avgDrawsPerSecond = 0D;

    drawCount++;
    var drawYears = drawCount / 104D;
    spendAmount += ticketCost;

    var draw = new Draw(random);

    var drawWinAmount = 0D;

    //var drawWinAmounts = new ConcurrentBag<double>();
    //var drawWinAmounts = new List<double>();
    var drawWinAmounts = new double[ticketLines];

    //var index = 0;

    // var winAmounts = ticket.Lines.AsParallel().Select(c=> {
    //     var win = 0D;

    //     var result = draw.Compare(c);

    //     switch (result)
    //     {
    //         case Result.Div1WithPowerball:
    //             //bigWin = true;
    //             win += 20000000;
    //             break;
    //         case Result.Div2WithPowerball:
    //             //bigWin = true;
    //             win += 65000;
    //             break;
    //         case Result.Div3WithPowerball:
    //             win += 1100;
    //             break;
    //         case Result.Div4WithPowerball:
    //             win += 120;
    //             break;
    //         case Result.Div5WithPowerball:
    //             win += 60;
    //             break;
    //         case Result.Div6WithPowerball:
    //             win += 40;
    //             break;
    //         case Result.Div7WithPowerball:
    //             win += 15;
    //             win += 4 * ticketLineCost;
    //             break;
    //         case Result.Div1:
    //             //bigWin = true;
    //             win += 1000000;
    //             break;
    //         case Result.Div2:
    //             //bigWin = true;
    //             win += 24000;
    //             break;
    //         case Result.Div3:
    //             win += 600;
    //             break;
    //         case Result.Div4:
    //             win += 60;
    //             break;
    //         case Result.Div5:
    //             win += 30;
    //             break;
    //         case Result.Div6:
    //             win += 22;
    //             break;
    //         case Result.Div7:
    //             win += 4 * ticketLineCost;
    //             break;
    //     }
    //     return win;
    // });

    // drawWinAmount = winAmounts.Sum();
    // if (drawWinAmount >= 20000000)
    // {
    //     bigWin = true;
    // }


    //foreach (var ticketLine in ticket.Lines)
    for (var index = 0; index < ticketLines; index++)
    //var partitioner = Partitioner.Create(ticket.Lines, EnumerablePartitionerOptions.NoBuffering);
    //Parallel.ForEach<LottoLine, Result>(partitioner, new ParallelOptions() {MaxDegreeOfParallelism = 4}, ticketLine =>  // slower?
    //Parallel.For(0, ticketLines, new ParallelOptions() {MaxDegreeOfParallelism = 4}, index =>
    {
        var ticketLine = ticket.Lines[index];
        var win = 0D;

        var result = draw.Compare(ticketLine);

        switch (result)
        {
            case Result.Div1WithPowerball:
                bigWin = true;
                win += 20000000;
                break;
            case Result.Div2WithPowerball:
                //bigWin = true;
                win += 65000;
                break;
            case Result.Div3WithPowerball:
                win += 1100;
                break;
            case Result.Div4WithPowerball:
                win += 120;
                break;
            case Result.Div5WithPowerball:
                win += 60;
                break;
            case Result.Div6WithPowerball:
                win += 40;
                break;
            case Result.Div7WithPowerball:
                win += 15;
                win += 4 * ticketLineCost;
                break;
            case Result.Div1:
                //bigWin = true;
                win += 1000000;
                break;
            case Result.Div2:
                //bigWin = true;
                win += 24000;
                break;
            case Result.Div3:
                win += 600;
                break;
            case Result.Div4:
                win += 60;
                break;
            case Result.Div5:
                win += 30;
                break;
            case Result.Div6:
                win += 22;
                break;
            case Result.Div7:
                win += 4 * ticketLineCost;
                break;
        }

        drawWinAmounts[index] = win;
        index++;
    };

    drawWinAmount = drawWinAmounts.Sum();
    wonAmount += drawWinAmount;

    drawStopWatch.Stop();
    avgDrawsPerSecond = drawCount / drawStopWatch.Elapsed.TotalSeconds;

    if (drawWinAmount > 1000)
    {       
        Console.Write($"Draw No. {drawCount} (Years: {drawYears:F1}): ");

        foreach (var number in draw.LottoNumbersSorted)
        {
            Console.Write($"{number:00} ");
        }

        Console.WriteLine(
            $"|{draw.PowerBall:00} | Draw Win: {drawWinAmount:C0} | Total Spent: {spendAmount:C0} | Total Won: {wonAmount:C0} ({avgDrawsPerSecond}/s)     ");

        if (!bigWin)
        {
            var top = Console.CursorTop > 0 ? Console.CursorTop -1 : 0;
            Console.SetCursorPosition(0, top);
        }
    }
}

drawStopWatch.Stop();

Console.WriteLine($"Won Powerball!!! Only took {drawCount/104} Years");

stopwatch.Stop();
Console.WriteLine($"Completed in {stopwatch.ElapsedMilliseconds} ms");