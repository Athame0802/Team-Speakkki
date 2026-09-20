using System;
using System.Collections.Generic;
using System.Text;
using Unity.Cinemachine;
using UnityEngine;

namespace TeamSpeakkki.Chaewon
{
    /// <summary>
    /// <para>플레이어 스포너 클래스.</para>
    /// <para>
    /// 해당 스포너는 <see cref="MapSetter"/>에서 <see cref="SetPlayerSpawner(Vector2)"/>으로 설정합니다.
    /// </para>
    /// </summary>
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        public void SetPlayerSpawner(Vector2 position)
        {
            transform.position = position;
            SpawnPlayer();
        }

        public void SpawnPlayer()
        {
            player.transform.position = transform.position;
            player.SetActive(true);
        }
    }
}
