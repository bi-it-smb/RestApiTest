namespace GameUi.Models;

public class BattleResult
{
    public string Message { get; set; } = "";

    public string Enemy { get; set; } = "";

    public int DamageTaken { get; set; }

    public int ExperienceGained { get; set; }

    public int GoldGained { get; set; }

    public Player Player { get; set; } = new();
}