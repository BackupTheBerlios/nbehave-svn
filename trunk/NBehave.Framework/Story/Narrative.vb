Option Strict On

Imports System.Collections.ObjectModel


Namespace Story

    Public Interface INarrativeIWant
        Function IWant(ByVal feature As String) As INarrativeSoThat
    End Interface

    Public Interface INarrativeSoThat
        Sub SoThat(ByVal benefit As String)
    End Interface

    Public Class Narrative
        Implements INarrativeIWant, INarrativeSoThat


        Private _role As String
        Private _feature As String
        Private _benefit As String


        Public Sub New()
            Me.new("", "", "")
        End Sub

        Public Sub New(ByVal role As String, ByVal feature As String, ByVal benefit As String)
            Me._role = role
            Me._feature = feature
            Me._benefit = benefit
        End Sub

        Public Function AsA(ByVal role As String) As INarrativeIWant
            _role = role
            Return Me
        End Function

        Public Function IWant(ByVal feature As String) As INarrativeSoThat Implements INarrativeIWant.IWant
            _feature = feature
            Return Me
        End Function

        Public Sub SoThat(ByVal benefit As String) Implements INarrativeSoThat.SoThat
            _benefit = benefit
        End Sub

        Public Overrides Function ToString() As String
            Return _
            "As a " & _role & Environment.NewLine & _
            "I want " & _feature & Environment.NewLine & _
            "So that " & _benefit
        End Function

        <Obsolete("Use Tostring instead")> _
        Public ReadOnly Property Text() As String
            Get
                Return ToString()
            End Get
        End Property


    End Class


End Namespace
