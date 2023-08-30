abstract public class Player
{
    protected List<Card> hand { get; set; }
    public Player() {
        hand = new List<Card>();
    }
    public void Hit(Card card) {
        // Draw a card
        hand.Add(card);
    }
    
    public int GetValue() {
        // return the value of the hand
        int value = 0;
        int numAces = 0;
        foreach (Card card in hand) {
            if (card.value != Value.Ace) {
                value += card.getNumValue();
            } else {
                numAces += 1;
            }
        }
        // add aces, with value of 11 or 1
        // in the event that a player has multiple aces, count aces as 11 
        // unless there are enough aces to bust or hit 21 
        for (int i = 0; i < numAces; i++) {
            int acesLeft = numAces - i + 1;
            if (value + acesLeft >= 21 || value + 11 > 21) {
                value += 1;
            } else {
                value += 11;
            }
        }

        return value;
    }
    public bool IsBust() {
        return GetValue() > 21;
    }
    public bool IsBlackjack() {
        return GetValue() == 21 && hand.Count() == 2;
    }
    public List<Card> GetHand() {
        return hand;
    }
    public void Reset() {
        hand = new List<Card>();
    }
}
public class Dealer : Player {
    // Dealer must draw until they have 17 or more
    public bool IsDrawing() {
        return GetValue() < 17 && !IsBust();
    }
    // Dealer's hand is hidden until the end of the round
    public string ShowHand()
    {
        string str = "\n";
        for (int i = 0; i < Card.HEIGHT; i++) {
            foreach (Card card in GetHand()) {
                str += card.GetASCIIString()[i] + " ";
            }
            str += "\n";
        }
        str += $"for a total of {GetValue()}";
        return str;
    }
    public override string ToString() {
        string str = "\n";
        for (int i = 0; i < Card.HEIGHT; i++) {
            bool firstCard = true;
            foreach (Card card in GetHand()) {
                if (firstCard) {
                    str += Card.GetFaceDownCard()[i] + " ";
                    firstCard = false;
                    continue;
                }
                str += card.GetASCIIString()[i] + " ";
            }
            str += "\n";
        }
        return str;
    }
}

public enum LossTypes
{
    Bust, DealBlackjack, Lose
}
public class User : Player {
    public int chips;
    public int bet;
    public User(int chips=100, int bet=10) 
    {
        this.chips = chips;
        this.bet = bet;
    }
    public void SetBet(int bet) 
    {
        this.bet = bet;
    }

    public void SetChips(int chips)
    {
        this.chips = chips;
    }
    public void Win() {
        Console.WriteLine("You win!");
        if (IsBlackjack()) {
            chips += bet + bet/2; // blackjack pays 3:2, rounded down
        } else {
            chips += bet;
        }
    }

    public override string ToString() {
        string str = "\n";
        for (int i = 0; i < Card.HEIGHT; i++) {
            foreach (Card card in GetHand()) {
                str += card.GetASCIIString()[i] + " ";
            }
            str += "\n";
        }
        str += $"for a total of {GetValue()}";
        return str;
    }

    public void Lose(LossTypes type=LossTypes.Lose) {
        chips -= bet;
        switch (type) {
            case LossTypes.Bust:
                Console.WriteLine("You bust.");
                break;
            case LossTypes.DealBlackjack:
                Console.WriteLine("Dealer blackjack! You lose");
                break;
            default:
                Console.WriteLine("You Lose.");
                break;
        }
    }
}
