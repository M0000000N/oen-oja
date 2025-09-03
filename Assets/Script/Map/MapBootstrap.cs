using System.Collections.Generic;
using UnityEngine;

public class MapBootstrap : MonoBehaviour
{
    [SerializeField] private GridConfig config;
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private Transform blockRoot;
    [SerializeField] private float cellSize = 1f; // 그리드→월드 좌표 스케일

    private MapDirector _director;
    private float _despawnTimer;

    // 간단한 좌표→인스턴스 매핑(데모용)
    private readonly Dictionary<(int,int), GameObject> _instances = new();

    void Start()
    {
        _director = new MapDirector(config, MapGeneratorFactory.GeneratorType.DiagonalSpawn);

        // 초기 웨이브 몇 번 생성(원하면)
        for (int k = 0; k < 3; k++)
            SpawnBlocks(_director.GenerateOneWave());
    }

    void Update()
    {
        // 입력 예시: 좌/우 버튼이 웨이브 트리거라 가정(실제 게임 로직과 통합하세요)
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            var created = _director.GenerateOneWave();
            SpawnBlocks(created);
        }

        // N초마다 바닥 제거
        _despawnTimer += Time.deltaTime;
        if (_despawnTimer >= config.bottomDespawnInterval)
        {
            _despawnTimer = 0f;
            var removed = _director.DespawnBottomRow();
            DespawnBlocks(removed);
        }
    }

    private void SpawnBlocks(List<GridPos> blocks)
    {
        foreach (var p in blocks)
        {
            var key = (p.x, p.y);
            if (_instances.ContainsKey(key)) continue;

            Vector3 world = GridToWorld(p.x, p.y);
            var go = Instantiate(blockPrefab, world, Quaternion.identity, blockRoot);
            _instances[key] = go;
        }
    }

    private void DespawnBlocks(List<GridPos> blocks)
    {
        foreach (var p in blocks)
        {
            var key = (p.x, p.y);
            if (_instances.TryGetValue(key, out var go))
            {
                Destroy(go); // 실제론 풀에 반환하세요
                _instances.Remove(key);
            }
        }
    }

    private Vector3 GridToWorld(int x, int y)
    {
        return new Vector3(x * cellSize, y * cellSize, 0f);
    }
}
