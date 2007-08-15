Option Strict On

Imports System.Collections.ObjectModel

Imports NBehave.Framework.Story
Imports NBehave.Framework.Scenario
Imports NBehave.Framework.Utility

Namespace Story


    Public MustInherit Class Story
        Implements IStory
        Implements INarrativeAsA



        Private Class StoryScenario
            Inherits Scenario.Scenario


            Private _title As String

            Public Sub New(ByVal title As String)
                _title = title
            End Sub

            Protected Overrides ReadOnly Property Title() As String
                Get
                    Return _title
                End Get
            End Property


            Public Overrides Sub Specify()

            End Sub

            Public Overrides Function SetupWorld() As Object
                Return Nothing
            End Function


        End Class


        Public MustOverride Sub Scenarios() Implements IStory.Scenarios

        Public Event ScenarioOutcome(ByVal sender As Object, ByVal e As NBehaveEventArgs) Implements IStory.ScenarioOutcome


        Private _narrative As Narrative = New Narrative()
        Private _scenarios As New List(Of IScenario)
        Private _title As String = String.Empty


        Protected Sub New()
            _title = CamelCaseToNormalSentence(Me.GetType.Name)
        End Sub



        ''' <summary>
        ''' Write yor story here using AsA("...").IWant("...").SoThat("...")
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub Story() Implements IStory.Story


        Protected Function Story(ByVal title As String) As INarrativeAsA
            _title = title
            Return Me
        End Function


        Protected Function AsA(ByVal role As String) As INarrativeIWant Implements INarrativeAsA.AsA
            Return _narrative.AsA(role)
        End Function


        Public ReadOnly Property Narrative() As Narrative Implements IStory.Narrative
            Get
                Return _narrative
            End Get
        End Property


        Public Sub AddScenario(ByVal scenario As IScenario) Implements IStory.AddScenario
            _scenarios.Add(scenario)
        End Sub


        Public Function Scenario(ByVal title As String) As Scenario.Scenario
            Dim s As New StoryScenario(title)
            _scenarios.Add(s)
            Return s
        End Function


        Protected ReadOnly Property GetScenarios() As IList(Of IScenario) Implements IStory.ScenarioItems
            Get
                Return New ReadOnlyCollection(Of IScenario)(_scenarios)
            End Get
        End Property


        Public Overridable Function Run() As Outcome Implements IStory.Run
            Dim storyResult As New Outcome(OutcomeResult.Passed, "") 'start positive...

            If _scenarios.Count = 0 Then
                storyResult = New Outcome(OutcomeResult.Failed, "Must have more than 0 scenarios")
            End If
            Try
                For Each scenario As IScenario In _scenarios
                    Dim scenarioResult As Outcome
                    scenarioResult = scenario.Run
                    storyResult.AddOutcomes(scenarioResult.Outcomes)
                    Dim e As New NBehaveEventArgs(scenarioResult)
                    RaiseEvent ScenarioOutcome(scenario, e)
                Next
            Catch ex As Exception
                Debug.WriteLine(ex.ToString)
            End Try

            Return storyResult

        End Function


        Public ReadOnly Property Title() As String Implements IStoryBase.Title
            Get
                Return _title
            End Get
        End Property

    End Class


End Namespace