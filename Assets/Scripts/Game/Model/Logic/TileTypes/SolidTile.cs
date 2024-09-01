namespace Model
{
    public sealed class SolidTile : IContainedTile 
    {
        public TileType GetTileType() => TileType.Solid;
        
        public bool MathcesWith(TileType tileData) => tileData == GetTileType();
        public bool CanBePlaced(TileType tileData) => tileData == TileType.Empty;
    }
}
