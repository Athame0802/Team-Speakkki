using School.PositionSync;
using UnityEngine;

namespace TeamSpeakkki.Chaewon
{
    public class NetworkManager : MonoBehaviour
    {
        public static NetworkManager Instance;


        public Server Server { private set; get; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            Server = new Server();
        }

        private void OnDestroy()
        {
            Server?.Dispose();
        }
    }
}