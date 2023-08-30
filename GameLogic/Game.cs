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
        /// <summary>
        /// Displays the user's hand and the dealer's hand (with the first card open)
        /// </summary>
        Console.WriteLine($"Your hand: {user}");
        Console.WriteLine($"Dealer's hand: {dealer.ShowHand()}");
    }

    private void EndRound()
    {
        /// <summary>
        /// Ends the round by checking for busts, then comparing the values of the hands.
        /// </summary>
        DisplayHands();
        // Check for blackjack
        if (user.IsBlackjack() && !dealer.IsBlackjack())
        {
            Console.WriteLine("Blackjack! Payout is 3:2 rounded down.");
            user.Win();
        }
        else if (!user.IsBlackjack() && dealer.IsBlackjack())
        {
            user.Lose(LossTypes.DealBlackjack);
        }
        else if (user.IsBlackjack() && dealer.IsBlackjack())
        {
            Console.WriteLine("Push");
        }
        // check for bust
        else if (user.IsBust())
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
        /// <summary>
        /// Allows the user to draw cards until they stay or bust
        /// </summary>
        while (!user.IsBust())
        {
            int choice = ReadNumFromInput($"1: Draw Card\n2: Stay");
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
        /// <summary>
        /// Dealer draws cards until they hit 17 or bust
        /// </summary>
        while (dealer.IsDrawing())
        {
            Card drawnCard = deck.Draw();
            dealer.Hit(drawnCard);
            Console.WriteLine($"Dealer draws {drawnCard}. Dealers hand: {dealer}");
        }
    }

    private void PlayRound()
    {   
        if (user.IsBlackjack() || dealer.IsBlackjack()) {
            EndRound();
        } else
        {
            UserTurn();

            if (user.IsBust())
            {
                EndRound();
            } else {
                DealerTurn();
                EndRound();
            }
        }
    }

    private int ReadNumFromInput(string display)
    {
        /// <summary>
        /// Displays the given message and then reads a number from the console, and checks for invalid input (non-numeric).
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
    public void StartGame() {

        // print welcome message
        Console.WriteLine("Welcome to Blackjack!");

        // take user input for starting chips
        user.SetChips(ReadNumFromInput("How many chips would you like to start with?"));

        // main game loop
        while (user.chips > 0) {
            // check if player wants to play
            int playAgain = ReadNumFromInput($"You have {user.chips} chips left. Would you like to play?\n1: Yes\n2: No");
            if (playAgain == 2)
            {
                break;
            } else if (playAgain != 1)
            {
                Console.WriteLine("Invalid input.");
                continue;
            }
            // take user input for bet
            int bet = ReadNumFromInput($"How much would you like to bet?");
            if (bet > user.chips || bet <= 0)
            {
                Console.WriteLine("Invalid input. Please make sure your bet is larger than 1 and is less than the number of chips you have.");
                continue;
            }
            user.SetBet(bet);
            // when there are less than 15 cards left in the deck, reshuffle
            if (deck.Cards.Count < 15) {
                Console.WriteLine("Reshuffling the deck...");
                deck.Shuffle();
            }
            // deal cards
            user.Reset();
            dealer.Reset();
            user.Hit(deck.Draw());
            dealer.Hit(deck.Draw());
            user.Hit(deck.Draw());
            dealer.Hit(deck.Draw());

            Console.WriteLine($"Your hand: {user}");
            Console.WriteLine($"Dealer's hand: {dealer}");

            PlayRound();
            
        }
        Console.WriteLine("Thanks for playing! Press any key to exit :)");
        Console.ReadKey();
        return;
    }
}

