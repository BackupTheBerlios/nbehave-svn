Option Strict On

Imports System.Collections.ObjectModel
Imports NBehave.Framework.Scenario
Imports system.Threading.Thread


Namespace Story

    Public Class FullTextRunner
        Inherits StreamRunner

        Public PrintNarrative As Boolean = False


        Public Sub New(ByVal outStream As IO.Stream)
            MyBase.New(outStream)
        End Sub

        Public Sub New(ByVal outStream As IO.Stream, ByVal stories As IList)
            MyBase.New(outStream, stories)
        End Sub

        Public Sub New(ByVal outStream As IO.Stream, ByVal assemblyToParseForStories As Reflection.Assembly)
            MyBase.New(outStream, assemblyToParseForStories)
        End Sub

        Protected Overrides Sub StreamRunnerRunStart(ByVal sender As Object, ByVal e As NBehaveEventArgs)
            MyBase.StreamRunnerRunStart(sender, e)
        End Sub

        Protected Overrides Sub StreamRunnerBeforeStoryRun(ByVal sender As Object, ByVal e As NBehaveEventArgs)
            WriteStoryName()
            If printNarrative Then WriteStoryNarrative()
            MyBase.StreamRunnerBeforeStoryRun(sender, e)
        End Sub

        Protected Overrides Sub StreamRunnerAfterStoryRun(ByVal sender As Object, ByVal e As NBehaveEventArgs)
            MyBase.StreamRunnerAfterStoryRun(sender, e)
        End Sub

        Protected Overrides Sub WriteResultAfterStoryRun(ByVal outcome As Outcome)
            'OutStream.WriteLine()
            If outcome.Passed Then
                OutStream.Write("  --> Passed")
            Else
                OutStream.Write(String.Format("  --> Failed - {0}", outcome.Message))
            End If
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