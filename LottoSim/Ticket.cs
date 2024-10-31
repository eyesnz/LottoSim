namespace LottoSim;

public class Ticket
{
    public List<LottoLine> Lines { get; set; }

    public static Ticket Dip(Random random, int lines = 8)
    {
        //Random random = new Random();
        var ticket = new Ticket();
        ticket.Lines = new List<LottoLine>(lines);

        for (int i = 0; i < lines; i++)
        {
            var line = LottoLine.NextRandom(random);
            ticket.Lines.Add(line);
        }
       
        return ticket;
    }
}