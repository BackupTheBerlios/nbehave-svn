Option Strict On

Imports System.Collections.ObjectModel


Namespace Story


    Public Class Narrative
        Private ReadOnly _asA As String
        Private ReadOnly _iWant As String
        Private ReadOnly _soThat As String



        Public Sub New(ByVal asA As String, ByVal iWant As String, ByVal soThat As String)
            Me._asA = asA
            Me._iWant = iWant
            Me._soThat = soThat
        End Sub

        Public ReadOnly Property Text() As String
            Get
                Return _
                "As a: " & _asA & Environment.NewLine & _
                "I want: " & _iWant & Environment.NewLine & _
                "So that: " & _soThat
            End Get
        End Property

    End Class


End Namespace
