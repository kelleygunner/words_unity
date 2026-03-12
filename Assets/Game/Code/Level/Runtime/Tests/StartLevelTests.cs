using System;
using Com.Game.Level.Application;
using Com.Game.Level.Application.UseCases;
using Com.Game.Level.Contracts;
using Com.Game.Level.Domain;
using Com.Game.Level.Infrastructure;
using NUnit.Framework;
using Zenject;

public class StartLevelTests
{
    private DiContainer _container;
    private ILevelFacade _levelFacade;

    [SetUp]
    public void Setup()
    {
        _container = new DiContainer();

        // регистрация Level
        _container.Bind<ILevelFacade>()
            .To<LevelFacade>()
            .AsSingle();

        _container.Bind<StartGeneratedLevelUseCase>().AsTransient();
        _container.Bind<ILevelSessionRepository>()
            .To<LevelSessionRepository>()
            .AsSingle();

        _levelFacade = _container.Resolve<ILevelFacade>();
    }

    [Test]
    public void StartGeneratedLevel_ReturnsSessionId()
    {
        var result = _levelFacade.StartGeneratedLevel(new StartLevelCommand());

        Assert.IsNotNull(result);
        Assert.AreNotEqual(Guid.Empty, result.SessionId);
    }
}