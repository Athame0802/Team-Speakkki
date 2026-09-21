using School.PositionSync;
using TeamSpeakkki.Chaewon;
using UnityEngine;
using System.Collections.Generic;

public class PositionSynchronizer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject otherPlayerPrefab;

    private Dictionary<int, GameObject> otherPlayersGameObject = new(10);
    private HashSet<int> activedOtherPlayersId = new(10);

    public void Start()
    {
        Server server = NetworkManager.Instance.Server;

        Info[] otherPlayers = server.GetPos();
        
        foreach (Info otherPlayer in otherPlayers)
            AddOtherPlayer(otherPlayer);
    }

    public void Update()
    {
        Server server = NetworkManager.Instance.Server;

        SyncThisClientPosition();
        SyncOtherClientsPosition();
    }

    private GameObject AddOtherPlayer(Info playerInfo)
    {
        Debug.Log("[PositionSynchronizer] 다른 플레이어 추가");

        Vector3 otherPlayerPosition = new Vector3(playerInfo.X, playerInfo.Y, 0f);

        otherPlayersGameObject.Add(playerInfo.Id, Instantiate(
                otherPlayerPrefab,
                otherPlayerPosition,
                Quaternion.identity));

        return otherPlayersGameObject[playerInfo.Id];
    }

    private void SyncThisClientPosition()
    {
        Server server = NetworkManager.Instance.Server;

        Vector2 playerPosition = player.transform.position;
        server.SetPos(new Info(playerPosition.x, playerPosition.y));
    }

    private void SyncOtherClientsPosition()
    {
        Server server = NetworkManager.Instance.Server;

        Info[] otherPlayers = server.GetPos();

        foreach (Info otherPlayer in otherPlayers)
        {
            activedOtherPlayersId.Add(otherPlayer.Id);

            bool isAlreadyJoinedPlayer = otherPlayersGameObject.TryGetValue(otherPlayer.Id, out GameObject otherPlayerGameObject);
            if (!isAlreadyJoinedPlayer)
                otherPlayerGameObject = AddOtherPlayer(otherPlayer);

            Vector2 otherPlayerPositon = new Vector2(otherPlayer.X, otherPlayer.Y);
            otherPlayerGameObject.transform.position = otherPlayerPositon;
        }

        foreach (KeyValuePair<int, GameObject> otherPlayer in otherPlayersGameObject)
        {
            int otherPlayerKey = otherPlayer.Key;

            bool isDisconnectedPlayer = !activedOtherPlayersId.Contains(otherPlayerKey);
            if (isDisconnectedPlayer)
            {
                Destroy(otherPlayer.Value);
                otherPlayersGameObject.Remove(otherPlayer.Key);
            }
        }

        activedOtherPlayersId.Clear();
    }
}