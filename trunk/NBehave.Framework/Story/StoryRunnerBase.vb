Option Strict On

Imports System.Collections.ObjectModel
Imports NBehave.Framework.Scenario
Imports NBehave.Framework.Story
Imports NBehave.Framework.Utility


Namespace Story

    Public MustInherit Class StoryRunnerBase
        Implements IStoryRunner

        Public MustOverride Sub Run()


        Private _stories As IList = New ArrayList

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

            If checkType Then
                If IsOfTypeStory(story.GetType) Then
                    Stories.Add(story)
                Else
                    Throw New ArgumentException(String.Format("The story must be a subclass of {0}.", GetType(Story).Name))
                End If
            Else
                Stories.Add(story)
            End If
        End Sub


        Protected Function IsOfTypeStory(ByVal type As Type) As Boolean
            Return type.IsClass AndAlso Not type.IsAbstract AndAlso type.BaseType IsNot Nothing AndAlso type.IsSubclassOf(GetType(Story))
        End Function

    End Class

End Namespace
