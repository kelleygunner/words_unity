namespace Com.Game.Level.Contracts
{
    public interface ILevelFacade
    {
        StartLevelResult StartGeneratedLevel(StartLevelCommand command);
    }
}