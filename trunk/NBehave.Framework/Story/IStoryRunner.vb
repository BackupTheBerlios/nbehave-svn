

Namespace Story

    Public Interface IStoryRunner
        Event RunStart As EventHandler(Of NBehaveEventArgs)
        Event ExecutingStory As EventHandler(Of StoryEventArgs)
        Event StoryExecuted As EventHandler(Of StoryEventArgs)
        Event RunFinished As EventHandler(Of NBehaveEventArgs)
        Event ScenarioExecuted As EventHandler(Of NBehaveEventArgs)
    End Interface

End Namespace