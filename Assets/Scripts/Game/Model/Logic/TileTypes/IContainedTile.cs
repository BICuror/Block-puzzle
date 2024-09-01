namespace Model
{
    public interface IContainedTile
    {
        public TileType GetTileType();

        public bool MathcesWith(TileType tileType);
        public bool CanBePlaced(TileType tileType);
    }
}

