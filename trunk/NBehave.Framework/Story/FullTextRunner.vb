Option Strict On

Imports System.Collections.ObjectModel
Imports NBehave.Framework.Scenario
Imports NBehave.Framework.Utility


Namespace Story

    Public Class FullTextRunner
        Inherits StreamRunner

        Public PrintNarrative As Boolean = False


        Public Sub New(ByVal outStream As IO.Stream, ByVal assemblyToParseForStories As Reflection.Assembly)
            MyBase.New(outStream, assemblyToParseForStories)
        End Sub


        Protected Overrides Sub StreamRunnerBeforeStoryRun(ByVal sender As Object, ByVal e As StoryEventArgs)
            OutStream.Write("Story: ")
            MyBase.StreamRunnerBeforeStoryRun(sender, e)
            OutStream.WriteLine()
            If PrintNarrative Then WriteStoryNarrative(CType(e.Story, IStoryBase))
        End Sub



        Protected Overrides Sub StreamRunnerScenarioExecuted(ByVal sender As Object, ByVal e As NBehaveEventArgs)
            OutStream.Write("   " & GetScenarioTitle(sender))
            WriteOutcome(e.Outcome)
            OutStream.WriteLine()
        End Sub



        Private Function GetScenarioTitle(ByVal sender As Object) As String
            Dim title As String = String.Empty

            If GetType(IScenarioBase).IsAssignableFrom(sender.GetType) Then
                title = CType(sender, IScenarioBase).Title
            Else
                title = CamelCaseToNormalSentence(sender.GetType.Name)
            End If
            Return title
        End Function



        Private Sub WriteStoryNarrative(ByVal story As IStoryBase)
            Dim narrative As String = story.Narrative.ToString
            narrative = "   " & narrative.Replace(Environment.NewLine, "." & Environment.NewLine & "   ") & "."
            OutStream.Write(narrative)
        End Sub


        Protected Overrides Sub WriteFinalOutcome()
            If MyBase.FailCount = 0 Then
                If PendingCount = 0 Then
                    Console.ForegroundColor = ConsoleColor.Green
                Else
                    Console.ForegroundColor = ConsoleColor.Yellow
                End If
            Else
                Console.ForegroundColor = ConsoleColor.Red
            End If
            MyBase.WriteFinalOutcome()
            OutStream.Flush()
            Console.ResetColor()
            OutStream.Flush()
        End Sub


    End Class


End Namespace