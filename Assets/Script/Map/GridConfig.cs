using UnityEngine;

[CreateAssetMenu(fileName = "GridConfig", menuName = "Game/Grid Config")]
public class GridConfig : ScriptableObject
{
    [Header("Grid Size")] public int width = 8;
    public int height = 16;

    [Header("Spawn Rules")] public Vector2Int spawnPoint = new Vector2Int(3, 0); // (x, y=바닥)
    public Vector2Int spawnCountRange = new Vector2Int(2, 5); // i개 범위
    public float topRowGapThreshold = 3f; // m개 이하 조건 판단용(상단 차이)

    [Header("Timing Rules")] public float bottomDespawnInterval = 2f; // N초 단위

    [Header("Random")] public int seed = 0; // 0이면 런타임 시 랜덤 씨드
}