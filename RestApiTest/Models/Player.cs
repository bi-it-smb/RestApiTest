namespace RestApiTest.Models;

public class Player
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Class { get; set; } = "Warrior";

    public int Level { get; set; } = 1;

    public int Experience { get; set; } = 0;

    public int Health { get; set; } = 100;

    public int Gold { get; set; } = 50;
}