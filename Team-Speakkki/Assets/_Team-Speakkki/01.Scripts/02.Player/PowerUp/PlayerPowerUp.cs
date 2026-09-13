namespace TeamSpeakkki.Chaewon
{
    public abstract class PlayerPowerUp
    {
        public virtual int PowerUpLevel => 2;

        public virtual void OnDamaged(PlayerState player)
        {
            PlayerPowerUp powerUpAfterDamaged = player.GetPowerUpWhenBeDamaged(PowerUpLevel);
            player.ChangePowerUp(powerUpAfterDamaged);
        }
    }
}
