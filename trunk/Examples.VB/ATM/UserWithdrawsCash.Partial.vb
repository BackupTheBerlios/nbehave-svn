Option Strict On
Option Explicit On

Imports NBehave.Framework
Imports NBehave.Framework.World
Imports Examples.VB.ATM.Domain
Imports NBehave.Framework.Story



Partial Public Class UserWithdrawsCash

    Private _account As IAccount
    Private _cashAccount As IAccount
    Delegate Sub Balance(ByVal bal As Int32)


    Public Overrides Sub Setup()
        _account = New Account
        _cashAccount = New Account
    End Sub


    Private Sub SetAccountBalance(ByVal newBalance As Int32)
        _account.Balance = newBalance
    End Sub


    Private Sub SetCashAccountBalance(ByVal newBalance As Int32)
        _cashAccount.Balance = newBalance
    End Sub

    Private Sub VerifyBalance(ByVal expected As Int32)
        MyBase.Ensure.IsTrue(_account.Balance = expected)
    End Sub

End Class



Public Class Account
    Implements IAccount

    Private _balance As Int32 = 0

    Public Property Balance() As Integer Implements ATM.Domain.IAccount.Balance
        Get
            Return _balance
        End Get
        Set(ByVal value As Integer)
            _balance = value
        End Set
    End Property

    Public Sub Withdraw(ByVal amount As Integer) Implements ATM.Domain.IAccount.Withdraw
        _balance -= amount
    End Sub


End Class