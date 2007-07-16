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


        Public Event StoryOutcome(ByVal sender As Object, ByVal e As Scenario.OutcomeEventArgs) Implements IStory(Of T).StoryOutcome


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


        Public Overridable Sub Run() Implements IStory(Of T).Run

            For Each scenario As IScenario(Of T) In _scenarios
                Dim results As ReadOnlyCollection(Of ScenarioOutcome)

                results = scenario.Run
                RaiseEvent StoryOutcome(Me, New OutcomeEventArgs(results))
            Next

        End Sub




        Private _scenario As Scenario.IScenario(Of T)

        Public Function Scenario(ByVal name As String) As Scenario.IScenario(Of T)
            Return _scenario
        End Function

    End Class


End Namespace