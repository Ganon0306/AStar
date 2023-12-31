using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMap : TileMapController
{
    private const string OBSTACLE_TILEMAP_OBJ_NAME = "ObstacleTilemap";
    private GameObject[] castleObjs = default;      //!< 길찾기 알고리즘을 테스트 할 출발지와 목적지를 캐싱한 오브잭트 배열

    //! Awake 타임에 초기화 할 내용을 재정의한다.
    public override void InitAwake(MapBoard mapController_)
    {
        this.tileMapObjName = OBSTACLE_TILEMAP_OBJ_NAME;
        base.InitAwake(mapController_);
    }       // InitAwake()

    private void Start()
    {
        StartCoroutine(DelayStart(0f));
    }
    private IEnumerator DelayStart(float delay)
    {
        yield return new WaitForSeconds(delay);
        DoStart();
    }       // DelayStart()

    private void DoStart()
    {
        // { 출발지와 목적지를 설정해서 타일을 배치한다.
        castleObjs = new GameObject[2];
        TerrainControler[] passableTerrains = new TerrainControler[2];

        List<TerrainControler> searchTerrains = default;
        int searchIdx = 0;
        TerrainControler foundTile = default;

        // 출발지는 좌측에서 우측으로 y축을 서치해서 빈 지형을 받아온다.
        searchIdx = 0;
        foundTile = default;
        while(foundTile ==  null || foundTile == default)
        {
            // 위에서 아래로 서치한다.
            searchTerrains = mapController.GetTerrains_Colum(searchIdx, true);
            foreach(var searchTerrain in searchTerrains)
            {
                if (searchTerrain.IsPassable)
                {
                    foundTile = searchTerrain;
                    break;
                }
                else { /* Do Nothing */ }
            }

            if (foundTile != null || foundTile != default) { break; }
            if(mapController.MapCellSize.x -1 <= searchIdx) { break; }  
            searchIdx++;

        }       // loop : 출발지를 찾는 루프
        passableTerrains[0] = foundTile;

        // 목적지는 우측에서 좌측으로 y축을 서치해서 빈 지형을 받아온다.
        searchIdx = mapController.MapCellSize.x - 1;
        foundTile = default;
        while(foundTile == null || foundTile == default)
        {
            // 아래에서 위로 서치한다.
            searchTerrains = mapController.GetTerrains_Colum(searchIdx);
            foreach(var searchTerrain in searchTerrains)
            {
                if(searchTerrain.IsPassable)
                {
                    foundTile = searchTerrain;
                    break;
                }
                else { /* Do Nothing */ }
            }

            if(foundTile != null || foundTile != default) { break; }
            if(searchIdx <= 0 ) { break; }
            searchIdx--;
        }       // loop : 출발지를 찾는 루프
        passableTerrains[1] = foundTile;

        // } 출발지와 목적지를 설정해서 타일을 배치한다.

        // { 출발지와 목적지에 지물을 추가한다.
        
        // } 출발지와 목적지에 지물을 추가한다.
    }       // DoStart()

    //! 지물을 추가한다.
    public void Add_Obstacle(GameObject obstacle_)
    {
        allTileObjs.Add(obstacle_);
    }       // Add_Obstacle()
}
