Option Strict On

Imports System.Collections.ObjectModel
Imports NBehave.Framework.Scenario
Imports NBehave.Framework.Story
Imports NBehave.Framework.Utility


Namespace Story
    'TODO: pass in an assembly and find all Stories in it.
    'probably another class should handle that...


    Public Class StoryRunner

        Public Event RunStart As EventHandler(Of NBehaveEventArgs)
        Public Event ExecutingStory As EventHandler(Of NBehaveEventArgs)
        Public Event StoryExecuted As EventHandler(Of NBehaveEventArgs)
        Public Event RunFinished As EventHandler(Of NBehaveEventArgs)
        Public Event ScenarioExecuted As EventHandler(Of NBehaveEventArgs)


        Private ReadOnly StoryType As Type = GetType(Story(Of ))

        Protected stories As IList
        Private scenarioOutcomes As ReadOnlyCollection(Of Outcome)


        Public Sub New()
            Me.New(New Collections.ArrayList)
        End Sub

        Public Sub New(ByVal stories As IList)
            Me.stories = stories
        End Sub

        Public Sub New(ByVal assemblyToParseForStories As Reflection.Assembly)
            Me.New()
            If assemblyToParseForStories Is Nothing Then Throw New ArgumentException("assemblyToParseForStories is NULL")

            For Each t As Type In assemblyToParseForStories.GetTypes()
                If t.IsClass AndAlso Not t.IsAbstract Then
                    Dim baseType As Type = t.BaseType
                    If baseType IsNot Nothing AndAlso baseType.IsGenericType AndAlso baseType.GetGenericTypeDefinition.Equals(StoryType) Then
                        Dim i As Object = System.Activator.CreateInstance(t, True)
                        AddStory(i, False)
                    End If
                End If
            Next
        End Sub


        Public Sub AddStory(ByVal story As Object)
            AddStory(story, True)
        End Sub


        Protected Sub AddStory(ByVal story As Object, ByVal checkType As Boolean)
            If story Is Nothing Then Throw New ArgumentException("story is NULL")
            If checkType Then
                Dim baseType As Type = story.GetType.BaseType
                Do While baseType IsNot Nothing
                    If baseType.IsGenericType AndAlso baseType.GetGenericTypeDefinition.Equals(StoryType) Then
                        stories.Add(story)
                        Exit Do
                    End If
                    baseType = baseType.BaseType
                Loop
            Else
                stories.Add(story)
            End If
        End Sub


        Public Sub Run()

            For Each aStory As Object In stories

                Dim evtInfoStory As Reflection.EventInfo = aStory.GetType.GetEvent("ScenarioOutcome")
                Dim storyDelegate As System.EventHandler(Of NBehaveEventArgs) = AddressOf ScenarioResultHandler

                'collect scenario events
                'Dim evtInfoScenario As Reflection.EventInfo = aStory.GetType.GetEvent("ScenarioExecuted")
                'Dim scenarioDelegate As System.EventHandler(Of ScenarioEventArgs) = AddressOf ScenarioResultHandler

                Try
                    evtInfoStory.AddEventHandler(aStory, storyDelegate)
                    'evtInfoScenario.AddEventHandler(aStory, scenarioDelegate)

                    InitStory(aStory)
                    AddScenarios(aStory)

                    RaiseEvent ExecutingStory(Me, New StoryEventArgs(aStory))
                    ExecuteScenariosInStory(aStory)
                    RaiseEvent StoryExecuted(Me, New StoryEventArgs(aStory, GetStoryOutcome))

                Catch ex As Exception ' ArgumentException
                    Debug.WriteLine(ex.ToString)


                Finally
                    evtInfoStory.RemoveEventHandler(aStory, storyDelegate)
                    'evtInfoScenario.RemoveEventHandler(aStory, scenarioDelegate)
                End Try

            Next

            RaiseEvent RunFinished(Me, Nothing)

        End Sub


        Private Sub AddScenarios(ByRef aStory As Object)
            Dim miSpecify As Reflection.MethodInfo = aStory.GetType.GetMethod("Scenarios")   'Should probably Invoke Istory<T>.Run
            miSpecify.Invoke(aStory, Nothing)
        End Sub


        Private Sub InitStory(ByRef aStory As Object)
            Dim miNarrative As Reflection.MethodInfo = aStory.GetType.GetMethod("Story")
            miNarrative.Invoke(aStory, Nothing)
        End Sub


        Private Sub ExecuteScenariosInStory(ByRef aStory As Object)
            Dim miRun As Reflection.MethodInfo = aStory.GetType.GetMethod("Run")   'Should probably Invoke Istory<T>.Run
            miRun.Invoke(aStory, Nothing)
        End Sub



        Public Function GetStoryOutcome() As Outcome
            Dim storyOutcome As Outcome = Nothing

            If scenarioOutcomes Is Nothing OrElse scenarioOutcomes.Count = 0 Then
                storyOutcome = New Outcome(False, "No outcomes")
            Else
                Dim outcomes(scenarioOutcomes.Count - 1) As Outcome

                scenarioOutcomes.CopyTo(outcomes, 0)
                storyOutcome = New Outcome(outcomes)
            End If

            Return storyOutcome

        End Function


        'Is called by run, via a Delegate
        Private Sub ScenarioResultHandler(ByVal sender As Object, ByVal e As NBehaveEventArgs)
            scenarioOutcomes = e.Outcome.Outcomes
            RaiseEvent ScenarioExecuted(sender, e)
        End Sub


    End Class


End Namespace