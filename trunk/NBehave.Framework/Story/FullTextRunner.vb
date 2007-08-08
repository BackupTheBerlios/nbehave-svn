Option Strict On

Imports System.Collections.ObjectModel
Imports NBehave.Framework.Scenario
Imports system.Threading.Thread


Namespace Story

    Public Class FullTextRunner
        Inherits StreamRunner

        Public PrintNarrative As Boolean = False


        Public Sub New(ByVal outStream As IO.Stream, ByVal assemblyToParseForStories As Reflection.Assembly)
            MyBase.New(outStream, assemblyToParseForStories)
        End Sub

        Protected Overrides Sub StreamRunnerRunStart(ByVal sender As Object, ByVal e As NBehaveEventArgs)
            MyBase.StreamRunnerRunStart(sender, e)
        End Sub

        Protected Overrides Sub StreamRunnerBeforeStoryRun(ByVal sender As Object, ByVal e As StoryEventArgs)
            WriteStoryName(e.Story)
            If PrintNarrative Then WriteStoryNarrative(e.Story)
            MyBase.StreamRunnerBeforeStoryRun(sender, e)
        End Sub

        Protected Overrides Sub StreamRunnerAfterStoryRun(ByVal sender As Object, ByVal e As StoryEventArgs)
            MyBase.StreamRunnerAfterStoryRun(sender, e)
        End Sub

        Protected Overrides Sub StreamRunnerScenarioExecuted(ByVal sender As Object, ByVal e As NBehaveEventArgs)
            MyBase.StreamRunnerScenarioExecuted(sender, e)
        End Sub


        Protected Overrides Sub WriteResultAfterStoryRun(ByVal outcome As Outcome)
            OutStream.Write("Story outcome")
            WriteOutcome(outcome)
            OutStream.Flush()
        End Sub


        Protected Overrides Sub StreamRunnerRunFinished(ByVal sender As Object, ByVal e As NBehaveEventArgs)
            MyBase.StreamRunnerRunFinished(sender, e)
        End Sub

        Protected Overrides Sub WriteFinalOutcome()
            OutStream.Flush()
        End Sub

        Protected Overrides Sub WriteSummary()
            OutStream.Flush()
        End Sub

        Protected Overrides Sub WriteFailures()
            OutStream.Flush()
        End Sub


    End Class


End Namespace