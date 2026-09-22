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
    private List<int> shouldDeletePlayersId = new(10);
    private Info[] otherPlayersInfo;

    public void OnPlayerUpdated(Info[] players)
    {
        otherPlayersInfo = players;

        SyncThisClientPosition();
        SyncOtherClientsPosition();
    }

    public void OnWorldReset()
    {
        otherPlayersGameObject.Clear();
        activedOtherPlayersId.Clear();
        shouldDeletePlayersId.Clear();
        otherPlayersInfo = null;
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

        foreach (Info otherPlayer in otherPlayersInfo)
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
                shouldDeletePlayersId.Add(otherPlayerKey);
            }
        }

        foreach (int id in shouldDeletePlayersId)
        {
            bool hasId = otherPlayersGameObject.TryGetValue(id, out GameObject shouldDeletePlayer);
            if (!hasId)
            {
                Debug.LogWarning("[PositionSynchronizer] 지우려는 플레이어를 딕셔너리에서 찾았지만 지우려고 했더니 플레이어를 찾을 수 없습니다!");
                continue;
            }

            Destroy(shouldDeletePlayer);
            otherPlayersGameObject.Remove(id);
        }

        activedOtherPlayersId.Clear();
        shouldDeletePlayersId.Clear();
    }
}