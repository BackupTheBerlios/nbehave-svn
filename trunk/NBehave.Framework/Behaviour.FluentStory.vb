Option Strict On

Imports NBehave.Framework.Story


Partial Public Class Behaviour
    Private Class FluentStory
        Implements INarrativeAsA

        Private narrative As Narrative

        Public Sub New(ByVal narrative As Narrative)
            Me.narrative = narrative
        End Sub
        Private Function AsA(ByVal role As String) As INarrativeIWant Implements Story.INarrativeAsA.AsA
            Return narrative.AsA(role)
        End Function
    End Class
End Class
