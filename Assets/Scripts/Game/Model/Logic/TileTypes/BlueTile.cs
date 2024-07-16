namespace Model
{
    public sealed class BlueTile : IContainedTile 
    {
        public TileType GetTileType() => TileType.Blue;
        
        public bool MathcesWith(TileType tileData) => tileData == GetTileType();
        public bool CanBePlaced(TileType tileData) => tileData == TileType.Empty;
    }
}
