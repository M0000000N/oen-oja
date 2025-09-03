using System;
using System.Collections.Generic;

public interface IMapGenerator
{
    // 반환: 이번 웨이브로 새로 생긴 블록들의 좌표
    List<GridPos> GenerateWave(GridMap map, GridConfig cfg, Random rng);
}