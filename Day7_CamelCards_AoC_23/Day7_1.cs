
using System.Diagnostics;
using static Joymg.AoC23.Day7.Day7_1;
using static Joymg.AoC23.Utils;

namespace Joymg.AoC23.Day7
{
    internal class Day7_1
    {
        static string[] inputs;
        const string inputFolderPath = "..\\..\\..\\Day7_CamelCards_AoC_23\\Input\\";
        static int result;


        static List<Hand> hands = new List<Hand>();

        static void Main(string[] args)
        {

            Stopwatch sw = Stopwatch.StartNew();
            inputs = ReadFile(inputFolderPath, InputType.First);

            result = CalculateTotalWinnings(inputs);

            Console.WriteLine(result);

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

        }

        private static int CalculateTotalWinnings(string[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                string[] play = inputs[i].Split(' ');
                hands.Add(new Hand(play[0], int.Parse(play[1])));
            }
            hands.Sort();

            int sum = 0;
            for (int i = 0; i < hands.Count; i++)
            {
                sum += hands[i].bid * (hands.IndexOf(hands[i]) + 1);

            }
            return sum;
        }



        public class Hand : IComparable<Hand>
        {
            public enum HandType
            {
                Five,
                Four,
                Full,
                Three,
                TwoPairs,
                Pair,
                One
            }

            public string cards;
            public int bid;
            public HandType type;

            public Hand(string cards, int bid)
            {
                this.cards = cards;
                this.bid = bid;

                CalculateHandType(cards);

            }

            private void CalculateHandType(string cards)
            {
                Dictionary<char, int> chars = new Dictionary<char, int>();
                for (int i = 0; i < cards.Length; i++)
                {
                    if (chars.ContainsKey(cards[i]))
                    {
                        chars[cards[i]]++;
                    }
                    else
                    {
                        chars.Add(cards[i], 1);
                    }
                }

                if (chars.Count == 1)
                {
                    type = HandType.Five;
                }

                else if (chars.Count == 2)
                {
                    if (chars.ContainsValue(4))
                    {
                        type = HandType.Four;
                    }
                    else if (chars.ContainsValue(3))
                    {
                        type = HandType.Full;
                    }
                }
                else if (chars.Count == 3)
                {
                    if (chars.ContainsValue(3))
                    {
                        type = HandType.Three;
                    }
                    else if (chars.ContainsValue(2))
                    {
                        type = HandType.TwoPairs;
                    }
                }
                else if (chars.Count == 4)
                {
                    type = HandType.Pair;

                }
                else
                {
                    type = HandType.One;
                }
            }

            public int CompareTo(Hand other)
            {
                if (type.Equals(other.type))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Card firstCard = new Card(cards[i]);
                        Card secondCard = new Card(other.cards[i]);
                        int comparationResult = firstCard.CompareTo(secondCard);

                        if (comparationResult == 0)
                        {
                            continue;
                        }
                        else if (comparationResult == -1)
                        {
                            return -1;
                        }
                        else if (firstCard.CompareTo(secondCard) == 1)
                        {
                            return 1;
                        }

                    }
                }

                if (type < other.type)
                    return 1;
                else
                    return -1;
            }
        }


        public class Card : IComparable<Card>
        {
            public char card;

            public Card(char card)
            {
                this.card = card;
            }

            public List<char> cardStrength = new List<char>() { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };

            public int CompareTo(Card other)
            {
                if (cardStrength.IndexOf(card) == cardStrength.IndexOf(other.card)) return 0;

                else if (cardStrength.IndexOf(card) < cardStrength.IndexOf(other.card))
                    return 1;

                else return -1;
            }
        }

    }

}
