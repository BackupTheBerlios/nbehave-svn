Option Strict On

Imports System.Collections.ObjectModel
Imports NBehave.Framework.Scenario

Namespace Story


    Public Interface IStory(Of T As Class)
        ReadOnly Property Narrative() As Narrative
        Sub AddScenario(ByVal scenario As IScenario(Of T))
        Sub Scenarios()
        Function Run() As Outcome
        Event ScenarioOutcome As EventHandler(Of NBehaveEventArgs)

        ReadOnly Property ScenarioItems() As ReadonlyScenarioCollection(Of T)

    End Interface

End Namespace