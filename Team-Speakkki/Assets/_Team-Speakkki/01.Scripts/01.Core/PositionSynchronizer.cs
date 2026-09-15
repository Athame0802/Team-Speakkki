using School.PositionSync;
using TeamSpeakkki.Chaewon;
using UnityEngine;
using System.Collections.Generic;

public class PositionSynchronizer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject otherPlayerPrefab;

    private Server server;

    // 만약 게임 도중에 플레이어가 들어올 수 있다면 List로 변경
    private List<GameObject> otherPlayers;

    public void Start()
    {
        server = NetworkManager.Instance.Server;

        Info[] players = server.GetPos();

        otherPlayers = new(players.Length);

        for (int i = 0; i < otherPlayers.Count; i++)
        {
            Vector3 otherPlayerPosition = new Vector3(players[i].X, players[i].Y, 0f);

            otherPlayers[i] = Instantiate(
                otherPlayerPrefab, 
                otherPlayerPosition, 
                Quaternion.identity);
        }
    }

    public void Update()
    {
        Vector2 playerPosition = player.transform.position;
        server.SetPos(new Info(playerPosition.x, playerPosition.y));

        Info[] otherPlayerInfos = server.GetPos();
        for (int i = 0; i < otherPlayers.Count && i < otherPlayerInfos.Length; i++)
        {
            Vector3 otherPlayerPosition = new Vector3(otherPlayerInfos[i].X, otherPlayerInfos[i].Y, 0f);

            otherPlayers[i].transform.position = otherPlayerPosition;
        }
    }
}