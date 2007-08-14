Option Strict On

Imports System.Collections.ObjectModel
Imports NBehave.Framework.Scenario

Namespace Story


    Public Interface IStory
        Inherits IStoryBase
        Sub AddScenario(ByVal scenario As IScenario)
        Sub Scenarios()
        ReadOnly Property ScenarioItems() As IList(Of IScenario)
    End Interface

    Public Interface IStoryBase
        ReadOnly Property Title() As String
        ReadOnly Property Narrative() As Narrative
        Function Run() As Outcome
        Sub Story()
        Event ScenarioOutcome As EventHandler(Of NBehaveEventArgs)
    End Interface

End Namespace