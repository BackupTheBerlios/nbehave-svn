Option Strict On

Imports System.Collections.ObjectModel
Imports NBehave.Framework.Scenario
Imports NBehave.Framework.Story



Namespace Story



    'TODO: pass in an assembly and find all Stories in it.
    'probably another class should handle that...


    Public Class StoryRunner

        Public Event RunStart As EventHandler(Of StoryEventArgs) ' (ByVal sender As Object, ByVal e As StoryEventArgs)
        Public Event ExecutingStory As EventHandler(Of StoryEventArgs) ' (ByVal sender As Object, ByVal e As StoryEventArgs) ' (ByVal story As Object)
        Public Event StoryExecuted As EventHandler(Of StoryEventArgs) ' (ByVal sender As Object, ByVal e As StoryEventArgs) ' (ByVal story As Object, ByVal outcome As Outcome)
        Public Event RunFinished As EventHandler(Of StoryEventArgs) ' (ByVal sender As Object, ByVal e As StoryEventArgs)


        Private ReadOnly StoryType As Type = GetType(Story(Of ))

        Private stories As IList
        Private scenarioOutcomes As ReadOnlyCollection(Of ScenarioOutcome)


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

                RaiseEvent ExecutingStory(Me, New StoryEventArgs(aStory))
                Dim evtInfo As Reflection.EventInfo = aStory.GetType.GetEvent("StoryOutcome")
                Dim storyDelegate As System.EventHandler(Of OutcomeEventArgs) = AddressOf StoryResultHandler

                Try
                    evtInfo.AddEventHandler(aStory, storyDelegate)

                    Dim miSpecify As Reflection.MethodInfo = aStory.GetType.GetMethod("Scenarios")   'Should probably Invoke Istory<T>.Run
                    miSpecify.Invoke(aStory, Nothing)

                    Dim miRun As Reflection.MethodInfo = aStory.GetType.GetMethod("Run")   'Should probably Invoke Istory<T>.Run
                    miRun.Invoke(aStory, Nothing)

                Catch ex As Exception ' ArgumentException
                    Debug.WriteLine(ex.ToString)


                Finally
                    evtInfo.RemoveEventHandler(aStory, storyDelegate)
                End Try

                RaiseEvent StoryExecuted(Me, New StoryEventArgs(aStory, GetStoryOutcome))
            Next

            RaiseEvent RunFinished(Me, Nothing)

        End Sub


        Public Function GetStoryOutcome() As StoryOutcome
            Dim storyOutcome As StoryOutcome = Nothing

            If scenarioOutcomes Is Nothing OrElse scenarioOutcomes.Count = 0 Then
                storyOutcome = New StoryOutcome(Nothing)
            Else
                Dim outcomes(scenarioOutcomes.Count - 1) As ScenarioOutcome

                scenarioOutcomes.CopyTo(outcomes, 0)
                storyOutcome = New StoryOutcome(outcomes)
            End If

            Return storyOutcome

        End Function


        'Is called by run, via a Delegate
        Private Sub StoryResultHandler(ByVal sender As Object, ByVal e As Scenario.OutcomeEventArgs)
            scenarioOutcomes = e.Outcomes
        End Sub

    End Class


End Namespace