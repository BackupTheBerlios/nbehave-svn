Option Strict On

Imports System.Collections.ObjectModel
Imports NBehave.Framework.Scenario

Namespace Story


    Public Interface IStory(Of T As Class)
        Inherits IStoryBase
        Sub AddScenario(ByVal scenario As IScenario(Of T))
        Sub Scenarios()
        ReadOnly Property ScenarioItems() As ReadonlyScenarioCollection(Of T)
    End Interface

    Public Interface IStoryBase
        ReadOnly Property Narrative() As Narrative
        Function Run() As Outcome
        Sub Story()
        Event ScenarioOutcome As EventHandler(Of NBehaveEventArgs)
    End Interface

End Namespace