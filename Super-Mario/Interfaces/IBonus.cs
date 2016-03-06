namespace SuperMario.Interfaces
{
    public interface IBonus
    {
        int PositionX { get; set; }
        int PositionY { get; set; }
        int BonusPoints { get; set; }
        bool IsVisible { get; set; }
    }
}
