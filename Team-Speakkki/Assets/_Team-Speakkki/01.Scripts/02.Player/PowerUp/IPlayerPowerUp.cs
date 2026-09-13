namespace TeamSpeakkki.Chaewon
{
    public abstract class IPlayerPowerUp
    {
        public int PowerUpLevel { get; }

        public void OnDamaged(PlayerState player)
        {
            IPlayerPowerUp powerUpAfterDamaged = player.GetPowerUpWhenBeDamaged(PowerUpLevel);
            player.ChangePowerUp(powerUpAfterDamaged);
        }
    }
}
