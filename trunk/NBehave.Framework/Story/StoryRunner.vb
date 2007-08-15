Option Strict On

Imports System.Collections.ObjectModel
Imports NBehave.Framework.Scenario
Imports NBehave.Framework.Story
Imports NBehave.Framework.Utility


Namespace Story


    Public Class StoryRunner
        Inherits StoryRunnerBase


        Private scenarioOutcomes As ReadOnlyCollection(Of Outcome)


        Public Sub New()
        End Sub

        Public Sub New(ByVal assemblyToParseForStories As Reflection.Assembly)
            'Me.New()
            If assemblyToParseForStories Is Nothing Then Throw New ArgumentException("assemblyToParseForStories is NULL")

            ParseAssemblyForStories(assemblyToParseForStories)
        End Sub


        Private Sub ParseAssemblyForStories(ByRef assemblyToParseForStories As Reflection.Assembly)
            For Each t As Type In assemblyToParseForStories.GetTypes()
                If IsOfTypeStory(t) Then
                    Dim i As Object = System.Activator.CreateInstance(t, True)
                    AddStory(i, False)
                End If
            Next
        End Sub


        Public Overrides Sub Run()
            OnRunStart(Nothing)

            For Each aStory As IStoryBase In Stories

                Try
                    AddHandler aStory.ScenarioOutcome, AddressOf ScenarioOutcome

                    InitStory(aStory)
                    AddScenarios(CType(aStory, Object))

                    OnExecutingStory(New StoryEventArgs(aStory))
                    ExecuteScenariosInStory(aStory)
                    OnStoryExecuted(New StoryEventArgs(aStory, GetStoryOutcome))

                Catch ex As Exception
                    Debug.WriteLine(ex.ToString)

                Finally
                    RemoveHandler aStory.ScenarioOutcome, AddressOf ScenarioOutcome
                End Try

            Next

            OnRunFinished(Nothing)

        End Sub


        Private Sub InitStory(ByRef aStory As IStoryBase)
            aStory.Story()
        End Sub


        Private Sub ExecuteScenariosInStory(ByRef aStory As IStoryBase)
            aStory.Run()
        End Sub


        Public Function GetStoryOutcome() As Outcome
            Dim storyOutcome As Outcome = Nothing

            If scenarioOutcomes Is Nothing OrElse scenarioOutcomes.Count = 0 Then
                storyOutcome = New Outcome(OutcomeResult.Failed, "No outcomes")
            Else
                Dim outcomes(scenarioOutcomes.Count - 1) As Outcome

                scenarioOutcomes.CopyTo(outcomes, 0)
                storyOutcome = New Outcome(outcomes)
            End If

            Return storyOutcome

        End Function


        'Is called by run, via a Delegate
        Private Sub ScenarioOutcome(ByVal sender As Object, ByVal e As NBehaveEventArgs)
            scenarioOutcomes = e.Outcome.Outcomes
            OnScenarioExecuted(sender, e)
        End Sub


    End Class


End Namespace