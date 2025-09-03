public static class MapGeneratorFactory
{
    public enum GeneratorType { DiagonalSpawn /*, Perlin, Cluster 등 추가*/ }

    public static IMapGenerator Create(GeneratorType type)
    {
        switch (type)
        {
            case GeneratorType.DiagonalSpawn:
            default:
                return new DiagonalSpawnGenerator();
        }
    }
}