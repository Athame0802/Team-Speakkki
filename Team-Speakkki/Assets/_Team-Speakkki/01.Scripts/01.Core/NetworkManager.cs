using School.PositionSync;
using UnityEngine;

namespace TeamSpeakkki.Chaewon
{
    public class NetworkManager : MonoBehaviour, IServerHandler
    {
        public static NetworkManager Instance;

        [SerializeField] private PlayerMove playerMove;
        [SerializeField] private PositionSynchronizer synchronizer;
        [SerializeField] private MapSetter mapSetter;

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

            Server = new Server(this);
        }

        public void OnConnected(MoveRules rules)
        {
            // TODO: 나중에 이벤트 기반으로 수정하기 ㅎㅎ;
            playerMove.OnConnected(rules);
        }

        public void OnDisconnected(string reason)
        {
            Debug.LogError(reason, this);
        }

        public void OnMapUpdated(GridMap map)
        {
            mapSetter.LoadMap(map);
        }

        public void OnMonstersUpdated(Monster[] monsters)
        {
            // TODO: 몬스터 만들어서 적용
        }

        public void OnPlayersUpdated(Info[] players)
        {
            // TODO: 나중에 이벤트 기반으로 수정하기 ㅎㅎ;
            synchronizer.OnPlayerUpdated(players);
        }

        public void OnWorldReset()
        {
            GameManager.Instance.LoadScene(Scenes.Ingame);

            Debug.Log("[NetworkManager] 월드가 리셋되었습니다!");
            synchronizer.OnWorldReset();
        }


        private void OnDestroy()
        {
            Server?.Dispose();
        }
    }
}