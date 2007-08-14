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

    'This is called once for every public function
    Protected Overrides Sub Setup()
        _account = New Account
        _cashAccount = New Account
    End Sub


    Private Sub SetAccountBalance(ByVal newBalance As Int32)
        _account.Balance = newBalance
    End Sub

    Private Sub SetCashAccountBalance(ByVal newBalance As Int32)
        _cashAccount.Balance = newBalance
    End Sub

    Private Sub TransferMoney(ByVal amount As Int32)
        _account.Transfer(amount, _cashAccount)
    End Sub

    Private Sub VerifyAccountBalance(ByVal expected As Int32)
        MyBase.Ensure.IsTrue(_account.Balance = expected)
    End Sub

    Private Sub VerifyCashAccountBalance(ByVal expected As Int32)
        MyBase.Ensure.IsTrue(_cashAccount.Balance = expected)
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


    Public Sub Transfer(ByVal amount As Integer, ByVal toAccount As ATM.Domain.IAccount) Implements ATM.Domain.IAccount.Transfer
        _balance -= amount
        toAccount.Balance += amount
    End Sub
End Class