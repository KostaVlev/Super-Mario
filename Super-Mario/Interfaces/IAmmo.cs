namespace SuperMario.Interfaces
{
    public interface IAmmo
    {
        int PositionX { get; set; }
        int PositionY { get; set; }
        int Damage { get; set; }
        bool IsShooted { get; set; }
    }
}
