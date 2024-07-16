namespace Model
{
    public sealed class EmptyTile : IContainedTile 
    {
        public TileType GetTileType() => TileType.Empty;
        
        public bool MathcesWith(TileType tileData) => false;
        public bool CanBePlaced(TileType tileData) => false;
    }
}
