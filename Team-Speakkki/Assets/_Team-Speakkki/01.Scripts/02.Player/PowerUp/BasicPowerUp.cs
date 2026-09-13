namespace TeamSpeakkki.Chaewon
{
    public class BasicPowerUp : PlayerPowerUp
    {
        public override int PowerUpLevel => 0;

        public override void OnDamaged(PlayerState player)
        {
            player.Die();
        }
    }
}