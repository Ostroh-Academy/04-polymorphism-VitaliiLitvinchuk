namespace ConsoleApp1
{
    internal class Program
    {
        public interface I2DShape
        {
            double GetArea();
        }

        public interface I3DShape
        {
            double GetVolume();
        }

        public class AreaCoord
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }

            public static double operator %(AreaCoord a, AreaCoord b) =>
                Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2) + Math.Pow(b.Z - a.Z, 2));
        }

        public class Triangle(Tuple<AreaCoord, AreaCoord, AreaCoord> coord) : I2DShape
        {
            public Tuple<AreaCoord, AreaCoord, AreaCoord> Coord { get; set; } = coord;

            public double GetArea()
            {
                double sideA = Coord.Item1 % Coord.Item2;
                double sideB = Coord.Item2 % Coord.Item3;
                double sideC = Coord.Item3 % Coord.Item1;

                double s = (sideA + sideB + sideC) / 2;

                return Math.Sqrt(s * (s - sideA) * (s - sideB) * (s - sideC));
            }

            public override string ToString() => $"Area: {GetArea()}";
            //$"first: [{Coord.Item1.X};{Coord.Item1.Y};{Coord.Item1.Z}] second: [{Coord.Item2.X};{Coord.Item2.Y};{Coord.Item2.Z}] third: [{Coord.Item3.X};{Coord.Item3.Y};{Coord.Item3.Z}]|square: {getArea()}";
        }

        public class Tetrahedron(Tuple<AreaCoord, AreaCoord, AreaCoord, AreaCoord> coord) : I3DShape
        {
            public Tuple<AreaCoord, AreaCoord, AreaCoord, AreaCoord> Coord { get; set; } = coord;

            public double GetVolume() => Math.Abs(
            1.0 / 6.0 * (
                Coord.Item1.X * (Coord.Item2.Y * Coord.Item3.Z - Coord.Item3.Y * Coord.Item2.Z) +
                Coord.Item2.X * (Coord.Item3.Y * Coord.Item4.Z - Coord.Item4.Y * Coord.Item3.Z) +
                Coord.Item3.X * (Coord.Item4.Y * Coord.Item1.Z - Coord.Item1.Y * Coord.Item4.Z) +
                Coord.Item4.X * (Coord.Item1.Y * Coord.Item2.Z - Coord.Item2.Y * Coord.Item1.Z)
            ));

            public override string ToString() => $"Volume: {GetVolume()}";
        }

        static void Main(string[] args)
        {
            object[] shapes =
            [
                new Triangle(new Tuple<AreaCoord, AreaCoord, AreaCoord>(
                                                new AreaCoord { X = 0, Y = 0, Z = 0 },
                                                new AreaCoord { X = 4, Y = 0, Z = 0 },
                                                new AreaCoord { X = 0, Y = 3, Z = 0 })),
                new Tetrahedron(new Tuple<AreaCoord, AreaCoord, AreaCoord, AreaCoord>(
                                                new AreaCoord { X = 1, Y = 0, Z = 0 },
                                                new AreaCoord { X = 0, Y = 1, Z = 0 },
                                                new AreaCoord { X = 0, Y = 0, Z = 1 },
                                                new AreaCoord { X = 0, Y = 0, Z = 0 }))
            ];

            foreach (var shape in shapes)
                Console.WriteLine(shape);
        }
    }
}
