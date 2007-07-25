Option Strict On

Imports System.Collections.ObjectModel
Imports System.Collections.Generic


Public Class Outcome
    Private _passed As Boolean
    Private _message As String
    Private _outcomes As IList(Of Outcome) = New List(Of Outcome)


    Public Property Passed() As Boolean
        Get
            Return _passed
        End Get
        Set(ByVal Value As Boolean)
            _passed = Value
        End Set
    End Property

    Public Property Message() As String
        Get
            Return _message
        End Get
        Set(ByVal Value As String)
            _message = Value
        End Set
    End Property


    Public Sub New(ByVal passed As Boolean, ByVal message As String)
        Me._passed = passed
        Me._message = message
    End Sub

    Public Sub New(ByVal outcomes() As Outcome)
        Me.New(False, "No outcomes exists")
        AddOutcomes(outcomes)
    End Sub


    Public Sub AddOutcome(ByVal outcome As Outcome)
        Dim arr() As Outcome = {outcome}
        AddOutcomes(arr)
    End Sub


    Public Sub AddOutcomes(ByVal outcomes As ReadOnlyCollection(Of Outcome))
        If outcomes.Count > 0 Then
            Dim arr(outcomes.Count - 1) As Outcome
            outcomes.CopyTo(arr, 0)
            AddOutcomes(arr)
        End If
    End Sub


    Public Sub AddOutcomes(ByVal outcomes() As Outcome)
        If outcomes IsNot Nothing Then
            If outcomes.Length > 0 Then Me.Passed = True
            For Each o As Outcome In outcomes
                Me.Passed = Me.Passed And o.Passed
                If Not o.Passed Then Me.Message &= o.Message & Environment.NewLine
                Me._outcomes.Add(o)
            Next
            If Me.Passed Then Me.Message = String.Empty
        End If
    End Sub


    Public ReadOnly Property Outcomes() As ReadOnlyCollection(Of Outcome)
        Get
            Return New Collections.ObjectModel.ReadOnlyCollection(Of Outcome)(_outcomes)
        End Get
    End Property

End Class
