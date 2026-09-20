using System;
using System.Collections.Generic;
using School.PositionSync;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TeamSpeakkki.Chaewon
{
    /// <summary>
    /// <para><see cref="MapSetter"/>에서 <see cref="TileKind"/>에 맞는 Tile을 등록하기 위한 Serializable Struct.</para>
    /// <para>
    /// <see cref="Dictionary{TKey, TValue}"/>는 인스펙터에서 등록할 수 없어 해당 struct를 List 형식으로 만들어 등록할 수 있게 한다.
    /// </para>
    /// </summary>
    [Serializable]
    public struct TileInfo
    {
        public TileKind Kind;
        public Tile Tile;
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    public class MapSetter : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private List<TileInfo> tileInfos;

        [SerializeField] private PlayerSpawner playerSpawner;
        [SerializeField] private DeathZone deathZone;

        private Dictionary<TileKind, Tile> tileDictionary = new(10);

        private void Awake()
        {
            foreach (TileInfo tileInfo in tileInfos)
                tileDictionary.Add(tileInfo.Kind, tileInfo.Tile);

            // TODO: 게임메니저가 로딩 창에서 맵 로딩하게 바꾸기
            LoadMap(MapCatalog.Default);
        }

        private void LoadMap(GridMap map)
        {
            tilemap.transform.position = new Vector2(map.OriginX, map.OriginY);

            foreach (GridCell cell in map.Cells)
                PlaceCell(cell);

            deathZone.SetDeathZone(map.OriginX, map.FallBoundaryY, map.Width);
            playerSpawner.SetPlayerSpawner(new Vector2(map.Spawn.X, map.Spawn.Y + 2));
        }

        private void PlaceCell(GridCell cell)
        {
            Vector3Int cellPosition = new Vector3Int(cell.X, cell.Y);
            bool isSupportedTileKind = tileDictionary.TryGetValue(cell.Kind, out Tile tile);

            if (!isSupportedTileKind)
                return;

            tilemap.SetTile(cellPosition, tile);
        }
    }
}
