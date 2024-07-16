namespace Model
{
    public sealed class RedTile : IContainedTile 
    {
        public TileType GetTileType() => TileType.Red;
        
        public bool MathcesWith(TileType tileData) => tileData == GetTileType();
        public bool CanBePlaced(TileType tileData) => tileData == TileType.Empty;
    }
}
