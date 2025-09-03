using System;
using System.Collections.Generic;

public class DiagonalSpawnGenerator : IMapGenerator
{
    public List<GridPos> GenerateWave(GridMap map, GridConfig cfg, Random rng)
    {
        var created = new List<GridPos>();
        int count = rng.Next(cfg.spawnCountRange.x, cfg.spawnCountRange.y + 1);
        int x = cfg.spawnPoint.x;
        int y = cfg.spawnPoint.y;

        if (!map.InBounds(x, y)) return created;

        // 시작점이 비어있으면 먼저 놓기(옵션)
        if (map.Get(x, y) == CellType.Empty)
        {
            map.Set(x, y, CellType.Block);
            created.Add(new GridPos(x, y));
        }

        for (int i = 0; i < count; i++)
        {
            // 좌상(-1, +1) 또는 우상(+1, +1)
            int dx = rng.Next(0, 2) == 0 ? -1 : 1;
            int nx = x + dx;
            int ny = y + 1;

            // 한 칸 위 대각선이 범위 밖이면 반대 방향도 시도
            if (!map.InBounds(nx, ny) || map.Get(nx, ny) != CellType.Empty)
            {
                dx = -dx;
                nx = x + dx;
                // y+1 유지
            }

            if (map.InBounds(nx, ny) && map.Get(nx, ny) == CellType.Empty)
            {
                map.Set(nx, ny, CellType.Block);
                created.Add(new GridPos(nx, ny));
                x = nx;
                y = ny;
            }
            else
            {
                // 양쪽 다 불가면 스탑
                break;
            }
        }

        return created;
    }
}