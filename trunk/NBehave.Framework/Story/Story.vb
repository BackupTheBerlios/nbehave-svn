Option Strict On

Imports System.Collections.ObjectModel

Imports NBehave.Framework.Story
Imports NBehave.Framework.Scenario


Namespace Story

    Public Class ScenarioCollection(Of T)
        Inherits Collection(Of IScenario(Of T))

    End Class

    Public Class ReadonlyScenarioCollection(Of T)
        Inherits ReadOnlyCollection(Of IScenario(Of T))

        Sub New(ByVal list As ScenarioCollection(Of T))
            MyBase.New(list)
        End Sub

    End Class


    Public MustInherit Class Story(Of T)
        Implements IStory(Of T)

        Public MustOverride Sub Specify() Implements IStory(Of T).Specify

        Public Event StoryOutcome(ByVal sender As Object, ByVal e As Scenario.OutcomeEventArgs) Implements IStory(Of T).StoryOutcome

        Private _narrative As Narrative
        Private _scenarios As New ScenarioCollection(Of T)


        Protected Sub New(ByVal narrative As Narrative)
            _narrative = narrative
        End Sub


        Protected Sub New(ByVal asA As String, ByVal iWant As String, ByVal soThat As String)
            _narrative = New Narrative(asA, iWant, soThat)
        End Sub


        Public ReadOnly Property Narrative() As Narrative Implements IStory(Of T).Narrative
            Get
                Return _narrative
            End Get
        End Property


        Public Sub AddScenario(ByVal scenario As IScenario(Of T)) Implements IStory(Of T).AddScenario
            _scenarios.Add(scenario)
        End Sub


        Protected ReadOnly Property Scenarios() As ReadonlyScenarioCollection(Of T) Implements IStory(Of T).Scenarios
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


    End Class


End Namespace