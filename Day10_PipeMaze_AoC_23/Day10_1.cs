using System.Diagnostics;
using System.Numerics;
using static Joymg.AoC23.Utils;

namespace Joymg.AoC23.Day10;

public class Day10_1
{
    static string[] inputs;
    const string inputFolderPath = "..\\..\\..\\Day10_PipeMaze_AoC_23\\Input\\";
    static long result;

    public static void Run()
    {
        Stopwatch sw = Stopwatch.StartNew();
        inputs = ReadFile(inputFolderPath, InputType.First);

        Solve(inputs);

        Console.WriteLine(result);

        sw.Stop();
        Console.WriteLine(sw.ElapsedMilliseconds);
    }

    private static void Solve(string[] inputs)
    {
        int[] ints;

        Map map = new Map();
        map.CreateMap(inputs);
        result = map.FindLongestPath();
    }

    public class Pipe
    {
        //Predefined Pipes
        public const char vertPipe = '|';
        public const char horPipe = '-';
        public const char NEPipe = 'L';
        public const char NWPipe = 'J';
        public const char SWPipe = '7';
        public const char SEPipe = 'F';
        public const char blankPipe = '.';
        public const char startPipe = 'S';

        public char pipeCharacter;
        public Vector2 location;
        public Direction[] connectedDirections;
        public Pipe[] neighbours;
        public bool isInMainLoop;

        public Pipe(char character, Vector2 coordinates)
        {
            pipeCharacter = character;
            location = coordinates;
            neighbours = new Pipe[4];
        }

        public Pipe GetNeightbour(Direction direction)
        {
            return neighbours[(int)direction];
        }

        public void SetNeighbour(Direction direction, Pipe neighbourPipe)
        {
            neighbours[(int)direction] = neighbourPipe;
            neighbourPipe.neighbours[(int)direction.Opposite()] = this;
        }

        public void SetValidNeighbours()
        {
            switch (pipeCharacter)
            {
                case vertPipe:
                    connectedDirections = new[] { Direction.N, Direction.S };
                    break;
                case horPipe:
                    connectedDirections = new[] { Direction.W, Direction.E };
                    break;
                case NEPipe:
                    connectedDirections = new[] { Direction.N, Direction.E };
                    break;
                case NWPipe:
                    connectedDirections = new[] { Direction.N, Direction.W };
                    break;
                case SWPipe:
                    connectedDirections = new[] { Direction.S, Direction.W };
                    break;
                case SEPipe:
                    connectedDirections = new[] { Direction.S, Direction.E };
                    break;
                case startPipe:
                    connectedDirections = CheckNeightbourPipes();
                    break;
                default:
                    break;
            }
        }


        private Direction[] CheckNeightbourPipes()
        {
            List<Direction> directions = new();

            for (Direction d = Direction.N; d <= Direction.W; d++)
            {
                if (neighbours[(int)d] == null || neighbours[(int)d].connectedDirections == null) continue;
                if (neighbours[(int)d].connectedDirections.Contains(d.Opposite()))
                {
                    directions.Add(d);
                }
            }

            return directions.ToArray();
        }

        public Direction GetConnectedDirection(Direction direction)
        {
            if (direction == connectedDirections[0])
            {
                return connectedDirections[1];
            }
            else if (direction == connectedDirections[1])
            {
                return connectedDirections[0];
            }
            else
            {
                throw new Exception($"This Pipe does not contain {direction} direction");
            }
        }
    }

    public class Map
    {
        public Pipe[] pipes;
        public int mapWidth;
        public int mapHeight;

        private Pipe startingPipe;

        public void CreateMap(string[] characterMap)
        {
            mapWidth = characterMap[0].Length;
            mapHeight = characterMap.Length;
            pipes = new Pipe[mapWidth * mapHeight];

            for (int y = 0, i = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    CreatePipe(x, y, i++, characterMap[y][x]);
                }
            }

            ConnectPipes();
            startingPipe.SetValidNeighbours();
        }

