Option Strict On

Imports System.Threading.Thread
Imports NBehave.Framework.World
Imports NBehave.Framework.Scenario


Public Interface IIsSomething
    Sub IsTrue(ByVal expected As Boolean)
    Sub IsFalse(ByVal expected As Boolean)
    'TODO: Add some more nice verifications...
End Interface


Public Class EnsureBase(Of T)
    Implements IIsSomething

    Private _outcome As Outcome


    Public Property Outcome() As Outcome
        Get
            Return _outcome
        End Get
        Protected Set(ByVal value As Outcome)
            _outcome = value
        End Set
    End Property

    Public Sub IsFalse(ByVal expected As Boolean) Implements IIsSomething.IsFalse
        Me.Expected(expected, False)
    End Sub

    Public Sub IsTrue(ByVal expected As Boolean) Implements IIsSomething.IsTrue
        Me.Expected(expected, True)
    End Sub

    Private Sub Expected(ByVal expected As Boolean, ByVal compareTo As Boolean)
        _outcome.Message = String.Format(CurrentThread.CurrentUICulture, "Expected {0}, but is {1}", compareTo.ToString, expected.ToString)
        If expected = compareTo Then
            _outcome.Result = OutcomeResult.Passed
        Else
            _outcome.Result = OutcomeResult.Failed
        End If
    End Sub

End Class



Public Class Ensure(Of T)
    Inherits EnsureBase(Of T)


    Public Sub New(ByVal outcome As Outcome)
        Me.Outcome = outcome
    End Sub

    Public Sub New(ByVal outcome As IWorldOutcome(Of T))
        Me.Outcome = outcome.Result
    End Sub

    Public Sub Failure()
        Failure(New Outcome(OutcomeResult.Failed, "Failure"))
    End Sub

    Public Sub Failure(ByVal theFailure As Outcome)
        Me.Outcome = theFailure
    End Sub



End Class
