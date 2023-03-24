using System;
using SFML.System;

class MapGenerator
{
    private int MapWidth;
    private int MapHeight;
    private int Seed;
    private FastNoiseLite NoiseGenerator;

    public MapGenerator(int mapWidth, int mapHeight, int seed)
    {
        MapWidth = mapWidth;
        MapHeight = mapHeight;
        Seed = seed;

        NoiseGenerator = new FastNoiseLite(Seed);
        NoiseGenerator.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
    }

    public Tile[ , ] GenerateMap()
    {
        Tile[ , ] tiles = new Tile[MapWidth, MapHeight];

        FastNoiseLite terrainNoise = new FastNoiseLite();
        terrainNoise.SetSeed(Environment.TickCount);
        terrainNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
        terrainNoise.SetFrequency(0.15f);
        terrainNoise.SetFractalOctaves(3);
        terrainNoise.SetFractalType(FastNoiseLite.FractalType.None);

        FastNoiseLite oreNoise = new FastNoiseLite();
        oreNoise.SetSeed(Environment.TickCount + 1000);
        oreNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);
        oreNoise.SetFrequency(0.09f);
        oreNoise.SetFractalOctaves(3);
        oreNoise.SetFractalType(FastNoiseLite.FractalType.None);

        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                float terrainValue = terrainNoise.GetNoise(x, y);
                float oreValue = oreNoise.GetNoise(x, y);

                Tile.Type tileType;
                if (terrainValue < 0.33f)
                {
                    tileType = Tile.Type.Dirt1;
                }
                else if (terrainValue < 0.66f)
                {
                    tileType = Tile.Type.Dirt2;
                }
                else
                {
                    tileType = Tile.Type.Grass;
                }

                tiles[x, y] = new Tile(tileType);

                if (oreValue > .7f)
                {
                    tiles[x, y].GiveResource(new Resource(Resource.ResourceType.Iron, tiles[x, y], new Vector2f(x*64, y*64)));
                }
            }
        }
        return tiles;
    }

}