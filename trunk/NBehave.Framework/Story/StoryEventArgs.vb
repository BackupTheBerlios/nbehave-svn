Option Strict On

Imports NBehave.Framework.Scenario


Namespace Story


    Public Class StoryEventArgs
        Inherits EventArgs

        Private _story As Object
        Private _outcome As StoryOutcome

        Public ReadOnly Property Outcome() As StoryOutcome
            Get
                Return _outcome
            End Get
        End Property

        Public ReadOnly Property Story() As Object
            Get
                Return _story
            End Get
        End Property


        Public Sub New(ByVal story As Object)
            Me.new(story, Nothing)
        End Sub


        Public Sub New(ByVal story As Object, ByVal outcome As StoryOutcome)
            Me._story = story
            Me._outcome = outcome
        End Sub

    End Class


End Namespace