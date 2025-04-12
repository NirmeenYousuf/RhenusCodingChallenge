namespace RhenusCodingChallenge.GameState;

public interface IGameState
{
    int CurrentNumber { get; }
    void GenerateNewNumber();
    
}