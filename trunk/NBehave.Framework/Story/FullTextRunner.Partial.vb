Option Strict Off

Imports System.Collections.ObjectModel
Imports NBehave.Framework.Scenario
Imports NBehave.Framework.Utility


Namespace Story

    Partial Public Class FullTextRunner

        Private Sub WriteStoryName()
            Dim story As String = stories.Item(StoryCount).GetType.Name
            story = CamelCaseToNormalSentence(story)
            OutStream.Write("Story: " + story)
        End Sub


        Private Sub WriteStoryNarrative()
            Dim narrative As String = stories.Item(StoryCount).Narrative.ToString
            narrative = "   " + narrative.Replace(Environment.NewLine, "." + Environment.NewLine + "   ") + "."
            OutStream.Write(narrative)
        End Sub

    End Class

End Namespace