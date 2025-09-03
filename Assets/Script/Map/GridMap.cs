using System;
using System.Collections.Generic;

public enum CellType { Empty, Block, Player }

public struct GridPos
{
    public int x, y;
    public GridPos(int x, int y){ this.x = x; this.y = y; }
}

public class GridMap
{
    private readonly CellType[,] _cells;
    public int Width { get; }
    public int Height { get; }

    public GridMap(int width, int height)
    {
        Width = width;
        Height = height;
        _cells = new CellType[width, height];
    }

    public bool InBounds(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;

    public CellType Get(int x, int y) => _cells[x, y];

    public void Set(int x, int y, CellType t)
    {
        _cells[x, y] = t;
    }

    public IEnumerable<GridPos> AllBlocks()
    {
        for (int y = 0; y < Height; y++)
        for (int x = 0; x < Width; x++)
            if (_cells[x, y] == CellType.Block)
                yield return new GridPos(x, y);
    }

    public int TopmostBlockY()
    {
        for (int y = Height - 1; y >= 0; y--)
        for (int x = 0; x < Width; x++)
            if (_cells[x, y] == CellType.Block)
                return y;
        return -1;
    }

    public int BottommostBlockY()
    {
        for (int y = 0; y < Height; y++)
        for (int x = 0; x < Width; x++)
            if (_cells[x, y] == CellType.Block)
                return y;
        return -1;
    }
}