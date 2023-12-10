namespace Joymg.AoC23.Day10;

public enum Direction
{
    N,
    E,
    S,
    W,
}

public static class DirectionExtensions
{
    public static Direction Opposite(this Direction direction)
    {
        return (int)direction < 2 ? direction + 2 : direction - 2;
    }

    public static Direction Next(this Direction direction)
    {
        return direction == Direction.W ? Direction.N : direction + 1;
    }

    public static Direction Previous(this Direction direction)
    {
        return direction == Direction.N ? Direction.W : direction + 1;
    }
}