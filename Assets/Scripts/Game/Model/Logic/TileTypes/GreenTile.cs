namespace Model
{
    public sealed class GreenTile : IContainedTile 
    {
        public TileType GetTileType() => TileType.Green;
        
        public bool MathcesWith(TileType tileData) => tileData == GetTileType();
        public bool CanBePlaced(TileType tileData) => tileData == TileType.Empty;
    }
}
