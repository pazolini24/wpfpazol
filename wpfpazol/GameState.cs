namespace wpfpazol
{
    public class GameState
    {
        private Block currentBlock;
        public Block CurrentBLock
        {
            get => currentBlock;
            private set
            {
                currentBlock= value;
                currentBlock.Reset();
            }
        }
        public GameGrid GameGrid { get; }
        public BlockQueue BlockQueue { get; }
        public bool GameOver { get; private set; }

        public GameState()
        {
            GameGrid = new GameGrid(22, 10);
            BlockQueue= new BlockQueue();
            CurrentBlock = BlockQueue.GetAndUpdate();
        }
        private bool BlockFits()
        {
            foreach (Position p in CurrentBlock.TilePositions())
            {
                if(!GameGrid.IsEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }
            return true;
        }
        public void RotateBlockCW()
        {
            CurrentBLock.RotateCW();
            if(!BlockFits())
            {
                CurrentBLock.RotateCCW(); 
            }
        }

        public void RotateBlockCCW()
        {
            CurrentBLock.RotateCCW();
            if (!BlockFits())
            {
                CurrentBLock.RotateCW();
            }
        }
        public void MoveBlockLeft()
        {
            CurrentBLock.Move(0, -1);

            if (!BlockFits() )
            {
                CurrentBLock.Move(0, 1);
            }
        }
        public void MoveBlockRight()
        {
            CurrentBLock.Move(0, 1);

            if(!BlockFits() )
            {
                CurrentBLock.Move(0, -1);
            }
        }
        private bool isGameOver()
        {
            return !(GameGrid.IsRowEmpty(0) && GameGrid.IsRowEmpty(1));
        }
        private void PlaceBlock()
        {
            foreach (Position p in CurrentBLock.TilePositions())
            {
                GameGrid[p.Row, p.Column] = CurrentBLock.Id;
            }
            GameGrid.ClearFullRows();

            if (isGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBLock = BlockQueue.GetAndUpdate();
            }
        }
        public void MoveBLockDown()
        {
            CurrentBLock.Move(1, 0);
            if (!BlockFits() )
            {
                CurrentBLock.Move(-1, 0);
                PlaceBlock();
            }
        }
    }
}
