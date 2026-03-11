namespace Com.Game.Level.Domain
{
    internal interface ILevelSessionRepository
    {
        void Save(LevelSession levelSession);
    }
}