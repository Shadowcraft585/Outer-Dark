public static class MovementCostCalculator
{
    public static int GetCost(TerrainType terrain)
    {
        return terrain switch
        {
            TerrainType.Plains => 1,
            TerrainType.Forest => 2,
            TerrainType.Swamp => 2,
            TerrainType.Mountain => 3,
            TerrainType.Water => 999,
            _ => 1
        };
    }
}