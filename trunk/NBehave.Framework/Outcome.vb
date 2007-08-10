Option Strict On

Imports System.Collections.ObjectModel
Imports System.Collections.Generic

Public Enum OutcomeResult
    Failed = 1
    Pending = 2
    Passed = 3
End Enum

Public Class Outcome
    Private _result As OutcomeResult
    Private _message As String
    Private _outcomes As IList(Of Outcome) = New List(Of Outcome)


    Public Property Result() As OutcomeResult
        Get
            Return _result
        End Get
        Set(ByVal Value As OutcomeResult)
            _result = Value
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


    Public Sub New(ByVal passed As OutcomeResult, ByVal message As String)
        Me._result = passed
        Me._message = message

    End Sub

    Public Sub New(ByVal outcomes() As Outcome)
        Me.New(OutcomeResult.Failed, "No outcomes exists")
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
            If outcomes.Length > 0 Then Me.Result = OutcomeResult.Passed
            For Each o As Outcome In outcomes
                If o.Result = OutcomeResult.Failed Then
                    Me.Result = OutcomeResult.Failed
                    Me.Message &= o.Message & Environment.NewLine
                End If
                If o.Result = OutcomeResult.Pending AndAlso Me.Result <> OutcomeResult.Failed Then
                    Me.Result = OutcomeResult.Pending
                End If
                Me._outcomes.Add(o)
            Next
            If Me.Result = OutcomeResult.Passed Then Me.Message = String.Empty
        End If
    End Sub


    Public ReadOnly Property Outcomes() As ReadOnlyCollection(Of Outcome)
        Get
            Return New Collections.ObjectModel.ReadOnlyCollection(Of Outcome)(_outcomes)
        End Get
    End Property

End Class
