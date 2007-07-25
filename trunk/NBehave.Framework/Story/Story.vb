Option Strict On

Imports System.Collections.ObjectModel

Imports NBehave.Framework.Story
Imports NBehave.Framework.Scenario


Namespace Story

    Public Class ScenarioCollection(Of T As Class)
        Inherits Collection(Of IScenario(Of T))

    End Class

    Public Class ReadonlyScenarioCollection(Of T As Class)
        Inherits ReadOnlyCollection(Of IScenario(Of T))

        Sub New(ByVal list As ScenarioCollection(Of T))
            MyBase.New(list)
        End Sub

    End Class


    Public MustInherit Class Story(Of T As Class)
        Implements IStory(Of T)

        Public MustOverride Sub Scenarios() Implements IStory(Of T).Scenarios


        Public Event ScenarioOutcome(ByVal sender As Object, ByVal e As NBehaveEventArgs) Implements IStory(Of T).ScenarioOutcome
        'Public Event ExecutingScenario As EventHandler(Of ScenarioEventArgs)
        'Public Event ScenarioExecuted As EventHandler(Of ScenarioEventArgs)


        Private _narrative As Narrative = New Narrative()
        Private _scenarios As New ScenarioCollection(Of T)


        ''' <summary>
        ''' Write yor story here using AsA("...").IWant("...").SoThat("...")
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub Story()


        Public Function AsA(ByVal role As String) As INarrativeIWant
            Return _narrative.AsA(role)
        End Function


        Public ReadOnly Property Narrative() As Narrative Implements IStory(Of T).Narrative
            Get
                Return _narrative
            End Get
        End Property


        Public Sub AddScenario(ByVal scenario As IScenario(Of T)) Implements IStory(Of T).AddScenario
            _scenarios.Add(scenario)
        End Sub


        Protected ReadOnly Property GetScenarios() As ReadonlyScenarioCollection(Of T) Implements IStory(Of T).ScenarioItems
            Get
                Return New ReadonlyScenarioCollection(Of T)(_scenarios)
            End Get
        End Property



        Public Overridable Function Run() As Outcome Implements IStory(Of T).Run
            Dim storyResult As New Outcome(_scenarios.Count > 0, "Must have more than 0 scenarios")

            Try
                For Each scenario As IScenario(Of T) In _scenarios
                    Dim scenarioResult As Outcome
                    scenarioResult = scenario.Run
                    storyResult.AddOutcomes(scenarioResult.Outcomes)
                    Dim e As New NBehaveEventArgs(scenarioResult)
                    RaiseEvent ScenarioOutcome(Me, e)
                Next
            Catch ex As Exception
                Debug.WriteLine(ex.ToString)
            End Try

            Return storyResult

        End Function


    End Class


End Namespace