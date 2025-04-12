namespace RhenusCodingChallenge.Models;

public class BetResponse
{
    public int Account { get; set; }     // Updated account balance
    public string Status { get; set; }     // "won" or "lost"
    public string Points { get; set; }     // Change in points (e.g., "+900" or "-100")
    public string Message { get; set; }    // Optional: error or additional message
}