namespace NotNullDemo
{
    public partial class Program
    {
        private static void Main(string[] args)
        {
            EmailAddress a1 = new EmailAddress("test@test.de");
            EmailAddress a2 = EmailAddress.Empty;

            PostleitzahlDE a3 = PostleitzahlDE.IsValid("345");
            PostleitzahlDE a4 = PostleitzahlDE.IsValid("67141");
            PostleitzahlDE a5 = PostleitzahlDE.Empty;

            DateTime d1 = Datum.Empty.Value;

            Point p1 = new Point(10, 20);

            Return<long> numberLong = ParseLong("123");
            if (numberLong.IsSuccess)
            {
                Console.WriteLine($"Erfolgreich geparst: {numberLong.Value}");
            }
            else
            {
                Console.WriteLine("Fehler beim Parsen");
            }

            Return<long> result1 = from a in ParseLong("10") from b in ParseLong("5") select a + b;

            result1.Match<long, Unit>(
                some: v => { Console.WriteLine($"Summe: {v}"); return Unit.Value; },
                none: () => { Console.WriteLine("Eingabe ungültig"); return Unit.Value; }
            );

            Return<long> result2 = ParseLong("20").SelectMany(a => ParseLong("50"), (a, b) => a + b);
            result2.Match<long, Unit>(
                some: v => { Console.WriteLine($"Summe: {v}"); return Unit.Value; },
                none: () => { Console.WriteLine("Eingabe ungültig"); return Unit.Value; }
            );

            Console.ReadKey();
        }

        private static Return<long> ParseLong(string? value)
        {
            if (int.TryParse(value, out var n))
            {
                return Return<long>.Success(n);
            }

            return Return<long>.Fail();
        }
    }

    [Serializable]
    public record class Point(int X, int Y) {};

    [Serializable]
    public class PointX
    {
        public PointX(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
    };

    public record EmailAddress(string Value)
    {        
        public static EmailAddress Empty => new(string.Empty);
    }

    public record PostleitzahlDE(string Value)
    {
        public static PostleitzahlDE Empty => new(new string('0',5));


        public static PostleitzahlDE IsValid(string value)
        {
            if (value.Length == 5 && value.All(char.IsDigit) && value != "00000")
            {
                return new PostleitzahlDE(value);
            }

            return Empty;
        }
    }

    public record Datum(DateTime Value)
    {
        public static Datum Empty => new(new DateTime(1900,1,1));
    };
}