        private void ConnectPipes()
        {
            for (int i = 0; i < pipes.Length; i++)
            {
                ConnectPipes(pipes[i]);
            }
        }

        private void ConnectPipes(Pipe pipe)
        {
            if (pipe.pipeCharacter != 'S')
            {
                pipe.SetValidNeighbours();
            }
        }


        private void CreatePipe(int x, int y, int i, char characterPipe)
        {
            Pipe pipe = pipes[i] = new Pipe(characterPipe, new Vector2(x, y));
            if (x > 0)
            {
                pipe.SetNeighbour(Direction.W, pipes[i - 1]);
            }

            if (y > 0)
            {
                pipe.SetNeighbour(Direction.N, pipes[i - mapWidth]);
            }

            if (characterPipe == 'S')
            {
                startingPipe = pipe;
                startingPipe.isInMainLoop = true;
            }
        }

        private void SubstituteStartingPipeCharacter()
        {
            char character = ' ';
            if (startingPipe.connectedDirections.All(value => new[] { Direction.N, Direction.S }.Contains(value)))
                character = '|';
            else if (startingPipe.connectedDirections.All(value => new[] { Direction.W, Direction.E }.Contains(value)))
                character = '-';
            else if (startingPipe.connectedDirections.All(value => new[] { Direction.N, Direction.E }.Contains(value)))
                character = 'J';
            else if (startingPipe.connectedDirections.All(value => new[] { Direction.N, Direction.W }.Contains(value)))
                character = 'L';
            else if (startingPipe.connectedDirections.All(value => new[] { Direction.S, Direction.W }.Contains(value)))
                character = '7';
            else if (startingPipe.connectedDirections.All(value => new[] { Direction.S, Direction.E }.Contains(value)))
                character = 'F';

            startingPipe.pipeCharacter = character;
        }

        private Pipe GetPipe(int x, int y)
        {
            if (y < 0 || y >= mapHeight)
            {
                return null;
            }

            if (x < 0 || x > mapWidth)
            {
                return null;
            }

            return pipes[x + y * mapWidth];
        }

        public int FindLongestPath()
        {
            int pathLength = 1;
            Direction currentDirection = startingPipe.connectedDirections[0];
            Pipe step = startingPipe.GetNeightbour(currentDirection);
            step.isInMainLoop = true;
            while (step.pipeCharacter != 'S')
            {
                currentDirection = step.GetConnectedDirection(currentDirection.Opposite());
                step = step.GetNeightbour(currentDirection);
                step.isInMainLoop = true;
                pathLength++;
            }

            return pathLength / 2;
        }

        public int GetEmptySpacesOnMap()
        {
            CleanPathSurroundings();
            SubstituteStartingPipeCharacter();
            int spaces = 0;
            int verticalCounter = 0;
            char previousCharacter = ' ';
            for (int i = 0; i < pipes.Length; i++)
            {
                char currentCharacter = pipes[i].pipeCharacter;
                if (verticalCounter % 2 == 1 && !pipes[i].isInMainLoop)
                {
                    spaces++;
                }
                else if (currentCharacter == '|' )
                {
                    verticalCounter++;
                }
                else if (currentCharacter == 'J' && previousCharacter == 'F')
                {
                    verticalCounter++;
                }
                else if (currentCharacter == '7' && previousCharacter == 'L')
                {
                    verticalCounter++;
                }


                if (currentCharacter != '-')
                {
                    previousCharacter = currentCharacter;
                }
            }

            return spaces;
        }

        private void CleanPathSurroundings()
        {
            bool loopFound = false;
            int line = 0;
            for (int i = 0; i < pipes.Length; i++)
            {
                if (i % mapWidth == 0)
                {
                    loopFound = false;
                }
                loopFound = pipes[i].isInMainLoop || loopFound;

                if (!loopFound)
                {
                    pipes[i].pipeCharacter = ' ';
                }

            }
        }
    }
}