using System;
using System.Collections.Generic;

public class MapDirector
{
    private readonly GridMap _map;
    private readonly GridConfig _cfg;
    private readonly IMapGenerator _generator;
    private readonly Random _rng;

    public MapDirector(GridConfig cfg, MapGeneratorFactory.GeneratorType type)
    {
        _cfg = cfg;
        _map = new GridMap(cfg.width, cfg.height);
        _generator = MapGeneratorFactory.Create(type);
        _rng = _cfg.seed == 0 ? new Random() : new Random(_cfg.seed);
    }

    public GridMap Map => _map;

    // 1회 웨이브 생성
    public List<GridPos> GenerateOneWave()
    {
        var created = _generator.GenerateWave(_map, _cfg, _rng);

        // "맨 위 블럭이랑 플레이어 블럭 차이 m개 이하면 맨 위 블럭 하나 더 생김" 처리 훅
        // 여기선 플레이어 y를 외부에서 받아 적용하도록 하고, 샘플로 y=0이라 가정
        int playerY = 0; // 실제 게임 로직에서 주입
        int topY = _map.TopmostBlockY();
        if (topY >= 0)
        {
            int gap = topY - playerY;
            if (gap <= _cfg.topRowGapThreshold)
            {
                // 스폰지점 위로 1칸 추가 시도(가능한 방향 랜덤)
                int x = _cfg.spawnPoint.x;
                int y = topY; // 상단 높이에 맞춰 한 칸 더
                int dx = _rng.Next(0, 2) == 0 ? -1 : 1;
                int nx = x + dx;
                int ny = y + 1;
                if (!_map.InBounds(nx, ny))
                {
                    dx = -dx;
                    nx = x + dx;
                }
                if (_map.InBounds(nx, ny) && _map.Get(nx, ny) == CellType.Empty)
                {
                    _map.Set(nx, ny, CellType.Block);
                    created.Add(new GridPos(nx, ny));
                }
            }
        }

        return created;
    }

    // N초마다 바닥 라인 제거 로직(데이터만)
    public List<GridPos> DespawnBottomRow()
    {
        var removed = new List<GridPos>();
        int bottomY = _map.BottommostBlockY();
        if (bottomY < 0) return removed;

        for (int x = 0; x < _map.Width; x++)
        {
            if (_map.Get(x, bottomY) == CellType.Block)
            {
                _map.Set(x, bottomY, CellType.Empty);
                removed.Add(new GridPos(x, bottomY));
            }
        }
        return removed;
    }
}
