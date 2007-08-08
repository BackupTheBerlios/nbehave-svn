Option Strict Off

Imports System.Collections.ObjectModel
Imports NBehave.Framework.Scenario
Imports NBehave.Framework.Utility


Namespace Story

    Partial Public Class FullTextRunner

        Private Sub WriteStoryName(ByVal story As Object)
            Dim storyText As String = CamelCaseToNormalSentence(story.GetType.Name)
            OutStream.WriteLine("Story: " & storyText)
        End Sub


        Private Sub WriteStoryNarrative(ByVal story As Object)
            Dim narrative As String = story.Narrative.ToString
            narrative = "   " & narrative.Replace(Environment.NewLine, "." & Environment.NewLine & "   ") & "."
            OutStream.Write(narrative)
        End Sub

    End Class

End Namespace