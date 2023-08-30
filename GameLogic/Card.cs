// Contains the Card class and enums for Suit and Value.
public enum Suit
{
    Diamonds, Clubs, Hearts, Spades
}
public enum Value
{
    Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King
}
public class Card
{
    public static readonly int HEIGHT = 9;
    private char suitChar;
    private string valueStr = "?";
    private int numValue;
    public Suit suit;
    public Value value;
    public Card(Suit suit, Value value)
    {
        this.suit = suit;
        this.value = value;
        switch (suit)
        {
            case Suit.Diamonds:
                suitChar = '♦';
                break;
            case Suit.Clubs:
                suitChar = '♣';
                break;
            case Suit.Hearts:
                suitChar = '♥';
                break;
            case Suit.Spades:
                suitChar = '♠';
                break;
        }
        switch (value) {
            case Value.Ace:
                numValue = 1;
                valueStr = " A";
                break;
            case Value.Two:
                numValue = 2;
                break;
            case Value.Three:
                numValue = 3;
                break;
            case Value.Four:
                numValue = 4;
                break;
            case Value.Five:
                numValue = 5;
                break;
            case Value.Six:
                numValue = 6;
                break;
            case Value.Seven:
                numValue = 7;
                break;
            case Value.Eight:
                numValue = 8;
                break;
            case Value.Nine:
                numValue = 9;
                break;
            // 10, Jack, Queen, King all have value of 10
            case Value.Ten:
            case Value.Jack:
                valueStr = " J";
                numValue = 10;
                break;
            case Value.Queen:
                valueStr = " Q";
                numValue = 10;
                break;
            case Value.King:
                valueStr = " K";
                numValue = 10;
                break;
        }
        if (valueStr == "?") {
            valueStr = numValue.ToString();
            if (valueStr.Length == 1) {
                // add a space to the front of the string to accomodate for 2 digit 10
                valueStr = " " + valueStr;
            }
        }
    }
    public override string ToString()
    {
        return $"{value} of {suit}";
    }
    public int getNumValue() {
        // return the numerical value of the card
        switch (value) {
            case Value.Ace:
                return 1;
            case Value.Two:
                return 2;
            case Value.Three:
                return 3;
            case Value.Four:
                return 4;
            case Value.Five:
                return 5;
            case Value.Six:
                return 6;
            case Value.Seven:
                return 7;
            case Value.Eight:
                return 8;
            case Value.Nine:
                return 9;
            // 10, Jack, Queen, King all have value of 10
            case Value.Ten:
            case Value.Jack:
            case Value.Queen:
            case Value.King:
                return 10;
            default:
                return 0;
        }
    }

    public string[] GetASCIIString()
    {
        
        string[] str = new string[9];
        str[0] =  "┌─────────┐";
        str[1] = $"│{valueStr}       │";
        str[2] =  "│         │";
        str[3] =  "│         │";
        str[4] = $"│    {suitChar}    │";
        str[5] =  "│         │";
        str[6] =  "│         │";
        str[7] = $"│      {valueStr} │";
        str[8] =  "└─────────┘";
        return str;
    }
    public static string[] GetFaceDownCard() {
        string[] str = new string[9];
        str[0] = "┌─────────┐";
        str[1] = "│░░░░░░░░░│";
        str[2] = "│░░░░░░░░░│";
        str[3] = "│░░░░░░░░░│";
        str[4] = "│░░░░░░░░░│";
        str[5] = "│░░░░░░░░░│";
        str[6] = "│░░░░░░░░░│";
        str[7] = "│░░░░░░░░░│";
        str[8] = "└─────────┘";
        return str;
    }
}

