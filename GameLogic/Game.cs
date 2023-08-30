using System;
using GameLogic;
public class Game {
    User user;
    Dealer dealer;
    Deck deck;
    public Game()
    {
        user = new User();
        dealer = new Dealer();
        deck = new Deck();
    }
    private void DisplayHands()
    {
        Console.WriteLine($"Your hand: {user}");
        Console.WriteLine($"Dealer's hand: {dealer.ShowHand()}");
    }

    private void EndRound()
    {
        DisplayHands();
        // check for bust
        if (user.IsBust())
        {
            user.Lose(LossTypes.Bust);
        } else if (dealer.IsBust())
        {
            user.Win();
        }
        // check for win by value
        else if (dealer.GetValue() > user.GetValue())
        {
            user.Lose(LossTypes.Lose);
        } else if (dealer.GetValue() < user.GetValue())
        {
            user.Win();
        } else
        {
            Console.WriteLine("Push.");
        }
        
    }

    private void UserTurn()
    {
        while (!user.IsBust())
        {
            int choice = readNumFromInput($"1: Draw Card\n2: Stay");
            if (choice == 1)
            {
                Card drawnCard = deck.Draw();
                user.Hit(drawnCard);
                Console.WriteLine($"You draw {drawnCard}. Your hand: {user}");
            }
            else if (choice == 2)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice");
            }
        }
    }
    private void DealerTurn()
    {
        while (dealer.IsDrawing())
        {
            Card drawnCard = deck.Draw();
            dealer.Hit(drawnCard);
            Console.WriteLine($"Dealer draws {drawnCard}. Dealers hand: {dealer}");
        }
    }

    private void playRound()
    {
        if (user.IsBlackjack() && !dealer.IsBlackjack())
        {
            DisplayHands();
            Console.WriteLine("Blackjack! Payout is 3:2");
            user.Win();
        }
        else if (!user.IsBlackjack() && dealer.IsBlackjack())
        {
            DisplayHands();
            user.Lose(LossTypes.DealBlackjack);
        }
        else if (user.IsBlackjack() && dealer.IsBlackjack())
        {
            DisplayHands();
            Console.WriteLine("Push");
        }
        else
        {
            UserTurn();

            if (user.IsBust())
            {
                EndRound();
                return;
            }

            DealerTurn();
            EndRound();
        }
    }

    private int readNumFromInput(string display)
    {
        Console.WriteLine(display);
        while (true)
        {
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            } catch (System.FormatException)
            {
                Console.WriteLine("Invalid input, please enter a number.");
                continue;
            }
        }
    }
    public void startGame() {

        // print welcome message
        Console.WriteLine("Welcome to Blackjack!");

        // take user input for starting chips
        user.SetChips(readNumFromInput("How many chips would you like to start with?"));

        // main game loop
        while (user.chips > 0) {
            try
            {
                int playAgain = readNumFromInput($"You have {user.chips} chips left. Would you like to play?\n1: Yes\n2: No");
                if (playAgain == 2)
                {
                    break;
                } else if (playAgain != 1)
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                int bet = readNumFromInput($"How much would you like to bet?");
                if (bet > user.chips || bet <= 0)
                {
                    Console.WriteLine("Invalid input. Please make sure your bet is larger than 1 and is less than the number of chips you have.");
                    continue;
                }
                user.SetBet(bet);
                if (deck.Cards.Count < 10) {
                    Console.WriteLine("Reshuffling the deck...");
                    deck.Shuffle();
                }
                user.Reset();
                dealer.Reset();
                user.Hit(deck.Draw());
                dealer.Hit(deck.Draw());
                user.Hit(deck.Draw());
                dealer.Hit(deck.Draw());

                Console.WriteLine($"Your hand: {user}");
                Console.WriteLine($"Dealer's hand: {dealer}");


                playRound();
            } catch (System.FormatException)
            {
                continue;
            }
        }
        Console.WriteLine("Thanks for playing!");
    }
}

