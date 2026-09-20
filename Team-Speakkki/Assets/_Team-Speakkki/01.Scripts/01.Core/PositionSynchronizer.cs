using School.PositionSync;
using TeamSpeakkki.Chaewon;
using UnityEngine;
using System.Collections.Generic;

public class PositionSynchronizer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject otherPlayerPrefab;

    // 만약 게임 도중에 플레이어가 들어올 수 있다면 List로 변경
    private List<GameObject> otherPlayers;

    public void Start()
    {
        Server server = NetworkManager.Instance.Server;

        Info[] players = server.GetPos();
        
        otherPlayers = new(players.Length);

        for (int i = 0; i < otherPlayers.Count; i++)
        {
            Debug.Log("추가");
            Vector3 otherPlayerPosition = new Vector3(players[i].X, players[i].Y, 0f);

            otherPlayers.Add(Instantiate(
                otherPlayerPrefab, 
                otherPlayerPosition, 
                Quaternion.identity));
        }
    }

    public void Update()
    {
        Server server = NetworkManager.Instance.Server;

        Vector2 playerPosition = player.transform.position;
        server.SetPos(new Info(playerPosition.x, playerPosition.y));

        Info[] otherPlayerInfos = server.GetPos();
        for (int i = 0; i < otherPlayerInfos.Length; i++)
        {
            Vector3 otherPlayerPosition = new Vector3(otherPlayerInfos[i].X, otherPlayerInfos[i].Y, 0f);

            if (i >= otherPlayers.Count)
            {
                Debug.Log("추가");
                otherPlayers.Add(Instantiate(
                otherPlayerPrefab,
                otherPlayerPosition,
                Quaternion.identity));

                return;
            }

            otherPlayers[i].transform.position = otherPlayerPosition;
        }
    }
}