using Clock_Patience.Deck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Clock_Patience.Deck.Card;

namespace Clock_Patience.Game
{
    public class Logic
    {
        //Global variables
        Card cardHolder = new Card();
        int KingCount = 0;

        public void startGame()
        {
            //Total Number of Cards 52
            int column = 4, row = 13;

            //Reading User input and storing the data into a 2D Array
            String[,] cardArray = readInput(column, row);

            //Card class that holds the current card playing in each round
            Card currentCard = new Card();

            //Places the cards into their respective section.
            addToHolder(column, row, cardArray, currentCard);

            //Gets the first card from the king pile and removing the first playing card
            currentCard = cardHolder.holder[getRankIndex(Rank.K)].First.Value;
            cardHolder.holder[getRankIndex(Rank.K)].RemoveFirst();

            //Game Starts
            currentCard = playGame(currentCard);

            string card = currentCard.rank.ToString() + currentCard.suit.ToString();

            //Get index of the last card played from the user input
            int index = getLastPlayingCard(card, column, row, cardArray) + 1;

            Console.WriteLine(index + "," + card);
            Console.ReadLine();
        }

        public String[,] readInput(int column, int row)
        {
            Console.WriteLine("Please enter your cards:");

            //2D Array initialization by the column and row
            String[,] cardArray = new String[column, row];

            bool continueReading = true;
            int colIndex = 0;

            while (continueReading)
            {
                string userInput = Console.ReadLine();

                //Checks terminated symbol
                if (userInput.Equals("#"))
                {
                    return cardArray;
                }

                //Checks if exceeds the column limit
                if (colIndex != column)
                {
                    //Splits each card into a string array
                    string[] userInputSplit = userInput.Split(' ');

                    for (int x = 0; x < row; x++)
                    {
                        //Storing the cards into the 2D array
                        cardArray[colIndex, x] = userInputSplit[x];
                    }
                    colIndex++;
                }
                else
                {
                    return cardArray;
                }
            }

            return null;
        }

        public void addToHolder(int column, int row, String[,] cardArray, Card currentCard)
        {
            //Index starter for the card holders. 
            int index = 0;

            //Loop starts from the last card listed
            for (int i = column - 1; i >= 0; i--)
            {
                for (int x = row - 1; x >= 0; x--)
                {
                    currentCard = new Card();
                    char[] charArr = cardArray[i, x].ToCharArray();
                    string currentRank = charArr[0].ToString(); //A, 2, 3, 4, 5, 6, 7, 8, 9, T, J, Q, K
                    string currentSuit = charArr[1].ToString(); // H, D, C, S

                    //Getting the actual enum values
                    currentCard.rank = getRankType(currentRank);
                    currentCard.suit = getSuitType(currentSuit);

                    if (cardHolder.holder[index] == null)
                    {
                        //Initializing linkedList array
                        cardHolder.holder[index] = new LinkedList<Card>();
                    }

                    //Adding the cards on top
                    cardHolder.holder[index].AddFirst(currentCard);
                    index++;
                }

                index = 0;
            }
        }

        public int getRankIndex(Rank currentRank)
        {
            int index = 0;
            switch (currentRank)
            {
                case Rank.A:
                    index = 0;
                    break;
                case Rank.Two:
                    index = 1;
                    break;
                case Rank.Three:
                    index = 2;
                    break;
                case Rank.Four:
                    index = 3;
                    break;
                case Rank.Five:
                    index = 4;
                    break;
                case Rank.Six:
                    index = 5;
                    break;
                case Rank.Seven:
                    index = 6;
                    break;
                case Rank.Eight:
                    index = 7;
                    break;
                case Rank.Nine:
                    index = 8;
                    break;
                case Rank.T:
                    index = 9;
                    break;
                case Rank.J:
                    index = 10;
                    break;
                case Rank.Q:
                    index = 11;
                    break;
                case Rank.K:
                    index = 12;

                    break;
            }

            return index;
        }

        public Rank getRankType(string currentRank)
        {
            Rank r;
            switch (currentRank)
            {
                case "A":
                    r = Rank.A;
                    break;
                case "T":
                    r = Rank.T;
                    break;
                case "J":
                    r = Rank.J;
                    break;
                case "Q":
                    r = Rank.Q;
                    break;
                case "K":
                    r = Rank.K;
                    break;
                case "2":
                    r = Rank.Two;
                    break;
                case "3":
                    r = Rank.Three;
                    break;
                case "4":
                    r = Rank.Four;
                    break;
                case "5":
                    r = Rank.Five;
                    break;
                case "6":
                    r = Rank.Six;
                    break;
                case "7":
                    r = Rank.Seven;
                    break;
                case "8":
                    r = Rank.Eight;
                    break;
                case "9":
                    r = Rank.Nine;
                    break;
                default:
                    throw new Exception("Something went wrong");
            };

            return r;
        }

        public Suit getSuitType(string currentSuit)
        {
            Suit s;
            switch (currentSuit)
            {
                case "H":
                    s = Suit.H;
                    break;
                case "D":
                    s = Suit.D;
                    break;
                case "C":
                    s = Suit.C;
                    break;
                case "S":
                    s = Suit.S;
                    break;
                default:
                    throw new Exception("Something went wrong");
            }
            return s;
        }

        public Card playGame(Card currentCard)
        {
            bool continuePlay = true;

            while (continuePlay)
            {
                //Getting the new card in each round
                currentCard = flipAndGetNewCard(currentCard);

                //Determines if all king piles were flipped
                if (KingCount == 4)
                {
                    //End Game
                    continuePlay = false;
                }
            }

            //Returns last card played
            return currentCard;
        }

        public Card flipAndGetNewCard(Card currentCard)
        {
            Card newCard = new Card();

            //Flipping card played
            currentCard.flipped = true;

            //Finding the actual rank holder and placing it in the last pile.
            int holderIndex = getRankIndex(currentCard.rank);
            cardHolder.holder[holderIndex].AddLast(currentCard);

            //If card was king
            if (holderIndex == 12)
            {
                KingCount++;

                //If all kings are inserted
                if (KingCount == 4)
                {
                    return currentCard;
                }
            }

            //Gets first card from pile and removing it.
            newCard = cardHolder.holder[holderIndex].First.Value;
            cardHolder.holder[holderIndex].RemoveFirst();
            return newCard;
        }

        public int getLastPlayingCard(string card, int column, int row, String[,] cardArray)
        {
            int index = 0;
            for (int i = 0; i < column; i++)
            {
                for (int x = 0; x < row; x++)
                {
                    //If card matches
                    if (cardArray[i, x] == card)
                    {
                        return index;
                    }
                    index++;
                }
            }

            return index;
        }
    }
}
