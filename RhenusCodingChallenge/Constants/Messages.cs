namespace RhenusCodingChallenge.Constants;

public static class Messages
{
    public const string PlayerNotFound = "Player not found.";
    public const string PlayerIdRequired = "Player is required.";
    public const string PlayerAlreadyExists = "Player already exists.";
    public const string PlayerCreated = "Player created";
    public const string InsufficientBalance = "Insufficient balance to place the bet. Please reset player's account and try again";

    public const string PlayerAccountResetSuccessful =
        "Player account has been reset successfully. The player has now 10000 points to place the new bet";
    public const string GameResetSuccessful = "Game reset performed successfully.New betting round has started and all players account have also been reset. You can play again";

    public const string InvalidBet =
        "Invalid data provided for Bet. Points should be greater than 0 and number should be between 0 and 9";
}