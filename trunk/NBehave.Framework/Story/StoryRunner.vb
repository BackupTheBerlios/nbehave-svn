Option Strict On

Imports System.Collections.ObjectModel
Imports NBehave.Framework.Scenario
Imports NBehave.Framework.Story
Imports NBehave.Framework.Utility


Namespace Story

    Public Interface IStoryRunner
        Event RunStart As EventHandler(Of NBehaveEventArgs)
        Event ExecutingStory As EventHandler(Of StoryEventArgs)
        Event StoryExecuted As EventHandler(Of StoryEventArgs)
        Event RunFinished As EventHandler(Of NBehaveEventArgs)
        Event ScenarioExecuted As EventHandler(Of NBehaveEventArgs)
    End Interface


    Public MustInherit Class StoryRunnerBase
        Implements IStoryRunner

        Public MustOverride Sub Run()


        Private _stories As IList = New ArrayList
        Protected ReadOnly StoryType As Type = GetType(Story(Of ))

        Public Event RunStart As EventHandler(Of NBehaveEventArgs) Implements IStoryRunner.RunStart
        Public Event ExecutingStory As EventHandler(Of StoryEventArgs) Implements IStoryRunner.ExecutingStory
        Public Event StoryExecuted As EventHandler(Of StoryEventArgs) Implements IStoryRunner.StoryExecuted
        Public Event RunFinished As EventHandler(Of NBehaveEventArgs) Implements IStoryRunner.RunFinished
        Public Event ScenarioExecuted As EventHandler(Of NBehaveEventArgs) Implements IStoryRunner.ScenarioExecuted


        Protected Sub OnRunStart(ByVal args As NBehaveEventArgs)
            RaiseEvent RunStart(Me, args)
        End Sub

        Protected Sub OnExecutingStory(ByVal args As StoryEventArgs)
            RaiseEvent ExecutingStory(Me, args)
        End Sub

        Protected Sub OnStoryExecuted(ByVal args As StoryEventArgs)
            RaiseEvent StoryExecuted(Me, args)
        End Sub

        Protected Sub OnRunFinished(ByVal args As NBehaveEventArgs)
            RaiseEvent RunFinished(Me, args)
        End Sub

        Protected Sub OnScenarioExecuted(ByVal sender As Object, ByVal args As NBehaveEventArgs)
            RaiseEvent ScenarioExecuted(sender, args)
        End Sub



        Public Overridable Property Stories() As IList
            Get
                Return _stories
            End Get
            Set(ByVal value As IList)
                _stories = value
            End Set
        End Property


        Public Sub AddStory(Of T)(ByVal story As T)
            AddStory(story, True)
        End Sub


        Protected Sub AddStory(Of T)(ByVal story As T, ByVal checkType As Boolean)
            If story Is Nothing Then Throw New ArgumentException("story is NULL")

            Dim storyAdded As Boolean = False
            If checkType Then
                Dim baseType As Type = story.GetType.BaseType
                Do While baseType IsNot Nothing
                    If baseType.IsGenericType AndAlso baseType.GetGenericTypeDefinition.Equals(StoryType) Then
                        Stories.Add(story)
                        storyAdded = True
                        Exit Do
                    End If
                    baseType = baseType.BaseType
                Loop
                If Not storyAdded Then Throw New ArgumentException(String.Format("The story must be a type or subtype of {0}.", StoryType.Name))
            Else
                Stories.Add(story)
            End If
        End Sub


    End Class



    Public Class StoryRunner
        Inherits StoryRunnerBase


        Private scenarioOutcomes As ReadOnlyCollection(Of Outcome)


        'Public Sub New()
        '    Me.New(New Collections.ArrayList)
        'End Sub

        'Public Sub New(ByVal stories As IList)
        '    Me.stories = stories
        'End Sub

        Public Sub New()
        End Sub

        Public Sub New(ByVal assemblyToParseForStories As Reflection.Assembly)
            'Me.New()
            If assemblyToParseForStories Is Nothing Then Throw New ArgumentException("assemblyToParseForStories is NULL")

            ParseAssemblyForStories(assemblyToParseForStories)
        End Sub


        Private Sub ParseAssemblyForStories(ByRef assemblyToParseForStories As Reflection.Assembly)
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


        Public Overrides Sub Run()
            OnRunStart(Nothing)

            For Each aStory As IStoryBase In Stories

                'Dim scenarioResultDelegate As System.EventHandler(Of NBehaveEventArgs) = AddressOf ScenarioOutcome

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