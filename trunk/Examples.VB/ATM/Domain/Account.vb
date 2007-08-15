Public Class Account
    Implements IAccount

    Private _balance As Integer = 0

    Public Sub Transfer(ByVal amount As Integer, ByVal toAccount As IAccount) Implements IAccount.Transfer
        _balance -= amount
        toAccount.Balance += amount
    End Sub


    Public Property Balance() As Integer Implements IAccount.Balance
        Get
            Return _balance
        End Get
        Set(ByVal value As Integer)
            _balance = value
        End Set
    End Property

End Class
