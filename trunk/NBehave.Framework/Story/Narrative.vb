Option Strict On

Imports System.Collections.ObjectModel


Namespace Story


    Public Class Narrative
        Private ReadOnly _role As String
        Private ReadOnly _feature As String
        Private ReadOnly _benefit As String



        Public Sub New(ByVal role As String, ByVal feature As String, ByVal benefit As String)
            Me._role = role
            Me._feature = feature
            Me._benefit = benefit
        End Sub

        Public ReadOnly Property Text() As String
            Get
                Return _
                "As a: " & _role & Environment.NewLine & _
                "I want: " & _feature & Environment.NewLine & _
                "So that: " & _benefit
            End Get
        End Property

    End Class


End Namespace
