namespace RhenusCodingChallenge.GameState;

public class GameState: IGameState
{
    private int _currentNumber;
    private readonly Random _random;

    public GameState()
    {
        _random = new Random();
        GenerateNewNumber(); // Initial number
    }

    public int CurrentNumber => _currentNumber;

    public void GenerateNewNumber()
    {
        _currentNumber = _random.Next(0, 10);
    }
    
    
}