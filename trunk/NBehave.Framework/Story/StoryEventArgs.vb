Option Strict On

Imports NBehave.Framework.Scenario


Namespace Story


    Public Class StoryEventArgs
        Inherits NBehaveEventArgs

        Private _story As Object


        Public ReadOnly Property Story() As Object
            Get
                Return _story
            End Get
        End Property


        Public Sub New(ByVal story As Object)
            Me.new(story, Nothing)
        End Sub


        Public Sub New(ByVal scenario As Object, ByVal outcome As Outcome)
            MyBase.New(outcome)
            Me._story = Story
        End Sub

    End Class


End Namespace