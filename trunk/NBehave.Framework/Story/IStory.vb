Option Strict On

Imports System.Collections.ObjectModel
Imports NBehave.Framework.Scenario

Namespace Story


    Public Interface IStory(Of T)
        ReadOnly Property Narrative() As Narrative
        Sub AddScenario(ByVal scenario As IScenario(Of T))
        Sub Specify()
        Sub Run()
        Event StoryOutcome As EventHandler(Of OutcomeEventArgs)


        ReadOnly Property Scenarios() As ReadonlyScenarioCollection(Of T)


    End Interface

End Namespace